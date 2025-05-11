using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FMODUnity;

public class Footsteps : MonoBehaviour
{
    [SerializeField] private PlayerMovement playerMovement;
    [SerializeField] private EventReference footstepSound;
    [SerializeField] private EventReference jumpSound;
    [SerializeField] private EventReference landSound;

    [SerializeField] private EventReference snowFootstepSound; 

    [Header("Footstep Settings")]
    [SerializeField] private float baseStepInterval = 0.5f;
    [SerializeField] private float sprintMultiplier = 0.75f;
    [SerializeField] private LayerMask groundMask;

    private float stepTimer;
    private Rigidbody rb;
    private bool wasGrounded;
    private bool footstepPlayedOnStartMove = false;
    private GameObject currentGroundObject;

    void Start()
    {
        rb = playerMovement.GetComponent<Rigidbody>();
    }

    void Update()
    {
        RaycastHit hitInfo;
        bool isGrounded = IsGrounded(out hitInfo); 
        
        if (isGrounded)
        {
            currentGroundObject = hitInfo.collider.gameObject;
        }
        else
        {
            currentGroundObject = null;
        }

        bool isMoving = IsMoving();
        bool isSprinting = Input.GetKey(KeyCode.LeftShift);

        float currentStepInterval = isSprinting ? baseStepInterval * sprintMultiplier : baseStepInterval;

        if (isMoving && isGrounded)
        {
            if (!footstepPlayedOnStartMove)
            {
                PlayFootstepSound();
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

        if (wasGrounded && !isGrounded && rb.linearVelocity.y > 1f)
        {
            PlayJumpSound();
        }

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

    bool IsGrounded(out RaycastHit hitInfo)
    {
        return Physics.Raycast(transform.position, Vector3.down, out hitInfo, 1.1f, groundMask);
    }

    void PlayFootstepSound()
    {
        if (currentGroundObject != null && currentGroundObject.CompareTag("Snow"))
        {
            RuntimeManager.PlayOneShotAttached(snowFootstepSound, gameObject);
        }
        else
        {
            RuntimeManager.PlayOneShotAttached(footstepSound, gameObject);
        }
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