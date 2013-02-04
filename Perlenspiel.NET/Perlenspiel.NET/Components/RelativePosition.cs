using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PerlenspielEngine;
using PerlenspielEngine.Entities;

namespace PerlenspielGame.Components
{
    class RelativePosition : Component, IPosition
    {
        public IEnumerable<GridPoint> Positions 
        {
            get { return new[] { new GridPoint(X, Y) }; }
        }
        public int X
        {
            get { return _basePosition.X + _xOffset; }
        }
        public int Y
        {
            get { return _basePosition.Y + _yOffset; }
        }

        private GridPoint _basePosition;
        private int _xOffset;
        private int _yOffset;

        public RelativePosition(GridPoint basePosition, int xOffset, int yOffset)
        {
            _basePosition = basePosition;
            _xOffset = xOffset;
            _yOffset = yOffset;
        }

        public bool IsAtPosition(int x, int y)
        {
            return x == X && y == Y;
        }

        public void ChangePosition(GridPoint oldPos, GridPoint newPos)
        {
            _xOffset = _basePosition.X - newPos.X;
            _yOffset = _basePosition.Y - newPos.Y;
        }
    }
}
