using Core.Models;
using Core.Services.Input;
using Core.Weapons;

namespace Core.Units.Player
{
    public class PlayerModel : UnitModel
    {
        public readonly PlayerConfig Config;
        public readonly IInputService InputService;
        public IWeapon PrimaryWeapon { get; set; }

        public PlayerModel(PlayerConfig config, IInputService inputService) : base(config)
        {
            Config = config;
            InputService = inputService;
        }       
    }
}
