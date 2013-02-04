using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PerlenspielEngine.Entities
{
    public abstract partial class Component : IComponent
    {
        public virtual Component Copy()
        {
            return this.MemberwiseClone() as Component;
        }
    }
}
