using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public CharacterController controller;
    public Transform cam;
    public Animator anim;

    public float speed;
    public float turnSmoothTime = 0.1f;
    public float acceleration = 1.1f;
    public float deceleration = 0.6f;
    float turnSmoothVelocity;
    float gravity = 9.81f;
    public float cooldownTime = 2f;
    public static int buttonPressCount = 0;
   
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

        }

        PlayerSprint();
        PlayerAttack();
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

    void PlayerAttack()
    {

        //check for attack 1
        if (anim.GetBool("attack1") == false)
        {
            if (Input.GetButtonDown("Attack"))
            {
                print("attack 1");
                anim.SetBool("attack1", true);
                anim.SetBool("attack2", false);

                return;
            }
        }

        //check for transition from 1 to 2

        if (anim.GetBool("attack1") == true)
        {
            if (anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.99f)
            {
                anim.SetBool("attack1", false);
            }



            if ( anim.GetCurrentAnimatorStateInfo(0).normalizedTime > 0.05f )
            {
                if (Input.GetButtonDown("Attack"))
                {
                    anim.SetBool("attack1", false);
                    anim.SetBool("attack2", true);

                    print("attack 2");


                }
            }
        }


            //print("animpos=" + anim.GetCurrentAnimatorStateInfo(0).normalizedTime);

        //if (anim.GetBool("attack1") == true )
        {

        }

    }

    
    
}
