using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PerlenspielEngine
{
    public class GridPoint
    {
        public int X;
        public int Y;

        public GridPoint(int x, int y)
        {
            X = x;
            Y = y;
        }

        public GridPoint Add(GridPoint point)
        {
            return new GridPoint(X + point.X, Y + point.Y);
        }

        public GridPoint Add(int x, int y)
        {
            return new GridPoint(X + x, Y + y);
        }

        public GridPoint Multiply(int multiplier)
        {
            return new GridPoint(X * multiplier, Y * multiplier);
        }

        public static GridPoint operator +(GridPoint p1, GridPoint p2)
        {
            return p1.Add(p2);
        }

        public static GridPoint operator *(GridPoint point, int multiplier)
        {
            return point.Multiply(multiplier);
        }
    }
}
