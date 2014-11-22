using DogBookApp.Models;
using DogBookApp.Pages;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Core;
using Windows.UI.Popups;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
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

        public NewStatusMessageView()
        {
            this.InitializeComponent();
            this.Messanger = MessageManager.Instance;
            this.StatsManager = MessagesContainer.Instance;
            this.StatusMessage = new StatusMessage();
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

        private void AddPictureButton_Click(object sender, RoutedEventArgs e)
        {
        }

        private void AddVideoButton_Click(object sender, RoutedEventArgs e)
        {

        }

        private void SendStatusButton_Click(object sender, RoutedEventArgs e)
        {
            string statusContent = this.StatusMessageContent.Text;
            if (statusContent == null || statusContent == string.Empty)
            {
                this.Messanger.ShowMessage("Invalid Input", "Cannot post empty message!");
                return;
            }

            this.StatusMessage.CreatedAt = DateTime.Now;
            this.StatusMessage.SenderId = "CurrentId";
            this.StatusMessage.SenderNickName = "CurrentUser";
            this.StatusMessage.Content = statusContent;
            this.StatsManager.StatusMessages.Insert(0, this.StatusMessage);
            this.StatusMessage = new StatusMessage();
        }
    }
}
