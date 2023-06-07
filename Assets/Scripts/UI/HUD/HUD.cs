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
            var weapon = _player.Value.PrimaryWeapon;

            _ammoCounter.SetAmmo(weapon.CurrentAmmo, weapon.MaxAmmo);
            weapon.AmmoChanged += _ammoCounter.SetAmmo;
        }
        private void OnDestroy()
        {
            _player.Value.PrimaryWeapon.AmmoChanged -= _ammoCounter.SetAmmo;
        }
    }
}
