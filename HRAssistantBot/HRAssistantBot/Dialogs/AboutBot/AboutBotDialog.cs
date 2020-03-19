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
    public class AboutBotDialog : RouterDialog
    {
        private static AboutBotResponses _responder = new AboutBotResponses();
        private BotServices _services;
        private UserState _userState;
        private ConversationState _conversationState;

        public AboutBotDialog(BotServices services, ConversationState conversationState, UserState userState, IBotTelemetryClient telemetryClient)
            : base(nameof(AboutBotDialog))
        {
            InitialDialogId = nameof(AboutBotDialog);
            _conversationState = conversationState;
            _userState = userState;
            TelemetryClient = telemetryClient;
        }

        protected override Task RouteAsync(DialogContext innerDc, CancellationToken cancellationToken = default(CancellationToken))
        {
            throw new System.NotImplementedException();
        }
    }
}
