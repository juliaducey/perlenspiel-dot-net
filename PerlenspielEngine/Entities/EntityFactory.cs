using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PerlenspielEngine.Entities
{
    class EntityFactory
    {
        private string _name;
        private List<Component> _components;

        public EntityFactory(string name, List<Component> components)
        {
            _name = name;
            _components = components;
        }

        public Entity CreateEntity()
        {
            var components = (from component in _components
                              select component.Copy()).ToList();
            return new Entity(_name, components);
        }
    }
}
