using System.Collections.Generic;
using UnityEngine;

public abstract class FlockBehaviour : ScriptableObject 
{
    /// <summary>
    /// 
    /// </summary>
    /// <param name="agent">The current (individual) flock agent. </param>
    /// <param name="transform">List of transforms for all of the current agents neighbours and/or obstacles.</param>
    /// <param name="flock">The entire flock.</param>
    /// <returns></returns>
    public abstract Vector3 CalculateMove(FlockAgent agent, List<Transform> transform, Flocking flock);
}
