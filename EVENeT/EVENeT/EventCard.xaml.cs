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

// The User Control item template is documented at http://go.microsoft.com/fwlink/?LinkId=234236

namespace EVENeT
{
    public sealed partial class EventCard : UserControl
    {
        public static readonly DependencyProperty EventDateProperty = DependencyProperty.Register("EventDate", typeof(object), typeof(EventCard), new PropertyMetadata(null));
        public object EventDate
        {
            get { return GetValue(EventDateProperty); }
            set { SetValue(EventDateProperty, value); }
        }

        public static readonly DependencyProperty EventTimeProperty = DependencyProperty.Register("EventTime", typeof(object), typeof(EventCard), new PropertyMetadata(null));
        public object EventTime
        {
            get { return GetValue(EventTimeProperty); }
            set { SetValue(EventTimeProperty, value); }
        }

        public static readonly DependencyProperty EventTitleProperty = DependencyProperty.Register("EventTitle", typeof(object), typeof(EventCard), new PropertyMetadata(null));
        public object EventTitle
        {
            get { return GetValue(EventTitleProperty); }
            set { SetValue(EventTitleProperty, value); }
        }

        public ImageBrush AvatarImage
        {
            get { return avatarImage; }
        }

        public Image EventImage
        {
            get { return eventImages; }
        }

        public TextBlock UserName
        {
            get { return userName; }
        }

        public RichEditBox EventDescription
        {
            get { return eventDescription; }
        }

        public int EventId { get; set; }

        public EventCard()
        {
            this.InitializeComponent();
        }
    }
}
