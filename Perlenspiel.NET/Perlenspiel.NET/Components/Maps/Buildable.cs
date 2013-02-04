using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PerlenspielEngine.Entities;

namespace PerlenspielGame.Components.Maps
{
    class Buildable : Component
    {
        public string Material      { get; set; }
        public int Cost             { get; set; }
        public string BuildPrereq   { get; set; }

        public Buildable(string material, int cost = 1, string buildPrereq = "None")
        {
            Material = material;
            Cost = cost;
            BuildPrereq = buildPrereq;
        }
    }
}
