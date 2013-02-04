using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PerlenspielEngine.Entities;

namespace PerlenspielGame.Components
{
    public class BeadGraphic : Component
    {
        public Color Color              { get; set; }

        private double _alpha;
        public double Alpha
        {
            get { return _alpha; }
            set { _alpha = Utilities.Constrain(value, GameState.MinAlpha, GameState.MaxAlpha); }
        }
        public bool Show                { get; set; }

        public int BorderWidth          { get; set; }
        public Color BorderColor        { get; set; }
        public int BorderAlpha          { get; set; }

        public char Glyph               { get; set; }
        public Color GlyphColor         { get; set; }

        public bool Flash               { get; set; }
        public Color FlashColor         { get; set; }

        public BeadGraphic()
        {
            SetDefault();
        }

        public BeadGraphic(Color color)
        {
            SetDefault();
            Color = color;
        }

        public BeadGraphic(int r, int g, int b)
        {
            SetDefault();
            Color = new Color(r, g, b);
        }

        public BeadGraphic(Color color, Color borderColor, int borderWidth)
        {
            SetDefault();
            Color = color;
            BorderColor = borderColor;
            BorderWidth = borderWidth;
        }

        private void SetDefault()
        {
            Color = Color.Transparent;
            Alpha = 1.0;
            Show = true;

            BorderWidth = 0;
            BorderColor = Color.Black;
            BorderAlpha = 100;

            Glyph = ' ';
            GlyphColor = Color.Black;

            Flash = false;
            FlashColor = Color.White;
        }
    }
}
