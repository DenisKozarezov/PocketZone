using UnityEngine;
using UnityEngine.UI;
using Core.Units;

namespace Core.UI
{
    public class HealthBarView : MonoBehaviour
    {
        private UnitModel _model;

        [SerializeField]
        private Image _foreground;

        private void SetFillAmount(float fillAmount)
        {
            _foreground.fillAmount = fillAmount;
        }
        private void OnHealthChanged(int currentHealth, int maxHealth)
        {
            SetFillAmount((float)currentHealth / maxHealth);
        }
        private void OnDied()
        {
            _model.HealthChanged -= OnHealthChanged;
        }
        public void Init(UnitModel model)
        {
            model.HealthChanged += OnHealthChanged;
            model.Died += OnDied;
            _model = model;
        }
    }
}