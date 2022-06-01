using System.Collections.Generic;
using UnityEngine;
using Maths;

[CreateAssetMenu(menuName = "Stadaroj/Boid Behaviours/Flee")]
public class FleeBehaviour : FilteredFlockingBehaviour
{
    public override Vector3 UpdatePosition(FlockAgent agent, List<Transform> neighbours, Flock flock)
    {
        if (flock.targetPosition == null) { return Vector3.zero; } //Validate targetToSeek

        Vector3 distance = MathsOperations.Vector3Subtract(flock.targetPosition.transform.position, agent.transform.position); //Get the distance between the agent and its target position
        if (distance == Vector3.zero) { return distance; } //If the agent has reached its target

        float currentPosition = distance.magnitude;

        return distance * -MathsOperations.Square(currentPosition);
    }
}
