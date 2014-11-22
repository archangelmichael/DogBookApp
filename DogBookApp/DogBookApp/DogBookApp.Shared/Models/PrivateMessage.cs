using System;
using System.Collections.Generic;
using System.Text;

namespace DogBookApp.Models
{
    public class PrivateMessage : Message
    {
        public PrivateMessage() : base() { }

        public PrivateMessage(string content, string senderId, string senderNick, string receiverId)
            : base(content, senderId, senderNick, receiverId, false) { }
    }
}
