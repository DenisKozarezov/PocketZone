using System;
using System.Collections.Generic;
using UnityEngine;
using Core.Models.Items;

namespace Core.Services.Inventory
{
    public interface IInventoryService
    {
        event Action<InventoryItemModel> ItemCollected;
        event Action<InventoryItemModel> ItemModified;
        event Action<InventoryItemModel> ItemRemoved;
        void DropRandomItem(Vector2 position, IEnumerable<ItemReward> items);
    }
}