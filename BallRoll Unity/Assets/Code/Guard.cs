using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI; 

public class Guard : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public AIWaypoint firstWaypoint;
    Transform player;

    public Light spotlight;
    public float viewDistance;
    public LayerMask viewMask;
    float viewAngle;

    bool startedGameOver = false;

    void Start()
    {
        navMeshAgent.SetDestination(firstWaypoint.transform.position);
        player = GameObject.FindGameObjectWithTag("Player").transform;
        viewAngle = spotlight.spotAngle;
    }

    void Update()
    {
        if(CanSeePlayer())
        {
            spotlight.color = Color.red;
            navMeshAgent.speed = 8;
            navMeshAgent.SetDestination(player.position);

            if (!startedGameOver)
            {
                GameManager.Instance.GameOver();
                startedGameOver = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<AIWaypoint>())
        {
            navMeshAgent.SetDestination(other.GetComponent<AIWaypoint>().nextWaypoint.transform.position);
        }
    }

    // Determines if the player is within the guard's FOV
    bool CanSeePlayer()
    {
        if(Vector3.Distance(transform.position, player.position) < viewDistance) // Player is in viewing distance
        {
            Vector3 directionToPlayer = (player.position - transform.position).normalized;
            float angleBetweenGuardAndPlayer = Vector3.Angle(transform.forward, directionToPlayer);
            if(angleBetweenGuardAndPlayer < viewAngle/2f) // Player is in viewing angle
            {
                if(!Physics.Linecast(transform.position, player.position, viewMask)) // Nothing is in the way of player
                {
                    return true; 
                }
            }
        }
        return false;
    }
}
