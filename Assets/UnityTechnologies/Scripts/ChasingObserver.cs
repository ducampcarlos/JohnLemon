using UnityEngine.AI;
using UnityEngine;
/// <summary>
/// Chasing Observer inherits from Observer but instead of ending the game, it chases the player.
/// </summary>
public class ChasingObserver : Observer
{
    public WaypointPatrol waypointPatrol; // Reference to the waypoint patrol script

    protected override void Start()
    {
        base.Start();
        waypointPatrol.player = player;
    }

    protected override void Update()
    {
        base.Update();

        if (m_IsPlayerVisible)
        {
            waypointPatrol.playerDetected = true;
        }
        else
        {
            waypointPatrol.playerDetected = false;
        }

        if (waypointPatrol.playerCaught)
        {
            gameEnding.CaughtPlayer();
        }
    }

    protected override void OnPlayerSpotted()
    {
        //gameEnding.CaughtPlayer();
    }

}
