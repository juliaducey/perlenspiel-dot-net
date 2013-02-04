using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PerlenspielEngine.Entities;

namespace PerlenspielEngine.Systems
{
    public abstract class AbstractSystem
    {
        protected List<EntityList> EntityLists { get; set; }

        protected AbstractSystem()
        {
            SystemManager.RegisterSystem(this);
            EntityLists = new List<EntityList>();
        }

        public virtual void Initialize()
        {
        }

        public virtual void Register(Entity entity)
        {
            foreach (var list in EntityLists)
            {
                list.Add(entity);
            }
        }

        public virtual void Deregister(Entity entity)
        {
            foreach (var list in EntityLists)
            {
                list.Remove(entity);
            }
        }
    }
}
