﻿using UnityEngine;
using Zenject;
using Core.Units;
using Core.Units.Player;
using Core.Models;
using Core.UI;
using Core.Weapons;
using Core.Services;

namespace Core.Factories
{
    public class PlayerFactory : IPlayerFactory
    {
        private readonly DiContainer _container;
        private readonly PlayerConfig _config;
        private readonly HealthBarManager _healthBarManager;
        private readonly ITimeUpdateService _timeUpdater;
        public PlayerFactory(
            DiContainer container, 
            PlayerConfig config, 
            HealthBarManager healthBarManager,
            ITimeUpdateService timeUpdate)
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
            
            controller.Died += controller.Disable;
            controller.Transformable.SetPosition(position);

            view.GetComponent<DamageReceiver>().Init(controller);

            BulletGunModel bulletGunModel = new BulletGunModel(_config.PrimaryWeapon, view.FirePoint);
            BulletGun bulletGun = _container.Instantiate<BulletGun>(new object[] { bulletGunModel });
            controller.SetPrimaryWeapon(bulletGun);

            _timeUpdater.RegisterFixedUpdate(controller);

            _container.BindInterfacesAndSelfTo<PlayerController>().FromInstance(controller);

            _healthBarManager.CreateHealthBar(model, controller);
            return controller;
        }
    }
}
