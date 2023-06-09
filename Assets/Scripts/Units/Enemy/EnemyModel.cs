﻿using Core.Models.Units;

namespace Core.Units.Enemy
{
    public class EnemyModel : UnitModel
    {
        public readonly int Damage;
        public readonly float AttackRadius;
        public readonly float AttackCooldown;
        public readonly float AggressionRadius;
        public readonly float PatrolRadius;

        public EnemyModel(EnemyConfig config) : base(config)
        {
            Damage = config.AttackDamage;
            AttackRadius = config.AttackRange;
            AttackCooldown = config.AttackCooldown;
            AggressionRadius = config.AggressionRadius;
            PatrolRadius = config.PatrolRadius;
        }  
    }
}
