using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/// <summary>
/// Handles player movement, animations, audio, and invisibility mechanics.
/// </summary>
public class PlayerMovement : MonoBehaviour
{
    public float turnSpeed = 20f; // Speed of player rotation
    public float maxEnergy = 5f; // Maximum energy for invisibility
    public float energyDrainRate = 1f; // Rate at which energy depletes
    public float energyRechargeRate = 0.5f; // Rate at which energy recharges
    public float cooldownDuration = 1f; // Cooldown time before invisibility can be reactivated
    public Slider energyBar; // UI Slider to represent energy
    public ParticleSystem invisibilityEffect; // Particle effect when turning invisible
    public GameObject invisibilityMarker; // Marker to indicate the player's position while invisible
    public Renderer playerRenderer; // Player's renderer to toggle visibility

    private Animator m_Animator; // Animator component for controlling animations
    private Rigidbody m_Rigidbody; // Rigidbody for physics-based movement
    private AudioSource m_AudioSource; // AudioSource for footstep sounds
    private Vector3 m_Movement; // Stores movement input
    private Quaternion m_Rotation = Quaternion.identity; // Stores rotation direction
    private float currentEnergy; // Current energy level
    private bool isInvisible = false; // Determines if the player is invisible
    private bool canUseInvisibility = true; // Determines if invisibility can be activated

    void Start()
    {
        m_Animator = GetComponent<Animator>();
        m_Rigidbody = GetComponent<Rigidbody>();
        m_AudioSource = GetComponent<AudioSource>();

        currentEnergy = maxEnergy;
        energyBar.maxValue = maxEnergy;
        energyBar.value = currentEnergy;

        invisibilityMarker.SetActive(false); // Hide marker initially
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxis("Horizontal");
        float vertical = Input.GetAxis("Vertical");

        m_Movement.Set(horizontal, 0f, vertical);
        m_Movement.Normalize();

        bool hasHorizontalInput = !Mathf.Approximately(horizontal, 0f);
        bool hasVerticalInput = !Mathf.Approximately(vertical, 0f);
        bool isWalking = hasHorizontalInput || hasVerticalInput;
        m_Animator.SetBool("IsWalking", isWalking);

        if (isWalking && !m_AudioSource.isPlaying)
        {
            m_AudioSource.Play();
        }
        else if (!isWalking)
        {
            m_AudioSource.Stop();
        }

        Vector3 desiredForward = Vector3.RotateTowards(transform.forward, m_Movement, turnSpeed * Time.deltaTime, 0f);
        m_Rotation = Quaternion.LookRotation(desiredForward);
    }

    void Update()
    {
        HandleInvisibility();
    }

    void OnAnimatorMove()
    {
        // Moves the character based on root motion from animations
        m_Rigidbody.MovePosition(m_Rigidbody.position + m_Movement * m_Animator.deltaPosition.magnitude);
        m_Rigidbody.MoveRotation(m_Rotation);
    }

    /// <summary>
    /// Handles the invisibility mechanic, including energy consumption, cooldown, and UI updates.
    /// </summary>
    void HandleInvisibility()
    {
        if (Input.GetKey(KeyCode.LeftShift) && currentEnergy > 0 && canUseInvisibility)
        {
            if (!isInvisible)
            {
                invisibilityEffect.Play(); // Play invisibility effect
            }
            isInvisible = true;
            playerRenderer.enabled = false; // Make player invisible
            invisibilityMarker.SetActive(true); // Show marker
            currentEnergy -= energyDrainRate * Time.deltaTime;
        }
        else
        {
            if (isInvisible)
            {
                StartCoroutine(InvisibilityCooldown()); // Start cooldown when invisibility is turned off
            }
            isInvisible = false;
            playerRenderer.enabled = true; // Make player visible
            invisibilityMarker.SetActive(false); // Hide marker
            if (currentEnergy < maxEnergy)
            {
                currentEnergy += energyRechargeRate * Time.deltaTime;
            }
        }

        energyBar.value = currentEnergy; // Update UI energy bar
    }

    /// <summary>
    /// Coroutine to handle cooldown after invisibility is turned off.
    /// </summary>
    private IEnumerator InvisibilityCooldown()
    {
        canUseInvisibility = false;
        yield return new WaitForSeconds(cooldownDuration);
        canUseInvisibility = true;
    }

    /// <summary>
    /// Returns whether the player is currently invisible.
    /// </summary>
    public bool IsInvisible()
    {
        return isInvisible;
    }
}