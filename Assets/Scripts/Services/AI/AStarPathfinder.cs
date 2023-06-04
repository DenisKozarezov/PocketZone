using System.Collections.Generic;
using UnityEngine;

namespace Core.Services.AI
{
    public class AStarPathfinder : MonoBehaviour, IPathfinderStrategy
    {
        private Node[,] grid;
        private int gridSizeX;
        private int gridSizeY;

        private List<Node> openSet = new();
        private HashSet<Node> closedSet = new();

        public LayerMask obstacleMask;
        public Vector2 gridWorldSize;
        public float nodeRadius;
        public float obstacleCheckRadius;
        public float updateInterval;

        private float updateTime;

        private List<Node> path = new List<Node>();
        private LinkedList<PathfinderAgent> _agents = new();

        private void Start()
        {
            gridSizeX = Mathf.RoundToInt(gridWorldSize.x / (nodeRadius * 2));
            gridSizeY = Mathf.RoundToInt(gridWorldSize.y / (nodeRadius * 2));
            CreateGrid();

            updateTime = Time.time;
        }
        private void Update()
        {
            if (Time.time - updateTime >= updateInterval)
            {
                UpdatePath();
                updateTime = Time.time;
            }
        }
        private void OnDrawGizmos()
        {
            Gizmos.DrawWireCube(transform.position, new Vector3(gridWorldSize.x, gridWorldSize.y, 1));
        }
        private void CreateGrid()
        {
            grid = new Node[gridSizeX, gridSizeY];
            Vector3 worldBottomLeft = transform.position - Vector3.right * gridWorldSize.x / 2 - Vector3.up * gridWorldSize.y / 2;

            for (int x = 0; x < gridSizeX; x++)
            {
                for (int y = 0; y < gridSizeY; y++)
                {
                    Vector3 worldPoint = worldBottomLeft + Vector3.right * (x * (nodeRadius * 2) + nodeRadius) + Vector3.up * (y * (nodeRadius * 2) + nodeRadius);
                    bool obstacle = Physics2D.OverlapCircle(worldPoint, obstacleCheckRadius, obstacleMask);
                    grid[x, y] = new Node(obstacle, worldPoint, x, y);
                }
            }
        }
        private void UpdatePath()
        {
            foreach (PathfinderAgent unit in _agents)
            {
                if (unit.IsFindingPath)
                {
                    path = FindPath(unit.transform.position, unit.Destination);

                    if (path != null && path.Count > 0)
                    {
                        unit.SetPath(path);
                    }
                }
            }
        }
        public List<Node> FindPath(Vector3 startPos, Vector3 targetPos)
        {
            Node startNode = GetNodeFromWorldPoint(startPos);
            Node targetNode = GetNodeFromWorldPoint(targetPos);

            openSet.Add(startNode);

            while (openSet.Count > 0)
            {
                Node currentNode = openSet[0];

                for (int i = 1; i < openSet.Count; i++)
                {
                    if (openSet[i].fCost < currentNode.fCost || (openSet[i].fCost == currentNode.fCost && openSet[i].hCost < currentNode.hCost))
                    {
                        currentNode = openSet[i];
                    }
                }

                openSet.Remove(currentNode);
                closedSet.Add(currentNode);

                if (currentNode == targetNode)
                {
                    openSet.Clear();
                    closedSet.Clear();
                    return RetracePath(startNode, targetNode);
                }

                foreach (Node neighbor in GetNeighbors(currentNode))
                {                    
                    if (closedSet.Contains(neighbor))
                    {
                        continue;
                    }

                    float newCostToNeighbor = currentNode.gCost + GetDistance(currentNode, neighbor);
                    if (newCostToNeighbor < neighbor.gCost || !openSet.Contains(neighbor))
                    {
                        neighbor.gCost = newCostToNeighbor;
                        neighbor.hCost = GetDistance(neighbor, targetNode);
                        neighbor.parent = currentNode;

                        if (!openSet.Contains(neighbor))
                        {
                            openSet.Add(neighbor);
                        }
                    }
                }
            }
            openSet.Clear();
            closedSet.Clear();
            return null;
        }

        private List<Node> RetracePath(Node startNode, Node endNode)
        {
            List<Node> path = new List<Node>();
            Node currentNode = endNode;

            while (currentNode != startNode)
            {
                path.Add(currentNode);
                currentNode = currentNode.parent;
            }

            path.Reverse();
            return path;
        }

        private Node GetNodeFromWorldPoint(Vector3 worldPos)
        {
            float percentX = Mathf.Clamp01((worldPos.x + gridWorldSize.x / 2) / gridWorldSize.x);
            float percentY = Mathf.Clamp01((worldPos.y + gridWorldSize.y / 2) / gridWorldSize.y);

            int x = Mathf.RoundToInt((gridSizeX - 1) * percentX);
            int y = Mathf.RoundToInt((gridSizeY - 1) * percentY);

            return grid[x, y];
        }

        private List<Node> GetNeighbors(Node node)
        {
            List<Node> neighbors = new List<Node>();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    if (x == 0 && y == 0)
                    {
                        continue;
                    }

                    int checkX = node.gridX + x;
                    int checkY = node.gridY + y;

                    if (checkX >= 0 && checkX < gridSizeX && checkY >= 0 && checkY < gridSizeY)
                    {
                        neighbors.Add(grid[checkX, checkY]);
                    }
                }
            }

            return neighbors;
        }

        private float GetDistance(Node nodeA, Node nodeB)
        {
            int distX = Mathf.Abs(nodeA.gridX - nodeB.gridX);
            int distY = Mathf.Abs(nodeA.gridY - nodeB.gridY);

            if (distX > distY)
            {
                return 14 * distY + 10 * (distX - distY);
            }

            return 14 * distX + 10 * (distY - distX);
        }
        public void RegisterPathfinder(PathfinderAgent unit)
        {
            _agents.AddLast(unit);
        }
    }
}