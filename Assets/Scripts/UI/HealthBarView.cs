using UnityEngine;
using UnityEngine.UI;

namespace Core.UI
{
    public class HealthBarView : MonoBehaviour
    {
        [SerializeField]
        private Image _foreground;

        public void SetFillAmount(float fillAmount)
        {
            _foreground.fillAmount = fillAmount;
        }
    }
}