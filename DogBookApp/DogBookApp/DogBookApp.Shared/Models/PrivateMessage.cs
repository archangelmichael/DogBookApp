using System;
using System.Collections.Generic;
using System.Text;

namespace DogBookApp.Models
{
    public class PrivateMessage : Message
    {
        public PrivateMessage(string text, string sender, string receiver)
            : base(text, sender, receiver, MessageType.Message, false)
        {
        }
    }
}
