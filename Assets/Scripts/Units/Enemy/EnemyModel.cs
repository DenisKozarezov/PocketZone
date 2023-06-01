using System;

namespace Core.Units.Enemy
{
    public class EnemyModel
    {
        private readonly int _maxHealth;

        public readonly float ReloadTime;
        public readonly float Velocity;
        public readonly float AggressionRadius;
        public readonly float PatrolRadius;
        public int Health;
        public bool IsDead => Health == 0;
        public event Action Died;

        public EnemyModel(
            float reloadTime,
            float velocity,
            float aggressionRadius,
            float patrolRadius,
            int maxHealth)
        {
            ReloadTime = reloadTime;
            Velocity = velocity;
            AggressionRadius = aggressionRadius;
            PatrolRadius = patrolRadius;
            Health = maxHealth;
            _maxHealth = maxHealth;
        }

        public void Hit()
        {
            Health = Math.Max(Health - 1, 0);
            if (IsDead) Died?.Invoke();
        }
        public void Reset()
        {
            Health = _maxHealth;
        }
    }
}
