using EVENeT.EVENeTServiceReference;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
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
    public sealed partial class ProfilePage : Page
    {
        string userName;
        int userType;

        public ProfilePage()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            userName = e.Parameter.ToString();
            if (userName == DatabaseHelper.CurrentUser)
            {
                // Remove follow button
                FollowBtn.Visibility = Visibility.Collapsed;
                UnfollowBtn.Visibility = Visibility.Collapsed;
            }
            else
            {
                // Remove setting button
                SettingBtn.Visibility = Visibility.Collapsed;
                if (await DatabaseHelper.Client.IsFollowingAsync(DatabaseHelper.CurrentUser, userName))
                    FollowBtn.Visibility = Visibility.Collapsed;
                else
                    UnfollowBtn.Visibility = Visibility.Collapsed;
            }

            userType = await DatabaseHelper.Client.UserTypeAsync(userName);
            
            GetIndividualRequest a = new GetIndividualRequest(userName);
            GetIndividualResponse r = await DatabaseHelper.Client.GetIndividualAsync(a);

            Firstname.Text = r.FirstName;
            AdditionalInfo.Text = userName;
            AddBasicInfoCard(r);


            //StorageFile file = await StorageFile.GetFileFromPathAsync(r.ProfilePic);
            //BitmapImage bmp = new BitmapImage();
            //await bmp.SetSourceAsync(await file.OpenAsync(FileAccessMode.Read));
            //AvatarBrush.ImageSource = bmp;

            //file = await StorageFile.GetFileFromPathAsync(r.CoverPic);
            //await bmp.SetSourceAsync(await file.OpenAsync(FileAccessMode.Read));
            //CoverImage.Source = bmp;
        }

        private void AddBasicInfoCard(GetIndividualResponse response)
        {
            DisplayCard card = new DisplayCard();
            StackPanel content = new StackPanel();

            card.CardTitle = "Basic information";
            content.Margin = new Thickness(16, 0, 16, 16);

            TextBlock info = new TextBlock();
            info.Text = "Firstname:\t" + response.FirstName + "\n" +
                ((response.MiddleName != "") ? "Middlename:\t" + response.MiddleName + "\n" : "") + 
                "Lastname:\t" + response.LastName + "\n\n";
            info.Text += "Birthday:\t\t" + response.DOB.ToString("d") + "\n";
            info.Text += "Gender:\t\t" + (response.Gender ? "Male" : "Female");

            content.Children.Add(info);
            card.PlaceHolder = content;
            AboutPanel.Children.Add(card);
        }

        private async void FollowBtn_Click(object sender, RoutedEventArgs e)
        {
            await DatabaseHelper.Client.FollowAsync(DatabaseHelper.CurrentUser, userName);
            FollowBtn.Visibility = Visibility.Collapsed;
            UnfollowBtn.Visibility = Visibility.Visible;
        }

        private async void UnfollowBtn_Click(object sender, RoutedEventArgs e)
        {
            await DatabaseHelper.Client.UnfollowAsync(DatabaseHelper.CurrentUser, userName);
            FollowBtn.Visibility = Visibility.Visible;
            UnfollowBtn.Visibility = Visibility.Collapsed;
        }
    }
}
