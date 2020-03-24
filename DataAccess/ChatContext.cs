using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Threading.Tasks;
using LightChatApp.Models;
using Microsoft.EntityFrameworkCore;

namespace LightChatApp.DataAccess
{
    public class ChatContext : DbContext
    {
        public DbSet<Message> Messages { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseMySQL("Uid=user;Pwd=user;Host=cloudsql;Database=chat_db;CharSet=utf8mb4;");
            // optionsBuilder.UseMySQL("Server=light-chat-app:europe-west1:database;Database=chat_db;Uid=user;Pwd=user;CharSet=utf8mb4;");
            // optionsBuilder.UseMySQL("Server=127.0.0.1;Database=chat_db;Uid=user;Pwd=user;CharSet=utf8mb4;");
        }
    }
}
