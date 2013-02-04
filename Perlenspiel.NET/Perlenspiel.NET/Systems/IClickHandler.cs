using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PerlenspielGame.Systems
{
    public interface IClickHandler
    {
        void HandleClick(int x, int y);
    }
}
