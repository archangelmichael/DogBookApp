using System;
using System.Collections.Generic;
using System.Text;

namespace DogBookApp.Models
{
    public class NotificationMessage : Message
    {
        public NotificationMessage() : base() { }

        /// <summary>
        /// Notification Message With No Options
        /// </summary>
        public NotificationMessage(string content, string receiverId)
            : base(content, receiverId, false)
        {
            this.hasOptions = false;
        }

        /// <summary>
        /// Notification Message With Options
        /// </summary>
        public NotificationMessage(string content, string senderId, string senderNick, string receiverId)
            : base(content, senderId, senderNick, receiverId, false)
        {
            this.hasOptions = true;
        }

        public bool hasOptions { get; set; }
    }
}
