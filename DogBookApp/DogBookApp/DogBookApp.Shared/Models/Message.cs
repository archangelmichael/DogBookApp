using System;
using System.Collections.Generic;
using System.Text;

namespace DogBookApp.Models
{
    public class Message
    {
        public Message(string text, string sender, string receiver, MessageType type, bool isRead = false)
        {
            this.Content = text;
            this.SenderId = sender;
            this.ReceiverId = receiver;
            this.Type = type;
            this.IsRead = isRead;
        }

        public string Id { get; set; }

        public string Content { get; set; }

        public string SenderId { get; set; }

        public string ReceiverId { get; set; }

        public MessageType Type { get; set; }

        public bool IsRead { get; set; }
    }
}
