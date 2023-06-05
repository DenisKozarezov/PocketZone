using UnityEngine;
using Core.Models.Units;

namespace Core.Models
{
    [CreateAssetMenu(menuName = "Configuration/Units/Player Config")]
    public sealed class PlayerConfig : UnitConfig
    {
        [field: Header("Player Characteristics")]
        [field: SerializeField] public BulletGunConfig PrimaryWeapon { get; set; }
    }
}
