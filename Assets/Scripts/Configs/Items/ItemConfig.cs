using System;
using UnityEngine;

namespace Core.Models.Items
{
    [Serializable]
    public struct ItemReward
    {
        public ItemConfig Item;
        [Range(0f, 1f)] public float Probability;
    }

    public abstract class ItemConfig : ScriptableObject, IEquatable<ItemConfig>
    {
        [field: Header("Settings")]
        [field: SerializeField] public string DisplayName { get; private set; }
        [field: SerializeField, TextArea] public string Description { get; private set; }
        [field: SerializeField] public Sprite Icon { get; private set; }

        public bool Equals(ItemConfig other)
        {
            if (other == null) return false;

            return DisplayName.Equals(other.DisplayName);
        }
    }
}