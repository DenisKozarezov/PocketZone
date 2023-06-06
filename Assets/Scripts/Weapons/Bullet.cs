using System;
using UnityEngine;
using Zenject;
using Core.Units;
using System.Collections;

namespace Core.Weapons
{
    [RequireComponent(typeof(Rigidbody2D))]
    [RequireComponent(typeof(Collider2D))]
    public class Bullet : MonoBehaviour, IPoolable<Vector2, Quaternion, float, float, IMemoryPool>, IDisposable
    {
        private IMemoryPool _pool;
        private float _bulletForce;
        private Coroutine _lifetimeCoroutine;

        public event Action<Bullet, IUnit> Hit;
        public event Action<Bullet> Disposed;
        public event Action LifetimeElapsed;

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
        private IEnumerator LifetimeCoroutine(float lifetime)
        {
            yield return new WaitForSeconds(lifetime);
            LifetimeElapsed?.Invoke();
        }
        public void Dispose()
        {
            if (_lifetimeCoroutine != null)
            {
                StopCoroutine(_lifetimeCoroutine);
                _lifetimeCoroutine = null;
            }
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
            _lifetimeCoroutine = StartCoroutine(LifetimeCoroutine(lifetime));
        }
    }
}