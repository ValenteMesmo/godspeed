using Microsoft.Xna.Framework;

namespace Godspeed.CommonProject
{
    public static class PointToArrayIndexExtensions
    {
        public static int ToArrayIndex(this Point point, int width)
        {
            var actualPosition = point.Y * width + point.X;
            return actualPosition;
        }

        public static Point FromArrayIndexToPoint(this int index, int width)
        {
            var result = new Point();

            if (index < width)
            {
                result.X = index;
                return result;
            }

            result.Y = (index / width);
            result.X = index % width;

            return result;
        }

    }
}
