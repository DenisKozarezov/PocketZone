using System;
using Core.Factories;
using Core.Units;

namespace Core.Weapons
{
    public class BulletGun : IWeapon
    {
        private readonly BulletGunModel _model;
        private readonly IBulletFactory _bulletFactory;
        //private readonly Explosion.Factory _explosionFactory;

        public event Action<IUnit> Hit;

        public BulletGun(BulletGunModel model, IBulletFactory bulletFactory/*, Explosion.Factory explosionFactory*/)
        {
            _model = model;
            _bulletFactory = bulletFactory;
            //_explosionFactory = explosionFactory;
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
        }

        //private void CreateExplosion(Vector2 position)
        //{
        //    Explosion explosion = _explosionFactory.Create();
        //    explosion.transform.position = position;
        //    explosion.Invoke(nameof(explosion.Dispose), 1.5f);
        //}
        private void OnDisposed(Bullet bullet)
        {
            bullet.LifetimeElapsed -= bullet.Dispose;
            bullet.Hit -= OnBulletHit;
            bullet.Disposed -= OnDisposed;
        }
        private void OnBulletHit(Bullet bullet, IUnit unit)
        {
            //CreateExplosion(bullet.transform.position);
            bullet.Dispose();
            Hit?.Invoke(unit);
        }
    }
}