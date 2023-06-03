using UnityEngine;
using Core.Models;
using Zenject;

namespace Core.UI
{
    [RequireComponent(typeof(LineRenderer))]
    public class AttackRangeCircle : MonoBehaviour
    {
        [SerializeField]
        private float _lineWidth = 0.01f;
        [SerializeField]
        private Color _lineColor = Color.white;
        [SerializeField]
        private Transform _crossfire;
        [SerializeField]
        private Transform _parent;

        private LineRenderer _renderer;
        private PlayerConfig _config;
        private float Radius => _config.AttackRange;
        private const byte Segments = 100;

        [Inject]
        private void Construct(PlayerConfig config)
        {
            _config = config;
        }

        private void Awake()
        {
            _renderer = GetComponent<LineRenderer>();
            _renderer.positionCount = Segments + 1;
            _renderer.startWidth = _renderer.endWidth = _lineWidth;
            _renderer.startColor = _renderer.endColor = _lineColor;
        }
        private void LateUpdate()
        {
            for (byte i = 0; i < Segments + 1; i++)
            {
                float angle = (i * 360f / Segments) * Mathf.Deg2Rad;
                float sin = Mathf.Sin(angle) * Radius;
                float cos = Mathf.Cos(angle) * Radius;
                _renderer.SetPosition(i, _parent.position + new Vector3(cos, sin));               
            }
        }
        public void SetAngle(float angle)
        {
            float sin = Mathf.Sin(angle * Mathf.Deg2Rad) * (Radius * 0.95f);
            float cos = Mathf.Cos(angle * Mathf.Deg2Rad) * (Radius * 0.95f);
            _crossfire.position = _parent.position + new Vector3(cos, sin);
            _crossfire.rotation = Quaternion.Euler(new Vector3(0f, 0f, 90f + angle));
        }
    }
}