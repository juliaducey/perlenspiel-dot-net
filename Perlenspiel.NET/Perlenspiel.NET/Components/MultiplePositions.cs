using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PerlenspielEngine;
using PerlenspielEngine.Entities;

namespace PerlenspielGame.Components
{
    class MultiplePositions : Component, IPosition
    {
        public IEnumerable<GridPoint> Positions
        {
            get { return _positions.AsEnumerable(); }
        }

        private HashSet<GridPoint> _positions;

        public MultiplePositions()
        {
            _positions = new HashSet<GridPoint>();
        }

        public MultiplePositions(IEnumerable<GridPoint> locations)
        {
            _positions = new HashSet<GridPoint>(locations);
        }

        public void AddPosition(int x, int y)
        {
            _positions.Add(new GridPoint(x, y));
        }

        public void RemovePosition(int x, int y)
        {
            _positions.Remove(new GridPoint(x, y));
        }

        public bool IsAtPosition(int x, int y)
        {
            var positions = from pos in _positions
                            where pos.X == x && pos.Y == y
                            select pos;
            return positions.Any();
        }

        public void ChangePosition(GridPoint oldPos, GridPoint newPos)
        {
            if (_positions.Contains(oldPos))
            {
                _positions.Remove(oldPos);
                _positions.Add(newPos);
            }
        }
    }
}
