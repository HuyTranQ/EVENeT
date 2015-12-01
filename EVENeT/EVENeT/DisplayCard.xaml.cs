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
    public sealed partial class DisplayCard : UserControl
    {
        public static readonly DependencyProperty SymbolGlyphProperty = DependencyProperty.Register("SymbolGlyph", typeof(object), typeof(DisplayCard), new PropertyMetadata(null));
        public object SymbolGlyph
        {
            get { return GetValue(SymbolGlyphProperty); }
            set { SetValue(SymbolGlyphProperty, value); }
        }

        public static readonly DependencyProperty CardTitleProperty = DependencyProperty.Register("CardTitle", typeof(object), typeof(DisplayCard), new PropertyMetadata(null));
        public object CardTitle
        {
            get { return GetValue(CardTitleProperty); }
            set { SetValue(CardTitleProperty, value); }
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
        }
    }
}
