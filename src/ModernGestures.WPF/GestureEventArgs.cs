using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace ModernGestures.WPF
{
    public class GestureEventArgs : RoutedEventArgs
    {
        private Point _screenCoords;
        private Point _feCoords;
        private FrameworkElement _sender;

        internal GestureEventArgs(
            FrameworkElement sender, 
            Point screenCoords,
            RoutedEvent routedEvent) : base(routedEvent)
        {
            _sender = sender;
            _screenCoords = screenCoords;
            _feCoords = this._sender.PointFromScreen(_screenCoords);
        }

        public Point GetPosition(IInputElement relativeTo)
        {
            if (relativeTo == null)
                return _feCoords;
            else
                return (relativeTo as FrameworkElement)
                    ?.PointFromScreen(_screenCoords) 
                    ?? default;
        }
    }
}
