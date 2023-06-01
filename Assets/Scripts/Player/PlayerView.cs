using UnityEngine;

namespace Core.Match.Player
{
    public class PlayerView : MonoBehaviour, ITransformable
    {
        [SerializeField]
        private Rigidbody2D _rigidbody;

        private Camera _mainCamera;
        private Vector2 _animationBlend;

        public Transform Transform => transform;
        public Vector3 Position => transform.position;
        public Quaternion Rotation => transform.rotation;

        private void Start()
        {
            _mainCamera = Camera.main;
        }
        public Vector3 CalculateWorldDirection(Vector2 inputDirection)
        {
            Vector3 direction = new Vector3(inputDirection.x, 0f, inputDirection.y);
            Vector3 worldDirection = _mainCamera.transform.TransformDirection(direction);
            worldDirection.y = 0;
            return worldDirection.normalized;
        }
        public void Translate(Vector3 worldDirection, float movementSpeed)
        {
            _rigidbody.velocity = worldDirection * movementSpeed;
        }
        public void SetPosition(Vector3 worldPosition)
        {
            transform.position = worldPosition;
        }
        public void Rotate(Quaternion rotation)
        {
            transform.rotation = rotation;
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
