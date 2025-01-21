using System;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{

    Rigidbody rigidbody;

    [Header("Move")]
    [SerializeField] float normalMoveSpeed;
    [SerializeField] float sprintMoveSpeed;
    [SerializeField] KeyCode sprintKey = KeyCode.LeftShift;
    float moveSpeed;

    [Header("Drag")]
    float playerHeight;
    [SerializeField] LayerMask groundMask;
    [SerializeField] float groundDrag;
    bool grounded;

    [Header("Jump")]
    [SerializeField] float jumpForce;

    [SerializeField] KeyCode jumpKey;
    [SerializeField] float jumpCD;
    [SerializeField] float airMultiplayer;
    bool canJump;

    float horizontalInput, verticalInpuit;

    Vector3 moveDirection;

    bool input;

    void Awake()
    {
        playerHeight = 0.7f + transform.localScale.y / 2f;
        
        
        rigidbody = GetComponent<Rigidbody>();
        rigidbody.freezeRotation = true;
        
        
        grounded = true;
        canJump = true;
        moveSpeed = normalMoveSpeed;

        
        input = true;
    }
    void Start()
    {
        StopInputEvent.current.OnStopInput += ChangeInput;
    }
    void ChangeInput(GameObject obj)
    {
        if (obj != gameObject)
        {
            input = false;
            rigidbody.constraints = RigidbodyConstraints.FreezeAll;
        }
        if (obj == null)
        {
            input = true;
            rigidbody.constraints = RigidbodyConstraints.None;
            rigidbody.freezeRotation = true;
        }
    }

    void Update()
    {
        if (input == false)
        {
            rigidbody.linearVelocity = Vector3.zero;
            return;
        }
        
        KeyboardInput();

        grounded = Physics.Raycast(transform.position, Vector3.down, playerHeight, groundMask);
        DragChange();
        SpeedControl();

        switch (Input.GetKey(sprintKey))
        {

            case true:
                moveSpeed = sprintMoveSpeed;
                break;
            case false:
                moveSpeed = normalMoveSpeed;
                break;
        }
    }
    void FixedUpdate()
    {
        MovePlayer();
    }

    void KeyboardInput()
    {
        horizontalInput = Input.GetAxisRaw("Horizontal");
        verticalInpuit = Input.GetAxisRaw("Vertical");

        if (Input.GetKey(jumpKey) && canJump && grounded)
        {
            Debug.Log("Test");
            canJump = false;
            
            Jump();
            
            Invoke(nameof(ResetJump),jumpCD );
        }
    }
    

    void MovePlayer()
    {
        moveDirection = transform.forward * verticalInpuit + transform.right * horizontalInput;

        if (grounded)
        {
            rigidbody.AddForce(moveDirection.normalized * moveSpeed * 10f);
        }
        else if (!grounded)
        {
            rigidbody.AddForce(moveDirection.normalized * moveSpeed * 10f * airMultiplayer);
        }
        
    }

    void DragChange()
    {
        if (grounded)
        {
            rigidbody.linearDamping = groundDrag;
        }
        else
        {
            rigidbody.linearDamping = 0f;
        }
    }

    void SpeedControl()
    {
        Vector3 flatVel = new Vector3(rigidbody.linearVelocity.x, 0f, rigidbody.linearVelocity.z);

        if (flatVel.magnitude > moveSpeed)
        {
            Vector3 limitedVel = flatVel.normalized * moveSpeed;

            rigidbody.linearVelocity = new Vector3(limitedVel.x, rigidbody.linearVelocity.y, limitedVel.z);
        }
    }

    void Jump()
    {
        rigidbody.linearVelocity = new Vector3(rigidbody.linearVelocity.x, 0f, rigidbody.linearVelocity.z);

        rigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    void ResetJump()
    {
        canJump = true;
    }
    
}
