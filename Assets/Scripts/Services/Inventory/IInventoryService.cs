using System;
using System.Collections.Generic;
using UnityEngine;
using Core.Models.Items;
using Core.Services.Serialization;

namespace Core.Services.Inventory
{
    public interface IInventoryService : ISerializableObject
    {
        event Action<InventoryItemModel> ItemCollected;
        event Action<InventoryItemModel> ItemModified;
        event Action<InventoryItemModel> ItemRemoved;
        void DropRandomItem(Vector2 position, IEnumerable<ItemReward> items);
        void RemoveItem(InventoryItemModel removedItem);
    }
}