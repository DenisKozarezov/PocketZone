using UnityEngine;

namespace Core.Services.AI
{
    public class Node
    {
        public bool isObstacle;
        public Vector2 worldPosition;
        public int gridX;
        public int gridY;
        public float gCost;
        public float hCost;
        public Node parent;

        public float fCost => gCost + hCost;

        public Node(bool _isObstacle, Vector2 _worldPosition, int _gridX, int _gridY)
        {
            isObstacle = _isObstacle;
            worldPosition = _worldPosition;
            gridX = _gridX;
            gridY = _gridY;
        }
    }
}