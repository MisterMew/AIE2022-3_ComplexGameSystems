using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Behaviour that when given a list of transforms, applies the boid alignment rule to each. 
/// Alignment causes every agent to steer towards the average heading of local flockmates.
/// </summary>
[CreateAssetMenu(menuName = "Stadaroj/Boid Behaviours/Alignment")]
public class AlignmentBehaviour : FilteredFlockingBehaviour
{
    /// <summary>
    /// Override the flocking behaviour by applying aligning movement to every agent based on its neighbouring agents.
    /// </summary>
    /// <param name="agent">The current agent.</param>
    /// <param name="neighbours">List of agent's current neighbours.</param>
    /// <param name="flock"></param>
    /// <returns></returns>
    public override Vector3 UpdatePosition(FlockAgent agent, List<Transform> neighbours, Flock flock)
    {
        /* Safety Check */
        if (neighbours.Count == 0) { return agent.transform.forward; } //Return agents current direction if no neighbours exist

        /* Calculate Alignment */
        Vector3 alignmentPosition = Vector3.zero;
        List<Transform> filteredNeighbours = (filter == null) ? neighbours : filter.Filter(agent, neighbours); //Decide whether or not to use the filtered flock 
        foreach (Transform neighbour in filteredNeighbours) //For each neighbour in the list of neighbours
        {
            alignmentPosition += neighbour.transform.forward; //Sum the facing directions of current neighbour
        }
        alignmentPosition /= neighbours.Count;          //Divide alignmentPosition by amount of neighbours to get average (normalised magnitude of 1)

        /* Return */
        return alignmentPosition * flock.alignmentCoeficient;
    }
}
