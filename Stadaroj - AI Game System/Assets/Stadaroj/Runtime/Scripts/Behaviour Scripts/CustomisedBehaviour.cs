using System.Collections.Generic;
using UnityEngine;
using Maths;

/// <summary>
/// Behaviour that when given a list of transforms, combines the three boid rules of alignment, cohesion, and seperation. Flocking.
/// This method was developed by Craig Reynolds in 1986.
/// </summary>
[CreateAssetMenu(menuName = "Stadaroj/Custom Behaviour")]
public class CustomisedBehaviour : FlockBehaviours
{
    public List<FlockBehaviours> behaviours;
    public List<float> weights;

    public override Vector3 CalculatePosition(FlockAgent agent, List<Transform> neighbours, Flock flock)
    {
        /* Error Validation Check */
        if (weights.Count != behaviours.Count) //Validate both arrays are equal
        {
            Debug.LogError("Data mismatch: " + name + " has more behaviours than data.", this); //Log error to console
            return Vector3.zero; //Prevent agent movement
        }

        Vector3 flockMovement = Vector3.zero;
        for (int i = 0; i < behaviours.Count; i++) //Iterate through all behaviours
        {
            Vector3 partialMove = behaviours[i].CalculatePosition(agent, neighbours, flock) * weights[i];

            if (partialMove != Vector3.zero) //If some movement exists
            {
                if (partialMove.sqrMagnitude > MathsOperations.Square(weights[i])) //If the movement exceeds the current behaviours coeficient factor
                {
                    partialMove.Normalize();  //Normalise to magnitude of 1
                    partialMove *= weights[i]; //Set to maximum
                }
                flockMovement += partialMove; //Move
            }
        }
        return flockMovement;
    }



    public void AddBehaviour()
    {
        behaviours.Add(null); //Adds empty object to end of list
        weights.Add(1F); //Adds empty object to end of list
    }

    public void RemoveBehaviour(int index)
    {
        behaviours.RemoveAt(index); //Adds empty object to end of list
        weights.RemoveAt(index); //Adds empty object to end of list
    }

    public void RemoveBehaviour()
    {
        RemoveBehaviour(behaviours.Count - 1);
    }

    public void AddWeight()
    {
        weights.Add(1F);
    }
}
