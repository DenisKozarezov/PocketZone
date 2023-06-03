namespace Core.Units.Enemy
{
    public interface IEnemy : IUnit
    {
        IUnit Target { get; }
        bool IsTaunted { get; }
        void Taunt(IUnit unit);
    }
}