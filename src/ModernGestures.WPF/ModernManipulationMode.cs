using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernGestures.WPF
{
    [Flags]
    public enum ModernManipulationMode
    {
        None = 0x00,
        TranslateX = 0x03,
        TranslateY = 0x05,
        TranslateRailsX = 0x100,
        TranslateRailsY = 0x200,
        MouseScaling = 0x800,
        Rotate = 0x09,
        Scale = 0x11,
        TranslateIntertia = 0x21,
        RotateInertia = 0x41,
        ScaleInertia = 0x81,
        All = 0xFF
    }
}
