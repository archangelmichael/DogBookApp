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
        public FriendsPage()
            : this(new FriendsPageViewModel())
        {
        }

        public FriendsPage(FriendsPageViewModel viewModel)
        {
            this.InitializeComponent();
            this.DataContext = viewModel;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        private void AddFriendAppBarButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement adding new friend
        }

        private void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var list = sender as ListBox;
            var selectedFriend = list.SelectedItem;
            this.Frame.Navigate(typeof(Pages.FriendDetailsPage), selectedFriend);
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
    }
}
