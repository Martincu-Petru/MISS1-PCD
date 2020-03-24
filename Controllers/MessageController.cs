using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using LightChatApp.DataAccess;
using LightChatApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LightChatApp.Controllers
{
    public static class MessageController
    {
        private static readonly HttpClient httpClient = new HttpClient();

        public static async Task AddMessage(Message message)
        {
            using (var context = new ChatContext())
            {
                message.Id = Guid.NewGuid().ToString();
                message.MessageDate = DateTime.Now;
                context.Messages.Add(message);
                await context.SaveChangesAsync();
            }

            await httpClient.SendAsync(new HttpRequestMessage(HttpMethod.Get,
                new Uri("https://us-central1-light-chat-app.cloudfunctions.net/logging_requests?id=" + message.Id)));
        }

        public static async Task<List<Message>> GetMessages()
        {
            List<Message> messages = null;
            using (var context = new ChatContext())
            {
                messages = await context.Messages.ToListAsync();
            }

            return messages.OrderBy(x => x.MessageDate).ToList();
        }
    }
}
