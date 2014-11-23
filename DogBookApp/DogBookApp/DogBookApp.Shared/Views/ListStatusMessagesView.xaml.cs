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
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace DogBookApp.Views
{
    public sealed partial class ListStatusMessagesView : UserControl
    {
        public List<StatusMessage> StatusMessages { get; set; }

        public MessagesContainer StatsManager { get; set; }

        public ListStatusMessagesView()
        {
            this.InitializeComponent();
            this.StatsManager = MessagesContainer.Instance;
            this.StatusMessages = StatsManager.StatusMessages;
            this.DataContext = this;
        }

        private void StatusMessagesCustomList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            var list = sender as ListBox;

            var item = list.SelectedItem;
            // TODO: Implement imformation sending
            ((Frame)Window.Current.Content).Navigate(typeof(Pages.StatusDetailsPage), item);
        }
    }
}
