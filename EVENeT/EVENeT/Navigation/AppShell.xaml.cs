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
using Windows.Foundation.Metadata;
using EVENeT.Navigation;
using Windows.UI.Core;
using System.Diagnostics;

// The Blank Page item template is documented at http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409

namespace EVENeT.Navigation
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class AppShell : Page
    {
        public static AppShell Current;
        public static Frame RootFrame = null;

        public static List<NavPaneItem> navList = new List<NavPaneItem>(
            new[]
            {
                new NavPaneItem()
                {
                    Symbol = Symbol.Home,
                    Label = "Home",
                    DestPage = typeof(HomePage)
                },

                new NavPaneItem()
                {
                    Symbol = Symbol.Add,
                    Label = "New event",
                    DestPage = typeof(CreateEventPage)
                },

                new NavPaneItem()
                {
                    Symbol = Symbol.Contact,
                    Label = "Your Profile",
                    DestPage = typeof(ProfilePage),
                    Arguments = DatabaseHelper.CurrentUser
                },

                new NavPaneItem()
                {
                    Symbol = Symbol.Bookmarks,
                    Label = "Events",
                    DestPage = typeof(EventDetailPage)
                }
            });

        public static SplitView NavPane
        {
            get { return Current.navPane; }
        }

        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);
        }

        public AppShell()
        {
            this.InitializeComponent();

            Current = this;
            RootFrame = frame;

            ((AppBarButton)Header.AppCommandBar.SecondaryCommands.Last()).Click += SignOut_Click;
            //(from i in Header.AppCommandBar.SecondaryCommands where ((AppBarButton)i).Label == "Sign Out" select i).SingleOrDefault();

            SystemNavigationManager.GetForCurrentView().BackRequested += SystemNavigationManager_BackRequested;
            PageHeader.selectedItemChangeEvent += new EventHandler(selectedItemHandler);
            // Use the hardware back button instead of the back button in the header of the page.
            // The back button in the header of the page is hidden in this case, of course.
            if (ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                Windows.Phone.UI.Input.HardwareButtons.BackPressed += (s, e) =>
                {
                    if (frame.CanGoBack)
                    {
                        frame.GoBack();
                        e.Handled = true;
                    }
                };
            }

            NavPaneList.ItemsSource = navList;
        }

        private void SignOut_Click(object sender, RoutedEventArgs e)
        {
            // Perform any last change to resources.
            //

            Frame frame = Window.Current.Content as Frame;

            // Pop all pages excetp the one at the bottom of the stack
            while (frame != null && frame.BackStackDepth > 1)
                frame.BackStack.RemoveAt(frame.BackStackDepth - 1);

            // Set autologin to false
            var localSettings = Windows.Storage.ApplicationData.Current.LocalSettings;
            localSettings.Values["Autologin"] = "false";

            // Go back to the login page
            if (frame != null && frame.CanGoBack)
                frame.GoBack();
        }

        private void selectedItemHandler(object sender, EventArgs e)
        {
            PageHeader.MyEventArgs parameter = e as PageHeader.MyEventArgs;
            int index = parameter.MyEventInt;
            NavPaneList.IsEnabled = true;
            Debug.WriteLine("Check selectedindex: " + NavPaneList.SelectedIndex);
            NavPaneList.SelectedIndex = index;
        }


        private void SystemNavigationManager_BackRequested(object sender, BackRequestedEventArgs e)
        {
            bool handled = e.Handled;
            Header.BackRequested(ref handled);
            e.Handled = handled;
        }

        /// <summary>
        /// Ensures the nav menu reflects reality when navigation is triggered outside of
        /// the nav menu buttons.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnNavigatingToPage(object sender, NavigatingCancelEventArgs e)
        {
            //if (e.NavigationMode == NavigationMode.Back)
            //{
            //    NavPaneItem item = (from p in navList where p.DestPage == e.SourcePageType select p).SingleOrDefault();
            //    if (item != null && RootFrame.BackStackDepth > 0)
            //    {
            //        // In cases where a page drills into sub-pages then we'll highlight the most recent
            //        // navigation menu item that appears in the BackStack
            //        foreach (var entry in RootFrame.BackStack.Reverse())
            //        {
            //            item = (from p in navList where p.DestPage == entry.SourcePageType select p).SingleOrDefault();
            //            if (item != null)
            //                break;
            //        }

            //        ListViewItem container = (ListViewItem)NavPaneList.ContainerFromItem(item);

            //        // While updating the selection state of the item prevent it from taking keyboard focus.  If a
            //        // user is invoking the back button via the keyboard causing the selected nav menu item to change
            //        // then focus will remain on the back button.
            //        if (container != null) container.IsTabStop = false;
            //        NavPaneList.SetSelectedItem(container);
            //        Header.TitleControl.Content = item.Label;
            //        if (container != null) container.IsTabStop = true;
            //    }
            //}
        }

        private void OnNavigatedToPage(object sender, NavigationEventArgs e)
        {

        }

        private void NavPaneItemInvoked(object sender, ListViewItem e)
        {
            NavPaneItem item = (NavPaneItem)((NavPaneListView)sender).ItemFromContainer(e);

            if (item != null)
            {
                //if (item.Label == "New event")
                //{
                //    ContentDialog dialog = new AddEventDialog();
                //    dialog.MinWidth = ActualWidth * 4 / 5;
                //    await dialog.ShowAsync();
                //}
                //else 
                if (item.DestPage != null)// &&
                    //item.DestPage != this.frame.CurrentSourcePageType)
                {
                    Header.TitleControl.Content = item.Label;
                    this.frame.Navigate(item.DestPage, item.Arguments);
                }
            }
        }

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {
            this.frame.Navigate(typeof(HomePage));
            Header.TitleControl.Content = "Home";
            NavPaneList.SelectedIndex = 0;
        }
    }
}
