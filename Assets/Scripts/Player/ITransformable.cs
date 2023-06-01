using UnityEngine;

namespace Core
{
    public interface ITransformable
    {
        Transform Transform { get; }
        Vector3 Position { get; }
        Quaternion Rotation { get; }
        void Translate(Vector3 worldDirection, float movementSpeed);
        void SetPosition(Vector3 worldPosition);
        void Rotate(Quaternion rotation);
    }
}
