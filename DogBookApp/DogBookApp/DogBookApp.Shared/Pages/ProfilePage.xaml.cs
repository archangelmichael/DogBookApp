﻿using DogBookApp.Models;
using DogBookApp.ViewModels;
using Parse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.Storage.Streams;
using Windows.UI.Core;
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
        private const string AgeNotSpecified = "Not Specified";
        private const string LocationMessageTitle = "Location Info";
        private const string LocationErrorText = "Location Services Error";

        public ProfilePage()
            : this(new ProfilePageViewModel())
        {
        }

        public ProfilePage(ProfilePageViewModel viewModel)
        {
            this.InitializeComponent();
            
            this.ViewModel = viewModel;
            this.Messanger = MessageManager.Instance;
        }

        private MessageManager Messanger { get; set; }

        public ProfilePageViewModel ViewModel
        {
            get
            {
                return this.DataContext as ProfilePageViewModel;
            }
            set
            {
                this.DataContext = value;
            }
        }

        // AVATAR CHANGING
        private void ChangeAvatarButton_Click(object sender, RoutedEventArgs e)
        {
            this.SelectPictureFromGallery();
        }

        private async void SelectPictureFromGallery()
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.ViewMode = PickerViewMode.Thumbnail;

            // Filter to include a sample subset of file types.
            openPicker.FileTypeFilter.Clear();
            openPicker.FileTypeFilter.Add(".bmp");
            openPicker.FileTypeFilter.Add(".png");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".jpg");

            // Open the file picker.
            StorageFile file = await openPicker.PickSingleFileAsync();

            // file is null if user cancels the file picker.
            if (file != null)
            {
                // Open a stream for the selected file.
                IRandomAccessStream fileStream = await file.OpenAsync(FileAccessMode.Read);

                // Set the image source to the selected bitmap.
                BitmapImage bitmapImage = new BitmapImage();

                bitmapImage.SetSource(fileStream);

                RandomAccessStreamReference rasr = RandomAccessStreamReference.CreateFromFile(file);
                //RandomAccessStreamReference rasr = RandomAccessStreamReference.CreateFromUri(bitmapImage.UriSource);
                var streamWithContent = await rasr.OpenReadAsync();
                byte[] buffer = new byte[streamWithContent.Size];
                try
                {
                    await streamWithContent.ReadAsync(buffer.AsBuffer(), (uint)streamWithContent.Size, InputStreamOptions.None);
                    var data = buffer;
                    if (data != null)
                    {
                        var user = ParseUser.CurrentUser;
                        ParseFile img = new ParseFile("picture.png", data);
                        user["avatar"] = img;
                        await user.SaveAsync();
                        this.ProfileAvatar.Source = bitmapImage;
                        // set new avatar to database
                    }
                }
                catch (Exception)
                {
                    // TODO: Catch exception here
                }
            }
            else
            {
                return;
            }
        }

        private async void GetLocationButton_Click(object sender, RoutedEventArgs e)
        {
            Geolocator locator = new Geolocator();
            locator.DesiredAccuracy = PositionAccuracy.High;
            locator.MovementThreshold = 200.2;
            locator.PositionChanged += async (snd, arg) =>
            {
                await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal, () =>
                {
                    var position = arg.Position;
                    string newAddress = string.Format(
                        "Lives in {0} {1} \n Latitude:{2} \n Longitude:{3}",
                        position.CivicAddress.Country,
                        position.CivicAddress.City,
                        position.Coordinate.Latitude,
                        position.Coordinate.Longitude);
                    this.ProfileAddressInput.Text = newAddress;
                    this.ViewModel.UpdateAddress(newAddress);
                });
            };

            try
            {
                await locator.GetGeopositionAsync();
            }
            catch (Exception)
            {
                this.Messanger.ShowMessage(LocationMessageTitle, LocationErrorText);
            }
        }

        private void SaveProfileChangesButton_Click(object sender, RoutedEventArgs e)
        {
            string nickname = this.ProfileNicknameInput.Text;
            if (!isValidName(nickname, "Nickname"))
            {
                return;
            }

            string ageInput = this.ProfileAgeInput.Text;
            if (!isValidAge(ageInput))
            {
                return;
            }

            string gender = this.GetGender();
            this.ViewModel.UpdateProfile(nickname, ageInput, gender);

            this.DisableFieldsEditing();
        }

        private bool isValidName(string input, string fieldName)
        {
            if (input == null || input.Length < 4)
            {
                Messanger.ShowMessage("Invalid " + fieldName, fieldName + "must be at least 4 symbols");
                return false;
            }

            return true;
        }

        private bool isValidAge(string age)
        {
            if (age != AgeNotSpecified)
            {
                int ageValue = 0;
                if (int.TryParse(age, out ageValue))
                {
                    if (ageValue > 0 && ageValue < 30)
                    {
                        age = ageValue.ToString();
                        this.ProfileAgeInput.Text = age;
                        return true;
                    }
                }

                Messanger.ShowMessage("Invalid Age", "Age must be positive number lower than 30");
                this.ProfileAgeInput.Text = "Not Specified";
                return false;
            }
            
            return true;
        }

        private string GetGender()
        {
            string gender = "Not Specified";
            if (this.ProfileGenderSelector.SelectedValue != null)
            {
                gender = this.ProfileGenderSelector.SelectedValue.ToString();
            }

            this.ProfileGenderInput.Text = gender;
            return gender;
        }
 
        private void EditProfileButton_Click(object sender, RoutedEventArgs e)
        {
            this.EnableFieldsEditing();
        }

        private void DisableFieldsEditing()
        {
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

        private void EnableFieldsEditing()
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

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }
    }
}
