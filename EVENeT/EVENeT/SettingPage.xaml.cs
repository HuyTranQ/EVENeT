using EVENeT.EVENeTServiceReference;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Threading.Tasks;
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
        string errorName = "Error!";

        string cover, profile;
        int userTypeValue;
        bool informationFilled = false;


        private void password_PasswordChanged(object sender, RoutedEventArgs e)
        {

        }

        private void userName_TextChanged(object sender, TextChangedEventArgs e)
        {

        }

        private void userName_LostFocus(object sender, RoutedEventArgs e)
        {

        }

        private async void ChooseAvatarBtn_Click(object sender, RoutedEventArgs e)
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
                indAvatar.Source = image;
            }
        }


        private async void ChooseCoverBtn_Click(object sender, RoutedEventArgs e)
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
                indCover.Source = image;
            }
        }

        private async void Save_Click(object sender, RoutedEventArgs e)
        {
            await saveValue();
        }

        private async void Reset_Click(object sender, RoutedEventArgs e)
        {
            error.Visibility = Visibility.Collapsed;
            await resetValue();
        }


        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
             userTypeValue = await DatabaseHelper.Client.UserTypeAsync(DatabaseHelper.CurrentUser);
            await resetValue();
        }


        private async Task saveValue()
        {
           
            CheckForError();
            if (informationFilled)
            {
                error.Visibility = Visibility.Collapsed; 
                if (userTypeValue == 1)
                    await DatabaseHelper.Client.SetIndividualInfoAsync(DatabaseHelper.CurrentUser, FirstNameTbx.Text, MidNameTbx.Text, LastnameTbx.Text, BirthdayPicker.Date.Date, GenderCbx.SelectedIndex == 0);
                else if (userTypeValue == 2)
                    await DatabaseHelper.Client.SetOrganizationInfoAsync(DatabaseHelper.CurrentUser, CompanyName.Text, CompanyDescription.Text, CompanyType.Text, CompanyPhone.Text, CompanySite.Text);

                await DatabaseHelper.Client.SetProfilePictureAsync(DatabaseHelper.CurrentUser, profile);
                await DatabaseHelper.Client.SetCoverPictureAsync(DatabaseHelper.CurrentUser, cover);
            }
            else
            {
                error.Text = errorName;
                error.Visibility = Visibility.Visible;
            }
              
        }



        private void CheckForError()
        {
            if ( userTypeValue == 1)
            {
                if (string.IsNullOrEmpty(FirstNameTbx.Text) ||
                    string.IsNullOrEmpty(LastnameTbx.Text) ||
                    BirthdayPicker.Date.CompareTo(DateTime.Now) >= 0 ||
                    GenderCbx.SelectedIndex == -1)
                {
                    informationFilled = false;
                }
                else
                    informationFilled = true;
            }
            else if ( userTypeValue == 2)
            {
                informationFilled = false;
            }
        }


        private async Task resetValue()
        {
            if ( userTypeValue == 2)
            {
                IndividualPanel.Visibility = Visibility.Collapsed;

            }
            else if ( userTypeValue == 1)
            {
                OrganizationPanel.Visibility = Visibility.Collapsed;
                GetIndividualRequest a = new GetIndividualRequest(DatabaseHelper.CurrentUser);
                GetIndividualResponse r = await DatabaseHelper.Client.GetIndividualAsync(a);
                getUserResult user = (await DatabaseHelper.Client.GetUserAsync(DatabaseHelper.CurrentUser)).First();
                await setIndividualValue(r, user);
            }
        }

        private void FirstNameTbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (FirstNameTbx.Text == "")
                errorName = "First name cannot be empty!";
        }

        private void LastnameTbx_TextChanged(object sender, TextChangedEventArgs e)
        {
            if (LastnameTbx.Text == "")
                errorName = "Last name cannot be empty!";
        }

        private async void ChooseAvatarBtn_Click(object sender, RoutedEventArgs e)
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
                indAvatar.Source = image;
            }
        }

        private async void ChooseCoverBtn_Click(object sender, RoutedEventArgs e)
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
                indCover.Source = image;
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

        private async Task setIndividualValue(GetIndividualResponse r, getUserResult user)
        {
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
            cover = user.coverPicture;
            profile = user.profilePicture;
            file = await StorageFile.GetFileFromPathAsync(user.coverPicture);
            bmp = new BitmapImage();
            await bmp.SetSourceAsync(await file.OpenAsync(FileAccessMode.Read));
            indCover.Source = bmp;
        }
    }
}
