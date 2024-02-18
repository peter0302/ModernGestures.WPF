using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using ICTest;

namespace ModernGestures.WPF
{

    public delegate void TappedEventHandler(object sender, TappedEventArgs e);

    public class TappedEventArgs : GestureEventArgs
    {
        internal TappedEventArgs(FrameworkElement sender, InteractionOutput output) 
            : base(sender, output, Gestures.TappedEvent)
        {
        }
    }
}
