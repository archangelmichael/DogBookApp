using System;
using System.Collections.Generic;
using System.Text;
using Windows.Devices.Geolocation;

namespace DogBookApp.Models
{
    public class StatusMessage : Message
    {
        public StatusMessage() : base() { }

        public StatusMessage(string content, string senderId, string senderNick, DateTime date)
            : this(content, senderId, senderNick, date, null) { }

        public StatusMessage(string content, string senderId, string senderNick, DateTime date, Geoposition location)
            : base(content, senderId, senderNick, date, false)
        {
            this.Location = location;
        }

        public Geoposition Location { get; set; }
    }
}
