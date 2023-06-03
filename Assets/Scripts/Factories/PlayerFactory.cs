using UnityEngine;
using Zenject;
using Core.Units.Player;
using Core.Models;
using Core.UI;

namespace Core.Factories
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly DiContainer _container;
        private readonly PlayerConfig _config;
        private readonly HealthBarManager _healthBarManager;
        private readonly TimeUpdateService _timeUpdate;

        public PlayerFactory(
            DiContainer container, 
            PlayerConfig config, 
            HealthBarManager healthBarManager,
            TimeUpdateService timeUpdate)
        {
            _container = container;
            _config = config;
            _healthBarManager = healthBarManager;
            _timeUpdate = timeUpdate;
        }
        public PlayerController Create(Vector3 position)
        {
            GameObject obj = _container.InstantiatePrefab(_config.Prefab, position, Quaternion.identity, null);
            PlayerView view = obj.GetComponent<PlayerView>();
            PlayerModel model = _container.Instantiate<PlayerModel>(new object[] { _config });

            PlayerController controller = new PlayerController(model, view);
            controller.Transformable.SetPosition(position);

            _timeUpdate.RegisterFixedUpdate(controller);

            _healthBarManager.CreateHealthBar(model, controller);
            return controller;
        }
    }
}
