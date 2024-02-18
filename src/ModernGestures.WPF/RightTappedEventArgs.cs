using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using ICTest;

namespace ModernGestures.WPF
{

    public delegate void RightTappedEventHandler(object sender, RightTappedEventArgs e);

    public class RightTappedEventArgs : GestureEventArgs
    {
        internal RightTappedEventArgs(FrameworkElement sender, InteractionOutput output)
            : base(sender, output, Gestures.RightTappedEvent)
        {
        }
    }
}
