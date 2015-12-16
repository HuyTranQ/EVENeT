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
                // Remove follow button, blah blah
            }
            else
            {
                
            }

            userType = await DatabaseHelper.Client.UserTypeAsync(userName);
            
            EVENeTServiceReference.GetIndividualRequest a = new EVENeTServiceReference.GetIndividualRequest(userName);
            EVENeTServiceReference.GetIndividualResponse r = await DatabaseHelper.Client.GetIndividualAsync(a);

            Firstname.Text = r.FirstName;
            AdditionalInfo.Text = r.MiddleName + " " + r.LastName;

            StorageFile file = await StorageFile.GetFileFromPathAsync(r.ProfilePic);
            BitmapImage bmp = new BitmapImage();
            await bmp.SetSourceAsync(await file.OpenAsync(FileAccessMode.Read));
            AvatarBrush.ImageSource = bmp;

            file = await StorageFile.GetFileFromPathAsync(r.CoverPic);
            await bmp.SetSourceAsync(await file.OpenAsync(FileAccessMode.Read));
            CoverImage.Source = bmp;
        }
    }
}
