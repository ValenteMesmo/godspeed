using System.Collections.Generic;
using Microsoft.Xna.Framework;

namespace Monogame.Common
{
    public abstract class TouchInputs
    {
        public abstract IEnumerable<Vector2> GetTouches();
    }
}
