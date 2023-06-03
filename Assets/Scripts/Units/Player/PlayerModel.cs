using Core.Models;
using Core.Services.Input;

namespace Core.Units.Player
{
    public class PlayerModel : UnitModel
    {
        public readonly PlayerConfig Config;
        public readonly IInputService InputService;

        public PlayerModel(PlayerConfig config, IInputService inputService) : base(config.Health)
        {
            Config = config;
            InputService = inputService;
        }       
    }
}
