﻿using UnityEngine;

namespace Core.Units.Enemy
{
    public class EnemyView : MonoBehaviour, ITransformable
    {
        [SerializeField]
        private Rigidbody2D _rigidbody;

        private Vector2 _animationBlend;

        public Transform Transform => transform;
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
            localScale.x = currentXScaleValue * (flipX ? 0f : 1f);
            transform.localScale = localScale;
        }
        public void SetActive(bool isActive)
        {
            gameObject.SetActive(isActive);
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
