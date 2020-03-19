// Copyright (c) Microsoft Corporation. All rights reserved.
// Licensed under the MIT License.

using FHL.Hack.SmartAssistantBot.Dialogs.Onboarding.Resources;
using Microsoft.Bot.Builder;
using Microsoft.Bot.Builder.TemplateManager;
using Microsoft.Bot.Schema;
using System.IO;
using FHL.Hack.SmartAssistantBot.Dialogs.AboutBot.Resources;
using AdaptiveCards;

namespace FHL.Hack.SmartAssistantBot.Dialogs.Onboarding
{
    public class AboutBotResponses : TemplateManager
    {
        private static LanguageTemplateDictionary _responseTemplates = new LanguageTemplateDictionary
        {
            ["default"] = new TemplateIdMap
            {
                { ResponseIds.AboutBot, (context, data) => BuildAboutBotCard(context, data) },
            }
        };

        public AboutBotResponses()
        {
            Register(new DictionaryRenderer(_responseTemplates));
        }

        public static IMessageActivity BuildAboutBotCard(ITurnContext turnContext, dynamic data)
        {
            var introCard = File.ReadAllText(AboutBotStrings.ABOUTBOT_PATH);
            var card = AdaptiveCard.FromJson(introCard).Card;
            var attachment = new Attachment(AdaptiveCard.ContentType, content: card);

            var response = MessageFactory.Attachment(attachment, ssml: card.Speak, inputHint: InputHints.IgnoringInput);

            return response;
        }

        public class ResponseIds
        {
            public const string AboutBot = "aboutBot";
        }
    }
}
