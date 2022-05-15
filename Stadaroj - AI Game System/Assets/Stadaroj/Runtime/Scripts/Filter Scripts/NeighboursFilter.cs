using System.Collections.Generic;
using UnityEngine;

public abstract class NeighboursFilter : ScriptableObject
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="agent">Agent to make comparisons to filter against.</param>
    /// <param name="originalNeighbours">The original list of neighbour transforms.</param>
    /// <returns></returns>
    public abstract List<Transform> Filter(FlockAgent agent, List<Transform> originalNeighbours);
}
