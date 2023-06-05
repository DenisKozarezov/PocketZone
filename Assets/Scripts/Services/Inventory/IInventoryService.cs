using System.Collections.Generic;
using UnityEngine;
using Core.Models.Items;

namespace Core.Services.Inventory
{
    public interface IInventoryService
    {
        public void DropRandomItem(Vector2 position, IEnumerable<ItemReward> items);
    }
}