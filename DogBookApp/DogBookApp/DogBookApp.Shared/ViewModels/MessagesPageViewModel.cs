using DogBookApp.Models;
using GalaSoft.MvvmLight;
using Parse;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;

namespace DogBookApp.ViewModels
{
    public class MessagesPageViewModel : ViewModelBase
    {
        private ObservableCollection<MessageViewModel> messages;
        private bool initializing;

        public MessagesPageViewModel()
        {
            this.Initializing = true;
            this.Messanger = MessageManager.Instance;
            this.FetchAllMessagees();
        }

        public IEnumerable<MessageViewModel> Messages
        {
            get
            {
                if (this.messages == null)
                {
                    this.messages = new ObservableCollection<MessageViewModel>();
                }

                return this.messages;
            }
            set
            {
                if (this.messages == null)
                {
                    this.messages = new ObservableCollection<MessageViewModel>();
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

            var fetchedMessages = await new ParseQuery<MessageModel>()
                .Where(mess => mess.Receiver == ParseUser.CurrentUser)
                .OrderByDescending(mess => mess.CreatedAt)
                .FindAsync();

            this.Messages = fetchedMessages
                .AsQueryable()
                .Select(MessageViewModel.FromModel);

            this.Initializing = false;
        }
    }
}
