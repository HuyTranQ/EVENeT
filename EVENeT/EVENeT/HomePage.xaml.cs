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
using System.Diagnostics;
using Windows.UI.Text;
using Windows.Storage;
using Windows.UI.Xaml.Media.Imaging;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EVENeT
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class HomePage : Page
    {

        public HomePage()
        {
            this.InitializeComponent();
            setEvent();
        }

        public async void setEvent()
        {
            IEnumerable<getAllEventResult> events = await DatabaseHelper.Client.getAllEventAsync();
            foreach (getAllEventResult e in events)
            {
                Debug.WriteLine(e.beginTime + "  " + e.description);
                EventCard eventCard = new EventCard();
                eventCard.EventTitle = e.title;
                eventCard.EventTime = e.beginTime.ToString("hh:mm tt");
                eventCard.EventDate = e.beginTime.ToString("MMM. dd, yyyy");
                eventCard.EventDescription.Document.SetText(TextSetOptions.FormatRtf, e.description);
                eventCard.Tapped += EventCard_Tapped;
                eventCard.IsTapEnabled = true;
                eventCard.EventId = e.id;
                var request = new GetNameAndAvatarRequest(e.username);
                var response = await DatabaseHelper.Client.GetNameAndAvatarAsync(request);
                StorageFile file = await StorageFile.GetFileFromPathAsync(response.Avatar);
                BitmapImage bmp = new BitmapImage();
                await bmp.SetSourceAsync(await file.OpenAsync(FileAccessMode.Read));
                eventCard.AvatarImage.ImageSource = bmp;
                eventCard.UserName.Text = response.Name;
                eventCard.UserName.Tapped += (sender, args) => { Frame.Navigate(typeof(ProfilePage), e.username); };

                if (e.thumbnail != "")
                {
                    file = await StorageFile.GetFileFromPathAsync(e.thumbnail);
                    bmp = new BitmapImage();
                    await bmp.SetSourceAsync(await file.OpenAsync(FileAccessMode.Read));
                    eventCard.EventImage.Source = bmp;
                }
                eventPanel.Children.Add(eventCard);
            }
        }

        private void EventCard_Tapped(object sender, TappedRoutedEventArgs e)
        {
            Frame.Navigate(typeof(EventDetailPage), (sender as EventCard).EventId);
        }
    }
}
