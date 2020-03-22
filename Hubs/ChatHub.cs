using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Google.Cloud.Language.V1;
using LightChatApp.Controllers;
using LightChatApp.Models;

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
            MessageController.AddMessage(new Message(user, text));

            var client = LanguageServiceClient.Create();
            var response = client.AnalyzeSentiment(new Document()
            {
                Content = text,
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

            await Clients.All.SendAsync("ReceiveMessage", user, text);
        }

    }
}
