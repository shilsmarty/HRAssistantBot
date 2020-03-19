// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Schema;
using FHL.Hack.SmartAssistantBot.Dialogs.Main;
using FHL.Hack.SmartAssistantBot.Extensions;
using Microsoft.Bot.Configuration;
using System.Net.Http;
using Newtonsoft.Json;
using FHL.Hack.SmartAssistantBot.Dialogs.Benefits;
using System.Linq;
using System.Collections.Generic;

namespace FHL.Hack.SmartAssistantBot
{
    /// <summary>
    /// Main entry point and orchestration for bot.
    /// </summary>
    public class Bot : IBot
    {
        private readonly BotServices _services;
        private readonly ConversationState _conversationState;
        private readonly UserState _userState;
        private readonly IBotTelemetryClient _telemetryClient;
        private readonly JobState _jobState;
        private readonly IStatePropertyAccessor<JobLog> _jobLogPropertyAccessor;
        private static bool proactiveMessageSent = false;
        private static object obj = new object();
        private DialogSet _dialogs;

        /// <summary>
        /// Initializes a new instance of the <see cref="FHL.Hack.SmartAssistantBot"/> class.
        /// </summary>
        /// <param name="botServices">Bot services.</param>
        /// <param name="conversationState">Bot conversation state.</param>
        /// <param name="userState">Bot user state.</param>
        public Bot(BotServices botServices, ConversationState conversationState, UserState userState, IBotTelemetryClient telemetryClient, JobState jobState, EndpointService endpointService)
        {
            _conversationState = conversationState ?? throw new ArgumentNullException(nameof(conversationState));
            _userState = userState ?? throw new ArgumentNullException(nameof(userState));
            _services = botServices ?? throw new ArgumentNullException(nameof(botServices));
            _telemetryClient = telemetryClient ?? throw new ArgumentNullException(nameof(telemetryClient));

            _dialogs = new DialogSet(_conversationState.CreateProperty<DialogState>(nameof(FHL.Hack.SmartAssistantBot)));
            _dialogs.Add(new MainDialog(_services, _conversationState, _userState, _telemetryClient));

            _jobState = jobState ?? throw new ArgumentNullException(nameof(jobState));
            _jobLogPropertyAccessor = _jobState.CreateProperty<JobLog>(nameof(JobLog));

            AppId = string.IsNullOrWhiteSpace(endpointService.AppId) ? AppId : endpointService.AppId;
        }

        private string AppId { get; }

        /// <summary>
        /// Run every turn of the conversation. Handles orchestration of messages.
        /// </summary>
        /// <param name="turnContext">Bot Turn Context.</param>
        /// <param name="cancellationToken">Task CancellationToken.</param>
        /// <returns>A <see cref="Task"/> representing the asynchronous operation.</returns>
        public async Task OnTurnAsync(ITurnContext turnContext, CancellationToken cancellationToken)
        {
            // Client notifying this bot took to long to respond (timed out)
            if (turnContext.Activity.Code == EndOfConversationCodes.BotTimedOut)
            {
                _services.TelemetryClient.TrackTrace($"Timeout in {turnContext.Activity.ChannelId} channel: Bot took too long to respond.");
                return;
            }

            var dc = await _dialogs.CreateContextAsync(turnContext);

            if (dc.ActiveDialog != null)
            {
                var result = await dc.ContinueDialogAsync();
            }
            else
            {
                await dc.BeginDialogAsync(nameof(MainDialog));
            }

            // Fire and forget, we should not wait for the proactive messages, otherwise the "typing" icon will be displayed in the chat
            Task.Run(() => SendProactiveMessages(turnContext));
        }

        private void SendProactiveMessages(ITurnContext turnContext)
        {
            if (turnContext.Activity.Type == ActivityTypes.Message)
            {
                lock (obj) // Needed to avoid concurrency issues
                {
                    JobLog jobLog = _jobLogPropertyAccessor.GetAsync(turnContext, () => new JobLog())
                        .GetAwaiter()
                        .GetResult();

                    if (!proactiveMessageSent)
                    {
                        JobLog.JobData job = CreateJob(turnContext, jobLog);

                        _jobLogPropertyAccessor.SetAsync(turnContext, jobLog)
                            .GetAwaiter()
                            .GetResult();

                        foreach (var jobInfo in jobLog)
                        {
                            turnContext.Adapter.ContinueConversationAsync(
                                AppId,
                                jobInfo.Value.Conversation,
                                CreateCallback(jobInfo.Value),
                                default(CancellationToken));
                        }

                        proactiveMessageSent = true;
                    }
                }
            }
        }

        private JobLog.JobData CreateJob(ITurnContext turnContext, JobLog jobLog)
        {
            JobLog.JobData jobInfo = new JobLog.JobData
            {
                Id = 1,
                TimeStamp = DateTime.Now.ToBinary(),
                Conversation = turnContext.Activity.GetConversationReference(),
            };

            jobLog[jobInfo.TimeStamp] = jobInfo;

            return jobInfo;
        }

        private BotCallbackHandler CreateCallback(JobLog.JobData jobInfo)
            => async (turnContext, token) =>
            {
                Thread.Sleep(20000);

                using (var httpClient = new HttpClient())
                {
                    var responseString = await httpClient.GetStringAsync(new Uri("https://hiddengemsapi.azurewebsites.net/api/benefits/bob")).ConfigureAwait(false);
                    var response = JsonConvert.DeserializeObject<BenefitResponse>(responseString);
                    var topBenefits = response
                    .Benefits
                    .OrderByDescending(b => b.Relevance)
                    .Take(5)
                    .ToList();

                    await turnContext.SendActivityAsync($"JUST A QUICK REMINDER! Don't forget to use your benefits, here is the top 5 of your benefits:");
                    foreach (var item in topBenefits)
                    {
                        await turnContext.SendActivityAsync($"{item.Name} | {item.Category} | {item.Description}");
                    }
                }
            };
    }
}
