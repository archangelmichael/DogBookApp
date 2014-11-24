using DogBookApp.Models;
using DogBookApp.ViewModels;
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
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace DogBookApp.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class StatusDetailsPage : Page
    {
#if WINDOWS_PHONE_APP
        private const double DefaultEnvironmentImageWidth = 120;
#endif
#if WINDOWS_APP
        private const double DefaultEnvironmentImageWidth = 300;
#endif

        public StatusViewModel CurrentStatusMessage { get; set; }

        public StatusDetailsPage()
        {
            this.InitializeComponent();
            this.StatusImage.Width = DefaultEnvironmentImageWidth;
            this.DataContext = CurrentStatusMessage;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            var data = e.Parameter as StatusViewModel;
            if (data != null)
            {
                this.CurrentStatusMessage = data;
            }
            this.DataContext = CurrentStatusMessage;
            base.OnNavigatedTo(e);
        }

        private void Image_Tapped(object sender, TappedRoutedEventArgs e)
        {
            var image = (sender as Image);
            image.Width = DefaultEnvironmentImageWidth;
        }

        private void Image_DoubleTapped(object sender, DoubleTappedRoutedEventArgs e)
        {
            var image = (sender as Image);
            image.Width *= 2;
            image.Height *= 2;
            e.Handled = true;
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        private void StatusImage_Drop(object sender, DragEventArgs e)
        {
            
        }

        private void StatusImage_ManipulationStarted(object sender, ManipulationStartedRoutedEventArgs e)
        {

        }

        private void StatusImage_ManipulationCompleted(object sender, ManipulationCompletedRoutedEventArgs e)
        {

        }

        private void StatusImage_ManipulationDelta(object sender, ManipulationDeltaRoutedEventArgs e)
        {

        }
    }
}
