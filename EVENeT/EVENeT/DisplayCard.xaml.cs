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
    /// DisplayCard displayCard = new DisplayCard();
    /// displayCard.SymbolGlyph = "\uF073";
    /// displayCard.CardTitle = "MyCard";
    /// displayCard.TitleFontSize = 24;
    /// TextBlock tbl = new TextBlock();
    /// tbl.Text = "Hello, World!";
    /// displayCard.PlaceHolder = tbl;
    /// MainContainer.Children.Add(displayCard);
    /// </code>
    /// </example>
    public sealed partial class DisplayCard : UserControl
    {
        public static readonly DependencyProperty SymbolGlyphProperty = DependencyProperty.Register("SymbolGlyph", typeof(object), typeof(DisplayCard), new PropertyMetadata(null));
        public object SymbolGlyph
        {
            get { return GetValue(SymbolGlyphProperty); }
            set { SetValue(SymbolGlyphProperty, value); Glyph.Visibility = Visibility.Visible; }
        }

        public static readonly DependencyProperty CardTitleProperty = DependencyProperty.Register("CardTitle", typeof(object), typeof(DisplayCard), new PropertyMetadata(null));
        public object CardTitle
        {
            get { return GetValue(CardTitleProperty); }
            set { SetValue(CardTitleProperty, value); }
        }

        public static readonly DependencyProperty TitleFontSizeProperty = DependencyProperty.Register("TitleFontSize", typeof(object), typeof(DisplayCard), new PropertyMetadata(null));
        public object TitleFontSize
        {
            get { return GetValue(TitleFontSizeProperty); }
            set { SetValue(TitleFontSizeProperty, value); }
        }

        public static readonly DependencyProperty PlaceHolderProperty = DependencyProperty.Register("PlaceHolder", typeof(object), typeof(DisplayCard), new PropertyMetadata(null));
        public object PlaceHolder
        {
            get { return GetValue(PlaceHolderProperty); }
            set { SetValue(PlaceHolderProperty, value); }
        }

        public DisplayCard()
        {
            this.InitializeComponent();
            Margin = new Thickness(0, 8, 0, 8);
        }
    }
}
