using System;
using System.Collections.Generic;
using System.Text;
using Windows.UI.Popups;

namespace DogBookApp.Models
{
    public class MessageManager
    {
        private static MessageManager instance;

        private MessageManager() { }

        public static MessageManager Instance
        {
            get 
            {
                if (instance == null)
                {
                    instance = new MessageManager();
                }
                return instance;
            }
        }

        public void ShowMessage(string messageTitle, string messageContent)
        {
            MessageDialog msgDialog = new MessageDialog(messageContent, messageTitle);

            //OK Button
            UICommand okBtn = new UICommand("OK");
            //okBtn.Invoked = OkBtnClick;
            msgDialog.Commands.Add(okBtn);

            /*
            //Cancel Button
            UICommand cancelBtn = new UICommand("Cancel");
            //cancelBtn.Invoked = CancelBtnClick;
            msgDialog.Commands.Add(cancelBtn);
            */
            //Show message
            msgDialog.ShowAsync();
        }
    }
}
