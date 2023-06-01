using Core.Factories;
using UnityEngine;
using Zenject;

namespace Core
{
    public class Level : IInitializable
    {
        private readonly IPlayerFactory _playerFactory;
        public Level(IPlayerFactory playerFactory) 
        { 
            _playerFactory = playerFactory;
        }
        public void Initialize()
        {
            _playerFactory.Create(Vector3.zero);
        }
    }
}