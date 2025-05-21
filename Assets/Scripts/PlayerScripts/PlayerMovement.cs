using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Animator anim;

    public float maximumSpeed = 5f;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;

    float gravity = -9.81f;
    Vector3 velocity;

    public float groundCheckDistance = 0.1f;
    public LayerMask groundMask;

    public float rollSpeed = 10f;
    public float rollDuration = 0.5f;
    public float iFrameDuration = 0.4f;
    private bool isRolling = false;
    private bool isInvincible = false;
    private float rollTimer = 0f;
    private Vector3 rollDirection;

    private void Update()
    {
        if (!isRolling)
        {
            Move();
            ApplyGravity();

            if (Input.GetButtonDown("Roll") && controller.isGrounded)
            {
                StartRoll();
            }
        }
        else
        {
            Roll();
        }
    }

    private void Move()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        float inputMagnitude = Mathf.Clamp01(direction.magnitude);
        float speed = inputMagnitude * maximumSpeed;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }

        if (Input.GetButton("Sprint"))
        {
            maximumSpeed = 5f;
        }
        else
        {
            maximumSpeed = 1.75f;
            inputMagnitude /= 2f;
        }

        anim.SetFloat("Input Magnitude", inputMagnitude, 0.05f, Time.deltaTime);
    }

    private void ApplyGravity()
    {
        bool isGrounded = controller.isGrounded;

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f; 
        }

        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
    }

    private void StartRoll()
    {
        isRolling = true;
        isInvincible = true;
        rollTimer = 0f;

        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 inputDir = new Vector3(horizontal, 0f, vertical).normalized;
        if (inputDir.magnitude < 0.1f)
        {
            inputDir = transform.forward;
        }
        else
        {
            float targetAngle = Mathf.Atan2(inputDir.x, inputDir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            inputDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }
        rollDirection = inputDir;

        if (anim != null)
        {
            anim.SetTrigger("Roll");
        }
    }

    private void Roll()
    {
        rollTimer += Time.deltaTime;

        // Move the player in the roll direction
        controller.Move(rollDirection * rollSpeed * Time.deltaTime);

        // Handle i-frames
        if (rollTimer >= iFrameDuration)
        {
            isInvincible = false;
        }

        // End roll
        if (rollTimer >= rollDuration)
        {
            isRolling = false;
            isInvincible = false;
        }
    }

    public bool IsInvincible()
    {
        return isInvincible;
    }
}

