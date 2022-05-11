using System.Collections.Generic;
using UnityEngine;
using Maths;

[CreateAssetMenu(menuName = "Flock/Flocking")]
public class FlockingBehaviour : FlockBehaviours
{
    public FlockBehaviours[] behaviours;
    public float[] coefs;

    public override Vector3 CalculatePosition(FlockAgent agent, List<Transform> neighbours, Flock flock)
    {
        /* Error Validation Check */
        if (coefs.Length != behaviours.Length) //Validate both arrays are equal
        {
            Debug.LogError("Data mismatch: " + name, this); //Log error to console
            return Vector3.zero; //Prevent agent movement
        }

        Vector3 flockMovement = Vector3.zero;
        for (int i = 0; i < behaviours.Length; i++) //Iterate through all behaviours
        {
            Vector3 partialMove = behaviours[i].CalculatePosition(agent, neighbours, flock) * coefs[i];

            if (partialMove != Vector3.zero) //If some movement exists
            {
                if (partialMove.sqrMagnitude > MathsOperations.Square(coefs[i])) //If the movement exceeds the current behaviours coeficient factor
                {
                    partialMove.Normalize();  //Normalise to magnitude of 1
                    partialMove *= coefs[i]; //Set to maximum
                }
                flockMovement += partialMove; //Move
            }
        }
        return flockMovement;
    }
}
