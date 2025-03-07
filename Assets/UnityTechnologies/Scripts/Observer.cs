using UnityEngine;
/// <summary>
/// Observer (enemy AI) that detects the player and triggers game over if seen.
/// </summary>
public class Observer : MonoBehaviour
{
    public Transform player; // Reference to the player
    public GameEnding gameEnding; // Reference to the game-ending script

    protected bool m_IsPlayerInRange; // Tracks if the player is within detection range
    protected PlayerMovement playerMovement; // Reference to the player's movement script
    protected bool m_IsPlayerVisible; // Tracks if the player is visible

    protected virtual void Start()
    {
        playerMovement = player.GetComponent<PlayerMovement>();
    }

    protected virtual void Update()
    {
        bool playerVisible = false;
        if (m_IsPlayerInRange && !playerMovement.IsInvisible())
        {
            Vector3 direction = player.position - transform.position + Vector3.up;
            Ray ray = new Ray(transform.position, direction);
            RaycastHit raycastHit;

            if (Physics.Raycast(ray, out raycastHit))
            {
                if (raycastHit.collider.transform == player)
                {
                    OnPlayerSpotted();
                    playerVisible = true;
                }
            }
        }
        m_IsPlayerVisible = playerVisible;
    }

    protected virtual void OnPlayerSpotted()
    {
        gameEnding.CaughtPlayer();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.transform == player)
        {
            m_IsPlayerInRange = false;
        }
    }
}
