using System;

namespace Core.Units
{
    public abstract class UnitModel
    {
        public readonly int MaxHealth;
        public int Health;
        public bool Dead => Health == 0;

        public event Action<int, int> HealthChanged;
        public event Action Died;

        public UnitModel(int health)
        {
            Health = health;
            MaxHealth = health;
        }

        public void Hit(int damage)
        {
            if (Dead)
                return;

            if (Health - damage > 0)
            {
                Health -= damage;
                HealthChanged?.Invoke(Health, MaxHealth);
            }
            else
            {
                Health = 0;
                Died?.Invoke();
            }
        }
        public void Heal(int value)
        {
            Health = Math.Clamp(Health + value, 0, MaxHealth);
            HealthChanged?.Invoke(Health, MaxHealth);
        }
        public void Reset()
        {
            Health = MaxHealth;
        }
    }
}