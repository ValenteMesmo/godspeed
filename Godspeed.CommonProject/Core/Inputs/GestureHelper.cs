using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;
using System;

namespace Godspeed
{
    class GestureHelper
    {
        public static void HandleTouchInput(Action<float, Point> asdasd, Action Nope)
        {
            while (TouchPanel.IsGestureAvailable)
            {
                GestureSample gesture = TouchPanel.ReadGesture();

                if (gesture.GestureType == GestureType.Pinch)
                {
                    Vector2 a = gesture.Position;
                    Vector2 aOld = gesture.Position - gesture.Delta;
                    Vector2 b = gesture.Position2;
                    Vector2 bOld = gesture.Position2 - gesture.Delta2;

                    float d = Vector2.Distance(a, b);
                    float dOld = Vector2.Distance(aOld, bOld);

                    float scaleChange = (d - dOld) * .01f;
                    //var point = new Point((int)((a.X + b.X)*0.5f), (int)((a.Y + b.Y) * 0.5f));

                    asdasd(scaleChange, new Vector2((a.X + b.X) * .5f, (a.Y + b.Y) * .5f).ToPoint());
                    continue;
                }
            }

            Nope();
        }
    }
}
