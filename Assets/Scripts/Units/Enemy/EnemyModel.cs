namespace Core.Units.Enemy
{
    public class EnemyModel : UnitModel
    {
        public readonly float ReloadTime;
        public readonly float Velocity;
        public readonly float AggressionRadius;
        public readonly float PatrolRadius;

        public EnemyModel(
            float reloadTime,
            float velocity,
            float aggressionRadius,
            float patrolRadius,
            int maxHealth) : base(maxHealth)
        {
            ReloadTime = reloadTime;
            Velocity = velocity;
            AggressionRadius = aggressionRadius;
            PatrolRadius = patrolRadius;
        }  
    }
}
