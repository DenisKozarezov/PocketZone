using UnityEngine;
using Zenject;
using Core.Units.Player;
using Core.Models;
using Core.UI;
using Core.Weapons;
using Core.Services;
using Core.Units;

namespace Core.Factories
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly DiContainer _container;
        private readonly PlayerConfig _config;
        private readonly HealthBarManager _healthBarManager;
        private readonly TimeUpdateService _timeUpdater;

        public PlayerFactory(
            DiContainer container, 
            PlayerConfig config, 
            HealthBarManager healthBarManager,
            TimeUpdateService timeUpdate)
        {
            _container = container;
            _config = config;
            _healthBarManager = healthBarManager;
            _timeUpdater = timeUpdate;
        }
        public PlayerController Create(Vector3 position)
        {
            PlayerView view = _container.InstantiatePrefabForComponent<PlayerView>(_config.Prefab, position, Quaternion.identity, null);
            PlayerModel model = _container.Instantiate<PlayerModel>(new object[] { _config });
            PlayerController controller = new PlayerController(model, view);
            controller.Transformable.SetPosition(position);

            view.GetComponent<DamageReceiver>().Init(controller);

            BulletGunModel bulletGunModel = new BulletGunModel(_config.PrimaryWeapon, view.FirePoint);
            BulletGun bulletGun = _container.Instantiate<BulletGun>(new object[] { bulletGunModel });
            controller.SetPrimaryWeapon(bulletGun);

            _timeUpdater.RegisterFixedUpdate(controller);

            _container.BindInstance(controller);

            _healthBarManager.CreateHealthBar(model, controller);
            return controller;
        }
    }
}
