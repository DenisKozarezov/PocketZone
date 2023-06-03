using System;
using UnityEngine;
using Zenject;

namespace Core.Units.Player
{
    public sealed class PlayerController : IUnit, IFixedTickable
    {
        private readonly PlayerModel _model;
        private readonly PlayerView _view;

        public bool Dead => _model.Dead;
        public event Action Died
        {
            add { _model.Died += value; }
            remove { _model.Died -= value; }
        }
        public ITransformable Transformable => _view;

        public PlayerController(PlayerModel model, PlayerView view)
        {
            _model = model;
            _view = view;
        }
        private void ProcessMovementInput(Vector2 inputDirection)
        {
            _view.Translate(inputDirection, _model.Config.Velocity);

            float horizontal = inputDirection.x;
            if (horizontal == 0f)
                return;

            if (_view.Rotation.eulerAngles.x != horizontal)
            {
                _view.SetDirection(horizontal > 0f);
            }            

            float angle = Mathf.Atan2(inputDirection.y, inputDirection.x) * Mathf.Rad2Deg;
            _view.RotateWeapon(angle);
        }
        public void Enable()
        {
            _model.InputService.Enable();
        }
        public void Disable()
        {
            _model.InputService.Disable();
        }
        public void FixedTick()
        {
            if (_model.Dead)
                return;

            ProcessMovementInput(_model.InputService.Direction);
        }
        public void Hit(int damage) => _model.Hit(damage);
    }
}