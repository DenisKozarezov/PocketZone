using System;
using UnityEngine;

namespace Core.Services.Input
{
    public interface IInputService
    {
        bool IsMoving { get; }
        Vector2 Direction { get; }
        Vector2 DeltaPosition { get; }
        void Enable();
        void Disable();
        event Action Back;
        event Action Jump;
    }
}