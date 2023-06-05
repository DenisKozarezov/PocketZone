using UnityEngine;

namespace Core.Units
{
    [RequireComponent(typeof(Collider2D))]
    public class DamageReceiver : MonoBehaviour
    {
        public IUnit Owner { private set; get; }
        public void Init(IUnit unit)
        {
            Owner = unit;
        }
    }
}