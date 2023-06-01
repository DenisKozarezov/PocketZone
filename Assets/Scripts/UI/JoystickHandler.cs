using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Core.UI
{
    public sealed class JoystickHandler : MonoBehaviour, IDragHandler, IBeginDragHandler, IEndDragHandler
    {
        [SerializeReference]
        private RawImage _placeholder;
        [SerializeReference]
        private RawImage _handle;
        [SerializeField, Range(0f, 100f)]
        private float _moveRadius;

        private Vector2 _startPos;

        public Vector2 Direction { get; private set; }
        public bool IsDragging { get; private set; }

        private void Start()
        {
            _startPos = _handle.rectTransform.position;
        }

        public void OnBeginDrag(PointerEventData eventData)
        {
            if (!IsDragging)
            {
                IsDragging = true;
            }
        }
        public void OnEndDrag(PointerEventData eventData)
        {
            IsDragging = false;
            Direction = Vector2.zero;
            _handle.rectTransform.localPosition = Vector3.zero;
        }
        public void OnDrag(PointerEventData eventData)
        {
            Vector2 touchOffset = eventData.position - _startPos;
            Vector2 clampedOffset = Vector2.ClampMagnitude(touchOffset, _moveRadius);
            _handle.rectTransform.position = _startPos + clampedOffset;
            Direction = touchOffset.normalized;
        }
    }
}