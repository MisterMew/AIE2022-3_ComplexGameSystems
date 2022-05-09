using UnityEngine;

[RequireComponent(typeof(Collider))]
public class FlockAgent : MonoBehaviour
{
    Collider agentCollider;
    public Collider AgentCollider { get { return agentCollider; } } //Public accessor for agentCollider


    /// <summary>
    /// Start is called before the first frame update 
    /// </summary>
    void Start()
    {
        agentCollider = GetComponent<Collider>(); //Cache the current agents collider
    }

    /// <summary>
    /// 
    /// </summary>
    /// <param name="velocity">Offset position for agent to move to.</param>
    public void Move(Vector3 velocity)
    {
        transform.forward = velocity; //Apply forward velocity
        transform.position += velocity * Time.deltaTime; //Apply constant movement regardless of framerate
    }
}
