using UnityEngine;
using TMPro;

namespace Core.UI
{
    public class AmmoCounterView : MonoBehaviour
    {
        [SerializeField]
        private TextMeshProUGUI _text;
        private void Awake()
        {
            _text = GetComponent<TextMeshProUGUI>();
        }
        public void SetAmmo(int currentAmmo, int maxAmmo)
        {
            _text.text = $"{currentAmmo}/{maxAmmo}";
        }
    }
}