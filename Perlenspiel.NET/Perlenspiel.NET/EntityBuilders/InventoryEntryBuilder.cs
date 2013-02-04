using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PerlenspielEngine;
using PerlenspielEngine.Entities;
using PerlenspielGame.Components;
using PerlenspielGame.Components.Menu;
using PerlenspielGame.Systems;
using PerlenspielLib;

namespace PerlenspielGame.EntityBuilders
{
    class InventoryEntryBuilder
    {
        public int CurrentMenuPosition;
        private List<Component> _requiredComponents;
        private List<Component> _optionalComponents;

        private string _name;
        private BeadGraphic _graphic;
        private InventoryItem _inventoryItem;

        public InventoryEntryBuilder()
        {
            CurrentMenuPosition = 0;
            _requiredComponents = new List<Component>();
            _optionalComponents = new List<Component>();
        }

        public static void Start(string name, BeadGraphic graphic)
        {
            Singleton<InventoryEntryBuilder>.Instance.StartInventory(name, graphic);
        }

        public static void Build()
        {
            Singleton<InventoryEntryBuilder>.Instance.BuildEntity();
        }

        public static EntityList InventoryEntryList()
        {
            return new EntityList(Singleton<InventoryEntryBuilder>.Instance._requiredComponents);
        }

        #region Private Methods
        private void Initialize()
        {
            _graphic = new BeadGraphic();

            _requiredComponents = new List<Component>();
            _requiredComponents.Add(_inventoryItem);
            _requiredComponents.Add(_graphic);
        }

        private void StartInventory(string name, BeadGraphic graphic)
        {
            _name = name;
            _graphic = graphic;
        }

        private void BuildEntity()
        {
            _inventoryItem.MenuPosition = Singleton<MenuSystem>.Instance.NextInventoryPosition;
            new Entity(_name, _requiredComponents.Union(_optionalComponents));
        }
        #endregion
    }
}
