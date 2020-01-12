using System.Collections.Generic;

namespace Monogame.Common
{
    public abstract class GameObject
    {
        public int X { get; set; }
        public int Y { get; set; }
        public bool Destroyed { get; private set; }

        public readonly List<Drawable> Sprites = new List<Drawable>();

        protected Behavior Behavior { get; set; }

        public void Update()
        {
            if (Behavior != null)
                Behavior.Update();
        }

        public void Destroy()
        {
            Destroyed = true;
        }

#if DEBUG
        public override string ToString()
        {
            return GetType().Name;
        }
#endif
    }
}
