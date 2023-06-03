using UnityEngine;

namespace Core.Units.Enemy
{
    public class EnemyPatrolState : EnemyBaseState
    {
        private readonly float _velocity;
        private readonly float _patrolRadius;
        private Vector2 _initPosition;
        private Vector2 _destination;

        public EnemyPatrolState(IStateMachine<EnemyController> stateMachine, EnemyModel model) : base(stateMachine)
        {
            _velocity = model.Velocity;
            _patrolRadius = model.PatrolRadius;
        }

        private Vector2 GetRandomDestination()
        {
            return _initPosition + Random.insideUnitCircle * _patrolRadius;
        }
        private bool HasReachedDestination(out Vector2 direction)
        {
            direction = _destination - Transformable.Position;
            return (_destination - Transformable.Position).sqrMagnitude <= 1f;
        }
        public override void Enter()
        {
            _initPosition = Transformable.Position;
            _destination = GetRandomDestination();
        }
        public override void Exit()
        {

        }
        public override void Update()
        {
            if (!HasReachedDestination(out Vector2 direction))
            {
                Transformable.SetDirection(direction.x > 0f);
                Transformable.Translate(direction.normalized, _velocity);

#if UNITY_EDITOR
                Debug.DrawLine(Transformable.Position, _destination, Color.yellow);
#endif
            }
            else _destination = GetRandomDestination();
        }
    }
}