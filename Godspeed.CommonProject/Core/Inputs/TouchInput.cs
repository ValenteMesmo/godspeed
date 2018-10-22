using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input.Touch;

namespace Godspeed
{
    public class TouchInput
    {
        public MemberValue<Point> Position { get; private set; } = new MemberValue<Point>(Point.Zero);
        public MemberValue<bool> touching { get; private set; } = new MemberValue<bool>(false);
        public void Update()
        {
            var touch = TouchPanel.GetState();
            if (touch.Count == 1)
            {
                Position.SetValue(touch[0].Position.ToPoint());
                touching.SetValue(true);
            }
            else
                touching.SetValue(false);

        }
    }
}
