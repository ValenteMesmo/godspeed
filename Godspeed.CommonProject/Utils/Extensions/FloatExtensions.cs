namespace Godspeed
{
    public static class FloatExtensions
    {
        public static int AsInt(this float number)
        {
            return (int)number;
        }

        public static float Lerp(this float firstFloat, float secondFloat, float by)
        {
            return firstFloat * (1 - by) + secondFloat * by;
        }
    }
}
