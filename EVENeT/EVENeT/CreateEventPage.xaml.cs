using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Devices.Geolocation;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Services.Maps;
using Windows.UI.Popups;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Maps;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Documents;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EVENeT
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class CreateEventPage : Page
    {
        List<ITextRange> m_highlightedWords = null;
        MapLocation location;
        bool infoFilled = false;

        public CreateEventPage()
        {
            this.InitializeComponent();
            m_highlightedWords = new List<ITextRange>();
            LocationMap.Loaded += LocationMap_Loaded;
            LocationMap.MapTapped += LocationMap_MapTapped;
            TicketNumberTbx.TextChanging += TicketNumberTbx_TextChanging;
        }

        private void TicketNumberTbx_TextChanging(TextBox sender, TextBoxTextChangingEventArgs args)
        {
            for (int i = 0; i < sender.Text.Length; ++i)
            {
                if (!char.IsNumber(sender.Text[i]))
                {
                    sender.Text = sender.Text.Remove(i, 1);
                    --i;
                }
            }
        }

        private  void LocationMap_MapTapped(MapControl sender, MapInputEventArgs args)
        {
            //MapIcon icon = new MapIcon();
            //icon.Location = args.Location;
            //icon.NormalizedAnchorPoint = new Point(0.5, 1.0);

            //MapLocationFinderResult result = await MapLocationFinder.FindLocationsAtAsync(args.Location);
            //if (result.Status == MapLocationFinderStatus.Success)
            //{
            //    icon.Title = result.Locations[0].Address.FormattedAddress;
            //}

            //LocationMap.MapElements.Clear();
            //LocationMap.MapElements.Add(icon);
        }

        private void LocationMap_Loaded(object sender, RoutedEventArgs e)
        {
            LocationMap.Center = new Geopoint(new BasicGeoposition()
            {
                Latitude = 10.762689,
                Longitude = 106.6823399
            });

            LocationMap.ZoomLevel = 15;
            LocationMap.LandmarksVisible = true;

            MapIcon icon = new MapIcon();
            icon.Location = LocationMap.Center;
            icon.NormalizedAnchorPoint = new Point(0.5, 1.0);
            icon.Title = "University of Science, Ho Chi Minh City";
            LocationMap.MapElements.Add(icon);
        }

        private void Bold_Click(object sender, RoutedEventArgs e)
        {
            ITextSelection selectedText = EventDescription.Document.Selection;
            if (selectedText != null)
            {
                selectedText.CharacterFormat.Bold = FormatEffect.Toggle;
            }
            EventDescription.Focus(FocusState.Programmatic);
        }

        private void Italic_Click(object sender, RoutedEventArgs e)
        {
            ITextSelection selectedText = EventDescription.Document.Selection;
            if (selectedText != null)
            {
                selectedText.CharacterFormat.Italic = FormatEffect.Toggle;
            }
            EventDescription.Focus(FocusState.Programmatic);
        }

        private void Underline_Click(object sender, RoutedEventArgs e)
        {
            ITextSelection selectedText = EventDescription.Document.Selection;
            if (selectedText != null)
            {
                ITextCharacterFormat charFormatting = selectedText.CharacterFormat;
                selectedText.CharacterFormat.Underline = (charFormatting.Underline == UnderlineType.None) ? UnderlineType.Single : UnderlineType.None;
            }
            EventDescription.Focus(FocusState.Programmatic);
        }

        private async void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            // Check if all informations are filled
            InformationFilled();
            if (infoFilled)
            {
                DateTime beginDate = EventBeginDate.Date.Date.Add(EventBeginTime.Time);
                DateTime endDate = EventEndDate.Date.Date.Add(EventEndTime.Time);

                string description;
                EventDescription.Document.GetText(TextGetOptions.FormatRtf, out description);
                int locationId = await DatabaseHelper.Client.GetLocationFromAddressAsync(location.Address.FormattedAddress);
                if (locationId == -1)
                {
                    await DatabaseHelper.Client.CreateLocationAsync("", "", location.Address.FormattedAddress, location.Point.Position.Latitude, location.Point.Position.Longitude, "");
                    locationId = await DatabaseHelper.Client.GetLocationFromAddressAsync(location.Address.FormattedAddress);
                }
                if (await DatabaseHelper.Client.CreateEventAsync(beginDate, endDate, description, "", EventTitle.Text, int.Parse(TicketNumberTbx.Text), locationId, DatabaseHelper.CurrentUser))
                {
                    MessageDialog dialog = new MessageDialog("Event created successfully!");
                    await dialog.ShowAsync();
                }
            }
        }

        private void InformationFilled()
        {
            string tmp;
            EventDescription.Document.GetText(TextGetOptions.None, out tmp);
            if (!string.IsNullOrEmpty(EventTitle.Text) &&
                !string.IsNullOrEmpty(tmp) &&
                EventBeginDate.Date <= EventEndDate.Date &&
                DateTime.Now <= EventBeginDate.Date && 
                location != null)
                infoFilled = true;
            else
            {
                infoFilled = false;
                // raise error
            }
        }

        private async void AddressTbx_QuerySubmitted(AutoSuggestBox sender, AutoSuggestBoxQuerySubmittedEventArgs args)
        {
            MapLocationFinderResult result = await MapLocationFinder.FindLocationsAsync(args.QueryText, new Geopoint(new BasicGeoposition() {
                Latitude = 10.762689,
                Longitude = 106.6823399
            }), 1);

            if (result.Status == MapLocationFinderStatus.Success)
            {
                MapIcon icon = new MapIcon();
                icon.Location = result.Locations[0].Point;
                icon.NormalizedAnchorPoint = new Point(0.5, 1.0);
                icon.Title = result.Locations[0].Address.FormattedAddress;
                location = result.Locations[0];

                LocationMap.MapElements.Clear();
                LocationMap.MapElements.Add(icon);
                LocationMap.Center = result.Locations[0].Point;
            }
        }
    }
}
