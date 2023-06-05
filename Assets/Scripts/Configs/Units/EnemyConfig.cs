using System.Collections.Generic;
using UnityEngine;
using Core.Models.Items;

namespace Core.Models.Units
{
    [CreateAssetMenu(menuName = "Configuration/Units/Enemy Config")]
    public class EnemyConfig : UnitConfig
    {
        [field: Header("Enemy Characteristics")]
        [field: SerializeField, Min(0f)] public float AttackCooldown { get; private set; }
        [field: SerializeField, Min(0f)] public float AggressionRadius { get; private set; }
        [field: SerializeField, Min(0f)] public float PatrolRadius { get; private set; }
        [field: Space, SerializeField] public List<ItemReward> Reward { get; private set; }
    }
}