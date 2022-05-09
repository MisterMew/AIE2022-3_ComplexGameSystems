using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(menuName = "Flock/Behaviour/Seperation")]
public class SeperationBehaviour : FlockBehaviour
{
    /// <summary>
    /// Override the flocking behaviour by applying seperation between every agent based on its neighbouring agents.
    /// </summary>
    /// <param name="agent">The current agent.</param>
    /// <param name="neighbours">List of agent's current neighbours.</param>
    /// <param name="flock"></param>
    /// <returns></returns>
    public override Vector3 CalculatePosition(FlockAgent agent, List<Transform> neighbours, Flock flock)
    {
        if (neighbours.Count == 0) { return Vector3.zero; } //Return zero if agent has no current neighbours

        Vector3 seperationSteer = Vector3.zero;
        int mSeperationCount = 0;
        foreach (Transform neighbour in neighbours) //For each neighbour in the list of neighbours
        {
            if (Vector3.SqrMagnitude(neighbour.position - agent.transform.position) < flock.SquareAvoidanceRadius) //Distance between agent and its neighbour
            { 
                mSeperationCount++; //Add to seperation count
                seperationSteer += agent.transform.position - neighbour.position; //Sum offset between the agent and its current neighbour
            }
        }
        
        if (mSeperationCount > 0)
        {
            seperationSteer /= mSeperationCount; //Average seperationSteer against count
        }

        return seperationSteer;
    }
}
