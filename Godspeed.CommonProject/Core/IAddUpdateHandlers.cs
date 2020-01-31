using System;

namespace Godspeed
{
    public interface IAddUpdateHandlers
    {
        void AddUpdateHandler(Action<GameObject> updateHandler);
    }
}
