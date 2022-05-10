using System.Collections.Generic;
using UnityEngine;
using Maths;

[CreateAssetMenu(menuName = "Flock/Behaviour/Seperation")]
public class SeperationBehaviour : FlockBehaviours
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
        /* Safety Check */
        if (neighbours.Count == 0) { return Vector3.zero; } //Return zero if agent has no current neighbours

        /* Formula */
        Vector3 seperationSteer = Vector3.zero;

        int mSeperationCount = 0;
        foreach (Transform neighbour in neighbours) //For each neighbour in the list of neighbours
        {
            float distanceToNeighbour = Vector3.SqrMagnitude(neighbour.position - agent.transform.position); //Calculate the distance between agent and the current neighbour
            if (distanceToNeighbour < flock.mPerceptionRadius && distanceToNeighbour > 0.001F)              //If the current neighbour is within the current agents perception radius
            {
                /* Seperation */
                Vector3 direction = Vector3.zero;
                direction = agent.transform.position - neighbour.position;       //Offset of agents position and neighbours position
                direction = Vector3.Normalize(direction) / distanceToNeighbour; //Calculate direction by dividing the normalised offset by the distance

                seperationSteer += direction; //Compound the sum of the direction and seperationSteering
                mSeperationCount++; //Add to seperation count
            }
        }
        
        if (mSeperationCount > 0)
        {
            seperationSteer /= (float)mSeperationCount; //Average seperationSteer against count
        }

        if (Vector3.Magnitude(seperationSteer) > 0)
        {
            seperationSteer = MathsOperations.Vector3Scale(Vector3.Normalize(seperationSteer), flock.getMaxSpeed);
            //seperationSteer = MathsOperations.Vector3Subtract(seperationSteer, /*AGENTS VELOCITY HERE*/);
        }

        return seperationSteer;
    }
}
