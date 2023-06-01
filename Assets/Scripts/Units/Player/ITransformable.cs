using UnityEngine;

namespace Core
{
    public interface ITransformable
    {
        Transform Transform { get; }
        Vector2 Position { get; }
        Quaternion Rotation { get; }
        void Translate(Vector2 worldDirection, float movementSpeed);
        void SetPosition(Vector2 worldPosition);
        void SetDirection(bool flipX);
    }
}
