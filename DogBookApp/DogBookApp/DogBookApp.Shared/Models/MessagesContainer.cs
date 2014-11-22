using DogBookApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace DogBookApp.Models
{
    public class MessagesContainer : BaseViewModel
    {
        private static MessagesContainer instance;
        private ObservableCollection<NotificationMessage> notifications;
        private ObservableCollection<PrivateMessage> privateMessages;
        private ObservableCollection<StatusMessage> statusMessages;


        public List<NotificationMessage> Notifications { get; set; }
        public List<PrivateMessage> PrivateMessages { get; set; }
        public List<StatusMessage> StatusMessages { get; set; }

        private MessagesContainer()
        {
            this.CreateAllKindsOfMessages();
        }

        private void CreateAllKindsOfMessages()
        {
            this.StatusMessages = new List<StatusMessage>() {
                new StatusMessage(),
                new StatusMessage(),
                new StatusMessage(),
                new StatusMessage(),
                new StatusMessage(),
                new StatusMessage(),
                new StatusMessage(),
                new StatusMessage(),
                new StatusMessage(),
                new StatusMessage(),
                new StatusMessage()
            };

            this.PrivateMessages = new List<PrivateMessage>() {
                new PrivateMessage(),
                new PrivateMessage(),
                new PrivateMessage(),
                new PrivateMessage(),
                new PrivateMessage(),
                new PrivateMessage(),
                new PrivateMessage(),
                new PrivateMessage(),
                new PrivateMessage(),
                new PrivateMessage(),
                new PrivateMessage()
            };

            this.Notifications = new List<NotificationMessage>() {
                new NotificationMessage(),
                new NotificationMessage(),
                new NotificationMessage(),
                new NotificationMessage(),
                new NotificationMessage(),
                new NotificationMessage(),
                new NotificationMessage(),
                new NotificationMessage(),
                new NotificationMessage(),
                new NotificationMessage(),
                new NotificationMessage()
            };
        }

        public static MessagesContainer Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new MessagesContainer();
                }
                return instance;
            }
        }
    }
}
