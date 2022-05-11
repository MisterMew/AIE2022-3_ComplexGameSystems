using System.Collections.Generic;
using UnityEngine;
using Maths;

/// <summary>
/// Restricts a flock from travelling too far from a desired position.
/// </summary>
[CreateAssetMenu(menuName = "Flock/Supporting Behaviours/Restrict Travel Distance")]
public class RestrictTravelDistance : FlockBehaviours
{
    /* Variables */
    [Tooltip("The transform which the flock is to orbit around.")]
    public GameObject orbitPoint;
    [Tooltip("How far from the orbit point the flock can travel.")]
    public float radiusFromCenter;

    public override Vector3 CalculatePosition(FlockAgent agent, List<Transform> neighbours, Flock flock)
    {
        Vector3 centerOffset = orbitPoint.transform.position - agent.transform.position;
        float positionFromBounds = centerOffset.magnitude / radiusFromCenter; //Position from the radius bounds
        
        if (positionFromBounds < 0.9F) //Within 90% of the radius bounds
        { 
            return Vector3.zero;
        }

        return centerOffset * MathsOperations.Square(positionFromBounds);
    }
}
