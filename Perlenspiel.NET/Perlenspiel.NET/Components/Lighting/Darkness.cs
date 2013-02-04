using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PerlenspielEngine.Entities;

namespace PerlenspielGame.Components.Lighting
{
    class Darkness : Component, IOverlay
    {
        public Color FogColor
        {
            get { return new Color(R, G, B); }
            set { R = value.R; G = value.G; B = value.B; }
        }

        #region IOverlay Members
        public int R { get; private set; }
        public int G { get; private set; }
        public int B { get; private set; }
        public double Alpha { get { return GameState.MaxAlpha; } }

        public bool AffectsColor { get { return true; } }
        public bool AffectsBorder { get { return true; } }
        public bool AffectsGlyph { get { return true; } }
        public bool AffectsFlash { get { return true; } }
        #endregion

        public Darkness(Color fogColor)
        {
            FogColor = fogColor;
        }
    }
}
