using System.Collections.Generic;
using UnityEngine;
using Core.Models.Items;
using System;

namespace Core.Models.Units
{
    [Serializable]
    public struct ItemReward
    {
        public ItemConfig Item;
        [Range(0f, 1f)] public float Probability;
    }

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