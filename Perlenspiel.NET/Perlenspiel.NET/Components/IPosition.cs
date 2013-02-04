using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PerlenspielEngine;
using PerlenspielEngine.Entities;

namespace PerlenspielGame.Components
{
    interface IPosition : IComponent
    {
        IEnumerable<GridPoint> Positions { get; } 
        bool IsAtPosition(int x, int y);
        void ChangePosition(GridPoint oldPos, GridPoint newPos);
    }
}
