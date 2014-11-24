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
    public class NotificationsPageViewModel : ViewModelBase
    {
        private ObservableCollection<NotificationViewModel> alerts;
        private bool initializing;

        public NotificationsPageViewModel()
        {
            this.Initializing = true;
            this.Messanger = MessageManager.Instance;
            //this.FetchAllAlerts();

            //this.Refresh = new DispatcherTimer();
            //Refresh.Interval = TimeSpan.FromSeconds(10);
            //Refresh.Tick += (obj, arg) =>
            //{
            //    //this.FetchAllAlerts();
            //};

            //Refresh.Start();
        }

        private DispatcherTimer Refresh { get; set; }

        public IEnumerable<NotificationViewModel> Alerts
        {
            get
            {
                if (this.alerts == null)
                {
                    this.alerts = new ObservableCollection<NotificationViewModel>();
                }

                return this.alerts;
            }
            set
            {
                if (this.alerts == null)
                {
                    this.alerts = new ObservableCollection<NotificationViewModel>();
                }

                this.alerts.Clear();
                foreach (var item in value)
                {
                    this.alerts.Add(item);
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

        private async void FetchAllAlerts()
        {
            this.Initializing = true;

            var fetchedAlerts = await new ParseQuery<NotificationModel>()
                .Where(mess => mess.Receiver == ParseUser.CurrentUser && !mess.IsRead)
                .OrderByDescending(mess => mess.CreatedAt)
                .FindAsync();

            this.Alerts = fetchedAlerts
                .AsQueryable()
                .Select(NotificationViewModel.FromModel);

            this.Initializing = false;
        }
    }
}
