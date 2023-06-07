using System;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using Newtonsoft.Json.Linq;
using Core.Models.Items;

namespace Core.Services.Inventory
{
    public class InventoryManager : MonoBehaviour, IInventoryService
    {
        [SerializeField]
        private GameObject _itemPrefab;

        private readonly Dictionary<int, InventoryItemModel> _items = new();

        public event Action<InventoryItemModel> ItemCollected;
        public event Action<InventoryItemModel> ItemModified;
        public event Action<InventoryItemModel> ItemRemoved;

        private void OnItemCollected(ItemConfig item)
        { 
            if (!item.Stackable || !_items.ContainsKey(item.ID))
            {
                InventoryItemModel newModel = new InventoryItemModel
                {
                    ID = item.ID,
                    DisplayName = item.DisplayName,
                    Icon = item.Icon,
                    Stackable = item.Stackable,
                    Stacks = item.GetStacks()
                };
                _items.TryAdd(item.ID, newModel);
                ItemCollected?.Invoke(newModel);
            }
            else
            {
                _items[item.ID].Stacks += item.GetStacks();
                ItemModified?.Invoke(_items[item.ID]);
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
            if (_items.ContainsKey(removedItem.ID))
            {
                _items.Remove(removedItem.ID);
                ItemRemoved?.Invoke(removedItem);
            }
        }
        public JToken Serialize()
        {
            JObject obj = new JObject();
            obj.Add("items", JToken.FromObject(_items.Values.Select(x => x.Serialize())));
            return obj;
        }
        public void Deserialize(JToken token)
        {
            var inventory = token[GetType().Name];
            var deserializedItems = inventory["items"].ToObject<IEnumerable<JToken>>();
            foreach (var json in deserializedItems) 
            {
                var item = new InventoryItemModel();
                item.Deserialize(json);

                _items.TryAdd(item.ID, item);
                ItemCollected?.Invoke(item);
            }
        }
    }
}