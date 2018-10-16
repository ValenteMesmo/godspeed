using Microsoft.Xna.Framework.Input;

namespace Godspeed
{
    public class KeyboardInput
    {
        public MemberValue<bool> ControlPressed { get; private set; } = new MemberValue<bool>(false);

        public void Update()
        {
            var keyboardState = Keyboard.GetState();
            ControlPressed.SetValue(
                keyboardState.IsKeyDown(Keys.LeftControl)
                || keyboardState.IsKeyDown(Keys.RightControl)
            );
        }
    }
}
