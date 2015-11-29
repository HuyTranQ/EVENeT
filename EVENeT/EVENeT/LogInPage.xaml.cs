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

namespace EVENeT
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LogInPage : Page
    {
        public LogInPage()
        {
            this.InitializeComponent();
        }
        
        private void signInButton_Click(object sender, RoutedEventArgs e)
        {
            if (UsernameExists(userName.Text) && CorrectUsernameAndPassword(userName.Text, password.Password))
            {
                Frame frame = Window.Current.Content as Frame;
                frame.Navigate(typeof(Navigation.AppShell));
                Window.Current.Activate();
            }
        }

        private bool CorrectUsernameAndPassword(string username, string password)
        {
            return true;
        }

        private bool UsernameExists(string username)
        {
            return true;
        }

        private void userName_LostFocus(object sender, RoutedEventArgs e)
        {
        }
    }
}
