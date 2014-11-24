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
    public sealed partial class FriendDetailsPage : Page
    {
        public FriendDetailsPage() : this(new FriendDetailsPageViewModel()) { }

        public FriendDetailsPage(FriendDetailsPageViewModel viewModel)
        {
            this.InitializeComponent();
            this.ViewModel = viewModel;
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.ViewModel.Friend = (e.Parameter as FriendViewModel);
            base.OnNavigatedTo(e);
        }

        public FriendDetailsPageViewModel ViewModel
        {
            get
            {
                return (FriendDetailsPageViewModel)this.DataContext; 
            }
            set
            {
                this.DataContext = value;
            }
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.GoBack();
        }

        private void SendMessageButton_Click(object sender, RoutedEventArgs e)
        {
            var receiver = this.ViewModel.Friend;
            this.Frame.Navigate(typeof(Pages.NewMessagePage), receiver);
        }

    }
}
