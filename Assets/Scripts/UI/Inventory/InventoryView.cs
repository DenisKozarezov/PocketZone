using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Core.Services.Inventory;

namespace Core.UI
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField]
        private Animator _animator;
        [SerializeField]
        private Button _inventoryButton;
        [SerializeField]
        private Button _leftScroll;
        [SerializeField]
        private Button _rightScroll;
        [SerializeField]
        private ScrollRect _scrollView;
        [SerializeField]
        private GameObject _itemTemplate;
        [SerializeField]
        private RectTransform _templatesParent;

        private IInventoryService _inventory;
        private readonly int InventoryShown = Animator.StringToHash("InventoryShown");

        private readonly Dictionary<InventoryItemModel, InventoryItemTemplate> _items = new();

        [Inject]
        private void Construct(IInventoryService inventory)
        {
            _inventory = inventory;
        }

        private void Awake()
        {
            _inventoryButton.onClick.AddListener(OnInventoryClick);
            _leftScroll.onClick.AddListener(OnLeftScroll);
            _rightScroll.onClick.AddListener(OnRightScroll);
            _inventory.ItemCollected += AddItem;
            _inventory.ItemModified += ModifyItem;
        }
        private void OnDestroy()
        {
            _inventoryButton.onClick.RemoveListener(OnInventoryClick);
            _leftScroll.onClick.RemoveListener(OnLeftScroll);
            _rightScroll.onClick.RemoveListener(OnRightScroll);
            _inventory.ItemCollected -= AddItem;
            _inventory.ItemModified -= ModifyItem;
        }
        private void OnLeftScroll()
        {
            _scrollView.horizontalNormalizedPosition += _scrollView.elasticity;
        }
        private void OnRightScroll()
        {
            _scrollView.horizontalNormalizedPosition -= _scrollView.elasticity;
        }
        private void OnInventoryClick()
        {
            bool isShown = _animator.GetBool(InventoryShown);
            _animator.SetBool(InventoryShown, !isShown);
        }

        private void AddItem(InventoryItemModel item)
        {
            var obj = Instantiate(_itemTemplate, _templatesParent);
            var view = obj.GetComponent<InventoryItemTemplate>();
            view.Init(item);
            _items.Add(item, view);
        }
        private void ModifyItem(InventoryItemModel item)
        {
            if (_items.TryGetValue(item, out var template))
            {
                template.SetStacks(item.Stacks);
            }
        }
    }
}