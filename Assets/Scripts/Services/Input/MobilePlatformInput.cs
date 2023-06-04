using System;
using UnityEngine;
using UnityEngine.UI;
using Core.UI;

namespace Core.Services.Input
{
    public sealed class MobilePlatformInput : MonoBehaviour, IInputService
    {
        [SerializeField]
        private JoystickHandler _joystick;
        [SerializeField]
        private Button _fireButton;

        public bool IsMoving => _joystick.IsDragging;
        public Vector2 Direction => _joystick.Direction;
        public event Action Fire;

        private void Start() => Enable();
        private void OnDisable() => Disable();
        private void OnFire() => Fire?.Invoke();

        public void Enable()
        {
            _fireButton.onClick.AddListener(OnFire);
        }
        public void Disable()
        {
            _fireButton.onClick.RemoveListener(OnFire);
        }
    }
}