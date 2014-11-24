using DogBookApp.Models;
using GalaSoft.MvvmLight;
using Parse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using Windows.Networking.Connectivity;
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
            if (!IsNetworkConnectionAvailable())
            {
                return false;
            }

            if (this.IsInvalidInput(this.User.Username, this.User.Password))
            {
                return false;
            }

            try
            {
                await UserModel.LogInAsync(this.User.Username, this.User.Password);
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
            if (!IsNetworkConnectionAvailable())
            {
                return false;
            }

            if (this.IsInvalidInput(this.User.Username, this.User.Password))
            {
                return false;
            }

            Stream data = new MemoryStream();
            var user = new UserModel()
            {
                Username = this.User.Username,
                Password = this.User.Password,
                Nickname = this.User.Username,
                Gender = "Not Specified",
                Age = "Not Specified",
                Breed = "Not Specified",
                Address = "No Address",
                Friends = new List<string>(),
                Location = new ParseGeoPoint(0, 0),
                Picture = new ParseFile("blank-avatar.png", data)
            };

            //var user = new UserModel()
            //{
            //    Username = this.User.Username,
            //    Password = this.User.Password
            //};

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

        private bool IsNetworkConnectionAvailable()
        {
            bool isConnected = NetworkInterface.GetIsNetworkAvailable();
            if (!isConnected)
            {
                isConnected = false;
                this.Messanger.ShowMessage("Connection Problem", "No internet connection is avaliable. DogBook Needs Live Internet Connection");
            }
            else
            {
                ConnectionProfile InternetConnectionProfile = NetworkInformation.GetInternetConnectionProfile();
                NetworkConnectivityLevel connection = InternetConnectionProfile.GetNetworkConnectivityLevel();
                if (connection == NetworkConnectivityLevel.None || connection == NetworkConnectivityLevel.LocalAccess)
                {
                    isConnected = false;

                    this.Messanger.ShowMessage("Connection Problem", "No internet connection is avaliable. DogBook Needs Live Internet Connection");
                }
                else
                {
                    isConnected = true;
                }
            }

            return isConnected;
        }
    }
}
