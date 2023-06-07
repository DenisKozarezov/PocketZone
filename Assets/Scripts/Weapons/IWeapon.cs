using System;

namespace Core.Weapons
{
    public interface IWeapon
    {
        int ID { get; }
        int CurrentAmmo { get; }
        int MaxAmmo { get; }
        event Action<int, int> AmmoChanged;
        void Reload();
        void Shoot();
    }
}