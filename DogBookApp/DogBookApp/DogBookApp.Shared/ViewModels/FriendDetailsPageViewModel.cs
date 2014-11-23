using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;

namespace DogBookApp.ViewModels
{
    public class FriendDetailsPageViewModel : ViewModelBase
    {
        private FriendViewModel friend;
        public FriendViewModel Friend
        {
            get
            {
                return this.friend;
            }
            set
            {
                this.friend = value;
                this.RaisePropertyChanged(() => this.Friend);
            }
        }
    }
}
