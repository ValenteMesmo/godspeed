using Microsoft.Xna.Framework;

namespace Godspeed.CommonProject
{
    public interface Camera
    {
        void ScrollDown();
        void ScrollUp();
        void ZoomIn();
        void ZoomOut();
        Vector2 GetPosition();
        void SetPosition(Vector2 position);
        Point ToWorldLocation(Point pressedPosition);
        Vector2 ToWorldLocation(Vector2 pressedPosition);
        void SetZoom(float pinch);
        void LerpPosition(Vector2 value, float amount);
    }
}
