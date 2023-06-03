using Core.Models.Units;

namespace Core.Units.Enemy
{
    public class EnemyModel : UnitModel
    {
        public readonly float AttackCooldown;
        public readonly float AggressionRadius;
        public readonly float PatrolRadius;

        public EnemyModel(EnemyConfig config) : base(config)
        {
            AttackCooldown = config.AttackCooldown;
            AggressionRadius = config.AggressionRadius;
            PatrolRadius = config.PatrolRadius;
        }  
    }
}
