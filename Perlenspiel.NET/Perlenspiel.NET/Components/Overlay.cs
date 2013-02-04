using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PerlenspielEngine.Entities;

namespace PerlenspielGame.Components
{
    public class Overlay : Component, IOverlay
    {
        public int R                { get; set; }
        public int G                { get; set; }
        public int B                { get; set; }
        private double _alpha;
        public double Alpha
        {
            get { return _alpha; }
            set { _alpha = Utilities.Constrain(value, GameState.MinAlpha, GameState.MaxAlpha); }
        }

        public bool AffectsColor    { get; set; }
        public bool AffectsBorder   { get; set; }
        public bool AffectsGlyph    { get; set; }
        public bool AffectsFlash    { get; set; }

        /// <summary>
        /// Constructor for individually set RGB values
        /// </summary>
        /// <param name="R"></param>
        /// <param name="G"></param>
        /// <param name="B"></param>
        /// <param name="alpha"></param>
        public Overlay(int r, int g, int b, bool lighten = true, double alpha = 1.0)
        {
            R = r;
            G = g;
            B = b;
            Alpha = alpha;
            SetDefault(lighten);
        }

        /// <summary>
        /// Constructor for applying a multiplying factor to an existing color
        /// </summary>
        /// <param name="baseColor"></param>
        /// <param name="factor"></param>
        /// <param name="alpha"></param>
        public Overlay(Color baseColor, double factor, bool lighten = true, double alpha = 1.0)
        {
            R = Convert.ToInt32(baseColor.R * factor);
            G = Convert.ToInt32(baseColor.G * factor);
            B = Convert.ToInt32(baseColor.B * factor);
            Alpha = alpha;
            SetDefault(lighten);
        }

        /// <summary>
        /// Call this to specify the overlay color directly
        /// </summary>
        /// <param name="overlayColor"></param>
        /// <param name="alpha"></param>
        public Overlay(Color overlayColor, bool lighten = true, double alpha = 1.0)
        {
            R = overlayColor.R;
            G = overlayColor.G;
            B = overlayColor.B;
            Alpha = alpha;
            SetDefault(lighten);
        }

        private void SetDefault(bool lighten)
        {
            if (lighten == false)
            {
                R = R * -1;
                G = G * -1;
                B = B * -1;
            }
            AffectsColor = true;
            AffectsBorder = true;
            AffectsGlyph = true;
            AffectsFlash = true;
        }
    }
}
