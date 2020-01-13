using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Input.Touch;

namespace Monogame.Common
{
    public class TouchController : TouchInputs
    {        
        private List<Vector2> touches = new List<Vector2>();

        public override IEnumerable<Vector2> GetTouches() => touches;

        public void Update(Camera camera)
        {
            touches.Clear();

            var touchCollection = TouchPanel.GetState();
            foreach (var touch in touchCollection)
                if (touch.State == TouchLocationState.Pressed)
                    touches.Add(camera.GetWorldPosition(touch.Position));

            var mouse = Mouse.GetState();
            if (mouse.LeftButton == ButtonState.Pressed)
                touches.Add(camera.GetWorldPosition(mouse.Position.ToVector2()));
        }
    }
}
