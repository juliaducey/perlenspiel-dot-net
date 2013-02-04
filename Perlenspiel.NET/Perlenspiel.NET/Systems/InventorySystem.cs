using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PerlenspielEngine.Entities;
using PerlenspielEngine.Systems;
using PerlenspielGame.Components.Menu;

namespace PerlenspielGame.Systems
{
    class InventorySystem : AbstractSystem
    {
        private Dictionary<string, InventoryItem> _inventory;

        #region System Members
        public InventorySystem()
        {
            _inventory = new Dictionary<string, InventoryItem>();
        }

        public override void Register(Entity entity)
        {
            var item = entity.Component<InventoryItem>();
            if (item != null && _inventory.ContainsKey(entity.Name) == false)
            {
                _inventory.Add(entity.Name, item);
            }
        }

        public override void Deregister(Entity entity)
        {
            if (_inventory.ContainsKey(entity.Name))
            {
                _inventory.Remove(entity.Name);
            }
        }
        #endregion

        public void ChangeItem(string item, int amount)
        {
            if (_inventory.ContainsKey(item))
            {
                _inventory[item].ChangeAmount(amount);
            }
        }

        public int GetAmount(string item)
        {
            if (_inventory.ContainsKey(item))
            {
                return _inventory[item].Amount;
            }
            return 0;
        }
    }
}
