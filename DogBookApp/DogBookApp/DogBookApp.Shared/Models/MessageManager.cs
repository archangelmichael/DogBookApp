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

        public async void ShowMessage(string messageTitle, string messageContent)
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

            await msgDialog.ShowAsync();
        }

        public void ShowInvalidUsernameMessage()
        {
            ShowMessage("Invalid Username!", "Username must be more than 3 symbols!");
        }

        public void ShowInvalidPasswordMessage()
        {
            ShowMessage("Invalid Password!", "Password must be more than 3 symbols!");
        }

        public void ShowObjectNotFoundMessage()
        {
            ShowMessage("User Not Registered!", "No Such User!");
        }

        public void ShowDuplicateUsernameMessage()
        {
            ShowMessage("Invalid Username!", "Username taken!");
        }

        public void ShowDuplicateValueMessage()
        {
            ShowMessage("Invalid Input!", "Already taken!");
        }

        public void ShowServerErrorMessage()
        {
            ShowMessage("Server Error!", "Cannot connect to server!");
        }

        public void ShowConnectionErrorMessage()
        {
            ShowMessage("Connection Error!", "Check internet connection!");
        }

        public void ShowErrorMessage()
        {
            ShowMessage("Error!", "Undefined Error Occured! Please Try Again!");
        }
    }
}
