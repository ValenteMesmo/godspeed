using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using System;

namespace Godspeed
{
    class GestureHelper
    {
        public static void HandleTouchInput(Action<float, Point> asdasd)
        {
            while (TouchPanel.IsGestureAvailable)
            {
                GestureSample gesture = TouchPanel.ReadGesture();

                switch (gesture.GestureType)
                {
                    case GestureType.Tap:
                    case GestureType.DoubleTap:
                        break;

                    case GestureType.Hold:
                        break;

                    case GestureType.FreeDrag:
                        break;

                    case GestureType.Flick:
                        break;

                    case GestureType.Pinch:
                        {
                            Vector2 a = gesture.Position;
                            Vector2 aOld = gesture.Position - gesture.Delta;
                            Vector2 b = gesture.Position2;
                            Vector2 bOld = gesture.Position2 - gesture.Delta2;

                            float d = Vector2.Distance(a, b);
                            float dOld = Vector2.Distance(aOld, bOld);

                            float scaleChange = (d - dOld) * .01f;
                            //var point = new Point((int)((a.X + b.X)*0.5f), (int)((a.Y + b.Y) * 0.5f));
                            asdasd(scaleChange, gesture.Position.ToPoint());
                        }
                        break;
                }
            }
        }
    }
}
