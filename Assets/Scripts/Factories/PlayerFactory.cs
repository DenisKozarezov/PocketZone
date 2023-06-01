using System;
using UnityEngine;
using Zenject;
using Core.Units.Player;
using Core.Models;

namespace Core.Factories
{
    public class PlayerFactory : IPlayerFactory, ITickable, IDisposable
    {
        private readonly DiContainer _container;
        private readonly PlayerConfig _config;
        private PlayerController _playerController;

        public PlayerFactory(DiContainer container, PlayerConfig config)
        {
            _container = container;
            _config = config;
        }
        public PlayerController Create(Vector3 position)
        {
            GameObject obj = _container.InstantiatePrefab(_config.Prefab, position, Quaternion.identity, null);
            PlayerView view = obj.GetComponent<PlayerView>();
            PlayerModel model = _container.Instantiate<PlayerModel>(new object[] { _config });
            
            PlayerController controller = new PlayerController(model, view);
            controller.Transformable.SetPosition(position);
            _playerController = controller;

            return controller;
        }

        public void Dispose()
        {
            _playerController?.Dispose();
        }
        public void Tick()
        {
            _playerController?.Tick();
        }
    }
}
