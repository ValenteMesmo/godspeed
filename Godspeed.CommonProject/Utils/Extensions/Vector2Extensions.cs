using Godspeed.CommonProject;
using Microsoft.Xna.Framework;
using System;

namespace Godspeed
{
    public static class Vector2Extensions
    {
        public static void ForEachPointUntil(this Point a, Point b, Action<int, int> action)
        {
            int w = b.X - a.X;
            int h = b.Y - a.Y;
            int dx1 = 0, dy1 = 0, dx2 = 0, dy2 = 0;
            if (w < 0) dx1 = -1; else if (w > 0) dx1 = 1;
            if (h < 0) dy1 = -1; else if (h > 0) dy1 = 1;
            if (w < 0) dx2 = -1; else if (w > 0) dx2 = 1;
            int longest = Math.Abs(w);
            int shortest = Math.Abs(h);
            if (!(longest > shortest))
            {
                longest = Math.Abs(h);
                shortest = Math.Abs(w);
                if (h < 0) dy2 = -1; else if (h > 0) dy2 = 1;
                dx2 = 0;
            }
            int numerator = longest >> 1;
            for (int i = 0; i <= longest; i++)
            {
                action(a.X, a.Y);
                numerator += shortest;
                if (!(numerator < longest))
                {
                    numerator -= longest;
                    a.X += dx1;
                    a.Y += dy1;
                }
                else
                {
                    a.X += dx2;
                    a.Y += dy2;
                }
            }
        }

        public static Point Add(this Point vector, int x, int y)
        {
            return new Point(vector.X + x, vector.Y + y);
        }

        public static Point AddX(this Point vector, int x)
        {
            return new Point(vector.X + x, vector.Y );
        }

        public static Point AddY(this Point vector, int y)
        {
            return new Point(vector.X, vector.Y + y);
        }

        public static Vector2 Lerp(this Vector2 firstVector, Vector2 secondVector, float by)
        {
            return Vector2.Lerp(firstVector, secondVector, by);
        }

        public static Vector2 Lerp(this Vector2 firstVector, Point secondVector, float by)
        {
            return Vector2.Lerp(firstVector, secondVector.ToVector2(), by);
        }

        public static Vector2 ToWorldPosition(this Vector2 position, Camera2d camera)
        {
            return camera.ToWorldLocation(position);
        }

        public static Point ToWorldPosition(this Point position, Camera2d camera)
        {
            return camera.ToWorldLocation(position);
        }
    }
}
