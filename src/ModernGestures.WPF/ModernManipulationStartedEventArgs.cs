using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

using ICTest;

namespace ModernGestures.WPF
{
    public delegate void ModernManipulationStartedEventHandler(object sender, ModernManipulationStartedEventArgs e);

    public class ModernManipulationStartedEventArgs : GestureEventArgs
    {
        internal ModernManipulationStartedEventArgs(
            FrameworkElement sender,
            InteractionOutput output) : base(sender, output, Gestures.ManipulationStartedEvent)
        {
        }        

        public Point ManipulationOrigin
        {
            get
            {
                return _manipulationOrigin ?? (_manipulationOrigin = new Point(
                    this.InteractionOutput.Data.X,
                    this.InteractionOutput.Data.Y)).Value; 
            }
        }

        Point? _manipulationOrigin;
    }
}
