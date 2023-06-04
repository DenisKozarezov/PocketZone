using System.Collections.Generic;
using UnityEngine;

namespace Core.Services.AI
{
    public interface IPathfinderStrategy
    {
        List<Node> FindPath(Vector3 startPos, Vector3 targetPos);
        void RegisterPathfinder(PathfinderAgent unit);
    }
}