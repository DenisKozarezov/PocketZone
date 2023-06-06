using UnityEngine;

namespace Core.Models.Items
{
    [CreateAssetMenu(menuName = "Configuration/Items/Consumable Item")]
    public class ConsumableItem : ItemConfig
    {
        [field: Header("Item")]
        [field: SerializeField, Min(0)] public int Charges { get; private set; }

        public override int GetStacks() => Charges;
    }
}