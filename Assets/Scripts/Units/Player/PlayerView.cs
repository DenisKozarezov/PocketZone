using UnityEngine;

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

        private Vector2 _animationBlend;

        public Transform Transform => _characterCenter;
        public Vector2 Position => transform.position;
        public Quaternion Rotation => transform.rotation;

        public void Translate(Vector2 worldDirection, float movementSpeed)
        {
            _rigidbody.velocity = worldDirection * movementSpeed;
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
        public void RotateWeapon(Quaternion rotation)
        {
            _weaponHolder.rotation = rotation;
        }
        public void SetRunningAnimation(bool isMoving, Vector2 inputDirection)
        {
            _animationBlend = Vector2.SmoothDamp(_animationBlend, inputDirection, ref _animationBlend, 0.05f);
            _animationBlend = Vector2.ClampMagnitude(_animationBlend, 1f);

            if (_animationBlend.sqrMagnitude <= 1E-02)
                _animationBlend = Vector2.zero;
        }
    }
}
