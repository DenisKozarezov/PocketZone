using System;
using UnityEngine;
using Zenject;

namespace Core.Match.Player
{
    public sealed class PlayerController : ITickable, IDisposable
    {
        private readonly PlayerModel _model;
        private readonly PlayerView _view;
        private bool _enabled = true;

        public ITransformable Transformable => _view;

        public PlayerController(PlayerModel model, PlayerView view)
        {
            _model = model;
            _view = view;
            Enable();
        }
        private void ProcessMovementInput(Vector2 inputDirection)
        {
            if (inputDirection == Vector2.zero)
                return;

            Vector3 worldDirection = _view.CalculateWorldDirection(inputDirection);

            _view.Translate(inputDirection, _model.Config.MovementSpeed);

            Vector3 localDirection = _view.Position + worldDirection;
            localDirection.y = _view.Position.y;

            Quaternion toRotation = Quaternion.LookRotation(localDirection - _view.Position, Vector3.up);
            Quaternion rotation = Quaternion.Slerp(_view.Rotation, toRotation, Time.deltaTime / _model.Config.RotationSpeed);

            _view.Rotate(rotation);
        }
        public void Enable()
        {
            _enabled = true;
        }
        public void Disable()
        {
            _enabled = false;
        }
        public void Tick()
        {
            ProcessMovementInput(_model.InputService.Direction);
            _view.SetRunningAnimation(_model.InputService.IsMoving, _model.InputService.Direction);
        }
        public void Dispose()
        {
            Disable();
        }
    }
}