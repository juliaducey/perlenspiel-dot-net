using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using PerlenspielEngine;

namespace PerlenspielGame
{
    static class Utilities
    {
        public static HashSet<GridPoint> CreateCircle(GridPoint center, int radius, bool filled = false)
        {
            var points = new HashSet<GridPoint>();
            points.Add(new GridPoint(center.X + radius, center.Y));
            points.Add(new GridPoint(center.X - radius, center.Y));
            points.Add(new GridPoint(center.X, center.Y + radius));
            points.Add(new GridPoint(center.X, center.Y - radius));
            if (filled)
                for (var i = center.X - radius + 1; i < center.X + radius; i++)
                    points.Add(new GridPoint(i, center.Y));

            var x = 0;
            var y = radius;
            var ddfX = 1;
            var ddfY = -2*radius;
            var f = 1 - radius;

            while (x < y)
            {
                if (f >= 0)
                {
                    y -= 1;
                    ddfY += 2;
                    f += ddfY;
                }
                x += 1;
                ddfX += 2;
                f += ddfX;

                points.Add(new GridPoint(center.X + x, center.Y + y));
                points.Add(new GridPoint(center.X - x, center.Y + y));
                if (filled)
                    for (var i = center.X - x + 1; i < center.X + x; i++)
                        points.Add(new GridPoint(i, center.Y + y));
                points.Add(new GridPoint(center.X + x, center.Y - y));
                points.Add(new GridPoint(center.X - x, center.Y - y));
                if (filled)
                    for (var i = center.X - x + 1; i < center.X + x; i++)
                        points.Add(new GridPoint(i, center.Y - y));
                points.Add(new GridPoint(center.X + y, center.Y + x));
                points.Add(new GridPoint(center.X - y, center.Y + x));
                if (filled)
                    for (var i = center.X - y + 1; i < center.X + y; i++)
                        points.Add(new GridPoint(i, center.Y + x));
                points.Add(new GridPoint(center.X + y, center.Y - x));
                points.Add(new GridPoint(center.X - y, center.Y - x)); 
                if (filled)
                    for (var i = center.X - y + 1; i < center.X + y; i++)
                        points.Add(new GridPoint(i, center.Y - x));
            }
            return points;
        }

        public static int Constrain(int num, int min, int max)
        {
            if (num > max)
                num = max;
            else if (num < min)
                num = min;
            return num;
        }

        public static double Constrain(double num, double min, double max)
        {
            if (num > max)
                num = max;
            else if (num < min)
                num = min;
            return num;
        }
    }
}
