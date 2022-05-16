using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Midle-man between filterd flock and flock behaviours
/// Flocking behaviour, but filtered
/// </summary>
public abstract class FilteredFlockingBehaviour : FlockBehaviours
{
    public NeighboursFilter filter;
}
