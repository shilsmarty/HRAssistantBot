// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using FHL.Hack.SmartAssistantBot.Dialogs.Immigrations;
using FHL.Hack.SmartAssistantBot.Dialogs.Main;
using FHL.Hack.SmartAssistantBot.Dialogs.Shared;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Remotion.Linq.Parsing.Structure.IntermediateModel;

namespace FHL.Hack.SmartAssistantBot.Dialogs.Immigration
{
    public class ImmigrationDialog : EnterpriseDialog
    {
        private static ImmigrationResponses _responder = new ImmigrationResponses();
        private IStatePropertyAccessor<ImmigrationState> _accessor;
        private ImmigrationState _state;

        public ImmigrationDialog(BotServices botServices, IStatePropertyAccessor<ImmigrationState> accessor, IBotTelemetryClient telemetryClient)
            : base(botServices, nameof(ImmigrationDialog))
        {
            _accessor = accessor;
            InitialDialogId = nameof(ImmigrationDialog);

            var immigrationScenarios = new WaterfallStep[]
            {
                AskForAction,
                AskForTravel,
                AskForContactLegalAttorney,
                FinishImmigrationDialog
            };

            // To capture built-in waterfall dialog telemetry, set the telemetry client 
            // to the new waterfall dialog and add it to the component dialog
            TelemetryClient = telemetryClient;
            AddDialog(new WaterfallDialog(InitialDialogId, immigrationScenarios) { TelemetryClient = telemetryClient });
            AddDialog(new TextPrompt(DialogIds.AskForAction));
            AddDialog(new TextPrompt(DialogIds.AskForTravel));
            AddDialog(new TextPrompt(DialogIds.AskForContactLegalAttorney));
        }

        public async Task<DialogTurnResult> AskForAction(WaterfallStepContext sc, CancellationToken cancellationToken)
        {
            _state = await _accessor.GetAsync(sc.Context, () => new ImmigrationState());

            if (!string.IsNullOrEmpty(_state.ActionType))
            {
                return await sc.NextAsync(_state.ActionType);
            }
            else
            {
                return await sc.PromptAsync(DialogIds.AskForAction, new PromptOptions
                {
                    Prompt = await _responder.RenderTemplate(sc.Context, sc.Context.Activity.Locale, ImmigrationResponses.ResponseIds.AskForAction)
                });
            }
        }

        public async Task<DialogTurnResult> AskForTravel(WaterfallStepContext sc, CancellationToken cancellationToken)
        {
            _state = await _accessor.GetAsync(sc.Context, () => new ImmigrationState());
            var action = _state.ActionType = (string)sc.Result;
            // string[] countries = {"United States", "US", "UnitedStates","USA", "United States of America"};
            // || (countries.Any(action.Contains)

            if (action.Contains("documents"))
            {
                await _responder.ReplyWith(sc.Context, ImmigrationResponses.ResponseIds.HaveTravelOutsideUSMessage,
                    new {action});
            }

            return await sc.PromptAsync(DialogIds.AskForTravel, new PromptOptions()
            {
                Prompt = await _responder.RenderTemplate(sc.Context, sc.Context.Activity.Locale, ImmigrationResponses.ResponseIds.AskForContactLegalAttorney),
            });
        }

        public async Task<DialogTurnResult> AskForContactLegalAttorney(WaterfallStepContext sc, CancellationToken cancellationToken)
        {
            _state = await _accessor.GetAsync(sc.Context, () => new ImmigrationState());
            var travel = _state.LegalContactResponse = (string)sc.Result;

            if (travel.ToLower().Contains("yes"))
            {
                await _responder.ReplyWith(sc.Context, ImmigrationResponses.ResponseIds.HaveLegalMessage, new { travel });
            }

            return await sc.ContinueDialogAsync();

            //return await sc.PromptAsync(DialogIds.AskForContactLegalAttorney, new PromptOptions()
            //{
            //    Prompt = await _responder.RenderTemplate(sc.Context, sc.Context.Activity.Locale, ImmigrationResponses.ResponseIds.AskForContactLegalAttorney),
            //});
        }

        public async Task<DialogTurnResult> FinishImmigrationDialog(WaterfallStepContext sc, CancellationToken cancellationToken)
        {
            _state = await _accessor.GetAsync(sc.Context);
            _state.LegalContactResponse = (string)sc.Result;

            await _responder.ReplyWith(sc.Context, ImmigrationResponses.ResponseIds.HaveTravelMessage, new { _state.ActionType, _state.LegalContactResponse });

            //sc = null;
            await sc.CancelAllDialogsAsync(cancellationToken);
            return await sc.EndDialogAsync();
            //return await sc.BeginDialogAsync(MainResponses.ResponseIds.Completed);
        }

        private class DialogIds
        {
            public const string AskForAction = "actionPrompt";
            public const string AskForTravel = "travelPrompt";
            public const string AskForContactLegalAttorney = "legalPrompt";
        }
    }
}
