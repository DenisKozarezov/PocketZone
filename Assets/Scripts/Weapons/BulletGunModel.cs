using UnityEngine;
using Core.Models;

namespace Core.Weapons
{
    public class BulletGunModel
    {
        public readonly BulletGunConfig BulletGunConfig;
        public readonly Transform FirePoint;
        public readonly Cooldown Cooldown;
        public readonly float ReloadTime;
        public int CurrentAmmo;
        public bool OutOfAmmo => CurrentAmmo == 0;

        public BulletGunModel(BulletGunConfig config, Transform firePoint)
        {
            BulletGunConfig = config;
            ReloadTime = config.ReloadTime;
            CurrentAmmo = config.Ammo;
            FirePoint = firePoint;
            Cooldown = new Cooldown();
        }
        public void Shoot()
        {
            CurrentAmmo = System.Math.Clamp(CurrentAmmo - 1, 0, BulletGunConfig.Ammo);
        }
    }
}