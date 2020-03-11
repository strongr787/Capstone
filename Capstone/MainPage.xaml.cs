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

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace Capstone
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class MainPage : Page
    {
        private bool IsMenuExpanded = false;
        public MainPage()
        {
            this.InitializeComponent();
            // hide the main menu
            this.MenuColumn.Width = new GridLength(0);
        }

        private void MenuButton_OnClick(object sender, RoutedEventArgs e)
        {
            if(!this.IsMenuExpanded)
            {
                // show the menu column
                this.MenuColumn.Width = new GridLength(320);
            } else
            {
                this.MenuColumn.Width = new GridLength(0);
            }
            this.IsMenuExpanded = !this.IsMenuExpanded;
        }

        private void SettingsButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO navigate to settings screen (this.Frame.Navigate(typeof(screenName)))
        }

        private void RemindersButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO navigate to settings screen (this.Frame.Navigate(typeof(screenName)))
        }

        private void AlarmsButton_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(AlarmsPage));
        }

        private void VoiceNotesButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO navigate to settings screen (this.Frame.Navigate(typeof(screenName)))
        }

        private void DataPrivacyButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO navigate to settings screen (this.Frame.Navigate(typeof(screenName)))
        }

        private void LibrariesButton_Click(object sender, RoutedEventArgs e)
        {
            // TODO navigate to settings screen (this.Frame.Navigate(typeof(screenName)))
        }
    }
}
