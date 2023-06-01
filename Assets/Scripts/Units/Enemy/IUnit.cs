namespace Core.Units
{
    public interface IUnit
    {
        ITransformable Transformable { get; }
        void Hit(int damage);
    }
}