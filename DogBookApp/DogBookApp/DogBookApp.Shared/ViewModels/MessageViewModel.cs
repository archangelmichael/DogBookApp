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
        public static Expression<Func<MessageModel, MessageViewModel>> FromModel
        {
            get
            {
                return model => new MessageViewModel()
                {
                    Content = model.Content,
                    CreatedAt = ((DateTime)model.CreatedAt).ToString("dd MM yyyy hh:mm:ss"),
                    SenderName = GetModel(model)
                };
            }
        }

        private static string GetModel(MessageModel model)
        {
            string nick = model.SenderNickname;
            return nick;
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
    }
}
