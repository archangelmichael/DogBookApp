using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Text;

namespace DogBookApp.ViewModels
{
    public class MessageDetailsPageViewModel : ViewModelBase
    {
        private MessageViewModel message;
        public MessageViewModel Message
        {
            get
            {
                return this.message;
            }
            set
            {
                this.message = value;
                this.RaisePropertyChanged(() => this.Message);
            }
        }
    }
}
