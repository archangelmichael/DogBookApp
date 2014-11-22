using System;
using System.Collections.Generic;
using System.Text;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace DogBookApp.Models
{
    public class StatusMessage : Message
    {
        public StatusMessage() : base() { }

        public StatusMessage(string content, string senderId, string senderNick, DateTime date)
            : base(content, senderId, senderNick, date, false) { }

        public BitmapImage Image { get; set; }

        public Geoposition Location { get; set; }

        public BitmapImage LocationImage { get; set; }
    }
}
