using UnityEngine;

namespace Core.UI
{
    [RequireComponent(typeof(LineRenderer))]
    public class AttackRangeCircle : MonoBehaviour
    {
        [SerializeField]
        private float _radius;
        [SerializeField]
        private float _lineWidth = 0.01f;
        [SerializeField]
        private Color _lineColor = Color.white;

        private LineRenderer _renderer;
        private Transform _parent;
        private const byte Segments = 100;

        private void Awake()
        {
            _renderer = GetComponent<LineRenderer>();
            _parent = transform.parent;
            _renderer.positionCount = Segments + 1;
            _renderer.startWidth = _renderer.endWidth = _lineWidth;
            _renderer.startColor = _renderer.endColor = _lineColor;
        }
        private void Update()
        {
            for (byte i = 0; i < Segments + 1; i++)
            {
                float angle = (i * 360f / Segments) * Mathf.Deg2Rad;
                float sin = Mathf.Sin(angle) * _radius;
                float cos = Mathf.Cos(angle) * _radius;
                _renderer.SetPosition(i, _parent.position + new Vector3(sin, cos));
            }
        }

        public void SetRadius(float radius) 
        {
            _radius = radius;
        }
    }
}