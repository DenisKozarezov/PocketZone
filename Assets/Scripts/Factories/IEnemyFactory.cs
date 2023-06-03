using UnityEngine;
using Core.Units.Enemy;

namespace Core.Factories
{
    public interface IEnemyFactory
    {
        EnemyController Spawn(Vector2 position);
    }
}
