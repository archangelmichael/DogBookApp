using DogBookApp.Models;
using DogBookApp.Pages;
using Parse;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Streams;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace DogBookApp.Views
{
    public sealed partial class NewStatusMessageView : UserControl
    {
        private const string LocationSuccessFormat = "Location taken: \n {0} {1}";
        private const string LocationMessageTitle = "Location Info";
        private const string LocationErrorText = "Location Services Error";

        public MessagesContainer StatsManager { get; set; }
        private MessageManager Messanger { get; set; }
        private StatusMessage StatusMessage { get; set; }

        private StatusModel NewStatus { get; set; }

        public NewStatusMessageView()
        {
            this.InitializeComponent();
            this.Messanger = MessageManager.Instance;
            this.StatsManager = MessagesContainer.Instance;
            this.StatusMessage = new StatusMessage();
            this.NewStatus = new StatusModel();
        }

        private async void AddLocationButton_Click(object sender, RoutedEventArgs e)
        {
            Geolocator locator = new Geolocator();
            locator.DesiredAccuracy = PositionAccuracy.High;
            locator.MovementThreshold = 200.2;
            locator.PositionChanged += async (snd, arg) =>
            {
                await this.Dispatcher.RunAsync(CoreDispatcherPriority.Normal,() => {
                    var position = arg.Position;
                    this.StatusMessage.Location = position;
                    this.StatusMessageContent.Text += 
                        string.Format(
                            " at {0} {1}", 
                            position.CivicAddress.Country,
                            position.CivicAddress.City);
                    
                    this.Messanger.ShowMessage(
                        LocationMessageTitle, 
                        string.Format(
                            LocationSuccessFormat,
                            position.CivicAddress.Country, 
                            position.CivicAddress.City));
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

        private async void AddPictureButton_Click(object sender, RoutedEventArgs e)
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
                this.StatusMessage.Image = bitmapImage;
                this.Messanger.ShowMessage("Image Upload", "Image added Successfully!");
                this.DataContext = file;

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
                        ParseFile img = new ParseFile("picture.png", data);
                        this.NewStatus.Image = img;
                    }
                }
                catch (Exception)
                {
                    // TODO: Catch exception here
                }
            }
        }

        private void AddLocationImage()
        {
            var position = this.StatusMessage.Location.Coordinate;
            var latlon = position.Latitude + "," + position.Longitude;
            var googleApi = "http://maps.googleapis.com/maps/api/staticmap?center=";
            var options = "&zoom=15&size=400x300&sensor=false";
            var marker = "&markers=icon:http://thumb18.shutterstock.com/photos/thumb_large/347836/347836,1327514947,4.jpg|" + latlon;
            var locationOnMapUri = googleApi + latlon + options + marker;
            this.StatusMessage.LocationImage = new BitmapImage(new Uri(locationOnMapUri, UriKind.RelativeOrAbsolute));
        }

        private void AddVideoButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void SendStatusButton_Click(object sender, RoutedEventArgs e)
        {
            string statusContent = this.StatusMessageContent.Text;
            this.StatusMessageContent.Text = "";
            if (statusContent == null || statusContent == string.Empty)
            {
                this.Messanger.ShowMessage("Invalid Input", "Cannot post empty message!");
                return;
            }

            if (this.StatusMessage.Location != null)
            {
                this.AddLocationImage();
            }

            var currentUser = (UserModel)UserModel.CurrentUser;
            this.NewStatus.SenderNickname = currentUser.Nickname;
            this.NewStatus.Sender = currentUser;
            this.NewStatus.Content = statusContent;
            this.NewStatus.SaveAsync();

            this.StatusMessage.CreatedAt = DateTime.Now;
            this.StatusMessage.SenderId = ParseUser.CurrentUser.ObjectId;
            this.StatusMessage.SenderNickname = ParseUser.CurrentUser["nickname"].ToString();
            this.StatusMessage.Content = statusContent;
            this.StatsManager.StatusMessages.Insert(0, this.StatusMessage);
            this.StatusMessage = new StatusMessage();
        }
    }
}
