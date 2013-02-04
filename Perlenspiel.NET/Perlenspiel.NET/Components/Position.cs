using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PerlenspielEngine;
using PerlenspielEngine.Entities;

namespace PerlenspielGame.Components
{
    class Position : Component, IPosition
    {
        public GridPoint Coords { get; set; }
        public int X
        {
            get { return Coords.X; }
            set { Coords = new GridPoint(value, Coords.Y); }
        }
        public int Y
        {
            get { return Coords.Y; }
            set { Coords = new GridPoint(Coords.X, value); }
        }

        public IEnumerable<GridPoint> Positions
        {
            get { return new[] { Coords }; }
        }

        public Position()
        {
            Coords = new GridPoint(0, 0);
        }

        public Position(int x, int y)
        {
            Coords = new GridPoint(x, y);
        }

        public Position(GridPoint coords)
        {
            Coords = coords;
        }

        public bool IsAtPosition(int x, int y)
        {
            return (x == X && y == Y);
        }

        public void ChangePosition(GridPoint oldPos, GridPoint newPos)
        {
            Coords = newPos;
        }
    }
}
