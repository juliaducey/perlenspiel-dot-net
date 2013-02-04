using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PerlenspielEngine.Entities;

namespace PerlenspielGame.Components.Menu
{
    class InventoryItem : Component
    {
        public int Amount       { get; private set; }
        public int MenuPosition { get; set; }
        private int _max;

        public InventoryItem(int menuPosition, int max = 999)
        {
            MenuPosition = menuPosition;
            _max = max;
            Amount = 0;
        }

        public void ChangeAmount(int amount)
        {
            Amount = Utilities.Constrain(0, Amount + amount, _max);   
        }
    }
}
