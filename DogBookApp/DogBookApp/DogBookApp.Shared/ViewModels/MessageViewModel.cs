using DogBookApp.Models;
using GalaSoft.MvvmLight;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace DogBookApp.ViewModels
{
    public class MessageViewModel : ViewModelBase
    {
        private string content;
        private string createdAt;
        private string senderName;
        private string senderId;
        private string messageId;
        private string receiverId;

        public static Expression<Func<MessageModel, MessageViewModel>> FromModel
        {
            get
            {
                return model => new MessageViewModel()
                {
                    MessageId = model.ObjectId,
                    Content = model.Content,
                    CreatedAt = ((DateTime)model.CreatedAt).ToString("dd MM yyyy hh:mm:ss"),
                    SenderName = model.SenderNickname,
                    SenderId = model.Sender.ObjectId,
                    ReceiverId = UserModel.CurrentUser.ObjectId
                };
            }
        }

        public string MessageId
        {
            get
            {
                return this.messageId;
            }
            set
            {
                this.messageId = value;
                this.RaisePropertyChanged(() => this.MessageId);
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

        public string ReceiverId
        {
            get
            {
                return this.receiverId;
            }
            set
            {
                this.receiverId = value;
                this.RaisePropertyChanged(() => this.ReceiverId);
            }
        }
    }
}
