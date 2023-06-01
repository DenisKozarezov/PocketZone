using System;
using UnityEngine;
using Zenject;

namespace Core.Services.Input
{
    public sealed class StandaloneInput : IInputService, IInitializable, ILateDisposable
    {
        private readonly PlayerControls _playerControls;

        public bool IsMoving => Direction.sqrMagnitude > 0f;
        public Vector2 Direction => _playerControls.Player.Movement.ReadValue<Vector2>();
        public Vector2 DeltaPosition => _playerControls.Player.MouseDelta.ReadValue<Vector2>();
        public event Action Back;
        public event Action Jump;

        public StandaloneInput()
        {
            _playerControls = new PlayerControls();
            _playerControls.Player.Back.performed += _ => Back?.Invoke();
        }

        public void Disable() => _playerControls.Disable();
        public void Enable() => _playerControls.Enable();

        public void Initialize() => Enable();
        public void LateDispose() => Disable();
    }
}