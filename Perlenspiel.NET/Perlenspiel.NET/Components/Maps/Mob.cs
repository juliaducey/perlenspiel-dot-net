using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PerlenspielEngine.Entities;

namespace PerlenspielGame.Components.Maps
{
    public class Mob : Component
    {
        public List<string> MovementTypes { get; set; }

        public Mob(IEnumerable<string> moveTypes = null)
        {
            // Default move type is just walk
            if (moveTypes == null)
            {
                MovementTypes = new List<string> { "walk" };
            }
            else
            {
                MovementTypes = moveTypes.ToList();
            }
        }
    }
}
