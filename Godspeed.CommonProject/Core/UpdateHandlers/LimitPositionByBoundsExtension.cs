using Microsoft.Xna.Framework;

namespace Godspeed
{
    public static class LimitPositionByBoundsExtension
    {
        public static void LimitPositionByBounds(this IAddUpdateHandlers updates, Rectangle bounds)
        {
            updates.AddUpdateHandler(gameObject =>
            {
                var position = gameObject.GetPosition();

                if (position.X > bounds.Right)
                    position.X = bounds.Right;

                if (position.X < bounds.Left)
                    position.X = bounds.Left;

                if (position.Y < bounds.Top)
                    position.Y = bounds.Top;

                if (position.Y > bounds.Bottom)
                    position.Y = bounds.Bottom;

                gameObject.SetPosition(position);
            });
        }
    }
}
