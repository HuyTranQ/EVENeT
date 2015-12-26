using EVENeT.DataModel;
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

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=234238

namespace EVENeT
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class SearchResultPage : Page
    {
        public UserSearchResults SearchResult_Users = new UserSearchResults();
        public EventSearchResults SearchResult_Events = new EventSearchResults();
        private string query;

        public SearchResultPage()
        {
            this.InitializeComponent();
        }

        protected async override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
            query = e.Parameter.ToString();

            await SearchResult_Users.getUsersFromName(query);
            await SearchResult_Events.getEventsFromName(query);
        }

        private void searchResultUsers_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int listview = (sender as GridView).SelectedIndex;
            if (listview >= 0)
                Navigation.AppShell.RootFrame.Navigate(typeof(ProfilePage), SearchResult_Users.users.ElementAt(listview).Username);
        }

        private void searchResultEvents_Tapped(object sender, TappedRoutedEventArgs e)
        {
            int listview = (sender as GridView).SelectedIndex;
            if (listview >= 0)
                Navigation.AppShell.RootFrame.Navigate(typeof(ProfilePage), SearchResult_Events.events.ElementAt(listview).EventId);
        }
    }
}
