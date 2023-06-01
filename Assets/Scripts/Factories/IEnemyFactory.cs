using UnityEngine;
using Core.Units.Enemy;

namespace Core.Factories
{
    public interface IEnemyFactory
    {
        bool Empty { get; }
        EnemyController Spawn(Vector2 position);
        void Despawn(EnemyController enemy);
    }
}
