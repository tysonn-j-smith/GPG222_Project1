using UnityEngine;
using Unity.Netcode;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : NetworkBehaviour
{
    [Header("Motor Settings")]
    [SerializeField] private float moveForce = 10f;
    [SerializeField] private float jumpForce = 10f;
    [SerializeField] private float gravityModifier = 5f;
    [SerializeField] private float groundCheckerRadius = .25f;
    [SerializeField] private Transform groundChecker;
    [SerializeField] private LayerMask groundMask;

    private float gravity = 9.81f;

    private Vector2 movementInput;

    private Rigidbody rb;

    public NetworkVariable<Vector3> netPos = new(Vector3.zero, NetworkVariableReadPermission.Everyone, NetworkVariableWritePermission.Server);

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        rb.interpolation = RigidbodyInterpolation.Interpolate;
    }

    private void FixedUpdate()
    {
        if(!IsServer)
        {
            return;
        }

        HandleMovement();
        HandleGravity();

        netPos.Value = rb.position;
    }

    private void OnDrawGizmosSelected()
    {
        if(!IsGrounded())
        {
            Gizmos.color = Color.red;
        }
        else
        {
            Gizmos.color = Color.white;
        }

        Gizmos.DrawWireSphere(groundChecker.position, groundCheckerRadius);
    }

    public void SetInput(Vector2 input)
    {
        if (!IsOwner)
        {
            return;
        }

        SetInputServerRpc(input);
    }

    public void TryJump()
    {
        if(!IsOwner)
        {
            return;
        }

        TryJumpServerRpc();
    }

    private void HandleJump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void HandleMovement()
    {
        Vector3 moveDir = (transform.forward * movementInput.y + transform.right * movementInput.x).normalized;
        Vector3 velocity = new Vector3(moveDir.x * moveForce, rb.linearVelocity.y, moveDir.z * moveForce);

        UpdatePlayerMovementServerRpc(velocity);
    }

    private void HandleGravity()
    {
        if(IsGrounded())
        {
            return;
        }

        Vector3 vel = rb.linearVelocity;
        float gravityForce = gravity * gravityModifier * Time.fixedDeltaTime;
        vel.y -= gravityForce;

        rb.linearVelocity = vel;
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(groundChecker.position, groundCheckerRadius, groundMask);
    }

    #region RPC
    [Rpc(SendTo.Server)]
    public void SetInputServerRpc(Vector2 input)
    {
        movementInput = input;
    }

    // MOVEMENT
    [Rpc(SendTo.Server)]
    private void UpdatePlayerMovementServerRpc(Vector3 velocity)
    {
        Vector3 currentVel = rb.linearVelocity;
        rb.linearVelocity = new Vector3(velocity.x, currentVel.y, velocity.z);
    }

    // JUMP
    [Rpc(SendTo.Server)]
    private void TryJumpServerRpc()
    {
        if (!IsGrounded())
        {
            return;
        }

        HandleJump();
    }
    #endregion
}
