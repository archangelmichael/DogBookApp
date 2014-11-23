using Parse;
using System;
using System.Collections.Generic;
using System.Text;

namespace DogBookApp.Models
{
    [ParseClassName("Message")]
    public class MessageModel : ParseObject
    {
        [ParseFieldName("content")]
        public string Content
        {
            get { return GetProperty<string>(); }
            set { SetProperty<string>(value); }
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
    }
}
