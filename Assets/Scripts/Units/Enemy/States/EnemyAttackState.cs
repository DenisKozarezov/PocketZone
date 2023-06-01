using UnityEngine;

namespace Core.Units.Enemy
{
    public class EnemyAttackState : EnemyBaseState
    {
        private readonly float _velocity;
        private readonly float _reloadTime;
        private readonly float _attackDistance;
        private float _timer;
        private float _lerp;

        public EnemyAttackState(IStateMachine<EnemyController> stateMachine, EnemyModel model) : base(stateMachine)
        {
            _velocity = model.Velocity;
            _reloadTime = model.ReloadTime;
            _attackDistance = model.AggressionRadius;
        }
        private bool IsCloseToTarget(out Vector2 direction)
        {
            direction = Context.Target.Transformable.Position - Transformable.Position;
            return direction.sqrMagnitude <= _attackDistance * _attackDistance;
        }
        private void LookAtTarget(Vector2 direction)
        {
            //float angle = (Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg) - 90f;
            //Quaternion targetRotation = Quaternion.AngleAxis(angle, Vector3.forward);
            //Quaternion lerpRotation = Quaternion.Slerp(Transformable.Rotation, targetRotation, Time.deltaTime * _rotationSpeed);
            //Transformable.Rotate(lerpRotation);
        }
        private void ChaseTarget(Vector2 direction)
        {
            Transformable.Translate(direction.normalized, _velocity);
        }
        private void MoveAway(Vector2 counterForce)
        {
            _lerp += 0.001f;
            if (_lerp > 1f) _lerp = 0f;

            Vector2 left = Vector2.Perpendicular(counterForce);
            Vector2 right = Vector2.Reflect(left, counterForce);
            Vector2 direction = Vector2.Lerp(left, right, _lerp);
            Transformable.Translate(direction, _velocity);
        }
        private void PeriodicShoot()
        {
            if (_timer <= _reloadTime) _timer += Time.deltaTime;
            else
            {
                Context.Attack();
                _timer = 0f;
            }
        }
        public override void Enter()
        {
            _timer = 0f;
            _lerp = 0f;
        }
        public override void Exit()
        {

        }
        public override void Update()
        {
            if (!IsCloseToTarget(out Vector2 direction))
                ChaseTarget(direction);
            else
                MoveAway(-direction.normalized);

            LookAtTarget(direction);
            PeriodicShoot();

#if UNITY_EDITOR
            Debug.DrawLine(Transformable.Position, Context.Target.Transformable.Position, Color.red);
#endif
        }
    }
}