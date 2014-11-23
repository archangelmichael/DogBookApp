using DogBookApp.Models;
using GalaSoft.MvvmLight;
using Parse;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DogBookApp.ViewModels
{
    public class MessagesPageViewModel : ViewModelBase
    {
        private ObservableCollection<MessageModel> messages;
        private bool initializing;

        public MessagesPageViewModel()
        {
            this.Initializing = true;
            this.Messanger = MessageManager.Instance;
            this.FetchAllMessagees();
        }

        public IEnumerable<MessageModel> Messages
        {
            get
            {
                if (this.messages == null)
                {
                    this.messages = new ObservableCollection<MessageModel>();
                }

                return this.messages;
            }
            set
            {
                if (this.messages == null)
                {
                    this.messages = new ObservableCollection<MessageModel>();
                }

                this.messages.Clear();
                foreach (var item in value)
                {
                    this.messages.Add(item);
                }
            }
        }

        private MessageManager Messanger { get; set; }

        private bool Initializing
        {
            get
            {
                return this.initializing;
            }
            set
            {
                this.initializing = value;
                this.RaisePropertyChanged(() => this.Initializing);
            }
        }

        private async void FetchAllMessagees()
        {
            this.Initializing = true;

            this.Messages = await new ParseQuery<MessageModel>()
                .Where(mess => mess.Receiver == ParseUser.CurrentUser)
                .OrderBy(mess => mess.CreatedAt)
                .FindAsync();

            this.Initializing = false;
        }
    }
}
