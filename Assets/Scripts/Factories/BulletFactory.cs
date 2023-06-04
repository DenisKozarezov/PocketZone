using UnityEngine;
using Zenject;
using Core.Weapons;

namespace Core.Factories
{
    public interface IBulletFactory : IFactory<Vector2, Quaternion, float, float, Bullet> { }
    public class BulletFactory : PlaceholderFactory<Vector2, Quaternion, float, float, Bullet>, IBulletFactory { }
}