using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ModernGestures.WPF.Internal
{
    // High WORD Secondary Button
    // Low WORD Primary Button

    [Flags]
    public enum TapMode : uint
    {
        None = 0,
        Single =        0x00000001,
        Double =        0x00000003,     // double tap also requires single tap apparently
        Secondary =     0x00010000,
        All = Single | Double | Secondary,
    }
}
