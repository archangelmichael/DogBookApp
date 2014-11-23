using DogBookApp.Models;
using GalaSoft.MvvmLight;
using Parse;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Media.Imaging;

namespace DogBookApp.ViewModels
{
    public class ProfilePageViewModel : ViewModelBase
    {
        private const string DefaultAvatarUri = @"https://raw.githubusercontent.com/archangelmichael/DogBookApp/master/DogBookApp/DogBookApp/DogBookApp.Shared/Images/blank-avatar.png";

        public UserViewModel User { get; set; }

        public ProfilePageViewModel()
        {
            //var currentUser = ParseUser.CurrentUser;
            //currentUser["address"] = "No Address";
            //currentUser.SaveAsync();

            this.populateUser();
        }

        private void populateUser()
        {
            var currentUser = ParseUser.CurrentUser;
            this.User = new UserViewModel() { UserId = currentUser.ObjectId, Username = currentUser.Username };
            this.User.Nickname = currentUser["nickname"].ToString();
            this.User.Gender = currentUser["gender"].ToString();
            this.User.Age = currentUser["age"].ToString();
            this.User.Breed = currentUser["breed"].ToString();
            this.User.Address = currentUser["address"].ToString();

            ParseFile userAvatar = (ParseFile)currentUser["avatar"];
            if (userAvatar.Name.Contains("blank-avatar.png"))
            {
                this.User.Avatar = new BitmapImage(new Uri(DefaultAvatarUri, UriKind.RelativeOrAbsolute));  
            }
            else
            {
                BitmapImage avatar = new BitmapImage(new Uri(userAvatar.Url.ToString(), UriKind.RelativeOrAbsolute));
                this.User.Avatar = avatar;
            }
        }

        public void UpdateAddress(string address)
        {
            ParseUser.CurrentUser["address"] = address;
            ParseUser.CurrentUser.SaveAsync();
        }

        public void UpdateProfile(string nickname, string age, string gender)
        {
            var currentUser = ParseUser.CurrentUser;
            currentUser["nickname"] = nickname;
            currentUser["age"] = age;
            currentUser["gender"] = gender;
            currentUser["breed"] = this.User.Breed;
            currentUser["address"] = this.User.Address;

            currentUser.SaveAsync();

            this.User.Nickname = nickname;
            this.User.Age = age;
            this.User.Gender = gender;
        }

        //public async Task<bool> Login()
        //{
        //    try
        //    {
        //        await ParseUser.LogInAsync(this.User.Username, this.User.Password);
        //        // Login was successful.
        //        return true;
        //    }
        //    }
        //}

        //public async Task<bool> Register()
        //{
        //    var user = new ParseUser()
        //    {
        //        Username = this.User.Username,
        //        Password = this.User.Password
        //    };
            
        //}
    }
}
