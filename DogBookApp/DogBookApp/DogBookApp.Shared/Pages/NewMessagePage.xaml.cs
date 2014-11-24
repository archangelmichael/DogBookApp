using DogBookApp.Models;
using DogBookApp.ViewModels;
using Parse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DogBookApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class NewMessagePage : Page
    {
        private MessageModel NewMessage { get; set; }
        private NotificationModel NewMessageNotification { get; set; }

        public NewMessagePage()
        {
            this.InitializeComponent();
            this.NewMessage = new MessageModel()
            {
                Sender = (UserModel) UserModel.CurrentUser,
                SenderNickname = ((UserModel)UserModel.CurrentUser).Nickname,
                IsRead = false
            };

            this.NewMessageNotification = new NotificationModel()
            {
                Title = "Alert",
                Content = "You Have A New Message",
                Sender = ParseUser.CurrentUser,
                HasOptions = false,
                IsRead = false
            };
           
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            var receiver = e.Parameter as FriendViewModel;
            if (receiver != null)
            {
                try
                {
                    var receivingUser = await ParseUser.Query.GetAsync(receiver.Id);
                    this.NewMessage.Receiver = receivingUser;
                    this.NewMessageNotification.Receiver = receivingUser;
                }
                catch (Exception)
                {
                    
                    throw;
                }
            }
            else
            {
                var currentUser = ParseUser.CurrentUser;
                this.NewMessage.Receiver = currentUser;
                this.NewMessageNotification.Receiver = currentUser;
            }




            base.OnNavigatedTo(e);
        }

        private void SendMessageButton_Click(object sender, RoutedEventArgs e)
        {
            string content = this.MessageTextInputField.Text;
            if (content.Length < 3)
	        {
		        // Send Message For Invalid Content
                return;
	        }

            this.NewMessage.Content = content;
            this.NewMessage.SaveAsync();
            this.NewMessageNotification.SaveAsync();

            // Send Message Confirmation

            this.Frame.Navigate(typeof(Pages.MainPage));
        }
    }
}
