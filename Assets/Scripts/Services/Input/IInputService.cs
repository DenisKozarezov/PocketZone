using UnityEngine;

namespace Core.Services.Input
{
    public interface IInputService
    {
        bool IsMoving { get; }
        Vector2 Direction { get; }        
        void Enable();
        void Disable();
    }
}