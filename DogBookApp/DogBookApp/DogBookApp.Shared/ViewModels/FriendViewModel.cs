using DogBookApp.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Windows.UI.Xaml.Media.Imaging;

namespace DogBookApp.ViewModels
{
    public class FriendViewModel : ViewModelBase
    {
        private const string DefaultAvatarUri = @"https://raw.githubusercontent.com/archangelmichael/DogBookApp/master/DogBookApp/DogBookApp/DogBookApp.Shared/Images/blank-avatar.png";
        private const string DefaultAvatar = "blank-avatar.png";

        private string id;
        private string name;
        private string age;
        private string gender;
        private string address;
        private BitmapImage avatar;
        private string breed;

        public static Expression<Func<UserModel, FriendViewModel>> FromUser
        {
            get
            {
                return user => new FriendViewModel()
                {
                    Id = user.ObjectId,
                    Name = user.Nickname,
                    Age = user.Age,
                    Gender = user.Gender,
                    Breed = user.Breed,
                    Address = user.Address,
                    Avatar = user.Picture.Name.Contains(DefaultAvatar) ? 
                        new BitmapImage(new Uri(DefaultAvatarUri, UriKind.RelativeOrAbsolute)) :
                        new BitmapImage(new Uri(user.Picture.Url.ToString(), UriKind.RelativeOrAbsolute))
                };
            }
        }

        public string Id
        {
            get
            {
                return this.id;
            }
            set
            {
                this.id = value;
                this.RaisePropertyChanged(() => this.Id);
            }
        }

        public string Name
        {
            get
            {
                return this.name;
            }
            set
            {
                this.name = value;
                this.RaisePropertyChanged(() => this.Name);
            }
        }

        public string Age
        {
            get
            {
                return this.age;
            }
            set
            {
                this.age = value;
                this.RaisePropertyChanged(() => this.Age);
            }
        }

        public string Breed
        {
            get
            {
                return this.breed;
            }
            set
            {
                this.breed = value;
                this.RaisePropertyChanged(() => this.Breed);
            }
        }

        public string Gender
        {
            get
            {
                return this.gender;
            }
            set
            {
                this.gender = value;
                this.RaisePropertyChanged(() => this.Gender);
            }
        }

        public string Address
        {
            get
            {
                return this.address;
            }
            set
            {
                this.address = value;
                this.RaisePropertyChanged(() => this.Address);
            }
        }

        public BitmapImage Avatar
        {
            get
            {
                return this.avatar;
            }
            set
            {
                this.avatar = value;
                this.RaisePropertyChanged(() => this.Avatar);
            }
        }
    }
}
