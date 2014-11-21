using System;
using System.Collections.Generic;
using System.Text;

namespace DogBookApp.Models
{
    public class UserProfile
    {
        public UserProfile(string username, string nickname, string password, GenderType gender, string breed = "DOG")
        {
            
        }

        public string UserId { get; set; }

        public string Username { get; set; }

        public string Nickname { get; set; }

        public string Password { get; set; }

        public int Age { get; set; }

        public GenderType Gender { get; set; }

        public string Breed { get; set; }

        public string Avatar { get; set; }

        public Location location { get; set; }
    }
}
