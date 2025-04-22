using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Animator anim;

    public float maximumSpeed;
    public float turnSmoothTime = 0.1f;
    float turnSmoothVelocity;
    float gravity = 9.81f;
    public float cooldownTime = 2f;
    public float buttonPressCount = 0;
   
    private void Start()
    {
        anim = GetComponent<Animator>();
    }

    private void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;
        float inputMagnitude = Mathf.Clamp01(direction.magnitude);
        float speed = inputMagnitude * maximumSpeed;
        gravity = Mathf.Clamp(gravity, 0f, 1f);

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(maximumSpeed * Time.deltaTime * moveDir.normalized);

        }

        if(Input.GetButton("Sprint") == false)
        {
            inputMagnitude /= 2;
            maximumSpeed = 1.75f;
        }
        else
        {
            maximumSpeed = 5f;
        }

        anim.SetFloat("Input Magnitude", inputMagnitude, 0.05f, Time.deltaTime);
    }
        
}
