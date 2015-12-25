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
        public LogInPage()
        {
            this.InitializeComponent();
            DatabaseHelper.Initialize();
        }
        
        private async void signInButton_Click(object sender, RoutedEventArgs e)
        {
            bool correct = await DatabaseHelper.Client.CorrectUserNameAndPasswordAsync(userName.Text, password.Password);
            if (correct)
            {
                Frame frame = Window.Current.Content as Frame;
                DatabaseHelper.CurrentUser = userName.Text;
                DatabaseHelper.CurrentUserType = await DatabaseHelper.Client.UserTypeAsync(userName.Text);

                if (remember.IsChecked.Value)
                {
                    var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                    localSettings.Values["Autologin"] = "true";
                    localSettings.Values["Username"] = userName.Text;
                    localSettings.Values["Password"] = password.Password;
                    localSettings.Values["Remember"] = "Checked";
                }

                if ((await DatabaseHelper.Client.IndividualFullySetUpAsync(userName.Text) && DatabaseHelper.CurrentUserType == 1) ||
                    (await DatabaseHelper.Client.OrganizationFullySetUpAsync(userName.Text) && DatabaseHelper.CurrentUserType == 2))
                    frame.Navigate(typeof(Navigation.AppShell));
                else
                {
                    frame.Navigate(typeof(AccountSetUpPage), userName.Text);
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
                TextBox dialogUsername = ((TextBox)dialog.FindName("userName"));
                PasswordBox dialogPassword = ((PasswordBox)dialog.FindName("password"));
                ComboBox dialogUserType = ((ComboBox)dialog.FindName("userType"));

                if (dialogUserType.SelectedIndex == 0)
                    await DatabaseHelper.Client.CreateUserAsync(dialogUsername.Text, dialogPassword.Password, "", "", 1);
                else
                    await DatabaseHelper.Client.CreateUserAsync(dialogUsername.Text, dialogPassword.Password, "", "", 2);

                var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
                var check = localSettings.Values["Autologin"] = "false";
                // Navigate to set up page
                Frame frame = Window.Current.Content as Frame;
                frame.Navigate(typeof(AccountSetUpPage), dialogUsername.Text);
            }
        }

        protected override async void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            var check = localSettings.Values["Autologin"];
            var rememberChecked = localSettings.Values["Remember"];

            if (check == null)
                return;

            if (rememberChecked != null && rememberChecked.ToString() == "Checked")
                remember.IsChecked = true;
          
            if (check != null && check.ToString() == "true")
            {
                LogInPane.Visibility = Visibility.Collapsed;
                loading.Visibility = Visibility.Visible;
                //await Task.Delay(2000);


                string sUsername = localSettings.Values["Username"].ToString();
                string sPassword = localSettings.Values["Password"].ToString();
                DatabaseHelper.CurrentUserType = await DatabaseHelper.Client.UserTypeAsync(sUsername);

                DatabaseHelper.CurrentUser = sUsername;
                if (await DatabaseHelper.Client.CorrectUserNameAndPasswordAsync(sUsername, sPassword))
                {
                    Frame frame = Window.Current.Content as Frame;
                    if ((await DatabaseHelper.Client.IndividualFullySetUpAsync(sUsername) && DatabaseHelper.CurrentUserType == 1) ||
                        (await DatabaseHelper.Client.OrganizationFullySetUpAsync(sUsername) && DatabaseHelper.CurrentUserType == 2))
                        frame.Navigate(typeof(Navigation.AppShell));
                    else
                        frame.Navigate(typeof(AccountSetUpPage), sUsername);
                    Window.Current.Activate();
                }
                else
                {
                    LogInPane.Visibility = Visibility.Visible;
                    errorMessage.Text = "Error";
                    errorMessage.Visibility = Visibility.Visible;
                }
                loading.Visibility = Visibility.Collapsed;
            }
        }

        private void remember_Checked(object sender, RoutedEventArgs e)
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["Remember"] = "Checked";
        }

        private void remember_Unchecked(object sender, RoutedEventArgs e)
        {
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["Remember"] = "UnChecked";
        }
    }
}
