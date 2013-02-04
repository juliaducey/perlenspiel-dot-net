using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;

namespace PerlenspielGame.Components
{
    interface IOverlay
    {
        int R               { get; }
        int G               { get; }
        int B               { get; }
        double Alpha           { get; }

        bool AffectsColor   { get; }
        bool AffectsBorder  { get; }
        bool AffectsGlyph   { get; }
        bool AffectsFlash   { get; }
    }
}
