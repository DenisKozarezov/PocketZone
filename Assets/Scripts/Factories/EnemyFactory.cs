using System;
using System.Collections.Generic;
using UnityEngine;
using Zenject;
using Core.Models;
using Core.Units.Enemy;
using Core.UI;

namespace Core.Factories
{
    public class EnemyFactory : IEnemyFactory, IDisposable
    {
        private readonly DiContainer _container;
        private readonly EnemySettings _enemySettings;
        private readonly Queue<EnemyController> _pool;
        private readonly HealthBarManager _healthBarManager;
        private readonly TimeUpdateService _timeUpdate;
        public bool Empty => _pool.Count == 0;

        public EnemyFactory(
            DiContainer container,
            EnemySettings enemySettings, 
            HealthBarManager healthBarManager,
            TimeUpdateService timeUpdate)
        {
            _container = container;
            _enemySettings = enemySettings;
            _pool = new Queue<EnemyController>(enemySettings.EnemiesLimit);
            _healthBarManager = healthBarManager;
            _timeUpdate = timeUpdate;

            for (int i = 0; i < enemySettings.EnemiesLimit; i++) Create();
        }
        private EnemyController Create()
        {
            EnemyView view = _container.InstantiatePrefabForComponent<EnemyView>(_enemySettings.EnemyConfig.Prefab);
            EnemyModel model = new EnemyModel(
                _enemySettings.EnemyConfig.ReloadTime,
                _enemySettings.EnemyConfig.Velocity,
                _enemySettings.EnemyConfig.AggressionRadius,
                _enemySettings.EnemyConfig.PatrolRadius,
                _enemySettings.EnemyConfig.Health);
            EnemyController controller = new EnemyController(model, view);

            _timeUpdate.RegisterUpdate(controller);

            _healthBarManager.CreateHealthBar(model, controller);

            view.SetActive(false);

            _pool.Enqueue(controller);
            return controller;
        }
        public EnemyController Spawn(Vector2 position)
        {
            if (_pool.TryDequeue(out EnemyController enemy))
            {
                (enemy as IPoolable<Vector2>).OnSpawned(position);
                return enemy;
            }
            else
                return Create();
        }
        public void Despawn(EnemyController enemy)
        {
            (enemy as IPoolable<Vector2>).OnDespawned();
            _pool.Enqueue(enemy);
        }
        public void Dispose()
        {
            while (_pool.Count > 0)
            {
                EnemyController enemy = _pool.Dequeue();
                enemy.Dispose();
            }
        }
    }
}