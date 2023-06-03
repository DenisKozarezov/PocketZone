using UnityEngine;

namespace Core.Units.Enemy
{
    public class EnemyAttackState : EnemyBaseState
    {
        private readonly float _velocity;
        private readonly float _attackCooldown;
        private readonly float _attackRadius;
        private float _timer;

        public EnemyAttackState(IStateMachine<EnemyController> stateMachine, EnemyModel model) : base(stateMachine)
        {
            _velocity = model.Velocity;
            _attackCooldown = model.AttackCooldown;
            _attackRadius = model.AttackRadius;
        }
        private bool IsCloseToTarget(out Vector2 direction)
        {
            direction = Context.Target.Transformable.Position - Transformable.Position;
            return direction.sqrMagnitude <= _attackRadius * _attackRadius;
        }
        private void ChaseTarget(Vector2 direction)
        {
            Transformable.Translate(direction.normalized, _velocity);
        }
        private void PeriodicAttack()
        {
            if (_timer <= _attackCooldown) _timer += Time.deltaTime;
            else
            {
                Context.Attack();
                _timer = 0f;
            }
        }
        public override void Enter()
        {
            _timer = 0f;
        }
        public override void Exit()
        {

        }
        public override void FixedUpdate()
        {
            if (!IsCloseToTarget(out Vector2 direction))
                ChaseTarget(direction);
            Transformable.SetDirection(direction.x > 0f);
        }
        public override void Update()
        {
            PeriodicAttack();
        }
    }
}