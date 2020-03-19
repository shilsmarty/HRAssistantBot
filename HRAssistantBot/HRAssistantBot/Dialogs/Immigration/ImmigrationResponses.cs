// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using FHL.Hack.SmartAssistantBot.Dialogs.Immigration.Resources;
using FHL.Hack.SmartAssistantBot.Dialogs.Onboarding.Resources;
using FHL.Hack.SmartAssistantBot.Dialogs.Immigration.Resources;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.TemplateManager;
using Microsoft.Bot.Schema;

namespace FHL.Hack.SmartAssistantBot.Dialogs.Immigrations
{
    public class ImmigrationResponses : TemplateManager
    {
        private static LanguageTemplateDictionary _responseTemplates = new LanguageTemplateDictionary
        {
            ["default"] = new TemplateIdMap
            {
                { ResponseIds.AskForTravel,
                    (context, data) =>
                    MessageFactory.Text(
                        text: ImmigrationStrings.TRAVEL_PROMPT,
                        ssml: ImmigrationStrings.TRAVEL_PROMPT,
                        inputHint: InputHints.ExpectingInput)
                },
                { ResponseIds.HaveTravelUSMessage,
                    (context, data) =>
                    MessageFactory.Text(
                        text: string.Format(ImmigrationStrings.HAVE_US),
                        ssml: string.Format(ImmigrationStrings.HAVE_US),
                        inputHint: InputHints.IgnoringInput)
                },
                { ResponseIds.HaveTravelOutsideUSMessage,
                    (context, data) =>
                        MessageFactory.Text(
                            text: string.Format(ImmigrationStrings.HAVE_NONUS),
                            ssml: string.Format(ImmigrationStrings.HAVE_NONUS),
                            inputHint: InputHints.IgnoringInput)
                },
                { ResponseIds.HaveSafeJourney,
                    (context, data) =>
                        MessageFactory.Text(
                            text: string.Format(ImmigrationStrings.TRAVEL_SAFE),
                            ssml: string.Format(ImmigrationStrings.TRAVEL_SAFE),
                            inputHint: InputHints.IgnoringInput)
                },
                { ResponseIds.HaveLegalMessage,
                    (context, data) =>
                    MessageFactory.Text(
                        text: string.Format(ImmigrationStrings.HAVE_ATTORNEY),
                        ssml: string.Format(ImmigrationStrings.HAVE_ATTORNEY),
                        inputHint: InputHints.IgnoringInput)
                },
                { ResponseIds.AskForAction,
                    (context, data) =>
                    MessageFactory.Text(
                        text: ImmigrationStrings.ACTION_PROMPT,
                        ssml: ImmigrationStrings.ACTION_PROMPT,
                        inputHint: InputHints.ExpectingInput)
                },
                { ResponseIds.AskForContactLegalAttorney,
                    (context, data) =>
                        MessageFactory.Text(
                            text: ImmigrationStrings.LEGAL_PROMPT,
                            ssml: ImmigrationStrings.LEGAL_PROMPT,
                            inputHint: InputHints.ExpectingInput)
                }
            }
        };

        public ImmigrationResponses()
        {
            Register(new DictionaryRenderer(_responseTemplates));
        }

        public class ResponseIds
        {
            public const string AskForAction = "actionPrompt";
            public const string AskForTravel = "travelPrompt";
            public const string AskForContactLegalAttorney = "legalPrompt";
            public const string HaveTravelMessage = "haveTravel";
            public const string HaveLegalMessage = "haveLegal";
            public const string HaveTravelUSMessage = "haveTravelUS";
            public const string HaveTravelOutsideUSMessage = "haveTravelOutsideUS";
            public const string HaveSafeJourney = "haveSafeJourney";
        }
    }
}
