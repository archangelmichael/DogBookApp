using System;
using System.Collections.Generic;
using System.Text;
using Windows.Devices.Geolocation;

namespace DogBookApp.Models
{
    public class StatusMessage : Message
    {
        public StatusMessage()
        {
        }

        public StatusMessage(string text, string sender, string receiver, Geoposition location)
            : base(text, sender, receiver, false)
        {
            this.Location = location;
        }

        public Geoposition Location { get; set; }
    }
}
