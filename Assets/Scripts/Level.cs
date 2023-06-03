using UnityEngine;
using Cinemachine;
using Zenject;
using Core.Factories;
using Core.Units.Player;
using Core.Units.Enemy;

namespace Core
{
    public class Level : IInitializable
    {
        private readonly IPlayerFactory _playerFactory;
        private readonly IEnemyFactory _enemyFactory;
        private readonly ICinemachineCamera _camera;
        public Level(IPlayerFactory playerFactory, IEnemyFactory enemyFactory, ICinemachineCamera camera) 
        { 
            _playerFactory = playerFactory;
            _enemyFactory = enemyFactory;
            _camera = camera;
        }
        public void Initialize()
        {
            SpawnPlayer();
            SpawnEnemies();
        }
        private void SpawnPlayer()
        {
            PlayerController player = _playerFactory.Create(Vector3.zero);
            player.Enable();
            _camera.Follow = player.Transformable.Transform;
        }
        private void SpawnEnemies()
        {
            for (int i = 0; i < 3; i++)
            {
                Vector2 position = Random.insideUnitCircle * 5f;
                _enemyFactory.Spawn(position);
            }
        }
    }
}