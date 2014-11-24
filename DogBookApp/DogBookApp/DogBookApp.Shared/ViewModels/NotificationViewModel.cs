using DogBookApp.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DogBookApp.ViewModels
{
    public class NotificationViewModel : ViewModelBase
    {
        private string notificationId;
        private string title;
        private string content;
        private string createdAt;
        private string senderName;
        private string senderId;
        private bool isRead;
        private bool hasOptions;

        public static Expression<Func<NotificationModel, NotificationViewModel>> FromModel
        {
            get
            {
                return model => new NotificationViewModel()
                {
                    NotificationId = model.ObjectId,
                    Title = model.Title,
                    Content = model.Content,
                    CreatedAt = ((DateTime) model.CreatedAt).ToString("dd MM YY hh:mm:ss"),
                    SenderName = model.Sender.Username,
                    senderId = model.Sender.ObjectId,
                    HasOptions = model.HasOptions,
                    IsRead = model.IsRead
                };
            }
        }

        public string NotificationId
        {
            get
            {
                return this.notificationId;
            }
            set
            {
                this.notificationId = value;
                this.RaisePropertyChanged(() => this.NotificationId);
            }
        }

        public string Title
        {
            get
            {
                return this.title;
            }
            set
            {
                this.title = value;
                this.RaisePropertyChanged(() => this.Title);
            }
        }

        public string Content
        {
            get
            {
                return this.content;
            }
            set
            {
                this.content = value;
                this.RaisePropertyChanged(() => this.Content);
            }
        }

        public string CreatedAt
        {
            get
            {
                return this.createdAt;
            }
            set
            {
                this.createdAt = value;
                this.RaisePropertyChanged(() => this.CreatedAt);
            }
        }

        public string SenderName
        {
            get
            {
                return this.senderName;
            }
            set
            {
                this.senderName = value;
                this.RaisePropertyChanged(() => this.SenderName);
            }
        }

        public string SenderId
        {
            get
            {
                return this.senderId;
            }
            set
            {
                this.senderId = value;
                this.RaisePropertyChanged(() => this.SenderId);
            }
        }

        public bool IsRead
        {
            get
            {
                return this.isRead;
            }
            set
            {
                this.isRead = value;
                this.RaisePropertyChanged(() => this.IsRead);
            }
        }

        public bool HasOptions
        {
            get
            {
                return this.hasOptions;
            }
            set
            {
                this.hasOptions = value;
                this.RaisePropertyChanged(() => this.HasOptions);
            }
        }
    }
}
