﻿using DogBookApp.Models;
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
    public sealed partial class MainPage : Page
    {
        public Common.NavigationHelper NavigationHelper { get; set; }

        public MainPage()
        {
            this.InitializeComponent();
            this.NavigationHelper = new Common.NavigationHelper(this);
        }

        private void BackButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO: Implement sign out
            this.NavigationHelper.GoBack();
        }

        private void OnButtonClick(object sender, RoutedEventArgs e)
        {

        }

        private void NewStatusMessageView_Loaded(object sender, RoutedEventArgs e)
        {

        }
    }
}
