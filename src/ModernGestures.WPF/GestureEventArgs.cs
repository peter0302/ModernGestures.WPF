using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using ICTest;

namespace ModernGestures.WPF
{
    public class GestureEventArgs : RoutedEventArgs
    {        
        internal GestureEventArgs(
            FrameworkElement sender, 
            InteractionOutput output,
            RoutedEvent routedEvent) : base(routedEvent)
        {
            InteractionOutput = output;
            Target = sender;
            _screenCoords = new Point(output.Data.X, output.Data.Y);
            _feCoords = this.Target.PointFromScreen(_screenCoords);
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

        internal InteractionOutput InteractionOutput { get; private set; }
        internal FrameworkElement Target { get; private set; }

        private Point _screenCoords;
        private Point _feCoords;
        
    }
}
