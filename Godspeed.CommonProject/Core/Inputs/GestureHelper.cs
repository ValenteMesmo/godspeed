using Godspeed.CommonProject;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Microsoft.Xna.Framework.Input.Touch;

namespace Godspeed
{
    public class TouchController
    {
        private readonly Camera2d camera;
        private readonly GraphicsDevice GraphicsDevice;
        private Vector2? pinchStartPosition;
        private Vector2 pinchCurrentPosition;
        private float pinch = 0;

        public TouchController(Camera2d camera, GraphicsDevice GraphicsDevice)
        {
            this.camera = camera;
            this.GraphicsDevice = GraphicsDevice;
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
                    //var pinchCenter = new Vector2(GraphicsDevice.Viewport.Width / 2, GraphicsDevice.Viewport.Height / 2) - currentPosition;
                    //camera.LerpPosition(
                    //    pinchCenter
                    //    , (1f - pinch) * 0.5f
                    //);

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
