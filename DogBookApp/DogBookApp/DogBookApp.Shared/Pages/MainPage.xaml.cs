using DogBookApp.Common;
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
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DogBookApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationHelper = new Common.NavigationHelper(this);
            
            
            //this.CreateDataForCurrentUser();
            // ControlViewName.GoToStatusDetailsPage += new EventHandler(ControlGoToStatusDetailsPage);
            //var currentUser = ParseUser.CurrentUser;
            //currentUser["nickname"] = "The Charmer";
            //currentUser["age"] = 0;
            //currentUser["breed"] = "Airedale Terrier"; 
            //currentUser["gender"] = 0;
            //currentUser.SaveAsync();
        }

        public NavigationHelper NavigationHelper { get; set; }

        private void SignOutButton_Click(object sender, RoutedEventArgs e)
        {
            ParseUser.LogOut();
            this.Frame.Navigate(typeof(Pages.LoginPage));
        }

        private void TakePictureAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Pages.CaptureImagePage));
        }

        private void NotificationsAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Pages.NotificationsPage));
        }

        private void FriendsAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Pages.FriendsPage));
        }

        private void ProfileAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Pages.ProfilePage));
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        private void MessagesAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Pages.MessagesPage));
        }

        private async void CreateDataForCurrentUser()
        {
            UserModel user = await new ParseQuery<UserModel>()
                .Where(usr => usr.ObjectId != UserModel.CurrentUser.ObjectId).FirstAsync();
            UserModel currentUser = (UserModel)UserModel.CurrentUser;

            string[] titles = new string[]{"Alert", "Notification", "Friend Request"};
            string[] contents = new string [] {"You Have A New Message", "Your Profile Picture Has Been Changed", "{0} send you a friend request"};
            for (int i = 0; i < 3; i++)
            {
                var message = new MessageModel()
                {
                    SenderNickname = user.Nickname,
                    Sender = user,
                    Receiver = currentUser,
                    Content = "Common Dog Message With Smile! DJAF",
                    IsRead = false
                    
                };

                await message.SaveAsync();

                var alert = new NotificationModel()
                {
                    Title = titles[0],
                    Content = contents[0],
                    Receiver = currentUser,
                    Sender = currentUser,
                    HasOptions = false,
                    IsRead = false
                };

                await alert.SaveAsync();

                var note = new NotificationModel()
                {
                    Title = titles[1],
                    Content = contents[1],
                    Receiver = currentUser,
                    Sender = currentUser,
                    HasOptions = false,
                    IsRead = false
                };

                await note.SaveAsync();

                var request = new NotificationModel()
                {
                    Title = titles[2],
                    Content = string.Format(contents[2], currentUser.Username),
                    Sender = user,
                    Receiver = currentUser,
                    HasOptions = false,
                    IsRead = false
                };

                await request.SaveAsync();
            }
        }
    }
}
