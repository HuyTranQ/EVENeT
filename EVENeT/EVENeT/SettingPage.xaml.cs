using EVENeT.EVENeTServiceReference;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EVENeT
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SettingPage : Page
    {
        public SettingPage()
        {
            this.InitializeComponent();
        }

        private void password_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }

        private void userName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void userName_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private void ChooseAvatarBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void ChooseCoverBtn_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Save_Click(object sender, RoutedEventArgs e)
        {

        }

        private void Cancel_Click(object sender, RoutedEventArgs e)
        {

        }


        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
          
            int userType = await DatabaseHelper.Client.UserTypeAsync(DatabaseHelper.CurrentUser);
            if (userType == 2)
            {
                IndividualPanel.Visibility = Visibility.Collapsed;

              

            }
            else if (userType == 1)
            {
                OrganizationPanel.Visibility = Visibility.Collapsed;
                GetIndividualRequest a = new GetIndividualRequest(DatabaseHelper.CurrentUser);
                GetIndividualResponse r = await DatabaseHelper.Client.GetIndividualAsync(a);
                getUserResult user =  (await DatabaseHelper.Client.GetUserAsync(DatabaseHelper.CurrentUser)).First();

                userName.Text = DatabaseHelper.CurrentUser;
                password.Password = user.password;
                FirstNameTbx.Text = r.FirstName;
                MidNameTbx.Text = r.MiddleName;
                LastnameTbx.Text = r.LastName;
                BirthdayPicker.Date = r.DOB.Date;
                GenderCbx.SelectedIndex = r.Gender ? 0 : 1;

                StorageFile file = await StorageFile.GetFileFromPathAsync(user.profilePicture);
                BitmapImage bmp = new BitmapImage();
                await bmp.SetSourceAsync(await file.OpenAsync(FileAccessMode.Read));
                indAvatar.Source = bmp;

                file = await StorageFile.GetFileFromPathAsync(user.coverPicture);
                bmp = new BitmapImage();
                await bmp.SetSourceAsync(await file.OpenAsync(FileAccessMode.Read));
                indCover.Source = bmp;
            }
        }

        private async void ChooseCompLogoBtn_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".png");
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".bmp");

            StorageFile file = await openPicker.PickSingleFileAsync();
            profile = file.Path;
            // Some magic, because I can only display the files directly on computer
            if (file != null)
            {
                BitmapImage image = new BitmapImage();
                await image.SetSourceAsync(await file.OpenAsync(FileAccessMode.Read));
                CompLogo.Source = image;
            }
        }

        private async void ChooseCompCoverBtn_Click(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.SuggestedStartLocation = PickerLocationId.PicturesLibrary;
            openPicker.FileTypeFilter.Add(".png");
            openPicker.FileTypeFilter.Add(".jpg");
            openPicker.FileTypeFilter.Add(".bmp");

            StorageFile file = await openPicker.PickSingleFileAsync();
            cover = file.Path;
            // Some magic, because I can only display the files directly on computer
            if (file != null)
            {
                BitmapImage image = new BitmapImage();
                await image.SetSourceAsync(await file.OpenAsync(FileAccessMode.Read));
                CompCover.Source = image;
            }
        }
    }
}
