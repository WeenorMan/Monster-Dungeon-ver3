using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Animator anim;

    public float speed;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    float yVelocity = 5f;
    float gravity = 9.81f;

    private void Start()
    {
        anim = GetComponent<Animator>();    
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        gravity = Mathf.Clamp(gravity, 0f, 1f);

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
            anim.SetBool("isWalking", true);
        }
        else
        {
            anim.SetBool("isWalking", false);
        }

        PlayerSprint();
    }

    void PlayerSprint()
    {
        if (Input.GetButton("Sprint"))
        {
            print("SPRINT");
            anim.SetBool("isRunning", true);
            speed = 7f;
        }
        else
        {
            anim.SetBool("isRunning", false);
            speed = 2f;
        }

    }
}
