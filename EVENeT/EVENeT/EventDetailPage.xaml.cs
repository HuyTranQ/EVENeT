using System;
using System.Collections.Generic;
using System.Diagnostics;
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
using static EVENeT.DatabaseHelper;
using EVENeT.EVENeTServiceReference;
using Windows.UI.Xaml.Documents;
using Windows.UI.Text;
using Windows.Devices.Geolocation;
using Windows.UI.Xaml.Controls.Maps;
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EVENeT
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EventDetailPage : Page
    {
        public int CurrentEvent;
        private string organizer;
        private getEventFromIDResult _Event;

        public EventDetailPage()
        {
            this.InitializeComponent();
        }


        public async void getEvent()
        {
            _Event = await Client.GetEventFromIdAsync(CurrentEvent);
            organizer = _Event.username;

            EventDetail.CardTitle = _Event.title;
            BeginTime.CardTitle = "Begin: " + _Event.beginTime.ToString(@"MMM. dd, yyyy a\t hh:mm tt");
            EndTime.CardTitle = "End: " + _Event.endTime.ToString(@"MMM. dd, yyyy a\t hh:mm tt");

            // Add organizer's name
            Run r = new Run();
            r.Text = _Event.username; // get the name instead of the username
            OrganizerName.Inlines.Add(r);

            // Set descripiton
            EventDescription.Document.SetText(TextSetOptions.FormatRtf, _Event.description);

            // Set location
            var location = await Client.GetLocationFromIdAsync(_Event.location);
            EventLocation.CardTitle = location.address;
            EventMap.Center = new Geopoint(new BasicGeoposition()
            {
                Latitude = location.latitude,
                Longitude = location.longitude
            });
            EventMap.ZoomLevel = 15;

            MapIcon icon = new MapIcon();
            icon.Location = EventMap.Center;
            icon.NormalizedAnchorPoint = new Point(0.5, 1.0);
            EventMap.MapElements.Add(icon);
        }

        private void Organizer_Hyperlink_Click(Hyperlink sender, HyperlinkClickEventArgs args)
        {
            Navigation.AppShell.RootFrame.Navigate(typeof(ProfilePage), organizer);
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
          
        }


        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            int eventId = int.Parse(e.Parameter.ToString());
            CurrentEvent = eventId;
            getEvent();
            //   Debug.WriteLine(eventId.ToString());
        }

        //private void PivotItem_PointerWheelChanged(object sender, PointerRoutedEventArgs e)
        //{
        //  //  PageScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
        //}

        //private void PagePivot_PointerEntered(object sender, PointerRoutedEventArgs e)
        //{
        //    PageScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Disabled;
        //}

        //private void PagePivot_PointerExited(object sender, PointerRoutedEventArgs e)
        //{
        //    PageScrollViewer.VerticalScrollBarVisibility = ScrollBarVisibility.Hidden;
        //}
    }
}
