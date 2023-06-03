using UnityEngine;
using Core.UI;

namespace Core.Units.Player
{
    public class PlayerView : MonoBehaviour, ITransformable
    {
        [SerializeField]
        private Rigidbody2D _rigidbody;
        [SerializeField]
        private Transform _characterCenter;
        [SerializeField]
        private Transform _weaponHolder;
        [SerializeField]
        private AttackRangeCircle _attackRangeCircle;

        public Transform Transform => _characterCenter;
        public Vector2 Position => transform.position;
        public Quaternion Rotation => transform.rotation;

        public void Translate(Vector2 worldDirection, float movementSpeed)
        {
            Vector2 velocity = worldDirection * movementSpeed * Time.fixedDeltaTime;
            _rigidbody.MovePosition(_rigidbody.position + velocity);
        }
        public void SetPosition(Vector2 worldPosition)
        {
            transform.position = worldPosition;
        }
        public void SetDirection(bool flipX)
        {
            float currentXScaleValue = Mathf.Abs(transform.localScale.x);
            
            Vector3 localScale = transform.localScale;
            localScale.x = currentXScaleValue * (flipX ? 1f : -1f);
            transform.localScale = localScale;
        }
        public void RotateWeapon(float angle)
        {
            _attackRangeCircle.SetAngle(angle);

            if (angle > 90f && angle < 180f)
                angle = 180f + Mathf.Abs(angle);
            else if (angle >= -180f && angle <= -90f)
                angle = angle - 180f;

            _weaponHolder.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
        }
    }
}
