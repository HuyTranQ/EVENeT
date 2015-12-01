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

        private List<NavPaneItem> navList = new List<NavPaneItem>(
            new[]
            {
                new NavPaneItem()
                {
                    Symbol = Symbol.Home,
                    Label = "Home",
                    DestPage = typeof(HomePage)
                }, 

                //new NavPaneItem()
                //{
                //    Symbol = Symbol.Add,
                //    Label = "Create event",
                //    DestPage = typeof(HomePage)
                //}, 

                new NavPaneItem()
                {
                    Symbol = Symbol.Contact,
                    Label = "Your Profile",
                    DestPage = typeof(ProfilePage)
                }
            });

        public static SplitView NavPane
        {
            get { return Current.navPane; }
        }

        public AppShell()
        {
            this.InitializeComponent();

            Current = this;
            RootFrame = frame;

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

        private void OnNavigatingToPage(object sender, NavigatingCancelEventArgs e)
        {

        }

        private void OnNavigatedToPage(object sender, NavigationEventArgs e)
        {

        }

        private void NavPaneItemInvoked(object sender, ListViewItem e)
        {
            NavPaneItem item = (NavPaneItem)((NavPaneListView)sender).ItemFromContainer(e);

            if (item != null)
            {
                if (item.DestPage != null &&
                    item.DestPage != this.frame.CurrentSourcePageType)
                {
                    this.frame.Navigate(item.DestPage, item.Arguments);
                }
            }
        }
    }
}
