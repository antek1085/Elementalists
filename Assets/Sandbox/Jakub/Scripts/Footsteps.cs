using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlayerFootsteps : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private EventReference footstepSound;
    [SerializeField] private EventReference jumpSound;
    [SerializeField] private EventReference landSound;

    [Header("Footstep Settings")]
    [SerializeField] private float baseStepInterval = 0.5f;
    [SerializeField] private float sprintMultiplier = 0.75f;
    [SerializeField] private LayerMask groundMask;

    private float stepTimer;
    private Rigidbody rb;
    private bool wasGrounded;
    private bool footstepPlayedOnStartMove = false;

    void Start()
    {
        rb = playerMovement.GetComponent<Rigidbody>();
    }

    void Update()
    {
        bool isGrounded = IsGrounded();
        bool isMoving = IsMoving();
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);

        float currentStepInterval = isSprinting ? baseStepInterval * sprintMultiplier : baseStepInterval;

        // Footsteps
        if (isMoving && isGrounded)
        {
            if (!footstepPlayedOnStartMove)
            {
                PlayFootstepSound(); // Play one step immediately when movement starts
                footstepPlayedOnStartMove = true;
                stepTimer = 0f;
            }

            stepTimer += Time.deltaTime;

            if (stepTimer >= currentStepInterval)
            {
                PlayFootstepSound();
                stepTimer = 0f;
            }
        }
        else
        {
            stepTimer = 0f;
            footstepPlayedOnStartMove = false;
        }

        // Jump detection
        if (wasGrounded && !isGrounded && rb.linearVelocity.y > 1f)
        {
            PlayJumpSound();
        }

        // Landing detection
        if (!wasGrounded && isGrounded && rb.linearVelocity.y < -1f)
        {
            PlayLandSound();
        }

        wasGrounded = isGrounded;
    }

    bool IsMoving()
    {
        return Mathf.Abs(Input.GetAxisRaw("Horizontal")) > 0 || Mathf.Abs(Input.GetAxisRaw("Vertical")) > 0;
    }

    bool IsGrounded()
    {
        return Physics.Raycast(transform.position, Vector3.down, 1.1f, groundMask);
    }

    void PlayFootstepSound()
    {
        RuntimeManager.PlayOneShotAttached(footstepSound, gameObject);
    }

    void PlayJumpSound()
    {
        RuntimeManager.PlayOneShotAttached(jumpSound, gameObject);
    }

    void PlayLandSound()
    {
        RuntimeManager.PlayOneShotAttached(landSound, gameObject);
    }
}
