using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ModernGestures.WPF
{
    public delegate void DoubleTappedEventHandler(object sender, DoubleTappedEventArgs e);

    public class DoubleTappedEventArgs : GestureEventArgs
    {
        internal DoubleTappedEventArgs(FrameworkElement sender, Point screenCoords)
            : base(sender, screenCoords, Gestures.DoubleTappedEvent)
        {
        }
    }
}
