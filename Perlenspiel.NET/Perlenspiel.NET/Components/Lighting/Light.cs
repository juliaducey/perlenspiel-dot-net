using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PerlenspielEngine.Entities;

namespace PerlenspielGame.Components.Lighting
{
    class Light : Component, IOverlay
    {
        private Color _lightColor;
        public Color LightColor
        {
            get { return _lightColor; }
            set { _lightColor = value; CalcColor(); }
        }

        private double _lightStrength;
        public double LightStrength
        {
            get { return _lightStrength; }
            set { _lightStrength = Utilities.Constrain(value, GameState.MinAlpha, GameState.MaxAlpha); CalcColor(); }
        }

        private double _fogStrength;
        public double FogStrength
        {
            get { return _fogStrength; }
            set { _fogStrength = Utilities.Constrain(value, GameState.MinAlpha, GameState.MaxAlpha); }
        }

        #region IOverlay Members
        public int R                { get; private set; }
        public int G                { get; private set; }
        public int B                { get; private set; }
        public double Alpha         { get { return GameState.MaxAlpha; } }

        public bool AffectsColor    { get { return true; } }
        public bool AffectsBorder   { get { return true; } }
        public bool AffectsGlyph    { get { return true; } }
        public bool AffectsFlash    { get { return true; } }
        #endregion

        public Light(Color lightColor, double lightStrength, double fogStrength)
        {
            _lightColor = lightColor;
            LightStrength = lightStrength;
            FogStrength = fogStrength;
            CalcColor();
        }

        private void CalcColor()
        {
            R = Utilities.Constrain(Convert.ToInt32(LightColor.R * LightStrength), 0, 255);
            G = Utilities.Constrain(Convert.ToInt32(LightColor.G * LightStrength), 0, 255);
            B = Utilities.Constrain(Convert.ToInt32(LightColor.B * LightStrength), 0, 255);
        }
    }
}
