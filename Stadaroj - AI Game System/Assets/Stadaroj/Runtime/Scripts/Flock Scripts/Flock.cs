using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Attach this script to a gameObject where you would like the flock to spawn.
/// 
/// This script handles the flocking behaviours of agents within a scene.
/// </summary>
public class Flock : MonoBehaviour
{
    /* Variables */
    public FlockAgent agentPrefab;
    private List<FlockAgent> agents = new List<FlockAgent>();
    public FlockBehaviour behaviour;

    [Header("Agent Spawning")]
    [Range(0F, 500F)]
    public int agentSpawnCount = 10;
    const float agentDensity = 0.1F;

    [Header("Agent Behaviours")]
    [Range(0F, 100F)]
    public float acceleration = 10F;
    [Range(0F, 100F)]
    public float maxSpeed = 5F;
    [Range(1F, 10F)]
    public float perceptionRadius = 1.5F;
    [Range(0F, 1F)]
    public float avoidanceRadiusMultiplier = 0.5F;

    /* Utility Variables */
    private float sqrtMaxSpeed;
    private float sqrtPerceptionRadius;
    private float sqrtAvoidanceRadius;
    public float SquareAvoidanceRadius { get { return sqrtAvoidanceRadius; } }


    /// <summary>
    /// Start is called before the first frame update 
    /// </summary>
    void Start()
    {
        sqrtMaxSpeed = Magnitude(sqrtMaxSpeed);
        sqrtPerceptionRadius = Magnitude(sqrtPerceptionRadius);
        sqrtAvoidanceRadius = Magnitude(sqrtAvoidanceRadius);

        /* Spawn Agents */
        for (int i = 0; i < agentSpawnCount; i++)
        {
            FlockAgent newAgent = Instantiate(
                agentPrefab, 
                Random.insideUnitSphere * agentSpawnCount * agentDensity,    //Random position within a sphere
                Quaternion.Euler(Vector3.forward * Random.Range(0F, 360F)), //Random Rotation
                transform //Parent
                );

            newAgent.name = "Agent" + i;
            agents.Add(newAgent);
        }
    }

    /// <summary>
    /// Update is called once per frame
    /// </summary>
    void Update()
    {
        foreach(FlockAgent agent in agents) //Iterate through each agent in the list of agents
        {
            List<Transform> context = FindNearbyObjects(agent);

            //DEMO ONLY
            agent.GetComponentInChildren<Renderer>().material.color = Color.Lerp(Color.white, Color.red, context.Count / 6F);

            Vector3 move = behaviour.CalculatePosition(agent, context, this);
            move *= acceleration;
            if (move.sqrMagnitude > sqrtMaxSpeed)
            {
                move = move.normalized * maxSpeed; //Reset to be exactly at the maximum speed
            }
            agent.Move(move);
        }
    }

    /* Utility Functions */
    /// <summary>
    /// Find and get all nearby objects (agents and obstacles) for each individual agent.
    /// </summary>
    /// <param name="agent"></param>
    /// <returns></returns>
    private List<Transform> FindNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider[] contextColliders = Physics.OverlapSphere(agent.transform.position, perceptionRadius);

        foreach(Collider c in contextColliders) {
            if (c != agent.AgentCollider) //Saftey check for the current agents collider
            {
                context.Add(c.transform);
            }
        }
        return context;
    }

    /* Maths Functions */
    /// <summary>
    /// Calculate the square root of a floating point variable
    /// </summary>
    /// <param name="value">Target value to perform sqrt operation.</param>
    /// <returns>The magnitude of the pass  value.</returns>
    private float Magnitude(float value) { return value * value; }
}
