using GalaSoft.MvvmLight;
using Parse;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace DogBookApp.ViewModels
{
    public class LoginPageViewModel : ViewModelBase
    {
        public UserViewModel User { get; set; }

        public LoginPageViewModel()
        {
            this.User = new UserViewModel() { Username = "mydog", Password = "mypass" };
        }

        public async Task<bool> Login()
        {
            try
            {
                await ParseUser.LogInAsync(this.User.Username, this.User.Password);
                // Login was successful.
                return true;
            }
            catch (ParseException exc)
            {
                switch (exc.Code)
                {
                    case ParseException.ErrorCode.ObjectNotFound:
                    //Error code indicating the specified object doesn't exist.
                    case ParseException.ErrorCode.UsernameMissing:
                    //Error code indicating that the username is missing or empty.
                    case ParseException.ErrorCode.DuplicateValue:
                    //Error code indicating that a unique field was given a value that is already taken.
                    case ParseException.ErrorCode.PasswordMissing:
                    //Error code indicating that the password is missing or empty.
                    case ParseException.ErrorCode.UsernameTaken:
                    //Error code indicating that the username has already been taken.
                    case ParseException.ErrorCode.InternalServerError:
                    //Error code indicating that something has gone wrong with the server. If you get this error code, it is Parse's fault. Please report the bug to https://parse.com/help.
                    case ParseException.ErrorCode.ConnectionFailed:
                    // Error code indicating the connection to the Parse servers failed
                    default:
                        break;
                }

                return false;
            }
        }

        public async Task<bool> Register()
        {
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
                switch (exc.Code)
                {
                    case ParseException.ErrorCode.ObjectNotFound:
                    //Error code indicating the specified object doesn't exist.
                    case ParseException.ErrorCode.UsernameMissing:
                    //Error code indicating that the username is missing or empty.
                    case ParseException.ErrorCode.DuplicateValue:
                    //Error code indicating that a unique field was given a value that is already taken.
                    case ParseException.ErrorCode.PasswordMissing:
                    //Error code indicating that the password is missing or empty.
                    case ParseException.ErrorCode.UsernameTaken:
                    //Error code indicating that the username has already been taken.
                    case ParseException.ErrorCode.InternalServerError:
                        //Error code indicating that something has gone wrong with the server. If you get this error code, it is Parse's fault. Please report the bug to https://parse.com/help.
                    case ParseException.ErrorCode.ConnectionFailed: 
                    // Error code indicating the connection to the Parse servers failed
                    default:
                        break;
                }
                
                return false;
            }
            
        }
    }
}
