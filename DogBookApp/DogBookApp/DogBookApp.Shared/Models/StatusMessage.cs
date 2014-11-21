using System;
using System.Collections.Generic;
using System.Text;

namespace DogBookApp.Models
{
    public class StatusMessage : Message
    {
        public StatusMessage(string text, string sender, string receiver, Location location)
            : base(text, sender, receiver, MessageType.Status, false)
        {
            this.Location = location;
        }

        public Location Location { get; set; }
    }
}
