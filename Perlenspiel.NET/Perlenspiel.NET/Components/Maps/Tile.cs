
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PerlenspielEngine.Entities;

namespace PerlenspielGame.Components.Maps
{
    class Tile : Component
    {
        public string Description       { get; set; }

        public Tile(string description)
        {
            Description = description;
        }
    }
}
