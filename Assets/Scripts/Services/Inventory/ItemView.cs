using System;
using UnityEngine;
using Core.Models.Items;
using Core.Units.Player;
using TMPro;

namespace Core.Services.Inventory
{
    [RequireComponent(typeof(Collider2D))]
    public class ItemView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshPro _itemName;
        private ItemConfig _config;

        public event Action<ItemConfig> Collected;

        public void Init(ItemConfig item)
        {
            _config = item;
            _itemName.text = item.DisplayName;
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent(out PlayerView player))
            {
                Collected?.Invoke(_config);
                Destroy(gameObject);
            }
        }
    }
}