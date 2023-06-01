using UnityEngine;
using Core.Match.Player;
using Zenject;

namespace Core.Factories
{
    public interface IPlayerFactory : IFactory<Vector3, PlayerController>
    {
        
    }
}
