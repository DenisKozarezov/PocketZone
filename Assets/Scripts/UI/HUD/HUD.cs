using UnityEngine;
using Core.Units.Player;
using Zenject;

namespace Core.UI
{
    public class HUD : MonoBehaviour
    {
        [SerializeField]
        private InventoryView _inventory;
        [SerializeField]
        private AmmoCounterView _ammoCounter;

        private LazyInject<PlayerController> _player;

        [Inject]
        private void Construct(LazyInject<PlayerController> player)
        {
            _player = player;
        }

        private void Start()
        {
            _player.Value.PrimaryWeapon.AmmoChanged += _ammoCounter.SetAmmo;
        }
        private void OnDestroy()
        {
            _player.Value.PrimaryWeapon.AmmoChanged -= _ammoCounter.SetAmmo;
        }
    }
}
