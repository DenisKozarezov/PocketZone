using System;

namespace Core.Units
{
    public interface IUnit
    {
        ITransformable Transformable { get; }
        bool Dead { get; }
        event Action Died;
        void Hit(int damage);
    }
}