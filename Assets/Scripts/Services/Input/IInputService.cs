using System;
using UnityEngine;

namespace Core.Services.Input
{
    public interface IInputService
    {
        bool IsMoving { get; }
        Vector2 Direction { get; }
        event Action Fire;
        void Enable();
        void Disable();
    }
}