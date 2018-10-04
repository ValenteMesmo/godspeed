using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Godspeed
{
    public class PencilAreaCalculator
    {
        public static List<Point> Calculate(int radiusSquared, Point point)
        {
            List<Point> indices = new List<Point>();

            for (int i = point.X - radiusSquared; i < point.X + radiusSquared; i++)
            {
                for (int j = point.Y - radiusSquared; j < point.Y + radiusSquared; j++)
                {
                    int deltaX = i - point.X;
                    int deltaY = j - point.Y;
                    double distanceSquared = Math.Pow(deltaX, 2) + Math.Pow(deltaY, 2);
                    if (distanceSquared <= radiusSquared)
                        indices.Add(new Point(i, j));
                }
            }

            return indices;
        }
    }
}
