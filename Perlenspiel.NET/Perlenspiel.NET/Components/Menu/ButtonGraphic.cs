using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PerlenspielEngine.Entities;

namespace PerlenspielGame.Components.Menu
{
    class ButtonGraphic : Component
    {
        public Color PressedColor           { get; set; }
        public Color PressedGlyphColor      { get; set; }
        public Color PressedBorderColor     { get; set; }
        public int PressedBorderWidth       { get; set; }

        public Color OriginalColor          { get; set; }
        public Color OriginalGlyphColor     { get; set; }
        public Color OriginalBorderColor    { get; set; }
        public int OriginalBorderWidth      { get; set; }

        public ButtonGraphic(BeadGraphic graphic)
        {
            Initialize(graphic);
        }

        public ButtonGraphic(BeadGraphic graphic, Color pressedColor)
        {
            Initialize(graphic);
            PressedColor = pressedColor;
        }

        public ButtonGraphic(BeadGraphic graphic, Color pressedColor, Color pressedGlyphColor, 
            Color pressedBorderColor, int pressedBorderWidth)
        {
            Initialize(graphic);
            PressedColor = pressedColor;
            PressedGlyphColor = pressedGlyphColor;
            PressedBorderColor = pressedBorderColor;
            PressedBorderWidth = pressedBorderWidth;
        }

        public void ChangeToPressedGraphic(BeadGraphic graphic)
        {
            graphic.Color = PressedColor;
            graphic.GlyphColor = PressedGlyphColor;
            graphic.BorderColor = PressedBorderColor;
            graphic.BorderWidth = PressedBorderWidth;
        }

        public void ChangeToOriginalGraphic(BeadGraphic graphic)
        {
            graphic.Color = OriginalColor;
            graphic.GlyphColor = OriginalGlyphColor;
            graphic.BorderColor = OriginalBorderColor;
            graphic.BorderWidth = OriginalBorderWidth;
        }

        private void Initialize(BeadGraphic graphic)
        {
            // Create deep copies of the colors in case they become classes instead of structs at some point
            // (otherwise bugs will happen when I reassign the graphic's color later)
            OriginalColor = new Color(graphic.Color.R, graphic.Color.G, graphic.Color.B);
            OriginalGlyphColor = new Color(graphic.GlyphColor.R, graphic.GlyphColor.G, graphic.GlyphColor.B);
            OriginalBorderColor = new Color(graphic.BorderColor.R, graphic.BorderColor.G, graphic.BorderColor.B);
            OriginalBorderWidth = graphic.BorderWidth;

            PressedColor = Color.White;
            PressedBorderColor = Color.Black;
            PressedGlyphColor = Color.Black;
            PressedBorderWidth = 0;
        }
    }
}
