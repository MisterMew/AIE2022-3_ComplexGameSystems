using System.Collections.Generic;
using UnityEngine;
using Maths;

/// <summary>
/// Restricts a flock from travelling too far from a desired position.
/// </summary>
[CreateAssetMenu(menuName = "Stadaroj/Supporting Behaviours/Restrict Travel Distance")]
public class RestrictTravelDistance : FilteredFlockingBehaviour
{
    public override Vector3 UpdatePosition(FlockAgent agent, List<Transform> neighbours, Flock flock)
    {
        Vector3 distanceFromCentre = MathsOperations.Vector3Subtract(flock.homePosition.transform.position, agent.transform.position); //Get the distance between the agent and its orbit center
        float positionFromBounds = distanceFromCentre.magnitude / flock.distanceFromHome; //Get the position from the radius bounds
        
        if (positionFromBounds < flock.radiusPadding) { return Vector3.zero; } //Validate position with padding

        return distanceFromCentre * MathsOperations.Square(positionFromBounds);
    }
}
