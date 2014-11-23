using DogBookApp.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using Windows.UI.Xaml.Media.Imaging;

namespace DogBookApp.ViewModels
{
    public class StatusViewModel : ViewModelBase
    {
        private string content;
        private string createdAt;
        private string senderName;
        private string senderId;
        private string statusId;
        private BitmapImage image;

        public static Expression<Func<StatusModel, StatusViewModel>> FromModel
        {
            get
            {
                return model => new StatusViewModel()
                {
                    StatusId = model.ObjectId,
                    Content = model.Content,
                    CreatedAt = ((DateTime)model.CreatedAt).ToString("dd MM yyyy hh:mm:ss"),
                    SenderName = model.SenderNickname,
                    SenderId = model.Sender.ObjectId,
                    Image = new BitmapImage(new Uri(model.Image.Url.ToString(), UriKind.RelativeOrAbsolute))
                };
            }
        }

        public string StatusId
        {
            get
            {
                return this.statusId;
            }
            set
            {
                this.statusId = value;
                this.RaisePropertyChanged(() => this.StatusId);
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

        public BitmapImage Image
        {
            get
            {
                return this.image;
            }
            set
            {
                this.image = value;
                this.RaisePropertyChanged(() => this.Image);
            }
        }
    }
}
