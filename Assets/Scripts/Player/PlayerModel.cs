using System;
using Core.Models;
using Core.Services.Input;

namespace Core.Match.Player
{
    public class PlayerModel
    {
        public readonly PlayerConfig Config;
        public readonly IInputService InputService;
        public int Health;
        public bool Dead => Health == 0;

        public event Action Died;

        public PlayerModel(PlayerConfig config, IInputService inputService)
        {
            Config = config;
            Health = config.MaxHealth;
            InputService = inputService;
        }

        public void Hit(int damage)
        {
            if (Dead) return;

            if (Health - damage > 0) 
                Health -= damage;
            else
            {
                Health = 0;
                Died?.Invoke();
            }
        }
    }
}
