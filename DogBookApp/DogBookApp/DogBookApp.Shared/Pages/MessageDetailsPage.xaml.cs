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
    public sealed partial class MessageDetailsPage : Page
    {
        public MessageDetailsPage() : this(new MessageDetailsPageViewModel()) { }

        public MessageDetailsPage(MessageDetailsPageViewModel viewModel)
        {
            this.InitializeComponent();

            this.ViewModel = viewModel;
        }

        public MessageDetailsPageViewModel ViewModel
        {
            get
            {
                return (MessageDetailsPageViewModel)this.DataContext; 
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

        private void ReplyButton_Click(object sender, RoutedEventArgs e)
        {
            var receiverId = this.ViewModel.Message.SenderId;
            this.Frame.Navigate(typeof(Pages.NewMessagePage), receiverId);
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            this.ViewModel.Message = e.Parameter as MessageViewModel;
            base.OnNavigatedTo(e);
        }

        
    }
}
