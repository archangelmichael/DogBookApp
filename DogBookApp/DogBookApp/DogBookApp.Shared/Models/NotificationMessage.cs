using System;
using System.Collections.Generic;
using System.Text;

namespace DogBookApp.Models
{
    public class NotificationMessage : Message
    {
        public NotificationMessage(string text, string sender, string receiver, bool hasOptions)
            : base(text, sender, receiver, MessageType.Notification, false)
        {
            this.hasOptions = hasOptions;
        }

        public bool hasOptions { get; set; }
    }
}
