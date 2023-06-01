using UnityEngine;
using Zenject;

namespace Core.Models
{
    [CreateAssetMenu(menuName = "Configuration/Player Config")]
    public sealed class PlayerConfig : ScriptableObjectInstaller
    {
        [field: SerializeField]
        public GameObject Prefab { get; private set; }

        [field: SerializeField, Header("Settings"), Min(0f)]
        public int MaxHealth { get; private set; } = 100;

        [field: SerializeField, Min(0f)]
        public float MovementSpeed { get; private set; } = 2f;

        [field: SerializeField, Range(0.01f, 0.5f)]
        public float RotationSpeed { get; private set; } = 1f;

        public readonly float VerticalRotationMin = -30f;
        public readonly float VerticalRotationMax = 30f;

        public override void InstallBindings()
        {
            Container.BindInstance(this).IfNotBound();
        }
    }
}
