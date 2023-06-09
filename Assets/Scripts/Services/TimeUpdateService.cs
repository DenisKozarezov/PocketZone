using System.Collections.Generic;
using Zenject;

namespace Core.Services
{
    public class TimeUpdateService : ITimeUpdateService, IFixedTickable, ITickable
    {
        private readonly LinkedList<IFixedTickable> _fixedUpdates = new();
        private readonly LinkedList<ITickable> _updates = new();

        public void RegisterFixedUpdate(IFixedTickable obj)
        {
            _fixedUpdates.AddLast(obj);
        }
        public void RegisterUpdate(ITickable obj)
        {
            _updates.AddLast(obj);
        }
        public void UnregisterFixedUpdate(IFixedTickable obj)
        {
            _fixedUpdates.Remove(obj);
        }
        public void UnregisterUpdate(ITickable obj)
        {
            _updates.Remove(obj);
        }
        void IFixedTickable.FixedTick()
        {
            foreach (var obj in _fixedUpdates)
                obj.FixedTick();
        }
        void ITickable.Tick()
        {
            foreach (var obj in _updates)
                obj.Tick();
        }
    }
}