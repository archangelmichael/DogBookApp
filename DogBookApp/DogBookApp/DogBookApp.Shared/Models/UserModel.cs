using Parse;
using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Xaml.Media.Imaging;

namespace DogBookApp.Models
{
    public class UserModel : ParseUser
    {
        [ParseFieldName("nickname")]
        public string Nickname
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("age")]
        public string Age
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("gender")]
        public string Gender
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("breed")]
        public string Breed
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("avatar")]
        public ParseFile Picture
        {
            get { return GetProperty<ParseFile>(); }
            set { SetProperty<ParseFile>(value); }
        }

        [ParseFieldName("location")]
        public ParseGeoPoint Location
        {
            get { return GetProperty<ParseGeoPoint>(); }
            set { SetProperty<ParseGeoPoint>(value); }
        }

        [ParseFieldName("address")]
        public string Address
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("friends")]
        public IEnumerable<string> Friends
        {
            get { return GetProperty<IEnumerable<string>>(); }
            set { SetProperty<IEnumerable<string>>(value); }
        }
    }
}
