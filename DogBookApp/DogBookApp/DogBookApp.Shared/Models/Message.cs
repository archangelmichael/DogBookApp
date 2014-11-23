using System;
using System.Collections.Generic;
using System.Text;

namespace DogBookApp.Models
{
    public class Message
    {
        public Message() 
            : this("No Text", DateTime.Now, "No ID", "Anonymous", "No Receiver", false) { }

        /// <summary>
        /// Notification Message With No Options Constructor
        /// </summary>
        public Message(string content, string receiverId, bool isRead = false)
            : this(content, DateTime.Now, "No ID", "Anonymous", receiverId, false) { }

        /// <summary>
        /// Status Message Constructor
        /// </summary>
        public Message(string content, string senderId, string sender, DateTime date, bool isRead = false)
            : this(content, date, "No ID", sender, "No Receiver", false) { }

        /// <summary>
        /// Private Message And Notification Message With Options Constructor
        /// </summary>
        public Message(string content, string senderId, string senderNick, string receiverId, bool isRead = false)
            : this(content, DateTime.Now, senderId, senderNick, receiverId, false) { }

        /// <summary>
        /// Full Constructor
        /// </summary>
        public Message(string content, DateTime date, string senderId, string senderNick, string receiverId, bool isRead = false)
        {
            this.Content = content;
            this.CreatedAt = date;
            this.SenderId = senderId;
            this.SenderNickname = senderNick;
            this.ReceiverId = receiverId;
            this.IsRead = isRead;
        }

        public string Id { get; set; }

        public string Content { get; set; }

        public DateTime CreatedAt { get; set; }

        public string SenderId { get; set; }

        public string SenderNickname { get; set; }

        public string ReceiverId { get; set; }

        public bool IsRead { get; set; }
    }
}
