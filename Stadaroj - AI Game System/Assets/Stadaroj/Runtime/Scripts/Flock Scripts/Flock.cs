using System.Collections.Generic;
using UnityEngine;
using Maths;

/// <summary>
/// Attach this script to a gameObject where you would like the flock to spawn.
/// 
/// Handles the agents of a flock including:
/// - Spawning a user-defined amount in a desired radius at the position of the gameobject this script is attached to.
/// - Checking every agent against the others within a flock and Updating their positions and behaviours
/// </summary>
public class Flock : MonoBehaviour 
{
    /* Variables */
    private List<FlockAgent> agents = new List<FlockAgent>();

    [Header("Agents")]
    [Tooltip("Prefab of the desired agent (must have 'FlockAgent' script attached)")]
    public FlockAgent agentPrefab;

    [Tooltip("HINT: Create a 'Custom Behaviour' in order to use a combination of behaviours")]
    public FlockBehaviours behaviour;


    [Header("Spawning")]
    [SerializeField] private int agentsToSpawn;

    [Tooltip("Spawns within this radius at this gameobjects transform.")]
    [SerializeField] private Vector3 spawnBounds;


    /* Variables */
    [Header("Agent Stats")]
    [SerializeField] private float acceleration = 0F;
    [SerializeField] private float perceptionRadius = 0F;
    [SerializeField] private float maxSpeed = 0F;
    private Vector3 velocity = Vector3.zero;

    /* Utility Variables */
    public float GetAcceleration { get { return acceleration; } private set { } }
    public float GetPerceptionRadius { get { return perceptionRadius; } private set { } }
    public float GetMaxSpeed { get { return maxSpeed; } }
    public Vector3 GetVelocity { get { return velocity; } private set { } }


    [Header("Behaviour Coeficients")]
    public float seperationCoeficient = 1F;
    public float alignmentCoeficient = 1F;
    public float cohesionCoeficient = 1F;

    [Header("Flock Positions")]
    [Tooltip("The transform the flock will remain nearby.")]
    public GameObject homePosition = null;

    [Tooltip("The distance away from the home position, that the flock can travel.")]
    public float distanceFromHome;

    [Tooltip("The percentage of leway within the radius' boundraries. Default: (0.9F = 90%)")]
    public float radiusPadding = 0.9F;

    [Header("Targeting")]
    [Tooltip("The transform the flock will target when seeking or fleeing.")]
    public GameObject targetPosition = null;


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

            velocity = behaviour.UpdatePosition(agent, context, this);
            velocity *= GetAcceleration;
            if (velocity.sqrMagnitude > GetMaxSpeed)
            {
                velocity = velocity.normalized * GetMaxSpeed; //Reset to be exactly at the maximum speed
            }
            agent.Move(velocity);
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
            Quaternion rotation = Quaternion.Euler(Vector3.forward * Random.Range(0F, 360F)); //Create a random Rotation

            //Instantiation
            FlockAgent newAgent = Instantiate(agentPrefab, spawnPosition, rotation, transform);

            newAgent.name = "Agent" + i;
            newAgent.Initialise(this);
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
        Collider[] neighbourColliders = Physics.OverlapSphere(agent.transform.position, MathsOperations.Square(GetPerceptionRadius));

        foreach (Collider nc in neighbourColliders) {
            if (nc != agent.AgentCollider) //Saftey check for the current agents collider
            {
                float distanceToNeighbour = Vector3.SqrMagnitude(nc.transform.position - agent.transform.position);
                if (distanceToNeighbour <= GetPerceptionRadius)
                {
                    context.Add(nc.transform);
                }
            }
        }
        return context;
    }
}
