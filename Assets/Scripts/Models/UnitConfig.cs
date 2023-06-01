using System;
using UnityEngine;
using Zenject;

namespace Core.Models.Units
{
    public abstract class UnitConfig : ScriptableObjectInstaller, IEquatable<UnitConfig>
    {
        [field: Header("Settings")]
        [field: SerializeField] public string DisplayName { get; private set; }
        [field: SerializeField, TextArea] public string Description { get; private set; }
        [field: Space, SerializeField, Min(1)] public int Health { get; private set; }
        [field: SerializeField, Range(0f, 50f)] public float Velocity { get; private set; }
        [field: SerializeField, Range(0f, 5f)] public float ReloadTime { get; private set; }
        [field: Space, SerializeField] public GameObject Prefab { get; private set; }

        public bool Equals(UnitConfig other)
        {
            if (other == null) return false;

            return DisplayName.Equals(other.DisplayName);
        }

        public override void InstallBindings()
        {
            Container.BindInstance(this).IfNotBound();
        }
    }
}