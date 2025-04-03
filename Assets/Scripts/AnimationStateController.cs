using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator anim;
    float velocityZ = 0.0f;
    float velocityX = 0.0f;
    public float acceleration = 2f;
    public float deceleration = 2f;
    float runSpeed = 0.0f;
    int velocityHashZ;
    int velocityHashX;
    void Start()
    {
        anim = GetComponent<Animator>();

        velocityHashX = Animator.StringToHash("velocityX");
        velocityHashZ = Animator.StringToHash("velocityZ");
    }

    // Update is called once per frame
    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        bool runPressed = Input.GetButton("Sprint");
        bool isMovingLeft = horizontal < -0.5f;
        bool isMovingRight = horizontal > 0.5f;
        bool isMovingForward = vertical > 0.5f;
        bool isMovingBack = vertical < -0.5f;

        if (isMovingForward && velocityZ < 0.5f && !runPressed)
        {
            velocityZ += Time.deltaTime * acceleration;
            print("forward");
        }

        if (isMovingBack && velocityZ > -0.5f && !runPressed)
        {
            print("backwards");
            velocityZ -= Time.deltaTime * acceleration;
        }

        if (isMovingLeft && velocityX > -0.5f && !runPressed)
        {
            print("left");
            velocityX -= Time.deltaTime * acceleration;
        }

        if (isMovingRight && velocityX < 0.5f && !runPressed)
        {
            print("right");
            velocityX += Time.deltaTime * acceleration;
        }

        anim.SetFloat("vx", horizontal);
        anim.SetFloat("vz", vertical);
    }
}
