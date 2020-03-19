// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using System.IO;
using System.Linq;
using AdaptiveCards;
using FHL.Hack.SmartAssistantBot.Dialogs.AboutBot.Resources;
using FHL.Hack.SmartAssistantBot.Dialogs.Onboarding.Resources;
using FHL.Hack.SmartAssistantBot.Dialogs.Tar.Resources;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.Dialogs;
using Microsoft.Bot.Builder.Dialogs.Choices;
using Microsoft.Bot.Builder.TemplateManager;
using Microsoft.Bot.Schema;
using Newtonsoft.Json;

namespace FHL.Hack.SmartAssistantBot.Dialogs.Tar
{
    public class TarResponses : TemplateManager
    {
        private static LanguageTemplateDictionary _responseTemplates = new LanguageTemplateDictionary
        {
            ["default"] = new TemplateIdMap
            {
                {
                    ResponseIds.FeelBetterSoon,
                    (context, data) =>
                        MessageFactory.Text(
                            text: TarStrings.FEEL_BETTER_SOON,
                            ssml: TarStrings.FEEL_BETTER_SOON,
                            inputHint: InputHints.IgnoringInput)
                },
                {
                    ResponseIds.ConfirmSickDaySubmitted,
                    (context, data) =>
                        MessageFactory.Text(
                            text: TarStrings.CONFIRM_SICK_DAY_SUBMITTED,
                            ssml: TarStrings.CONFIRM_SICK_DAY_SUBMITTED,
                            inputHint: InputHints.ExpectingInput)
                },
                {
                    ResponseIds.AskSendNoteToTeam,
                    (context, data) =>
                        MessageFactory.Text(
                            text: TarStrings.ASK_TO_SEND_NOTES_TO_TEAM,
                            ssml: TarStrings.ASK_TO_SEND_NOTES_TO_TEAM,
                            inputHint: InputHints.ExpectingInput)
                },
                {
                    ResponseIds.SentNoteToTeam,
                    (context, data) =>
                        MessageFactory.Text(
                            text: TarStrings.SENT_NOTES_TO_TEAM,
                            ssml: TarStrings.SENT_NOTES_TO_TEAM,
                            inputHint: InputHints.ExpectingInput)
                },
                {
                    ResponseIds.MeetingsCancelled,
                    (context, data) =>
                        MessageFactory.Text(
                            text: TarStrings.MEETING_CANCELLED,
                            ssml: TarStrings.MEETING_CANCELLED,
                            inputHint: InputHints.ExpectingInput)
                },
                {
                    ResponseIds.TakeCare,
                    (context, data) =>
                        MessageFactory.Text(
                            text: TarStrings.TAKE_CARE,
                            ssml: TarStrings.TAKE_CARE,
                            inputHint: InputHints.ExpectingInput)
                }
            }
        };

        public TarResponses()
        {
            Register(new DictionaryRenderer(_responseTemplates));
        }

        public class ResponseIds
        {
            public const string FeelBetterSoon = "FeelBetterSoon";
            public const string ConfirmSickDaySubmitted = "ConfirmSickDaySubmitted";
            public const string AskSendNoteToTeam = "AskSendNoteToTeam";
            public const string SentNoteToTeam = "SentNoteToTeam";
            public const string MeetingsCancelled = "MeetingsCancelled";
            public const string TakeCare = "TakeCare";
        }
    }
}