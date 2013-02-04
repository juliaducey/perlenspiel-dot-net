using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PerlenspielEngine.Systems;
using PerlenspielLib;

namespace PerlenspielEngine.Entities
{
    public class Entity
    {
        public string Name                      { get; set; }
        private List<Component> _components;

        public Entity(string name, IEnumerable<Component> components)
        {
            Name = name;
            _components = components.ToList();
            Register();
        }

        /// <summary>
        /// Adds a component to this entity. You can't have more than one of a kind.
        /// </summary>
        /// <param name="component"></param>
        public void AddComponent(Component component)
        {
            if (HasComponentType(component) == false)
            {
                _components.Add(component);
                Reregister();
            }
        }

        /// <summary>
        /// Removes all components of the given type
        /// </summary>
        public void RemoveComponent<T>() where T : class, IComponent
        {
            var components = from component in _components
                             where !(component is T)
                             select component;

            _components = components.ToList();
            Reregister();
        }

        public T Component<T>() where T : class, IComponent
        {
            var components = from component in _components
                             where component is T
                             select component;

            if (components.Any())
            {
                return components.First() as T;
            }
            return null;
        }

        public bool HasComponentType(Component component)
        {
            var type = component.GetType();
            return _components.Any(comp => comp.GetType() == type);
        }

        public bool HasSpecificComponent(Component component)
        {
            return _components.Contains(component);
        }

        public void Destroy()
        {
            Deregister();
        }

        private void Register()
        {
            Singleton<SystemManager>.Instance.Register(this);
        }

        private void Deregister()
        {
            Singleton<SystemManager>.Instance.Deregister(this);
        }

        private void Reregister()
        {
            Singleton<SystemManager>.Instance.Reregister(this);
        }
    }
}
