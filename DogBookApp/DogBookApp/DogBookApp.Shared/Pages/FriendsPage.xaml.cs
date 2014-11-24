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

namespace DogBookApp.Pages
{
    public sealed partial class FriendsPage : Page
    {
        private MessageManager Messanger { get; set; }

        public FriendsPage()
            : this(new FriendsPageViewModel())
        {
        }

        public FriendsPage(FriendsPageViewModel viewModel)
        {
            this.InitializeComponent();
            this.Messanger = MessageManager.Instance;
            this.DataContext = viewModel;
            this.SelectedFriend = new FriendViewModel();
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        private void AddFriendAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            //var currentUser = (UserModel)UserModel.CurrentUser;
            //if (this.isUserSelected())
            //{
            //    currentUser.AddUniqueToList("friends", this.SelectedFriend.Name);
            //    var selectedUser = await ParseUser.Query.GetAsync(this.SelectedFriend.Id);

            //    var friendRequest = new NotificationModel()
            //    {
            //        Title = "Friend Request",
            //        Content = string.Format("{0} sent you friend invitation. Do you accept it?", currentUser.Nickname),
            //        Sender = currentUser,
            //        Receiver = selectedUser,
            //        HasOptions = false,
            //        IsRead = false
            //    };

            //    this.Messanger.ShowMessage("Friend Added", string.Format("{0} added successfully to your friends", this.SelectedFriend.Name));
                
            //}

            //this.Messanger.ShowMessage("Invalid Selection", "Select User To Add");

            if(this.isUserSelected())
            {
                this.Messanger.ShowMessage("Friend Added", string.Format("{0} added successfully to your friends", this.SelectedFriend.Name));
            }
        }

        private void ListBox_Holding(object sender, HoldingRoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Pages.MessagesPage));

                // ADD SELECTED FRIEND TO LIST
            //var list = (sender as ListBox);
            //var selectedUser = (FriendViewModel) list.SelectedItem;
            //string selectedUserName = selectedUser.Name;
            //var currentUser = (UserModel)ParseUser.CurrentUser.AddUnique("friends", selecredUser.Nickname);
            //IList<string> friends = currentUser.Friends.ToList();
            //if (!friends.Contains(selectedUserName))
            //{
            //    friends.Add(selectedUser.Name);
            //}

            //currentUser.Friends = friends;
            //currentUser.SaveAsync();
        }

        private bool isUserSelected()
        {
            if (this.SelectedFriend.Id == null)
            {
                this.Messanger.ShowMessage("Invalid Selection", "Select User");
                return false;
            }

            return true;
        }

        private void SendMessageAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.isUserSelected())
            {
                this.Frame.Navigate(typeof(Pages.MessagesPage), this.SelectedFriend);
            }
        }

        private FriendViewModel SelectedFriend { get; set; }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var list = sender as ListBox;
            var selectedFriend = list.SelectedItem;
            this.SelectedFriend = selectedFriend as FriendViewModel;
            //this.Frame.Navigate(typeof(Pages.FriendDetailsPage), selectedFriend);
        }

        private void ChangeStatusAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Pages.MainPage));
        }

        private void ChangeProfileAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(Pages.ProfilePage));
        }

        private void SeeProfileAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            if (this.isUserSelected())
            {
                this.Frame.Navigate(typeof(Pages.FriendDetailsPage), this.SelectedFriend);
            }
        }
    }
}
