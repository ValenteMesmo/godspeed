using Microsoft.Xna.Framework;
using System;
using System.Collections.Generic;

namespace Godspeed
{
    public class GameObject : IAddUpdateHandlers
    {
        private List<Action<GameObject>> Updates = new List<Action<GameObject>>();
        protected Vector2 position;

        public virtual void SetPosition(Vector2 value)
        {
            position = value;
        }

        public virtual Vector2 GetPosition()
        {
            return position;
        }

        public void AddUpdateHandler(Action<GameObject> updateHandler)
        {
            Updates.Add(updateHandler);
        }

        public void Update()
        {
            foreach (var update in Updates)
                update(this);
        }
    }

    public interface IAddUpdateHandlers
    {
        void AddUpdateHandler(Action<GameObject> updateHandler);
    }
}
