using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PerlenspielEngine.Entities
{
    public class EntityList : IList<Entity>
    {
        private List<Entity> _entities;
        private List<Component> _components; 

        /// <summary>
        /// Constructs a new EntityList, which only adds entities with the set of required components
        /// </summary>
        /// <param name="components">Dummy components; all added entities must have all of these types</param>
        public EntityList(List<Component> components)
        {
            _components = components;
            _entities = new List<Entity>();
        }

        private bool CanAdd(Entity entity)
        {
            return _components.All(component => entity.HasComponentType(component) != false);
        }

        public IEnumerator<Entity> GetEnumerator()
        {
            return _entities.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(Entity item)
        {
            if (CanAdd(item) == true)
            {
                _entities.Add(item);
            }
        }

        public void Clear()
        {
            _entities.Clear();
        }

        public bool Contains(Entity item)
        {
            return _entities.Contains(item);
        }

        public void CopyTo(Entity[] array, int arrayIndex)
        {
            _entities.CopyTo(array, arrayIndex);
        }

        public bool Remove(Entity item)
        {
            return _entities.Remove(item);
        }

        /// <summary>
        /// Removes ALL entities with the given name from the list
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public void Remove(string name)
        {
            _entities = (from entity in _entities
                         where entity.Name != name
                         select entity).ToList();
        }

        public int Count { get; private set; }
        public bool IsReadOnly { get; private set; }

        public int IndexOf(Entity item)
        {
            return _entities.IndexOf(item);
        }

        public void Insert(int index, Entity item)
        {
            if (CanAdd(item) == true)
            {
                _entities.Insert(index, item);
            }
        }

        public void RemoveAt(int index)
        {
            _entities.RemoveAt(index);
        }

        public Entity this[int index]
        {
            get { return _entities[index]; }
            set { if (CanAdd(value) == true) _entities[index] = value; }
        }

        /// <summary>
        /// Returns first occurrence of the entity with the given name
        /// </summary>
        /// <param name="name"></param>
        /// <returns></returns>
        public Entity this[string name]
        {
            get { return _entities.FirstOrDefault(entity => entity.Name == name); }
        }
    }
}
