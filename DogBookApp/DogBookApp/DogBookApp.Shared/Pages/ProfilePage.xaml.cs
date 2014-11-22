using DogBookApp.Models;
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
        private const string LocationMessageTitle = "Location Info";
        private const string LocationErrorText = "Location Services Error";

        private MessageManager Messanger { get; set; }

        

        public ProfilePage()
        {
            this.InitializeComponent();

            this.PopulateDataFields();
            this.Messanger = MessageManager.Instance;
        }

        private void PopulateDataFields()
        {
            ParseFile userAvatar = (ParseFile)ParseUser.CurrentUser["avatar"];
            if (userAvatar != null)
            {
                BitmapImage avatar = new BitmapImage(new Uri(userAvatar.Url.ToString(), UriKind.RelativeOrAbsolute));
                this.ProfileAvatar.Source = avatar;
            }

        }

        
        // AVATAR CHANGING
        private void ChangeAvatarButton_Click(object sender, RoutedEventArgs e)
        {
            this.SelectPictureFromGallery();
           // this.ProfileAvatar.Source = new BitmapImage(new Uri(@"http://images.soulpancake.s3.amazonaws.com/4345269135_ff83289372_z.jpg", UriKind.RelativeOrAbsolute));
        }

        private async void SelectPictureFromGallery()
        {
            Windows.Storage.Pickers.FileOpenPicker openPicker = new Windows.Storage.Pickers.FileOpenPicker();
            openPicker.SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.PicturesLibrary;
            openPicker.ViewMode = Windows.Storage.Pickers.PickerViewMode.Thumbnail;

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
                Windows.Storage.Streams.IRandomAccessStream fileStream =
                    await file.OpenAsync(Windows.Storage.FileAccessMode.Read);

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

                    throw;
                }
            }
            else
            {
                return;
            }
        }

        // INPUT VALIDATION
        private bool isValidString(string input, string fieldName)
        {
            if (input == null || input.Length < 4)
            {
                Messanger.ShowMessage("Invalid " + fieldName, fieldName + "must be at least 4 symbols");
                return false;
            }

            return true;
        }

        // ENABLE TEXT BOXES FOR EDITING 
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


        // SAVE CHANGES TO ACCOUNT
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
                    this.ProfileAddressInput.Text = string.Format(
                        "Lives in {0} {1} \n Latitude:{2} \n Longitude:{3}", 
                        position.CivicAddress.Country, 
                        position.CivicAddress.City,
                        position.Coordinate.Latitude, 
                        position.Coordinate.Longitude);
                    // TODO: Change Location in Used Data
                });

            };

            try
            {
                await locator.GetGeopositionAsync();
            }
            catch (Exception)
            {
                this.Messanger.ShowMessage(
                       LocationMessageTitle,
                       LocationErrorText);
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }
    }
}
