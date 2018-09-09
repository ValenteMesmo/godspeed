using Godspeed.CommonProject;
using Microsoft.Xna.Framework;

namespace Godspeed
{
    public static class Vector2Extensions
    {
        public static Point Add(this Point vector, int x, int y)
        {
            return new Point(vector.X + x, vector.Y + y);
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
