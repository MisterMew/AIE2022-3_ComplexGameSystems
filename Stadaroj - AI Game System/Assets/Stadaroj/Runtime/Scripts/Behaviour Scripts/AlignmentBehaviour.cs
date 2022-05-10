using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Alignment")]
public class AlignmentBehaviour : FlockBehaviours
{
    /// <summary>
    /// Override the flocking behaviour by applying aligning movement to every agent based on its neighbouring agents.
    /// </summary>
    /// <param name="agent">The current agent.</param>
    /// <param name="neighbours">List of agent's current neighbours.</param>
    /// <param name="flock"></param>
    /// <returns></returns>
    public override Vector3 CalculatePosition(FlockAgent agent, List<Transform> neighbours, Flock flock)
    {
        if (neighbours.Count == 0) { return agent.transform.forward; } //Return agents current direction if no neighbours exist

        Vector3 alignmentPosition = Vector3.zero;
        foreach (Transform neighbour in neighbours) //For each neighbour in the list of neighbours
        {
            alignmentPosition += neighbour.transform.forward; //Sum the facing directions of current neighbour
        }
        alignmentPosition /= neighbours.Count;          //Divide alignmentPosition by amount of neighbours to get average (normalised magnitude of 1)

        return alignmentPosition;
    }
}
