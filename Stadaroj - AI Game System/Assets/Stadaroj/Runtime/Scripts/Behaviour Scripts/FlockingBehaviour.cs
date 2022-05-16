using System.Collections.Generic;
using UnityEngine;
using Maths;

/// <summary>
/// Behaviour that when given a list of transforms, combines the three boid rules of alignment, cohesion, and seperation. Flocking.
/// This method was developed by Craig Reynolds in 1986.
/// </summary>
[CreateAssetMenu(menuName = "Stadaroj/Flocking")]
public class FlockingBehaviour : FlockBehaviours
{
    public FlockBehaviours[] behaviours;
    public float[] BehaviourStrength;

    public override Vector3 CalculatePosition(FlockAgent agent, List<Transform> neighbours, Flock flock)
    {
        /* Error Validation Check */
        if (BehaviourStrength.Length != behaviours.Length) //Validate both arrays are equal
        {
            Debug.LogError("Data mismatch: " + name, this); //Log error to console
            return Vector3.zero; //Prevent agent movement
        }

        Vector3 flockMovement = Vector3.zero;
        for (int i = 0; i < behaviours.Length; i++) //Iterate through all behaviours
        {
            Vector3 partialMove = behaviours[i].CalculatePosition(agent, neighbours, flock) * BehaviourStrength[i];

            if (partialMove != Vector3.zero) //If some movement exists
            {
                if (partialMove.sqrMagnitude > MathsOperations.Square(BehaviourStrength[i])) //If the movement exceeds the current behaviours coeficient factor
                {
                    partialMove.Normalize();  //Normalise to magnitude of 1
                    partialMove *= BehaviourStrength[i]; //Set to maximum
                }
                flockMovement += partialMove; //Move
            }
        }
        return flockMovement;
    }
}
