using System;
using Core.Services.Serialization;

namespace Core.Weapons
{
    public interface IWeapon : ISerializableObject
    {
        int ID { get; }
        int CurrentAmmo { get; }
        int MaxAmmo { get; }
        event Action<int, int> AmmoChanged;
        void Reload();
        void Shoot();
    }
}