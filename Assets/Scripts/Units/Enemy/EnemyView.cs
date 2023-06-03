using UnityEngine;

namespace Core.Units.Enemy
{
    public class EnemyView : MonoBehaviour, ITransformable
    {
        [SerializeField]
        private Rigidbody2D _rigidbody;

        public Transform Transform => transform;
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
        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
        }
    }
}
