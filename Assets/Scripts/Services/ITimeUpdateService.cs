using Zenject;

namespace Core.Services
{
    public interface ITimeUpdateService
    {
        void RegisterFixedUpdate(IFixedTickable obj);
        void RegisterUpdate(ITickable obj);
        void UnregisterFixedUpdate(IFixedTickable obj);
        void UnregisterUpdate(ITickable obj);
    }
}