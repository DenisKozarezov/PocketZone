using System;
using Core.Models.Units;
using Core.Services.Serialization;
using Newtonsoft.Json.Linq;

namespace Core.Units
{
    public abstract class UnitModel : ISerializableObject
    {
        private int _health;
        private int _maxHealth;

        public readonly float Velocity;
        public int Health
        {
            get => _health;
            private set
            {
                _health = Math.Clamp(value, 0, _maxHealth);
                HealthChanged?.Invoke(_health, _maxHealth);
            }
        }
        public int MaxHealth
        {
            get => _maxHealth;
            private set
            {
                _maxHealth = value;
                HealthChanged?.Invoke(_health, _maxHealth);
            }
        }
        public bool Dead => Health == 0;

        public event Action<int, int> HealthChanged;
        public event Action Died;

        public UnitModel(UnitConfig config)
        {
            _maxHealth = config.Health;
            Health = config.Health;
            Velocity = config.Velocity;
        }

        public void Hit(int damage)
        {
            if (Dead)
                return;

            if (Health - damage > 0)
            {
                Health -= damage;
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
        }
        public void Reset()
        {
            Health = MaxHealth;
        }
        public JToken Serialize()
        {
            JObject obj = new JObject();
            obj.Add("health", JToken.FromObject(Health));
            obj.Add("maxHealth", JToken.FromObject(MaxHealth));
            return obj;
        }
        public void Deserialize(JToken token)
        {
            Health = token["health"].Value<int>();
            MaxHealth = token["maxHealth"].Value<int>();
        }
    }
}