using DogBookApp.ViewModels;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Media.Imaging;

namespace DogBookApp.Models
{
    public class MessagesContainer : BaseViewModel
    {
        private static MessagesContainer instance;

        private ObservableCollection<StatusMessage> statusMessages;

        public List<StatusMessage> StatusMessages { get; set; }

        private MessagesContainer()
        {
            this.CreateAllKindsOfMessages();
        }

        private void CreateAllKindsOfMessages()
        {
            this.StatusMessages = new List<StatusMessage>() {
                new StatusMessage("I Love You ALL! ! :* :)", "DogLover", "Dog Lover", DateTime.Now ),
                new StatusMessage("I am out on a date tonight!! :) (happy) \n Wish me luck :)", "Don Juan", "Don Juan", DateTime.Now ),
                new StatusMessage(),
                new StatusMessage("OOOPsss i DIT it Again :D:D HAHA", "No Boundries", "No Boundries", DateTime.Now ),
                new StatusMessage(),
                new StatusMessage("How do you like my new haircut? :)", "Hairstyler", "Hairstyler", DateTime.Now ),
                new StatusMessage(),
                new StatusMessage("Anyone up for a threesome? :)", "Siblings", "Siblings", DateTime.Now ),
                new StatusMessage(),
                new StatusMessage(),
                new StatusMessage()
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
