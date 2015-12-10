using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text.RegularExpressions;
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

// The Content Dialog item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EVENeT
{
    public sealed partial class SignUpDialog : ContentDialog
    {
        string emailPattern = @"([^@\.]+)@([^@\.]+)\.([^@\.]+)(\.[^@\.]+)*";
        Regex regex = null;
        public SignUpDialog()
        {
            this.InitializeComponent();
            this.IsPrimaryButtonEnabled = false;
            regex = new Regex(emailPattern);
        }

        private void ContentDialog_PrimaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
            ServiceClient client = new ServiceClient();
            client.CreateUserAsync(userName.Text, password.Password, null, userType.SelectedIndex + 1);
        }

        private void ContentDialog_SecondaryButtonClick(ContentDialog sender, ContentDialogButtonClickEventArgs args)
        {
        }

        private void userName_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (ShouldEnablePrimaryButton())
                IsPrimaryButtonEnabled = true;
            else
                IsPrimaryButtonEnabled = false;
        }

        private void password_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (ShouldEnablePrimaryButton())
                IsPrimaryButtonEnabled = true;
            else
                IsPrimaryButtonEnabled = false;
        }

        private void passwordConfirm_PasswordChanged(object sender, RoutedEventArgs e)
        {
            if (ShouldEnablePrimaryButton())
                IsPrimaryButtonEnabled = true;
            else
                IsPrimaryButtonEnabled = false;
        }

        private bool ShouldEnablePrimaryButton()
        {
            return !string.IsNullOrEmpty(userName.Text) && IsEmailValid() && 
                !string.IsNullOrEmpty(password.Password) &&
                !string.IsNullOrEmpty(passwordConfirm.Password) &&
                password.Password == passwordConfirm.Password;
        }

        private bool IsEmailValid()
        {
            return regex.IsMatch(userName.Text);
        }

        private void userName_LostFocus(object sender, RoutedEventArgs e)
        {
            if (!string.IsNullOrEmpty(userName.Text) && !IsEmailValid())
                error.Text = "Please enter a valid email address.";
            else
                error.Text = "";
        }
    }
}
