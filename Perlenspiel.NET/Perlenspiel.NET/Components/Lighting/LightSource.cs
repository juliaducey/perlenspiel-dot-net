using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PerlenspielEngine.Entities;

namespace PerlenspielGame.Components.Lighting
{
    class LightSource : Component
    {
        public int Radius   { get; set; }
        public Color Color  { get; set; }
        private double _strength;
        public double Strength
        {
            get { return _strength; }
            set { _strength = Utilities.Constrain(value, GameState.MinAlpha, GameState.MaxAlpha); }
        }

        public LightSource(int radius, Color color, double strength = 0.5)
        {
            Radius = radius;
            Color = color;
            Strength = strength;
        }
    }
}
