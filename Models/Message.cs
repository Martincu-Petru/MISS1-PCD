using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace LightChatApp.Models
{
    [Table("history")]
    public class Message
    {
        [Key]
        public string Id { get; set; }
        public string User { get; set; }

        public string Text { get; set; }

        [Column("message_date")]
        public DateTime MessageDate { get; set; }

        public Message(string user, string text)
        {
            User = user;
            Text = text;
        }

    }
}
