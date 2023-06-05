using UnityEngine;

namespace Core.Models.Items
{
    [CreateAssetMenu(menuName = "Configuration/Items/Consumable Item")]
    public class ConsumableItem : ItemConfig
    {
        [field: Header("Item")]
        [field: SerializeField] public byte Charges { get; private set; }
    }
}