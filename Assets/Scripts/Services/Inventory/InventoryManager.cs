using System.Collections.Generic;
using UnityEngine;
using Core.Models.Items;

namespace Core.Services.Inventory
{
    public class InventoryManager : IInventoryService
    {
        public void DropRandomItem(Vector2 position, IEnumerable<ItemReward> items)
        {
            float totalWeight = 0f;
            foreach (ItemReward dropItem in items)
            {
                totalWeight += dropItem.Probability;
            }

            float randomValue = Random.Range(0f, totalWeight);
            float weightSum = 0f;

            foreach (ItemReward dropItem in items)
            {
                weightSum += dropItem.Probability;
                if (randomValue <= weightSum)
                {
                    Debug.Log(dropItem.Item.DisplayName + " " + dropItem.Probability);
                    break;
                }
            }
        }
    }
}