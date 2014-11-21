using DogBookApp.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DogBookApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class ProfilePage : Page
    {
        private MessageManager Messanger { get; set; }

        public ProfilePage()
        {
            this.InitializeComponent();
            this.Messanger = MessageManager.Instance;
        }

        private void ChangeAvatarButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement avatar changing
            this.ProfileAvatar.Source = new BitmapImage(new Uri(@"http://images.soulpancake.s3.amazonaws.com/4345269135_ff83289372_z.jpg", UriKind.RelativeOrAbsolute));
        }

        private bool isValidString(string input, string fieldName)
        {
            if (input == null || input.Length < 4)
            {
                Messanger.ShowMessage("Invalid " + fieldName, fieldName + "must be at least 4 symbols");
                return false;
            }

            return true;
        }

        private void EditProfileButton_Click(object sender, RoutedEventArgs e)
        {
            this.ProfileNicknameInput.IsEnabled = true;
            this.ProfileGenderSelector.IsEnabled = true;
            this.ProfileGenderSelector.Visibility = Windows.UI.Xaml.Visibility.Visible;
            this.ProfileGenderInput.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            this.ProfileAgeInput.IsEnabled = true;
            this.ProfileBreedInput.IsEnabled = true;
            this.ProfileAddressInput.IsEnabled = true;
            this.EditProfileButton.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            this.SaveProfileChangesButton.Visibility = Windows.UI.Xaml.Visibility.Visible;
        }

        private void SaveProfileChangesButton_Click(object sender, RoutedEventArgs e)
        {
            string nickname = this.ProfileNicknameInput.Text;
            if (!isValidString(nickname, "Nickname"))
            {
                return;
            }

            string ageInput = this.ProfileAgeInput.Text;
            int age = 1;
            if (ageInput != null && ageInput != string.Empty)
	        {
                if (!int.TryParse(ageInput, out age) || age < 0 || age > 30)
                {
                    Messanger.ShowMessage("Invalid Age", "Age must be positive number lower than 30");
                    return;
                }
                else
                {
                    this.ProfileAgeInput.Text = age.ToString();
                    // TODO: ADD AGE TO USER PROFILE
                }
            }
            else
            {
                this.ProfileAgeInput.Text = ageInput;
            }

            string breed = this.ProfileBreedInput.Text;

            GenderType gender = GenderType.NotSpecified;
            if (this.ProfileGenderSelector.SelectedValue != null)
            {
                string genderSelected = this.ProfileGenderSelector.SelectedValue.ToString();
                if (genderSelected != null)
                {
                    switch (genderSelected)
                    {
                        case "Male": gender = GenderType.Male;
                            break;
                        case "Female": gender = GenderType.Female;
                            break;
                        default: gender = GenderType.NotSpecified;
                            break;
                    }
                }
            }

            this.ProfileGenderInput.Text = gender.ToString();

            string address = this.ProfileAddressInput.Text;
            if (address != null && address.Length > 5)
            {
                // TODO: add address to user data
            }

            // TODO: Implement User Data Changes in DB

            this.ProfileNicknameInput.IsEnabled = false;
            this.ProfileGenderSelector.IsEnabled = false;
            this.ProfileGenderSelector.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            this.ProfileGenderInput.Visibility = Windows.UI.Xaml.Visibility.Visible;
            this.ProfileAgeInput.IsEnabled = false;
            this.ProfileBreedInput.IsEnabled = false;
            this.ProfileAddressInput.IsEnabled = false;
            this.EditProfileButton.Visibility = Windows.UI.Xaml.Visibility.Visible;
            this.SaveProfileChangesButton.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
        }
    }
}
