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
    public class FriendsPageViewModel : ViewModelBase
    {
        private ObservableCollection<FriendViewModel> friends;
        private bool initializing;

        public FriendsPageViewModel()
        {
            this.Initializing = true;
            this.Messanger = MessageManager.Instance;
            this.FetchAllMessagees();
        }

        public IEnumerable<FriendViewModel> Friends
        {
            get
            {
                if (this.friends == null)
                {
                    this.friends = new ObservableCollection<FriendViewModel>();
                }

                return this.friends;
            }
            set
            {
                if (this.friends == null)
                {
                    this.friends = new ObservableCollection<FriendViewModel>();
                }

                this.friends.Clear();
                foreach (var item in value)
                {
                    this.friends.Add(item);
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

            string currentUserId = ParseUser.CurrentUser.ObjectId;

            //var names = (string[])ParseUser.CurrentUser["friends"];
            //var query = from user in UserModel.GetQuery("User")
            //            where user.ObjectId != currentUserId && names.Contains(user.ObjectId)
            //            select user;

            var fetchedUsers = await new ParseQuery<UserModel>()
                .Where(user => user.ObjectId != currentUserId)
                .OrderByDescending(user => user.Username)
                .FindAsync();

            var allUsers = fetchedUsers
                .AsQueryable()
                .Select(FriendViewModel.FromUser);

            this.Friends = allUsers;
            
            //UserModel currentUser = await new ParseQuery<UserModel>().GetAsync(currentUserId);
            //List<string> friendNames = (List<string>)ParseUser.CurrentUser["friends"];
            //if (friendNames != null)
            //{
            //    IList<FriendViewModel> currentUserFriends = new List<FriendViewModel>();
            //    foreach (var user in allUsers)
            //    {
            //        if (friendNames.Contains(user.Name))
            //        {
            //            currentUserFriends.Add(user);
            //        }
            //    }

            //    this.Friends = currentUserFriends;
            //}
            

            this.Initializing = false;
        }
    }
}
