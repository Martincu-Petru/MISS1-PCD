using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Google.Cloud.Language.V1;
using LightChatApp.Controllers;
using LightChatApp.Models;
using Microsoft.EntityFrameworkCore;
using Google.Cloud.Translation.V2;

namespace LightChatApp.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string text)
        {

            if (user?.Length == 0)
            {
                user = "Unknown";
            }

            if (text?.Length == 0)
            {
                text = "Nothing";
            }

            var client = LanguageServiceClient.Create();
            var response = client.AnalyzeSentiment(new Document()
            {
                Content = TranslateText(text, "en"),
                Type = Document.Types.Type.PlainText
            });
            var sentiment = response.DocumentSentiment;

            if (sentiment.Score >= 0.5 && sentiment.Magnitude >= 0.3)
            {
                text += " 😍";
            }
            else if (sentiment.Score <= -0.3 && sentiment.Magnitude >= 0.3)
            {
                text += " 😭";
            }
            else
            {
                text += " 🤔";
            }

            await MessageController.AddMessage(new Message(user, text));
            await Clients.All.SendAsync("ReceiveMessage", user, text);
        }

        public async Task DisplayHistory(string userLanguage)
        {
            var messages = await MessageController.GetMessages();
            foreach (var message in messages)
            {
                message.Text = TranslateText(message.Text, userLanguage);
            }
            await Clients.All.SendAsync("ReceiveHistory", messages);
        }

        public string TranslateText(string text, string targetLanguage)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            TranslationClient client = TranslationClient.Create();
            string sourceLanguage = DetectLanguage(text);
            if (targetLanguage == sourceLanguage)
            {
                return text;
            }
            else
            {
                TranslationResult response = client.TranslateText(text, targetLanguage, sourceLanguage);
                return response.TranslatedText;
            }
        }

        public string DetectLanguage(string text)
        {
            TranslationClient client = TranslationClient.Create();
            var detection = client.DetectLanguage(text);

            return detection.Language;
        }

    }
}
