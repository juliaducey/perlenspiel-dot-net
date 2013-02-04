using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;

namespace PerlenspielLib
{
    class Bead
    {
        public Point Coords                 { get; set; }
        public Rectangle Area               { get; set; }

        public Color Color                  { get; set; }
        public int Alpha                    { get; set; }
        public bool Show                    { get; set; }

        public int BorderWidth              { get; set; }
        public Color BorderColor            { get; set; }
        public int BorderAlpha              { get; set; }

        public char Glyph                   { get; set; }
        public Color GlyphColor             { get; set; }

        public bool Flash                   { get; set; }
        public Color FlashColor             { get; set; }

        public Bead(int x, int y)
        {
            Coords = new Point(x, y);

            // Default bead parameters; reassign after creation if desired
            Color = Color.White;
            Alpha = 100;
            Show = true;

            BorderWidth = 1;
            BorderColor = Color.WhiteSmoke;
            BorderAlpha = 100;

            Glyph = ' ';
            GlyphColor = Color.Black;

            Flash = true;
            FlashColor = Color.White;
        }

        public bool IsInArea(int x, int y)
        {
            return Area.Intersects(new Rectangle(x, y, 1, 1));
        }
    }
}
