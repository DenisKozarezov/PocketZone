using Core.Units;
using System.Collections.Generic;
using UnityEngine;

namespace Core.Services.AI
{
    public class PathfinderAgent : MonoBehaviour
    {
        private ITransformable Transformable;
        private float moveSpeed = 5f;

        private List<Node> path;
        private int currentPathIndex;

        private Vector2 InvalidNode = new Vector2(-100, -100);

        public float stoppingDistance = 0.2f;
        public Vector2 Destination;
        public bool IsFindingPath => Destination != InvalidNode;
        
        public void Init(ITransformable transformable, UnitModel model)
        {
            Transformable = transformable;
            moveSpeed = model.Velocity;
    }
        public void SetDestination(Vector2 destination)
        {
            Destination = destination;
        }
        public void SetDestination(Transform target)
        {
            Destination = target.position;
        }
        public void SetPath(List<Node> newPath)
        {
            path = newPath;
            currentPathIndex = 0;
        }
        private void Awake()
        {
            Destination = InvalidNode;
        }
        private void Update()
        {
            if (path != null && path.Count > 0)
            {
                MoveAlongPath();
            }
        }
        private void MoveAlongPath()
        {
            if (currentPathIndex >= path.Count)
            {
                path = null;
                return;
            }

            Node currentWaypoint = path[currentPathIndex];
            Transformable.Translate(currentWaypoint.worldPosition - Transformable.Position, moveSpeed);
            if (Vector3.Distance(transform.position, currentWaypoint.worldPosition) <= stoppingDistance)
            {
                currentPathIndex++;
            }
        }
    }
}