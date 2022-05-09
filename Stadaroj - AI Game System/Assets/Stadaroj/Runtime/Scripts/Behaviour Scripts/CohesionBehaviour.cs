using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Calculates the average position of an agents neighbours, and tries to move to that position.
/// </summary>
[CreateAssetMenu(menuName = "Flock/Behaviour/Cohesion")]
public class CohesionBehaviour : FlockBehaviour
{
    /// <summary>
    /// Override the flocking behaviour by applying cohesive movement to every agent based on its neighbouring agents.
    /// </summary>
    /// <param name="agent">The current agent.</param>
    /// <param name="neighbours">List of agent's current neighbours.</param>
    /// <param name="flock"></param>
    /// <returns></returns>
    public override Vector3 CalculatePosition(FlockAgent agent, List<Transform> neighbours, Flock flock)
    {
        if (neighbours.Count == 0) { return Vector3.zero; } //Return zero if agent has no current neighbours

        Vector3 cohesionPosition = Vector3.zero;
        foreach (Transform neighbour in neighbours) //For each neighbour in the list of neighbours
        {
            cohesionPosition += neighbour.position; //Sum the position of the current neighbour 
        }
        cohesionPosition /= neighbours.Count;          //Divide cohesionPosition by amount of neighbours to get average
        cohesionPosition -= agent.transform.position; //Offset the cohesion position from the current agents position

        return cohesionPosition;
    }
}
