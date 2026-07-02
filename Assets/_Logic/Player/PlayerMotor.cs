using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class PlayerMotor : MonoBehaviour
{
    [Header("Motor Settings")]
    [SerializeField] private float moveForce = 10f;

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
    }

    public void SetInput(Vector2 input)
    {
        movementInput = input;
    }

    private void HandleMovement()
    {
        Vector3 moveDir = (transform.forward * movementInput.y + transform.right * movementInput.x).normalized;
        Vector3 velocity = new Vector3(moveDir.x * moveForce, rb.linearVelocity.y, moveDir.z * moveForce);

        rb.linearVelocity = velocity;
    }
}
