using System;
using System.Collections.Generic;
using UnityEngine;
using Core.Models.Items;

namespace Core.Services.Inventory
{
    public class InventoryManager : MonoBehaviour, IInventoryService
    {
        [SerializeField]
        private GameObject _itemPrefab;

        private readonly Dictionary<string, InventoryItemModel> _items = new();

        public event Action<InventoryItemModel> ItemCollected;
        public event Action<InventoryItemModel> ItemModified;
        public event Action<InventoryItemModel> ItemRemoved;

        private void OnItemCollected(ItemConfig item)
        { 
            if (!_items.ContainsKey(item.DisplayName))
            {
                InventoryItemModel newModel = new InventoryItemModel
                {
                    DisplayName = item.DisplayName,
                    Icon = item.Icon,
                    Stackable = item.Stackable,
                    Stacks = item.GetStacks()
                };
                _items.TryAdd(item.DisplayName, newModel);
                ItemCollected?.Invoke(newModel);
            }
            else
            {
                _items[item.DisplayName].Stacks += item.GetStacks();
                ItemModified?.Invoke(_items[item.DisplayName]);
            }
        }
        public void DropRandomItem(Vector2 position, IEnumerable<ItemReward> items)
        {
            float totalWeight = 0f;
            foreach (ItemReward dropItem in items)
            {
                totalWeight += dropItem.Probability;
            }

            float randomValue = UnityEngine.Random.Range(0f, totalWeight);
            float weightSum = 0f;

            foreach (ItemReward dropItem in items)
            {
                weightSum += dropItem.Probability;
                if (randomValue <= weightSum)
                {
                    var obj = Instantiate(_itemPrefab, position, Quaternion.identity);
                    var item = obj.GetComponent<ItemView>();
                    item.Init(dropItem.Item);
                    item.Collected += OnItemCollected;
                    break;
                }
            }
        }
        public void RemoveItem(InventoryItemModel removedItem)
        {
            if (_items.ContainsKey(removedItem.DisplayName))
            {
                _items.Remove(removedItem.DisplayName);
                ItemRemoved?.Invoke(removedItem);
            }
        }
    }
}