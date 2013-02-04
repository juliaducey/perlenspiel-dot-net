using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PerlenspielEngine;
using PerlenspielEngine.Entities;

namespace PerlenspielGame.Components.Maps
{
    public class Obstacle : Component
    {
        public List<string> MovementTypesBlocked { get; set; }

        public Obstacle(IEnumerable<string> moveTypes = null)
        {
            // By default, obstacles block everything
            if (moveTypes == null)
            {
                MovementTypesBlocked = new List<string>{ "all" };
            }
            else
            {
                MovementTypesBlocked = moveTypes.ToList();
            }
        }
    }
}
