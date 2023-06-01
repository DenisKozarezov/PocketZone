using UnityEngine;
using Core.UI;
using Zenject;

namespace Core.Services.Input
{
    public sealed class JoystickInput : IInputService, IInitializable, ILateDisposable
    {
        private readonly PlayerControls _playerControls;
        private readonly JoystickHandler _joystick;
        public bool IsMoving => _joystick.IsDragging;
        public Vector2 Direction => _joystick.Direction;

        public JoystickInput(JoystickHandler joystick)
        {
            _playerControls = new PlayerControls();
            _joystick = joystick;
        }

        public void Disable() => _playerControls.Disable();
        public void Enable() => _playerControls.Enable();

        public void Initialize() => Enable();
        public void LateDispose() => Disable();
    }
}