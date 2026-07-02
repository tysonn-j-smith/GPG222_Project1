using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
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

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    private void FixedUpdate()
    {
        if(rb == null)
        {
            return;
        }

        HandleMovement();
        HandleGravity();
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
        movementInput = input;
    }

    public void TryJump()
    {
        if(!IsGrounded())
        {
            return;
        }

        HandleJump();
    }

    private void HandleJump()
    {
        rb.AddForce(transform.up * jumpForce, ForceMode.Impulse);
    }

    private void HandleMovement()
    {
        Vector3 moveDir = (transform.forward * movementInput.y + transform.right * movementInput.x).normalized;
        Vector3 velocity = new Vector3(moveDir.x * moveForce, rb.linearVelocity.y, moveDir.z * moveForce);

        rb.linearVelocity = velocity;
    }

    private void HandleGravity()
    {
        if(IsGrounded())
        {
            return;
        }

        if(rb.linearVelocity.y < 0f)
        {
            float grav = gravity * gravityModifier;
            rb.AddForce(-transform.up * grav, ForceMode.Force);
        }
    }

    private bool IsGrounded()
    {
        return Physics.CheckSphere(groundChecker.position, groundCheckerRadius, groundMask);
    }
}
