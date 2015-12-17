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
// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EVENeT
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class EventDetailPage : Page
    {
        public int CurrentEvent;
        public EventDetailPage()
        {
            this.InitializeComponent();
        }



        public async void getEvent()
        {
            getEventFromIDResult e = await Client.GetEventFromIdAsync(CurrentEvent);
            eventDetail.CardTitle = e.title;
            
        }

        private void Organizer_Hyperlink_Click(Windows.UI.Xaml.Documents.Hyperlink sender, Windows.UI.Xaml.Documents.HyperlinkClickEventArgs args)
        {
            //Navigation.AppShell.RootFrame.Navigate(typeof(ProfilePage));
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
    }
}
