using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Used to manage and instantiate other behaviours, and apply them to the flock.
/// </summary>
public abstract class FlockBehaviours : ScriptableObject 
{
    /// <summary>
    /// Empty Abstract function to calculate the position of an agent.
    /// </summary>
    /// <param name="agent">The current (individual) flock agent.</param>
    /// <param name="neighbours">List of neighbours for all of the current agents neighbours and/or obstacles.</param>
    /// <param name="flock">The entire flock.</param>
    /// <returns></returns>
    public abstract Vector3 UpdatePosition(FlockAgent agent, List<Transform> neighbours, Flock flock);
}
