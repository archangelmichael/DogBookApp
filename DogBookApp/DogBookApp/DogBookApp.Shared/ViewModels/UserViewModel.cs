using DogBookApp.Models;
using Parse;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Media.Imaging;

namespace DogBookApp.ViewModels
{
    public class UserViewModel : BaseViewModel
    {
        public string UserId { get; set; }

        public string Username { get; set; }

        public string Password { get; set; }

        public string Nickname { get; set; }

        public string Age { get; set; }

        public string Gender { get; set; }

        public string Breed { get; set; }

        public BitmapImage Avatar { get; set; }

        public String Address { get; set; }
    }
}
