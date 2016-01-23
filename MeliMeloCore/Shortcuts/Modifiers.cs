using System;

namespace MeliMelo.Core.Shortcuts
{
    /// <summary>
    /// The enumeration of possible modifiers
    /// </summary>
    [Flags]
    public enum Modifiers : uint
    {
        kAlt = 1,
        kControl = 2,
        kShift = 4,
        kWin = 8
    }
}
