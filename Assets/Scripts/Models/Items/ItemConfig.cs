using UnityEngine;

namespace Core.Models.Items
{
    public abstract class ItemConfig : ScriptableObject
    {
        [field: Header("Settings")]
        [field: SerializeField, Min(0)] public int ID { get; private set; }
        [field: SerializeField] public string DisplayName { get; private set; }
        [field: SerializeField, TextArea] public string Description { get; private set; }
    }
}