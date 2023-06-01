using UnityEngine;
using Core.Units.Player;
using Zenject;

namespace Core.Factories
{
    public interface IPlayerFactory : IFactory<Vector3, PlayerController>
    {
        
    }
}
