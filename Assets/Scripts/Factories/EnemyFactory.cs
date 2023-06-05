using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Core.Units.Enemy;
using Core.UI;
using Core.Services;
using Core.Models.Units;
using Core.Units.Player;
using Core.Units;

namespace Core.Factories
{
    public class EnemyFactory : IEnemyFactory, ITickable
    {
        private readonly DiContainer _container;
        private readonly EnemyConfig _config;
        private readonly HealthBarManager _healthBarManager;
        private readonly TimeUpdateService _timeUpdater;
        private readonly LazyInject<PlayerController> _player;

        private readonly LinkedList<EnemyController> _enemies = new();

        public EnemyFactory(
            DiContainer container,
            EnemyConfig config,
            HealthBarManager healthBarManager,
            TimeUpdateService timeUpdate,
            LazyInject<PlayerController> player)
        {
            _container = container;
            _config = config;
            _healthBarManager = healthBarManager;
            _timeUpdater = timeUpdate;
            _player = player;
            _timeUpdater.RegisterUpdate(this);
        }
        private bool HasNearbyEnemy(ITransformable patrollingEnemy, float distance)
        {
            return (patrollingEnemy.Position - _player.Value.Transformable.Position).sqrMagnitude <= distance * distance;
        }
        private void OnEnemyDisposed(EnemyController enemy)
        {
            _timeUpdater.UnregisterUpdate(enemy);
            _timeUpdater.UnregisterFixedUpdate(enemy);
            enemy.Disposed -= OnEnemyDisposed;
            _enemies.Remove(enemy);
        }
        public EnemyController Spawn(Vector2 position)
        {
            EnemyView view = _container.InstantiatePrefabForComponent<EnemyView>(_config.Prefab);
            EnemyModel model = new EnemyModel(_config);
            EnemyController controller = new EnemyController(model, view);
            controller.Disposed += OnEnemyDisposed;

            view.GetComponent<DamageReceiver>().Init(controller);

            controller.OnSpawned(position);

            _timeUpdater.RegisterUpdate(controller);
            _timeUpdater.RegisterFixedUpdate(controller);

            _healthBarManager.CreateHealthBar(model, controller);

            _enemies.AddLast(controller);

            return controller;
        }
        public void Tick()
        {
            foreach (EnemyController enemy in _enemies)
            {
                if (!enemy.IsTaunted && HasNearbyEnemy(enemy.Transformable, _config.AggressionRadius))
                {
                    enemy.Taunt(_player.Value);
                }
            }
        }
    }
}