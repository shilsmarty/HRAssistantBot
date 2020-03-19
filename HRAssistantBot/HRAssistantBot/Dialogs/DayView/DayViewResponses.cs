// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using FHL.Hack.SmartAssistantBot.Dialogs.Onboarding.Resources;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.TemplateManager;
using Microsoft.Bot.Schema;
using System.IO;
using FHL.Hack.SmartAssistantBot.Dialogs.DayView.Resources;
using AdaptiveCards;

namespace FHL.Hack.SmartAssistantBot.Dialogs.DayView
{
    public class DayViewResponses : TemplateManager
    {
        private static LanguageTemplateDictionary _responseTemplates = new LanguageTemplateDictionary
        {
            ["default"] = new TemplateIdMap
            {
                { ResponseIds.DayViewPrompt, (context, data) => BuildDayViewCard(context, data) },
            }
        };

        public DayViewResponses()
        {
            Register(new DictionaryRenderer(_responseTemplates));
        }

        public static IMessageActivity BuildDayViewCard(ITurnContext turnContext, dynamic data)
        {
            var introCard = File.ReadAllText(DayViewStrings.DAYVIEW_PATH);
            var card = AdaptiveCard.FromJson(introCard).Card;
            var attachment = new Attachment(AdaptiveCard.ContentType, content: card);

            var response = MessageFactory.Attachment(attachment, ssml: card.Speak, inputHint: InputHints.IgnoringInput);

            return response;
        }

        public class ResponseIds
        {
            public const string DayViewPrompt = "dayViewBot";
        }
    }
}
