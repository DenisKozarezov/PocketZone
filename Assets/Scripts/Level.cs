using System;
using UnityEngine;
using Cinemachine;
using Zenject;
using Core.Factories;
using Core.Units.Player;
using Core.Services.Serialization;

namespace Core
{
    public class Level : IInitializable, IDisposable
    {
        private readonly IPlayerFactory _playerFactory;
        private readonly IEnemyFactory _enemyFactory;
        private readonly ICinemachineCamera _camera;
        private readonly GameState _gameState;

        public Level(
            IPlayerFactory playerFactory, 
            IEnemyFactory enemyFactory, 
            ICinemachineCamera camera, 
            GameState gameState) 
        { 
            _playerFactory = playerFactory;
            _enemyFactory = enemyFactory;
            _camera = camera;
            _gameState = gameState;
        }
        public void Initialize()
        {
            if (GameState.IsLoadingGame && _gameState.HasAnySave())
                LoadGame();
            else
                CreateNewGame();
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
                Vector2 position = UnityEngine.Random.insideUnitCircle * 7f;
                _enemyFactory.Spawn(position);
            }
        }
        private void CreateNewGame()
        {
            SpawnPlayer();
            SpawnEnemies();
        }
        public void LoadGame()
        {
            SpawnPlayer();
            SpawnEnemies();
            _gameState.Deserialize();
            GameState.IsLoadingGame = false;
        }
        public void Dispose()
        {
            _gameState.Serialize();
        }
    }
}