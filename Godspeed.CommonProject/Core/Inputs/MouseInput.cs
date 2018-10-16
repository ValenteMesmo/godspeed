using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;

namespace Godspeed
{
    public class MouseInput
    {
        public MemberValue<Point> Position { get; private set; } = new MemberValue<Point>(Point.Zero);
        public MemberValue<int> Scroll { get; private set; } = new MemberValue<int>(0);
        public MemberValue<bool> LeftButtonPressed { get; private set; } = new MemberValue<bool>(false);

        public void Update()
        {
            var mouseState = Mouse.GetState();
            Scroll.SetValue(mouseState.ScrollWheelValue);
            Position.SetValue(mouseState.Position);
            LeftButtonPressed .SetValue(mouseState.LeftButton == ButtonState.Pressed);
        }
    }
}
