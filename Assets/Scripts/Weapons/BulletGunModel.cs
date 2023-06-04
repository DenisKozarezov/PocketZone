using UnityEngine;
using Core.Models;

namespace Core.Weapons
{
    public class BulletGunModel
    {
        public readonly BulletGunConfig Config;
        public readonly Transform FirePoint;
        public readonly Cooldown Cooldown;
        public int CurrentAmmo;
        public bool OutOfAmmo => CurrentAmmo == 0;

        public BulletGunModel(BulletGunConfig config, Transform firePoint)
        {
            Config = config;
            CurrentAmmo = config.Ammo;
            FirePoint = firePoint;
            Cooldown = new Cooldown();
        }
        public void Shoot()
        {
            CurrentAmmo = System.Math.Clamp(CurrentAmmo - 1, 0, Config.Ammo);
        }
        public void Reload()
        {
            CurrentAmmo = Config.Ammo;
        }
    }
}