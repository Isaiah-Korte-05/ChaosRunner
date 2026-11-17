using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;
public class PlayerController : MonoBehaviour
{

    public float[] lanes = { -3f, 0f, 3f };
    private int currentLaneIndex = 1;
    public float laneChangeSpeed = 15f;

    public float targetJumpHeight = 3f;
    public float timeToApex = 0.35f;
    public float fallMultiplier = 2.2f;
    public LayerMask groundLayer;
    public Transform groundCheck;
    public float groundCheckRadius = 0.2f;

    private Rigidbody rb;
    private bool isGrounded;

    private float gravity;
    private float initialJumpVelocity;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

        rb.useGravity = false;

        Vector3 pos = transform.position;
        pos.x = lanes[currentLaneIndex];
        transform.position = pos;

        CalculateJumpPhysics();
    }

    private void OnValidate()
    {
        CalculateJumpPhysics();
    }

    void CalculateJumpPhysics()
    {
        if (timeToApex > 0f)
        {
            gravity = (-2f * targetJumpHeight) / (timeToApex * timeToApex);
            initialJumpVelocity = (2f * targetJumpHeight) / timeToApex;
        }
    }

    void Update()
    {
        if (Keyboard.current.aKey.wasPressedThisFrame || Keyboard.current.leftArrowKey.wasPressedThisFrame) { ChangeLane(-1); }
        if (Keyboard.current.dKey.wasPressedThisFrame || Keyboard.current.rightArrowKey.wasPressedThisFrame) { ChangeLane(1); }

        isGrounded = Physics.CheckSphere(groundCheck.position, groundCheckRadius, groundLayer);

        if(Keyboard.current.spaceKey.wasPressedThisFrame && isGrounded) { Jump(); }
    }

    void FixedUpdate()
    {
        float targetX = lanes[currentLaneIndex];
        float xDifference = targetX - transform.position.x;
        float targetXVelocity = xDifference * laneChangeSpeed;

        float currentGravity = gravity;

        if (rb.linearVelocity.y < 0f) { currentGravity *= fallMultiplier; }

        float newVelocityY = rb.linearVelocity.y + (currentGravity * Time.fixedDeltaTime);

        rb.linearVelocity = new Vector3(targetXVelocity, newVelocityY, 0f);
    }

    void ChangeLane(int direction)
    {
        currentLaneIndex += direction;
        currentLaneIndex = Mathf.Clamp(currentLaneIndex, 0, lanes.Length - 1);
    }

    void Jump()
    {
        rb.linearVelocity = new Vector3(rb.linearVelocity.x, initialJumpVelocity, rb.linearVelocity.z);
    }
}