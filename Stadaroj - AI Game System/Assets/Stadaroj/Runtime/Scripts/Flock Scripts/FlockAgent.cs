using UnityEngine;

/// <summary>
/// Handles individual agents within a flock
/// </summary>
[RequireComponent(typeof(Collider))]
public class FlockAgent : MonoBehaviour
{
    /* Variables */
    Collider agentCollider;
    public Collider AgentCollider { get { return agentCollider; } } //Public accessor for agentCollider


    /* FUNCTIONS */
    /// <summary>
    /// Start is called before the first frame update 
    /// </summary>
    void Start()
    {
        agentCollider = GetComponent<Collider>(); //Cache the agents collider
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="velocity">Offset position for agent to move to.</param>
    public void Move(Vector3 velocity)
    {
        transform.forward = velocity;                     //Rotate transform to face the direction of velocity
        transform.position += velocity * Time.deltaTime; //Apply constant movement regardless of framerate
    }
}
