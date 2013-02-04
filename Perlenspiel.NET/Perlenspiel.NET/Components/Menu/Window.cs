using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PerlenspielEngine;
using PerlenspielEngine.Entities;

namespace PerlenspielGame.Components
{
    class Window : Component
    {
        public Color Color      { get; set; }
        public int Layer        { get; set; }

        public Window(Color color, int layer = 0)
        {
            Color = color;
            Layer = layer;
        }

        public Window()
        {
            Color = Color.Black;
            Layer = 0;
        }
    }
}
