using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Text;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
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

        public CreateEventPage()
        {
            this.InitializeComponent();
            m_highlightedWords = new List<ITextRange>();
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

        private void CreateButton_Click(object sender, RoutedEventArgs e)
        {
            //string value;
            //EventDescription.Document.GetText(TextGetOptions.FormatRtf, out value);
            //tmp.Document.SetText(TextSetOptions.FormatRtf, value);
        }
    }
}
