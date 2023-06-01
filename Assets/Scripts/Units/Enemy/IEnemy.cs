namespace Core.Units.Enemy
{
    public interface IEnemy
    {
        IUnit Target { get; }
        bool IsTaunted { get; }
        void Taunt(IUnit unit);
    }
}