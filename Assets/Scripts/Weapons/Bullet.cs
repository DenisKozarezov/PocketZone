using System;
using UnityEngine;
using Zenject;
using Core.Units;

namespace Core.Weapons
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class Bullet : MonoBehaviour, IPoolable<Vector2, Quaternion, float, float, IMemoryPool>, IDisposable
    {
        private IMemoryPool _pool;
        private float _bulletForce;

        public event Action<Bullet, IUnit> Hit;
        public event Action<Bullet> Disposed;

        private void Update()
        {
            transform.Translate(transform.up * _bulletForce * Time.deltaTime, Space.World);
        }
        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out DamageReceiver collider))
            {
                Hit?.Invoke(this, collider.Owner);
            }
        }
        public void Dispose()
        {
            _pool?.Despawn(this);
            Disposed?.Invoke(this);
        }

        void IPoolable<Vector2, Quaternion, float, float, IMemoryPool>.OnDespawned()
        {
            _pool = null;
        }
        void IPoolable<Vector2, Quaternion, float, float, IMemoryPool>.OnSpawned(Vector2 position, Quaternion rotation, float force, float lifetime, IMemoryPool pool)
        {
            _pool = pool;
            transform.position = position;
            transform.rotation = rotation;
            _bulletForce = force;
        }
    }
}