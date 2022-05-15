using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Filters all agents to decide if they belong to the same flock
/// </summary>
[CreateAssetMenu(menuName = "Stadaroj/Filters/Flock Group")]
public class FlockFilter : NeighboursFilter
{
    public override List<Transform> Filter(FlockAgent agent, List<Transform> originalNeighbours)
    {
        List<Transform> filtered = new List<Transform>(); //Temporary list for filtered agents
        foreach (Transform neighbour in originalNeighbours) //For each neighbour in the original neighbours list
        {
            FlockAgent currentNeighbour = neighbour.GetComponent<FlockAgent>();
            if (currentNeighbour != null && currentNeighbour.AgentFlock == agent.AgentFlock) //If the neighbour has a flock agent and is within the same flock as the current agent
            {
                filtered.Add(neighbour); //Add it to the list of filtered agents
            }
        }
        return filtered; //Return the filtered list
    }
}
