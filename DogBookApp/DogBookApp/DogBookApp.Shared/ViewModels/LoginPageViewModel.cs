using DogBookApp.Models;
using GalaSoft.MvvmLight;
using Parse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Xaml.Media.Imaging;

namespace DogBookApp.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        private MessageManager Messanger { get; set; }

        public UserViewModel User { get; set; }

        public LoginPageViewModel()
        {
            this.Messanger = MessageManager.Instance;
            this.User = new UserViewModel();
            //this.User = new UserViewModel() { Username = "mydog", Password = "mypass" };
        }

        public async Task<bool> Login()
        {
            if (this.IsInvalidInput(this.User.Username, this.User.Password))
            {
                return false;
            }

            try
            {
                await ParseUser.LogInAsync(this.User.Username, this.User.Password);
                // Login was successful.
                return true;
            }
            catch (ParseException exc)
            {
                this.ShowError(exc.Code);
                return false;
            }
        }

        public async Task<bool> Register()
        {
            if (this.IsInvalidInput(this.User.Username, this.User.Password))
            {
                return false;
            }

            var user = new ParseUser()
            {
                Username = this.User.Username,
                Password = this.User.Password
            };

            // other fields can be set just like with ParseObject
            try
            {
                await user.SignUpAsync();
                return true;
            }
            catch (ParseException exc)
            {
                this.ShowError(exc.Code);
                return false;
            }
        }

        private bool IsInvalidInput(string name, string pass)
        {
            if (name == null || name.Length < 3)
            {
                this.Messanger.ShowInvalidUsernameMessage();
                return true;
            }

            if (pass == null || pass.Length < 3)
            {
                this.Messanger.ShowInvalidPasswordMessage();
                return true;
            }

            return false;
        }

        private void ShowError(ParseException.ErrorCode errorCode)
        {
            switch (errorCode)
            {
                case ParseException.ErrorCode.ObjectNotFound:
                    this.Messanger.ShowObjectNotFoundMessage();
                    break;
                case ParseException.ErrorCode.UsernameMissing:
                    this.Messanger.ShowInvalidUsernameMessage();
                    break;
                case ParseException.ErrorCode.DuplicateValue:
                    this.Messanger.ShowDuplicateValueMessage();
                    break;
                case ParseException.ErrorCode.PasswordMissing:
                    this.Messanger.ShowInvalidPasswordMessage();
                    break;
                case ParseException.ErrorCode.UsernameTaken:
                    this.Messanger.ShowDuplicateUsernameMessage();
                    break;
                case ParseException.ErrorCode.InternalServerError:
                    this.Messanger.ShowServerErrorMessage();
                    break;
                case ParseException.ErrorCode.ConnectionFailed:
                    this.Messanger.ShowConnectionErrorMessage();
                    break;
                default:
                    this.Messanger.ShowErrorMessage();
                    break;
            }
        }
    }
}
