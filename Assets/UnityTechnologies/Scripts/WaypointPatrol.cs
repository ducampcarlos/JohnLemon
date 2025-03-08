using UnityEngine;
using UnityEngine.AI;
using static UnityEngine.Rendering.DebugUI;

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

    [Header("Control Material Color")]
    [SerializeField] Material m_Material;
    [SerializeField] Color m_DefaultColor;
    [SerializeField] Color m_FollowingColor;

    [Header("Debug")]
    public bool ShowDebug;

    void Start()
    {
        m_Material = GetComponentInChildren<Renderer>().material;
        navMeshAgent.SetDestination(waypoints[0].position);
    }


    void Update()
    {
        if (playerDetected)
        {
            OnPlayerSpotted();
            SetMaterialColor(m_FollowingColor);
        }
        else
        {
            Patrol();
            SetMaterialColor(m_DefaultColor);
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

    void SetMaterialColor(Color c)
    {
        m_Material.SetColor("_Color", c);
    }
}
