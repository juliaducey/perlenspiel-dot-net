using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PerlenspielEngine.Entities;
using PerlenspielEngine.Systems;
using PerlenspielGame.Components;
using PerlenspielGame.Components.Maps;
using PerlenspielLib;

namespace PerlenspielGame.Systems
{
    class ConstructionSystem : AbstractSystem
    {
        private List<Entity> _harvestables;
        private List<string> _tools;

        #region System Members
        public ConstructionSystem()
        {
            _tools = new List<string>();
            _harvestables = new List<Entity>();
        }

        public override void Register(Entity entity)
        {
            if (entity.Component<Harvestable>() != null)
            {
                _harvestables.Add(entity);
            }
        }

        public override void Deregister(Entity entity)
        {
            _harvestables.Remove(entity);
        }
        #endregion

        public void Harvest(int x, int y)
        {
            var harvestables = from entity in _harvestables
                               where entity.Component<IPosition>().IsAtPosition(x, y)
                               select entity;
            if (harvestables.Count() != 0)
            {
                var harvestable = harvestables.First();
                var component = harvestable.Component<Harvestable>();
                Singleton<MapSystem>.Instance.SetTile(x, y, component.BaseTile);
                Singleton<InventorySystem>.Instance.ChangeItem(component.HarvestItem, component.ItemYield);
                PS.StatusText(component.Message);
            }
        }

        public void Build(int x, int y, string tile, string material, int cost)
        {
            if (Singleton<InventorySystem>.Instance.GetAmount(material) < cost)
            {
                PS.StatusText("You don't have enough materials to build that.");
            }
            else
            {
                Singleton<MapSystem>.Instance.SetTile(x, y, tile);
                Singleton<InventorySystem>.Instance.ChangeItem(material, cost * -1);
            }
        }
    }
}
