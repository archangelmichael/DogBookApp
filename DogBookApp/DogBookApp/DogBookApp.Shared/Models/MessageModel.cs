using Parse;
using System;
using System.Collections.Generic;
using System.Text;

namespace DogBookApp.Models
{
    [ParseClassName("Message")]
    public class MessageModel : ParseObject
    {
        [ParseFieldName("objectId")]
        public string Id
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
    }
}
