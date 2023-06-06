using System;
using UnityEngine;
using Core.Factories;
using Core.Units;

namespace Core.Weapons
{
    public class BulletGun : IWeapon
    {
        private readonly BulletGunModel _model;
        private readonly IBulletFactory _bulletFactory;
        private readonly DamageVFXFactory _vfxFactory;

        public event Action<int, int> AmmoChanged;

        public BulletGun(
            BulletGunModel model, 
            IBulletFactory bulletFactory, 
            DamageVFXFactory vfxFactory)
        {
            _model = model;
            _bulletFactory = bulletFactory;
            _vfxFactory = vfxFactory;
        }
        public void Reload()
        {
            _model.Reload();
        }
        public void Shoot()
        {
            if (!_model.Cooldown.IsOver || _model.OutOfAmmo) 
                return;

            Bullet bullet = _bulletFactory.Create
            (
                _model.FirePoint.position,
                _model.FirePoint.rotation,
                _model.Config.BulletForce,
                _model.Config.BulletLifetime
            );

            bullet.LifetimeElapsed += bullet.Dispose;
            bullet.Hit += OnBulletHit;
            bullet.Disposed += OnDisposed;

            _model.Shoot();
            _model.Cooldown.Run(_model.Config.BulletSpawnInterval);

            AmmoChanged?.Invoke(_model.CurrentAmmo, _model.Config.Ammo);
        }

        private void CreateDamageVFX(Vector2 position, Vector2 direction)
        {
            string text = $"-{_model.Config.BulletDamage}";
            _vfxFactory.Create(
                position,
                direction,
                text,
                1.5f);
        }
        private void OnDisposed(Bullet bullet)
        {
            bullet.LifetimeElapsed -= bullet.Dispose;
            bullet.Hit -= OnBulletHit;
            bullet.Disposed -= OnDisposed;
        }
        private void OnBulletHit(Bullet bullet, IUnit unit)
        {
            unit.Hit(_model.Config.BulletDamage);
            CreateDamageVFX(bullet.transform.position, bullet.transform.up);
            bullet.Dispose();
        }
    }
}