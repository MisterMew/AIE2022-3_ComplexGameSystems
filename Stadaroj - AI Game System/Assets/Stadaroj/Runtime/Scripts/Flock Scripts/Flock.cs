using System.Collections.Generic;
using UnityEngine;
using Maths;

/// <summary>
/// Attach this script to a gameObject where you would like the flock to spawn.
/// 
/// This script handles the flocking behaviours of agents within a scene.
/// </summary>
public class Flock : MonoBehaviour
{
    /* Variables */
    private List<FlockAgent> agents = new List<FlockAgent>();
    public FlockAgent agentPrefab;
    public FlockBehaviours behaviour;

    [Header("Spawning")]
    [SerializeField] private int agentsToSpawn;
    [SerializeField] private Vector3 spawnBounds;
    const float agentDensity = 0.1F;

    [Header("Behaviours")]
    [SerializeField] private float acceleration = 10F;
    [SerializeField] private float maxSpeed = 5F;
    [SerializeField] private float perceptionRadius = 1.5F;
    [SerializeField] private float avoidanceRadius = 0.5F;

    /* Utility Variables */
    public float getMaxSpeed { get { return maxSpeed; } }
    public float mPerceptionRadius { get { return perceptionRadius; } }
    public float mAvoidanceRadius { get { return avoidanceRadius; } }


    /// <summary>
    /// Start is called before the first frame update 
    /// </summary>
    void Start()
    {
        SpawnFlock(); //Spawn agents in scene
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
            if (move.sqrMagnitude > maxSpeed)
            {
                move = move.normalized * maxSpeed; //Reset to be exactly at the maximum speed
            }
            agent.Move(move);
        }
    }

    /* Utility Functions */
    private void SpawnFlock()
    {
        for (int i = 0; i < agentsToSpawn; i++)
        {
            //Random Position
            Vector3 randomVector = Random.insideUnitSphere; //Within the bounds of a sphere
            randomVector = new Vector3(randomVector.x * spawnBounds.x, randomVector.y * spawnBounds.y, randomVector.z * spawnBounds.z); //Calculate a random position within the given bounds

            Vector3 spawnPosition = transform.position + randomVector; //Sum the position with the random position

            //Rotation
            //Quaternion rotation = Quaternion.Euler(0, Random.Range(0F, 360F), 0); //Create a random Rotation
            Quaternion rotation = Quaternion.Euler(Vector3.forward * Random.Range(0F, 360F)); //Create a random Rotation

            //Instantiation
            FlockAgent newAgent = Instantiate(agentPrefab, spawnPosition, rotation, transform);

            newAgent.name = "Agent" + i;
            agents.Add(newAgent);
        }
    }

    /// <summary>
    /// Find and get all nearby objects (agents and obstacles) for each individual agent.
    /// </summary>
    /// <param name="agent"></param>
    /// <returns></returns>
    private List<Transform> FindNearbyObjects(FlockAgent agent)
    {
        List<Transform> context = new List<Transform>();
        Collider[] neighbourColliders = Physics.OverlapSphere(agent.transform.position, MathsOperations.Square(perceptionRadius));

        foreach (Collider nc in neighbourColliders) {
            if (nc != agent.AgentCollider) //Saftey check for the current agents collider
            {
                float distanceToNeighbour = Vector3.SqrMagnitude(nc.transform.position - agent.transform.position);
                if (distanceToNeighbour <= perceptionRadius)
                {
                    context.Add(nc.transform);
                }
            }
        }
        return context;
    }
}
