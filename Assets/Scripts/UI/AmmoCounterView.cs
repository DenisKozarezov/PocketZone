using UnityEngine;
using TMPro;

namespace Core.UI
{
    public class AmmoCounterView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _text;
        public void SetAmmo(int currentAmmo, int maxAmmo)
        {
            _text.text = $"{currentAmmo}/{maxAmmo}";
        }
    }
}