using System;
using UnityEngine;
using Zenject;

namespace Core.Units.Enemy
{
    public class EnemyController : IEnemy, IPoolable<Vector2>, ITickable, IDisposable
    {
        private readonly EnemyModel _model;
        private readonly EnemyView _view;
        private readonly EnemyStateMachine _stateMachine;

        public IUnit Target { get; private set; }
        public bool Dead => _model.Dead;
        public bool IsTaunted => Target != null;
        public ITransformable Transformable => _view;
        public event Action<EnemyController> Disposed;
        public event Action Died
        {
            add { _model.Died += value; }
            remove { _model.Died -= value; }
        }

        public EnemyController(EnemyModel enemyModel, EnemyView enemyView)
        {
            _model = enemyModel;
            _view = enemyView;
            _stateMachine = new EnemyStateMachine(this, enemyModel);
        }

        public void Attack() { }
        public void Hit(int damage) => _model.Hit(damage);
        public void Taunt(IUnit unit)
        {
            Target = unit;
            _stateMachine.SwitchState<EnemyAttackState>();
        }
        public void Dispose()
        {
            Disposed?.Invoke(this);
        }
        public void Tick()
        {
            if (_model.Dead) 
                return;

            _stateMachine.CurrentState.Update();
        }

        void IPoolable<Vector2>.OnDespawned()
        {
            _model.Died -= Dispose;
            _view.SetActive(false);
            Target = null;
        }
        void IPoolable<Vector2>.OnSpawned(Vector2 position)
        {
            _model.Reset();
            _model.Died += Dispose;
            _view.SetPosition(position);
            _view.SetActive(true);
            _stateMachine.SwitchState<EnemyPatrolState>();
        }
    }
}