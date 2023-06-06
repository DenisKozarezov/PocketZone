using System;
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
        private Button _button;
        [SerializeField]
        private RectTransform _removeButtonPrefab;
        [SerializeField]
        private TextMeshProUGUI _stacksValue;

        public event Action Removed;

        private void Awake()
        {
            _button.onClick.AddListener(OnClick);
        }
        private void OnClick()
        {
            _button.interactable = false;

            var prefab = Instantiate(_removeButtonPrefab, transform);
            prefab.GetComponent<Button>().onClick.AddListener(() => Removed?.Invoke());
            prefab.GetComponent<RectTransform>().anchoredPosition = Vector2.up * 30f;
        }
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