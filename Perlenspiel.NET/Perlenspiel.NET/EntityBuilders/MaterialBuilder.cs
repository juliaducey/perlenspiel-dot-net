using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework;
using PerlenspielEngine.Entities;
using PerlenspielGame.Components;
using PerlenspielGame.Components.Menu;
using PerlenspielLib;

namespace PerlenspielGame.EntityBuilders
{
    class MaterialBuilder
    {
        private string _name;
        private BeadGraphic _graphic;
        private List<Component> _requiredComponents;

        public MaterialBuilder()
        {
            Initialize();
        }

        #region Static Methods
        public static void Start(string name, Color color)
        {
            Singleton<MaterialBuilder>.Instance.Initialize();
            Singleton<MaterialBuilder>.Instance._name = name;
            Singleton<MaterialBuilder>.Instance._graphic.Color = color;
        }

        public static void Build()
        {
            Singleton<MaterialBuilder>.Instance.BuildEntity();
        }

        public static void Build(string name, Color color)
        {
            Start(name, color);
            Build();
        }

        public static EntityList CreateEntityList()
        {
            return new EntityList(Singleton<MaterialBuilder>.Instance._requiredComponents);
        }
        #endregion
        #region Private Methods
        private void Initialize()
        {
            _graphic = new BeadGraphic(Color.Black, Color.Black, 4);
            _requiredComponents = new List<Component>();
            _requiredComponents.Add(_graphic);
            _requiredComponents.Add(new Window());
            // TODO: Fix this
            _requiredComponents.Add(new InventoryItem(0));
        }

        private void BuildEntity()
        {
            new Entity(_name, _requiredComponents);
        }
        #endregion
    }
}
