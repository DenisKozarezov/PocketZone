using System;
using UnityEngine;
using TMPro;
using Zenject;
using DG.Tweening;

namespace Core.UI
{
    [RequireComponent(typeof(TextMeshPro))]
    public class DamageVFX : MonoBehaviour, IDisposable, IPoolable<Vector2, Vector2, string, float, IMemoryPool>
    {
        private TextMeshPro _text;
        private Color _startColor;
        private IMemoryPool _pool;

        private void Awake()
        {
            _text = GetComponent<TextMeshPro>();
            _startColor = _text.color;
        }

        public void Dispose() => _pool?.Despawn(this);
        void IPoolable<Vector2, Vector2, string, float, IMemoryPool>.OnDespawned()
        {
            _pool = null;
            _text.color = _startColor;
        }
        void IPoolable<Vector2, Vector2, string, float, IMemoryPool>.OnSpawned(Vector2 position, Vector2 direction, string label, float lifetime, IMemoryPool pool)
        {
            transform.position = position;
            _pool = pool;
            _text.text = label;

            transform.DOMove((Vector2)transform.position + direction, lifetime);
            _text.DOFade(0f, lifetime)
                .SetLink(gameObject)
                .SetEase(Ease.Linear);

            Invoke(nameof(Dispose), lifetime);
        }
    }
}