// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using AdaptiveCards;
using FHL.Hack.SmartAssistantBot.Dialogs.Shared;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;

namespace FHL.Hack.SmartAssistantBot.Dialogs.Tar
{
    public class TarDialog : EnterpriseDialog
    {
        private static TarResponses _responder = new TarResponses();
        private IStatePropertyAccessor<TarState> _accessor;
        private TarState _state;

        public TarDialog(BotServices botServices, IStatePropertyAccessor<TarState> accessor,
            IBotTelemetryClient telemetryClient)
            : base(botServices, nameof(TarDialog))
        {
            _accessor = accessor;
            InitialDialogId = nameof(TarDialog);

            var tarScenarios = new WaterfallStep[]
            {
                ConfirmSickDay,
                AskToSendNoteToTeam,
                PromptCalendarCancellation,
                CompleteDialog
            };

            TelemetryClient = telemetryClient;
            AddDialog(new WaterfallDialog(InitialDialogId, tarScenarios) {TelemetryClient = telemetryClient});
            AddDialog(new TextPrompt(DialogIds.ConfirmSickDay));
            AddDialog(new TextPrompt(DialogIds.AskSendNoteToTeam));
            AddDialog(new TextPrompt(DialogIds.PromptCalendarCancellation));
            AddDialog(new TextPrompt(DialogIds.CompleteDialog));
        }

        public async Task<DialogTurnResult> ConfirmSickDay(WaterfallStepContext sc,
            CancellationToken cancellationToken)
        {
            _state = await _accessor.GetAsync(sc.Context, () => new TarState());

            if (_state.ConfirmSickDay)
                return await sc.NextAsync(_state.ConfirmSickDay);

            await _responder.ReplyWith(sc.Context, TarResponses.ResponseIds.FeelBetterSoon);

            var choices = new[] {"Use sick day", "No"};

            var card = new AdaptiveCard
            {
                Version = new AdaptiveSchemaVersion(1, 0),
                Body =
                {
                    new AdaptiveTextBlock("Here is your sick day balance as of today"),
                    new AdaptiveTextBlock(),
                    new AdaptiveColumnSet()
                    {
                        Columns = new List<AdaptiveColumn>()
                        {
                            new AdaptiveColumn()
                            {
                                Items = new List<AdaptiveElement>()
                                {
                                    new AdaptiveTextBlock()
                                    {
                                        Text = "2",
                                        Size = AdaptiveTextSize.ExtraLarge
                                    }
                                }
                            },
                            new AdaptiveColumn()
                            {
                                Items = new List<AdaptiveElement>()
                                {
                                    new AdaptiveTextBlock(),
                                    new AdaptiveTextBlock(),
                                    new AdaptiveTextBlock()
                                    {
                                        Text = "days",
                                        Size = AdaptiveTextSize.Small
                                    }
                                }
                            }
                        }
                    },
                    new AdaptiveTextBlock(),
                    new AdaptiveTextBlock("Would you use your sick day?"),
                },
                Actions = choices.Select(choice => new AdaptiveSubmitAction
                {
                    Title = choice,
                    Data = choice,
                }).ToList<AdaptiveAction>(),
            };

            return await sc.PromptAsync(
                DialogIds.ConfirmSickDay,
                new PromptOptions
                {
                    Choices = ChoiceFactory.ToChoices(choices),
                    Prompt = (Activity) MessageFactory.Attachment(new Attachment
                    {
                        ContentType = AdaptiveCard.ContentType,
                        Content = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(card)),
                    }),
                },
                cancellationToken);
        }

        public async Task<DialogTurnResult> AskToSendNoteToTeam(WaterfallStepContext sc,
            CancellationToken cancellationToken)
        {
            _state = await _accessor.GetAsync(sc.Context, () => new TarState(), cancellationToken);
            var result = sc.Result as string;
            if (result != "Use sick day")
            {
                await _accessor.DeleteAsync(sc.Context);
                return await sc.EndDialogAsync(null, cancellationToken);
            }

            _state.ConfirmSickDay = true;
            await _responder.ReplyWith(sc.Context, TarResponses.ResponseIds.ConfirmSickDaySubmitted);

            var choices = new[] {"Notify team about my sick day", "No"};

            var card = new AdaptiveCard
            {
                Version = new AdaptiveSchemaVersion(1, 0),
                Body =
                {
                    new AdaptiveTextBlock("Would you like to send a note to your team?"),
                    new AdaptiveTextBlock()
                },
                Actions = choices.Select(choice => new AdaptiveSubmitAction
                {
                    Title = choice,
                    Data = choice,
                }).ToList<AdaptiveAction>(),
            };

            return await sc.PromptAsync(
                DialogIds.AskSendNoteToTeam,
                new PromptOptions
                {
                    Choices = ChoiceFactory.ToChoices(choices),
                    Prompt = (Activity) MessageFactory.Attachment(new Attachment
                    {
                        ContentType = AdaptiveCard.ContentType,
                        Content = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(card)),
                    }),
                },
                cancellationToken);
        }

        public async Task<DialogTurnResult> PromptCalendarCancellation(WaterfallStepContext sc,
            CancellationToken cancellationToken)
        {
            _state = await _accessor.GetAsync(sc.Context, () => new TarState(), cancellationToken);
            var result = sc.Result as string;
            if (result != "Notify team about my sick day")
            {
                await _accessor.DeleteAsync(sc.Context);
                return await sc.EndDialogAsync(null, cancellationToken);
            }

            await _responder.ReplyWith(sc.Context, TarResponses.ResponseIds.SentNoteToTeam);

            var choices = new[] {"Yes", "No"};

            var card = new AdaptiveCard
            {
                Version = new AdaptiveSchemaVersion(1, 0),
                Body =
                {
                    new AdaptiveTextBlock("I see you have 3 meetings scheduled today."),
                    new AdaptiveTextBlock(),
                    new AdaptiveFactSet()
                    {
                        Facts = new List<AdaptiveFact>()
                        {
                            new AdaptiveFact()
                            {
                                Title = "Title",
                                Value = "Stand up"
                            },
                            new AdaptiveFact()
                            {
                                Title = "Time",
                                Value = "11:00 AM to 11:30 AM"
                            },
                            new AdaptiveFact()
                            {
                                Title = "Attendees",
                                Value = "ravigoridir"
                            },
                        }
                    },
                    new AdaptiveTextBlock("---------------"),
                    new AdaptiveFactSet()
                    {
                        Facts = new List<AdaptiveFact>()
                        {
                            new AdaptiveFact()
                            {
                                Title = "Title",
                                Value = "Discuss HR Bot"
                            },
                            new AdaptiveFact()
                            {
                                Title = "Time",
                                Value = "2:00 PM to 3:00 PM"
                            },
                            new AdaptiveFact()
                            {
                                Title = "Attendees",
                                Value = "HRBot Stakeholders"
                            },
                        }
                    },
                    new AdaptiveTextBlock("---------------"),
                    new AdaptiveFactSet()
                    {
                        Facts = new List<AdaptiveFact>()
                        {
                            new AdaptiveFact()
                            {
                                Title = "Title",
                                Value = "Sprint Planning"
                            },
                            new AdaptiveFact()
                            {
                                Title = "Time",
                                Value = "3:00 PM to 4:00 PM"
                            },
                            new AdaptiveFact()
                            {
                                Title = "Attendees",
                                Value = "ravigoridir"
                            },
                        }
                    },
                    new AdaptiveTextBlock(),
                    new AdaptiveTextBlock("Would you like to cancel those meetings?"),
                },
                Actions = choices.Select(choice => new AdaptiveSubmitAction
                {
                    Title = choice,
                    Data = "I am done with tar",
                }).ToList<AdaptiveAction>()
            };

            return await sc.PromptAsync(
                DialogIds.PromptCalendarCancellation,
                new PromptOptions
                {
                    Choices = ChoiceFactory.ToChoices(choices),
                    Prompt = (Activity) MessageFactory.Attachment(new Attachment
                    {
                        ContentType = AdaptiveCard.ContentType,
                        Content = JsonConvert.DeserializeObject(JsonConvert.SerializeObject(card)),
                    }),
                },
                cancellationToken);
        }

        public async Task<DialogTurnResult> CompleteDialog(WaterfallStepContext sc,
            CancellationToken cancellationToken)
        {
            _state = await _accessor.GetAsync(sc.Context, () => new TarState(), cancellationToken);

            await _responder.ReplyWith(sc.Context, TarResponses.ResponseIds.MeetingsCancelled);
            await _responder.ReplyWith(sc.Context, TarResponses.ResponseIds.TakeCare);
            await _accessor.DeleteAsync(sc.Context);

            return await sc.EndDialogAsync(null, cancellationToken);
        }

        private class DialogIds
        {
            public const string ConfirmSickDay = "ConfirmSickDay";
            public const string AskSendNoteToTeam = "AskSendNoteToTeam";
            public const string PromptCalendarCancellation = "PromptCalendarCancellation";
            public const string CompleteDialog = "CompleteDialog";
        }
    }
}