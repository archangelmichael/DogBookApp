using System;
using System.Collections.Generic;
using System.Text;

namespace DogBookApp.Models
{
    public class Message
    {
        public Message()
        {
        }

        public Message(string text, string sender, string receiver, bool isRead = false)
        {
            this.Content = text;
            this.SenderId = sender;
            this.ReceiverId = receiver;
            this.IsRead = isRead;
        }

        public string Id { get; set; }

        public string Content { get; set; }

        public string SenderId { get; set; }

        public string ReceiverId { get; set; }

        public bool IsRead { get; set; }
    }
}
