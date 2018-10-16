using Godspeed.CommonProject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace Godspeed
{
    public class PinchController
    {
        private readonly Camera camera;
        private Vector2? pinchStartPosition;
        private Vector2 pinchCurrentPosition;
        private float pinch = 0;

        public PinchController(Camera camera)
        {
            this.camera = camera;
        }

        public void Update()
        {
            var pinching = false;
            while (TouchPanel.IsGestureAvailable)
            {
                GestureSample gesture = TouchPanel.ReadGesture();

                if (gesture.GestureType == GestureType.Pinch)
                {
                    pinching = true;

                    Vector2 a = gesture.Position;
                    Vector2 aOld = gesture.Position - gesture.Delta;
                    Vector2 b = gesture.Position2;
                    Vector2 bOld = gesture.Position2 - gesture.Delta2;

                    float d = Vector2.Distance(a, b);
                    float dOld = Vector2.Distance(aOld, bOld);

                    float scaleChange = (d - dOld) * .01f;

                    var currentPosition = new Vector2((a.X + b.X) * .5f, (a.Y + b.Y) * .5f);

                    if (pinchStartPosition.HasValue)
                        pinchCurrentPosition = currentPosition;
                    else
                        pinchStartPosition = pinchCurrentPosition = currentPosition;

                    pinch += scaleChange;
                    camera.SetZoom(pinch);

                    continue;
                }
            }

            if (pinching)
                camera.SetPosition(camera.GetPosition() + (pinchStartPosition.Value - pinchCurrentPosition) * .05f);
            else
                pinchStartPosition = null;
        }
    }

}
