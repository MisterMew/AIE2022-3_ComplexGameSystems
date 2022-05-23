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
    public FlockBehaviours[] behaviours;
    public float[] weights;

    public override Vector3 CalculatePosition(FlockAgent agent, List<Transform> neighbours, Flock flock)
    {
        /* Error Validation Check */
        if (weights.Length != behaviours.Length) //Validate both arrays are equal
        {
            Debug.LogError("Data mismatch: " + name + " has more behaviours than data.", this); //Log error to console
            return Vector3.zero; //Prevent agent movement
        }

        Vector3 flockMovement = Vector3.zero;
        for (int i = 0; i < behaviours.Length; i++) //Iterate through all behaviours
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
        int oldCount = (behaviours != null) ? behaviours.Length : 0;
        FlockBehaviours[] newBehaviors = new FlockBehaviours[oldCount + 1];
        float[] newWeights = new float[oldCount + 1];

        for (int i = 0; i < oldCount; i++)
        {
            newBehaviors[i] = behaviours[i];
            newWeights[i] = weights[i];
        }

        newWeights[oldCount] = 1f;
        behaviours = newBehaviors;
        weights = newWeights;
    }

    public void RemoveBehaviour()
    {
        int oldCount = behaviours.Length;
        if (oldCount == 1)
        {
            behaviours = null;
            weights = null;
            return;
        }

        FlockBehaviours[] newBehaviours = new FlockBehaviours[oldCount - 1];
        float[] newWeights = new float[oldCount - 1];

        for (int i = 0; i < oldCount - 1; i++)
        {
            newBehaviours[i] = behaviours[i]; //Remove the behaviour from the list
            newWeights[i] = weights[i]; //Remove the modifier from the list
        }

        behaviours = newBehaviours;
        weights = newWeights;
    }

    public void AddWeight()
    {
        float[] newWeights = new float[behaviours.Length]; //New list of weigths

        if (weights != null) //Validate current weights list isn't empty
        {
            for (int i = 0; i < weights.Length; i++)
            {
                if (weights[i] != 0F) //If the current weight is not 0
                {
                    newWeights[i] = weights[i]; //Add it to the list of new weights
                }
            }
        }

        weights = newWeights;
    }
}
