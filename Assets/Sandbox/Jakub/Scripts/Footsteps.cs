using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class PlayerFootsteps : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private EventReference footstepSound;

    [Header("Footstep Settings")]
    [SerializeField] private float baseStepInterval = 0.5f;
    [SerializeField] private float sprintMultiplier = 0.75f;
    [SerializeField] private LayerMask groundMask;

    private float stepTimer;
    private Rigidbody rb;

    void Start()
    {
        rb = playerMovement.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (IsMoving() && IsGrounded())
        {
            bool isSprinting = Input.GetKey(KeyCode.LeftShift); // Check sprint key
            float currentStepInterval = isSprinting
                ? baseStepInterval * sprintMultiplier
                : baseStepInterval;

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
        }
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
}