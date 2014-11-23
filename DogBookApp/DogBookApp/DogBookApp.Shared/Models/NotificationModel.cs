using Parse;
using System;
using System.Collections.Generic;
using System.Text;

namespace DogBookApp.Models
{
    [ParseClassName("Notification")]
    public class NotificationModel : ParseObject
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

        [ParseFieldName("receiver")]
        public ParseUser Receiver
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

        [ParseFieldName("hasOptions")]
        public bool HasOptions
        {
            get { return GetProperty<bool>(); }
            set { SetProperty<bool>(value); }
        }
    }
}
