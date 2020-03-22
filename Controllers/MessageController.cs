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
        public static void AddMessage(Message message)
        {
            using (var context = new ChatContext())
            {
                message.Id = Guid.NewGuid().ToString();
                context.Messages.Add(message);
                context.SaveChanges();
            }
        }

        public static DbSet<Message> GetMessages()
        {
            using (var context = new ChatContext())
            {
                return context.Messages;
            }
        }
    }
}
