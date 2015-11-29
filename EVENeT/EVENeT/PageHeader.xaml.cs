using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Windows.Input;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Foundation.Metadata;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace EVENeT
{
    public sealed partial class PageHeader : UserControl
    {
        public static readonly DependencyProperty TitleProperty = DependencyProperty.Register("Title", typeof(object), typeof(PageHeader), new PropertyMetadata(null));
        public object Title
        {
            get { return GetValue(TitleProperty); }
            set { SetValue(TitleProperty, value); }
        }

        public static readonly DependencyProperty WideLayoutThresholdProperty = DependencyProperty.Register("WideLayoutThreshold", typeof(double), typeof(PageHeader), new PropertyMetadata(600));
        public double WideLayoutThreshold
        {
            get { return (double)GetValue(WideLayoutThresholdProperty); }
            set
            {
                SetValue(WideLayoutThresholdProperty, value);
                WideLayoutTrigger.MinWindowWidth = value;
            }
        }

        private ICommand _goBackCommand;

        public ICommand GoBackCommand
        {
            get
            {
                if (_goBackCommand == null)
                {
                    // TODO: handle relay command
                }
                return _goBackCommand;
            }
        }

        public PageHeader()
        {
            this.InitializeComponent();

            if (ApiInformation.IsTypePresent("Windows.Phone.UI.Input.HardwareButtons"))
            {
                //Remove the backbutton because physical buttons are present
                backButton.Visibility = Visibility.Collapsed;
            }
        }

        private void navPaneToggle_Click(object sender, RoutedEventArgs e)
        {
            Navigation.AppShell.NavPane.IsPaneOpen = !Navigation.AppShell.NavPane.IsPaneOpen;
        }
    }
}
