// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Threading;
using System.Threading.Tasks;
using FHL.Hack.SmartAssistantBot.Dialogs.Shared;
using Microsoft.ApplicationInsights;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;

namespace FHL.Hack.SmartAssistantBot.Dialogs.Onboarding
{
    public class ExperiencesDialog : EnterpriseDialog
    {
        private static ExperiencesResponses _responder = new ExperiencesResponses();
        private IStatePropertyAccessor<ExperiencesState> _accessor;
        private ExperiencesState _state;

        public ExperiencesDialog(BotServices botServices, IStatePropertyAccessor<ExperiencesState> accessor, IBotTelemetryClient telemetryClient)
            : base(botServices, nameof(ExperiencesDialog))
        {
            _accessor = accessor;
            InitialDialogId = nameof(ExperiencesDialog);

            var onboarding = new WaterfallStep[]
            {
                AskForName,
                AskForEmail,
                AskForLocation,
                FinishOnboardingDialog,
            };

            // To capture built-in waterfall dialog telemetry, set the telemetry client 
            // to the new waterfall dialog and add it to the component dialog
            TelemetryClient = telemetryClient;
            AddDialog(new WaterfallDialog(InitialDialogId, onboarding) { TelemetryClient = telemetryClient });
            AddDialog(new TextPrompt(DialogIds.NamePrompt));
            AddDialog(new TextPrompt(DialogIds.EmailPrompt));
            AddDialog(new TextPrompt(DialogIds.LocationPrompt));
        }

        public async Task<DialogTurnResult> AskForName(WaterfallStepContext sc, CancellationToken cancellationToken)
        {
            _state = await _accessor.GetAsync(sc.Context, () => new ExperiencesState());

            if (!string.IsNullOrEmpty(_state.Name))
            {
                return await sc.NextAsync(_state.Name);
            }
            else
            {
                return await sc.PromptAsync(DialogIds.NamePrompt, new PromptOptions()
                {
                    Prompt = await _responder.RenderTemplate(sc.Context, sc.Context.Activity.Locale, ExperiencesResponses.ResponseIds.NamePrompt),
                });
            }
        }

        public async Task<DialogTurnResult> AskForEmail(WaterfallStepContext sc, CancellationToken cancellationToken)
        {
            _state = await _accessor.GetAsync(sc.Context, () => new ExperiencesState());
            var name = _state.Name = (string)sc.Result;

            await _responder.ReplyWith(sc.Context, ExperiencesResponses.ResponseIds.HaveNameMessage, new { name });

            return await sc.PromptAsync(DialogIds.EmailPrompt, new PromptOptions()
            {
                Prompt = await _responder.RenderTemplate(sc.Context, sc.Context.Activity.Locale, ExperiencesResponses.ResponseIds.EmailPrompt),
            });
        }

        public async Task<DialogTurnResult> AskForLocation(WaterfallStepContext sc, CancellationToken cancellationToken)
        {
            _state = await _accessor.GetAsync(sc.Context, () => new ExperiencesState());
            var email = _state.Email = (string)sc.Result;

            await _responder.ReplyWith(sc.Context, ExperiencesResponses.ResponseIds.HaveEmailMessage, new { email });

            return await sc.PromptAsync(DialogIds.LocationPrompt, new PromptOptions()
            {
                Prompt = await _responder.RenderTemplate(sc.Context, sc.Context.Activity.Locale, ExperiencesResponses.ResponseIds.LocationPrompt),
            });
        }

        public async Task<DialogTurnResult> FinishOnboardingDialog(WaterfallStepContext sc, CancellationToken cancellationToken)
        {
            _state = await _accessor.GetAsync(sc.Context);
            _state.Location = (string)sc.Result;

            await _responder.ReplyWith(sc.Context, ExperiencesResponses.ResponseIds.HaveLocationMessage, new { _state.Name, _state.Location });
            return await sc.EndDialogAsync();
        }

        private class DialogIds
        {
            public const string NamePrompt = "namePrompt";
            public const string EmailPrompt = "emailPrompt";
            public const string LocationPrompt = "locationPrompt";
        }
    }
}
