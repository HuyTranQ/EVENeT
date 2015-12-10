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
    /// <summary>
    /// General-purpose display card. Contains a card header for the title and an optional icon to the left.
    /// The size of the title and the icon can be changed via TitleFontSize
    /// The card's content is placed inside PlaceHolder.
    /// </summary>
    /// <example>
    /// An example of adding a DisplayCard dynamically.
    /// <code>
    /// </code>
    /// </example>
    public sealed partial class ItemCard : UserControl
    {
        public static readonly DependencyProperty PictorialContentProperty = DependencyProperty.Register("picture", typeof(object), typeof(ItemCard), new PropertyMetadata(null));
        public object PictorialContent
        {
            get { return GetValue(PictorialContentProperty); }
            set { SetValue(PictorialContentProperty, value); }
        }

        public static readonly DependencyProperty DescriptiveContentProperty = DependencyProperty.Register("description", typeof(object), typeof(ItemCard), new PropertyMetadata(null));
        public object DescriptiveContent
        {
            get { return GetValue(DescriptiveContentProperty); }
            set { SetValue(DescriptiveContentProperty, value); }
        }
        public ItemCard()
        {
            this.InitializeComponent();
        }
    }
}
