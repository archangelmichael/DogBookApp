using DogBookApp.Models;
using Parse;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Media.Imaging;

namespace DogBookApp.ViewModels
{
    public class UserViewModel : ParseUser
    {
        [ParseFieldName("objectId")]
        public string UserId { get; set; }

        [ParseFieldName("username")]
        public string Username { get; set; }

        [ParseFieldName("password")]
        public string Password { get; set; }

        [ParseFieldName("nickname")]
        public string Nickname { get; set; }

        [ParseFieldName("age")]
        public string Age { get; set; }

        [ParseFieldName("gender")]
        public string Gender { get; set; }

        [ParseFieldName("breed")]
        public string Breed { get; set; }

        [ParseFieldName("avatar")]
        public ParseFile Picture { get; set; }

        public BitmapImage Avatar { get; set; }

        [ParseFieldName("location")]
        public ParseGeoPoint Location { get; set; }

        [ParseFieldName("address")]
        public String Address { get; set; }

        [ParseFieldName("friends")]
        public IEnumerable<ParseUser> Friends { get; set; }
    }
}
