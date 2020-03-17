using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Google.Cloud.Language.V1;
using Code.DataAccess;

namespace LightChatApp.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string user, string message)
        {
            var client = LanguageServiceClient.Create();
            var response = client.AnalyzeSentiment(new Document()
            {
                Content = message,
                Type = Document.Types.Type.PlainText
            });
            var sentiment = response.DocumentSentiment;

            await new MessageRepository().Add(new MessageModel { Message = message });

            if (sentiment.Score >= 0.5 && sentiment.Magnitude >= 0.3)
            {
                message += " 😍";
            }
            else if (sentiment.Score <= -0.3 && sentiment.Magnitude >= 0.3)
            {
                message += " 😭";
            }
            else
            {
                message += " 🤔";
            }

            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public async Task DisplayHistory()
        {
            var results = await new MessageRepository().GetAll();

            await Clients.All.SendAsync("ReceiveHistory", results);
        }
    }
}
