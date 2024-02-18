using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

using ICTest;

namespace ModernGestures.WPF
{
    public delegate void ModernManipulationDeltaEventHandler(object sender, ModernManipulationDeltaEventArgs e);

    public class ModernManipulationDeltaEventArgs : GestureEventArgs
    {
        internal ModernManipulationDeltaEventArgs(
            FrameworkElement sender,
            InteractionOutput output) : base(sender, output, Gestures.ManipulationDeltaEvent)
        {
        }

        public ManipulationDelta DeltaManipulation
        {
            get
            {
                if (_delta == null)
                {
                    var scale = Gestures.GetDPIScale(this.Target);
                    _delta = new ManipulationDelta(
                        new Vector(
                            InteractionOutput.Data.Manipulation.Delta.TranslationX / scale,
                            InteractionOutput.Data.Manipulation.Delta.TranslationY / scale),
                        InteractionOutput.Data.Manipulation.Delta.Rotation,
                        new Vector(
                            InteractionOutput.Data.Manipulation.Delta.Scale,
                            InteractionOutput.Data.Manipulation.Delta.Scale),
                        default);
                }
                return _delta;
            }
        }

        public ManipulationDelta CumulativeManipulation
        {
            get
            {
                if (_cumulative == null)
                {
                    var scale = Gestures.GetDPIScale(this.Target);
                    _cumulative = new ManipulationDelta(
                        new Vector(
                            InteractionOutput.Data.Manipulation.Cumulative.TranslationX / scale,
                            InteractionOutput.Data.Manipulation.Cumulative.TranslationY / scale),
                        InteractionOutput.Data.Manipulation.Cumulative.Rotation,
                        new Vector(
                            InteractionOutput.Data.Manipulation.Cumulative.Scale,
                            InteractionOutput.Data.Manipulation.Cumulative.Scale),
                        default);
                }
                return _cumulative;
            }
        }

        ManipulationDelta? _delta;
        ManipulationDelta? _cumulative;
    }
}
