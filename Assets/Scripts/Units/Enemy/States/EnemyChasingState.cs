using UnityEngine;

namespace Core.Units.Enemy
{
    public class EnemyChasingState : EnemyBaseState
    {
        private readonly float _velocity;
        private readonly float _attackRadius;

        public EnemyChasingState(IStateMachine<EnemyController> stateMachine, EnemyModel model) : base(stateMachine)
        {
            _velocity = model.Velocity;
            _attackRadius = model.AttackRadius;
        }

        private bool HasReachedTarget(out Vector2 direction)
        {
            direction = Context.Target.Transformable.Position - Transformable.Position;
            return direction.sqrMagnitude <= _attackRadius * _attackRadius;
        }
        public override void Enter()
        {

        }
        public override void Exit()
        {

        }
        public override void FixedUpdate()
        {
            if (!HasReachedTarget(out Vector2 direction))
            {
                Transformable.SetDirection(direction.x > 0f);
                Transformable.Translate(direction.normalized, _velocity);

#if UNITY_EDITOR
                Debug.DrawLine(Transformable.Position, Context.Target.Transformable.Position, Color.red);
#endif
            }
            else StateMachine.SwitchState<EnemyAttackState>();
        }
    }
}