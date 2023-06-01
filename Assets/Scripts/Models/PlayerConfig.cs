using UnityEngine;
using Core.Models.Units;

namespace Core.Models
{
    [CreateAssetMenu(menuName = "Configuration/Player Config")]
    public sealed class PlayerConfig : UnitConfig
    {
        public readonly float VerticalRotationMin = -30f;
        public readonly float VerticalRotationMax = 0f;
    }
}
