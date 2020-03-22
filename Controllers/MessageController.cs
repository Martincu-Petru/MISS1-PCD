using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LightChatApp.DataAccess;
using LightChatApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LightChatApp.Controllers
{
    public static class MessageController
    {
        public static async Task AddMessage(Message message)
        {
            using (var context = new ChatContext())
            {
                message.Id = Guid.NewGuid().ToString();
                context.Messages.Add(message);
                await context.SaveChangesAsync();
            }
        }

        public static async Task<List<Message>> GetMessages()
        {
            List<Message> messages = null;
            using (var context = new ChatContext())
            {
                messages = await context.Messages.ToListAsync();
            }
            messages.Reverse();

            return messages;
        }
    }
}
