using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Core.Services.Inventory;

namespace Core.UI
{
    public class InventoryItemTemplate : MonoBehaviour
    {
        [SerializeField]
        private Image _icon;
        [SerializeField]
        private TextMeshProUGUI _stacksValue;
        
        public void Init(InventoryItemModel item)
        {
            _icon.sprite = item.Icon;

            _stacksValue.gameObject.SetActive(item.Stackable);   
            
            if (item.Stackable)
                SetStacks(item.Stacks);
        }
        public void SetStacks(int value)
        {
            _stacksValue.gameObject.SetActive(value > 1);
            _stacksValue.text = value.ToString();
        }
    }
}