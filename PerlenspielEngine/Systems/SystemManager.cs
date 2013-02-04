using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PerlenspielEngine.Entities;
using PerlenspielLib;

namespace PerlenspielEngine.Systems
{
    public class SystemManager
    {
        // List of all systems used
        private List<AbstractSystem> _systems;

        public SystemManager()
        {
            _systems = new List<AbstractSystem>();
        }

        public static void RegisterSystem(AbstractSystem system)
        {
            Singleton<SystemManager>.Instance.AddSystem(system);
        }

        private void AddSystem(AbstractSystem system)
        {
            _systems.Add(system);
        }

        /// <summary>
        /// Registers an entity with any systems that want it
        /// </summary>
        /// <param name="entity"></param>
        public void Register(Entity entity)
        {
            foreach (var system in _systems)
            {
                system.Register(entity);
            }
        }

        /// <summary>
        /// Deregisters an entity with any systems that have it
        /// </summary>
        /// <param name="entity"></param>
        public void Deregister(Entity entity)
        {
            foreach (var system in _systems)
            {
                system.Deregister(entity);
            }
        }

        /// <summary>
        /// Deregisters and then registers an entity (presumably because its components changed)
        /// </summary>
        /// <param name="entity"></param>
        public void Reregister(Entity entity)
        {
            Deregister(entity);
            Register(entity);
        }
    }
}
