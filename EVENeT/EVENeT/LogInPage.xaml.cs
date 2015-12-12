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
        
        private async void signInButton_Click(object sender, RoutedEventArgs e)
        {
            bool correct = await client.CorrectUserNameAndPasswordAsync(userName.Text, password.Password);
            if (correct)
            {
                Frame frame = Window.Current.Content as Frame;

                if (await client.IndividualFullySetUpAsync(userName.Text))
                    frame.Navigate(typeof(AccountSetUpPage), userName.Text);
                else
                {
                    frame.Navigate(typeof(Navigation.AppShell), userName.Text);
                }
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
                ServiceClient client = new ServiceClient();

                TextBox dialogUsername = ((TextBox)dialog.FindName("userName"));
                PasswordBox dialogPassword = ((PasswordBox)dialog.FindName("password"));
                ComboBox dialogUserType = ((ComboBox)dialog.FindName("userType"));

                if (dialogUserType.SelectedIndex == 0)
                    await client.CreateIndividualAsync(dialogUsername.Text, dialogPassword.Password, "", "", "", "", "", new DateTime(1900, 1, 1), false);
                else
                    await client.CreateOrganizationAsync(dialogUsername.Text, dialogPassword.Password, "", "", "", "", "", "");

                // Navigate to set up page
                Frame frame = Window.Current.Content as Frame;
                frame.Navigate(typeof(AccountSetUpPage), dialogUsername.Text);
            }
        }
    }
}
