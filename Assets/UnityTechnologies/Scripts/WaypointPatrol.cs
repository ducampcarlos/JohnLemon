using UnityEngine;
using UnityEngine.AI;

public class WaypointPatrol : MonoBehaviour
{
    public NavMeshAgent navMeshAgent;
    public Transform[] waypoints;
    int m_CurrentWaypointIndex;
    [HideInInspector]
    public bool playerDetected;
    [HideInInspector]
    public bool playerCaught;
    [HideInInspector]
    public Transform player;

    [Header("Debug")]
    public bool ShowDebug;

    void Start()
    {
        navMeshAgent.SetDestination(waypoints[0].position);
    }


    void Update()
    {
        if (playerDetected)
        {
            OnPlayerSpotted();
        }
        else
        {
            Patrol();
        }
    }

    private void Patrol()
    {
        if (ShowDebug)
            Debug.Log("Patrolling");

        if(navMeshAgent.destination != waypoints[m_CurrentWaypointIndex].position)
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);

        if (navMeshAgent.remainingDistance < navMeshAgent.stoppingDistance)
        {
            m_CurrentWaypointIndex = (m_CurrentWaypointIndex + 1) % waypoints.Length;
            navMeshAgent.SetDestination(waypoints[m_CurrentWaypointIndex].position);
        }
    }

    void OnPlayerSpotted()
    {
        if (ShowDebug)
            Debug.Log("Chasing player");
        navMeshAgent.SetDestination(player.position);
        if (Vector3.Distance(transform.position, player.position) < navMeshAgent.stoppingDistance)
        {
            playerCaught = true;
        }
    }
}
