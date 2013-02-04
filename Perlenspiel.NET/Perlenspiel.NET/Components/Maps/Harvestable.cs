using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PerlenspielEngine.Entities;

namespace PerlenspielGame.Components.Maps
{
    class Harvestable : Component
    {
        public string Message       { get; set; }
        public string HarvestItem   { get; set; }
        public string HarvestPrereq { get; set; }
        public string BaseTile      { get; set; }
        public int ItemYield        { get; set; }

        public Harvestable(string message, string harvestItem, int itemYield = 1, 
            string harvestPrereq = "None", string baseTile = "Default")
        {
            Message = message;
            HarvestItem = harvestItem;
            HarvestPrereq = harvestPrereq;
            BaseTile = baseTile;
            ItemYield = itemYield;
        }
    }
}
