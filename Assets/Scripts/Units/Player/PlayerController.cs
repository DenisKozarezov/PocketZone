using System;
using UnityEngine;
using Zenject;

namespace Core.Units.Player
{
    public sealed class PlayerController : IUnit, ITickable, IDisposable
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
            _view.Translate(inputDirection, _model.Config.Velocity);

            float horizontal = inputDirection.x;
            if (horizontal != 0f && _view.Rotation.eulerAngles.x != horizontal)
            {
                _view.SetDirection(horizontal >= 0f);
            }
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
            //_view.SetRunningAnimation(_model.InputService.IsMoving, _model.InputService.Direction);
        }
        public void Dispose()
        {
            Disable();
        }
        public void Hit(int damage)
        {
            throw new NotImplementedException();
        }
    }
}