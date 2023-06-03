using System.Collections.Generic;
using UnityEngine;
using Core.Units;

namespace Core.UI
{
    public class HealthBarManager : MonoBehaviour
    {
        [SerializeField]
        private GameObject _healthBarPrefab;

        private Camera _camera;
        private readonly Dictionary<IUnit, RectTransform> _bars = new();
        private readonly LinkedList<IUnit> _barsToRemove = new();

        private void Awake()
        {
            _camera = Camera.main;
        }
        private void LateUpdate()
        {
            foreach (var item in _bars)
            {
                if (item.Key.Dead)
                {
                    RemoveHealthBar(item.Key);
                    continue;
                }

                Vector2 screenPosition = _camera.WorldToScreenPoint(item.Key.Transformable.Position);
                screenPosition.y += 30f;
                item.Value.position = screenPosition;
            }

            foreach (var item in _barsToRemove)
                _bars.Remove(item);
        }
        public void CreateHealthBar(UnitModel model, IUnit followTarget)
        {
            var bar = Instantiate(_healthBarPrefab).GetComponent<HealthBarView>();
            bar.transform.SetParent(transform, false);
            bar.Init(model);

            var rect = bar.GetComponent<RectTransform>();
            _bars.TryAdd(followTarget, rect);
        }
        public void RemoveHealthBar(IUnit followTarget)
        {
            if (_bars.TryGetValue(followTarget, out var healthBarRect))
            {
                _barsToRemove.AddLast(followTarget);
                Destroy(healthBarRect.gameObject);
            }
        }     
    }
}