using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Filters a flock to check if it shares a layer with the selected layer
/// </summary>
[CreateAssetMenu(menuName = "Stadaroj/Filters/Physics Layer Filter")]
public class PhysicsLayerFilter : NeighboursFilter
{
    public LayerMask mask;
    public override List<Transform> Filter(FlockAgent agent, List<Transform> originalNeighbours)
    {
        List<Transform> filtered = new List<Transform>(); //Temporary list for filtered agents
        foreach (Transform neighbour in originalNeighbours) //For each neighbour in the original neighbours list
        {
            /**/
            if (mask == (mask | (1 << neighbour.gameObject.layer))) //Bitshifting and logic gate to check both things are on the same layer
            {
                filtered.Add(neighbour); //Add the neighbour to the list
            }
        }
        return filtered; //Return the filtered list
    }
}
