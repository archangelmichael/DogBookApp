using Parse;
using System;
using System.Collections.Generic;
using System.Text;

namespace DogBookApp.Models
{
    [ParseClassName("Status")]
    public class StatusModel : ParseObject
    {
        [ParseFieldName("objectId")]
        public string ObjectId
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("content")]
        public string Content
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
        }

        [ParseFieldName("createdAt")]
        public DateTime CreatedAt
        {
            get { return GetProperty<DateTime>(); }
            set { SetProperty<DateTime>(value); }
        }

        [ParseFieldName("sender")]
        public ParseUser Sender
        {
            get { return GetProperty<ParseUser>(); }
            set { SetProperty<ParseUser>(value); }
        }

        [ParseFieldName("isRead")]
        public bool IsRead
        {
            get { return GetProperty<bool>(); }
            set { SetProperty<bool>(value); }
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

        [ParseFieldName("image")]
        public ParseFile Image
        {
            get { return GetProperty<ParseFile>(); }
            set { SetProperty<ParseFile>(value); }
        }
    }
}
