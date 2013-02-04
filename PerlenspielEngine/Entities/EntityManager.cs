using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PerlenspielLib;

namespace PerlenspielEngine.Entities
{
    public class EntityManager
    {
        private Dictionary<string, EntityFactory> _factories;

        public EntityManager()
        {
            _factories = new Dictionary<string, EntityFactory>();
        }

        #region Static Wrapper Methods
        public static Entity Create(string name)
        {
            return Singleton<EntityManager>.Instance.MakeEntity(name);
        }

        public static void CreateType(string name, List<Component> components)
        {

            Singleton<EntityManager>.Instance.MakeType(name, components);
        }
        #endregion

        private Entity MakeEntity(string name)
        {
            return _factories[name.ToLower()].CreateEntity();
        }

        private void MakeType(string name, List<Component> components)
        {
            _factories[name.ToLower()] = new EntityFactory(name, components);
        }
    }
}
