using System;
using Core.Units.Enemy;

namespace Core.Weapons
{
    public interface IWeapon
    {
        event Action<IEnemy> Hit;
        void Shoot();
    }
}