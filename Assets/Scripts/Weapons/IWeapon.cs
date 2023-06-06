using System;

namespace Core.Weapons
{
    public interface IWeapon
    {
        event Action<int, int> AmmoChanged;
        void Reload();
        void Shoot();
    }
}