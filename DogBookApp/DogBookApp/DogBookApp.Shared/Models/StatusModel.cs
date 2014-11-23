using Parse;
using System;
using System.Collections.Generic;
using System.Text;

namespace DogBookApp.Models
{
    [ParseClassName("Status")]
    public class StatusModel : ParseObject
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

        [ParseFieldName("senderNickname")]
        public string SenderNickname
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
