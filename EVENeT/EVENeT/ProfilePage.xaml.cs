using EVENeT.DataModel;
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
        public UserList followingListViewModel = new UserList();
        public UserList followerListViewModel = new UserList();

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

            //Get User following and follwers
            await followingListViewModel.getFollowingList(userName);
            await followerListViewModel.getFollowerList(userName);

            if (userType == 1)
            {
                GetIndividualRequest a = new GetIndividualRequest(userName);
                GetIndividualResponse r = await DatabaseHelper.Client.GetIndividualAsync(a);

                Firstname.Text = r.FirstName;
                AdditionalInfo.Text = userName;
                AddBasicInfoCard(userName, r);

                // Set profile picture
                StorageFile file;
                BitmapImage bmp;
                if (r.ProfilePic != null)
                {
                    file = await StorageFile.GetFileFromPathAsync(r.ProfilePic);
                    bmp = new BitmapImage();
                    await bmp.SetSourceAsync(await file.OpenAsync(FileAccessMode.Read));
                    AvatarBrush.ImageSource = bmp;
                }

                // Set cover picture
                if (r.CoverPic != null)
                {
                    file = await StorageFile.GetFileFromPathAsync(r.CoverPic);
                    bmp = new BitmapImage();
                    await bmp.SetSourceAsync(await file.OpenAsync(FileAccessMode.Read));
                    CoverImage.Source = bmp;
                }
            }
            else if (userType == 2)
            {
                GetOrganizationRequest a = new GetOrganizationRequest(userName);
                GetOrganizationResponse r = await DatabaseHelper.Client.GetOrganizationAsync(a);

                Firstname.Text = r.Name;
                AdditionalInfo.Text = r.Type;
                AddBasicInfoCard(userName, r);

                // Set profile picture
                StorageFile file;
                BitmapImage bmp;
                if (r.ProfilePic != null)
                {
                    file = await StorageFile.GetFileFromPathAsync(r.ProfilePic);
                    bmp = new BitmapImage();
                    await bmp.SetSourceAsync(await file.OpenAsync(FileAccessMode.Read));
                    AvatarBrush.ImageSource = bmp;
                }

                // Set cover picture
                if (r.CoverPic != null)
                {
                    file = await StorageFile.GetFileFromPathAsync(r.CoverPic);
                    bmp = new BitmapImage();
                    await bmp.SetSourceAsync(await file.OpenAsync(FileAccessMode.Read));
                    CoverImage.Source = bmp;
                }
            }
        }

        private void AddBasicInfoCard(string username, GetIndividualResponse response)
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

        private void AddBasicInfoCard(string username, GetOrganizationResponse response)
        {
            DisplayCard card = new DisplayCard();
            StackPanel content = new StackPanel();

            card.CardTitle = "About";
            content.Margin = new Thickness(16, 0, 16, 16);

            TextBlock info = new TextBlock();
            info.Text = response.Description;
            info.TextWrapping = TextWrapping.WrapWholeWords;

            content.Children.Add(info);
            card.PlaceHolder = content;
            AboutPanel.Children.Add(card);

            card = new DisplayCard();
            content = new StackPanel();
            card.CardTitle = "Contact";
            content.Margin = new Thickness(16, 0, 16, 16);

            TextBlock contactInfo = new TextBlock();
            contactInfo.Text = "Phone: " + response.Phone + "\n";
            contactInfo.Text += "Email: " + userName + "\n";
            contactInfo.Text += "Website: " + response.Website;

            content.Children.Add(contactInfo);
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


        private void PivotFollowing_Tapped(object sender, TappedRoutedEventArgs e)
        {
            
        }

        private void StackPanel_RightTapped(object sender, RightTappedRoutedEventArgs e)
        {
            FrameworkElement senderElement = sender as FrameworkElement;
            FlyoutBase flyoutBase = FlyoutBase.GetAttachedFlyout(senderElement);
            flyoutBase.ShowAt(senderElement);
        }

        private void followingList_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int listview = (sender as GridView).SelectedIndex;
            if (listview >= 0)
                Navigation.AppShell.RootFrame.Navigate(typeof(ProfilePage), followingListViewModel.users.ElementAt(listview).Username);
        }

        private void followerList_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int listview = (sender as GridView).SelectedIndex;
            if (listview >= 0)
                Navigation.AppShell.RootFrame.Navigate(typeof(ProfilePage), followerListViewModel.users.ElementAt(listview).Username);
        }

        private void SettingBtn_Click(object sender, RoutedEventArgs e)
        {
            Frame.Navigate(typeof(SettingPage));
        }
    }
}
