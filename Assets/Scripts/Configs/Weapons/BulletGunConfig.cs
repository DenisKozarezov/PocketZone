using UnityEngine;

namespace Core.Models
{
    [CreateAssetMenu(menuName = "Configuration/Weapons/Bullet Gun")]
    public class BulletGunConfig : WeaponConfig
    {
        [field: SerializeField, Min(0f)] public float ReloadTime { get; private set; }
        [field: SerializeField, Min(0)] public int Ammo { get; private set; }

        [field: Header("Bullets")]
        [field: SerializeField, Min(0f)] public float BulletForce { get; private set; }
        [field: SerializeField, Min(0f)] public float BulletLifetime { get; private set; }
        [field: SerializeField, Min(0)] public int BulletDamage { get; private set; }
        [field: SerializeField, Min(0f)] public float BulletSpawnInterval { get; private set; }
    }
}