using System;
using System.Collections.Generic;
using System.Text;

namespace DogBookApp.Models
{
    public class Location
    {
        public Location(double longitude, double latitude, string address = "")
        {
            this.Longitude = longitude;
            this.Latitude = latitude;
            this.Address = address;
        }

        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public string Address { get; set; }
    }
}
