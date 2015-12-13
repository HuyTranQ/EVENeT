using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.UI.Xaml.Controls;

namespace EVENeT.Navigation
{
    /// <summary>
    /// Data to represent an item in the nav pane.
    /// </summary>
   public class NavPaneItem
    {
        public string Label { get; set; }
        public Symbol Symbol { get; set; }
        public char SymbolAsChar
        {
            get { return (char)this.Symbol; }
        }

        public Type DestPage { get; set; }
        public object Arguments { get; set; }
    }
}
