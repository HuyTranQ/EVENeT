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
using EVENeT.EVENeTServiceReference;
using System.Threading.Tasks;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EVENeT
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class LogInPage : Page
    {
        ServiceClient client;
        public LogInPage()
        {
            this.InitializeComponent();
            client = new ServiceClient();
        }
        
        private void signInButton_Click(object sender, RoutedEventArgs e)
        {
            bool correct = client.CorrectUserNameAndPasswordAsync(userName.Text, password.Password).Result;
            if (correct)
            {
                Frame frame = Window.Current.Content as Frame;
                // TODO: Check if account is fully set up, if not, go to the set up page.
                frame.Navigate(typeof(Navigation.AppShell), userName.Text);
                Window.Current.Activate();
            }
            else
            {
                errorMessage.Text = "Error";
                errorMessage.Visibility = Visibility.Visible;
            }
        }

        private async void signUpButton_Click(object sender, RoutedEventArgs e)
        {
            ContentDialog dialog = new SignUpDialog();
            ContentDialogResult result = await dialog.ShowAsync();
            if (result == ContentDialogResult.Primary)
            {
                // Navigate to set up page
                Frame frame = Window.Current.Content as Frame;
                frame.Navigate(typeof(AccountSetUpPage), userName.Text);
            }
        }
    }
}
