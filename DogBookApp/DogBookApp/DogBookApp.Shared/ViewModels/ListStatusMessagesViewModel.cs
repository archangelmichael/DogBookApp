using DogBookApp.Models;
using GalaSoft.MvvmLight;
using Parse;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Linq;
using Windows.UI.Xaml;

namespace DogBookApp.ViewModels
{
    public class ListStatusMessagesViewModel : ViewModelBase
    {
        private ObservableCollection<StatusViewModel> posts;
        private DispatcherTimer Refresher { get; set; }
        private bool initializing;

        public ListStatusMessagesViewModel()
        {
            this.Initializing = true;

            this.FetchAllPosts();
            this.Refresher = new DispatcherTimer();
            Refresher.Interval = TimeSpan.FromSeconds(30);
            Refresher.Tick += (obj, arg) =>
            {
                this.FetchAllPosts();
            };

            Refresher.Start();
        }

        public void Refresh()
        {
            this.FetchAllPosts();
        }

        public void StopRefresh()
        {
            this.Refresher.Stop();
        }

        public IEnumerable<StatusViewModel> Posts
        {
            get
            {
                if (this.posts == null)
                {
                    this.posts = new ObservableCollection<StatusViewModel>();
                }

                return this.posts;
            }
            set
            {
                if (this.posts == null)
                {
                    this.posts = new ObservableCollection<StatusViewModel>();
                }

                this.posts.Clear();
                foreach (var item in value)
                {
                    this.posts.Add(item);
                }
            }
        }

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

        private async void FetchAllPosts()
        {
            this.Initializing = true;

            var fetchedPosts = await new ParseQuery<StatusModel>()
                .OrderByDescending(mess => mess.CreatedAt)
                .FindAsync();

            this.Posts = fetchedPosts
                .AsQueryable()
                .Select(StatusViewModel.FromModel);

            this.Initializing = false;
        }
    }
}
