using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    enum EnemyStates
    {
        Idle,
        Patrol,
        Attack,
        Follow,
        Dead
    }

    EnemyStates state;
    bool inFollowZone;

    Rigidbody rb;
    Animator anim;

    public GameObject player;

    Health health;


    void Start()
    {
        state = EnemyStates.Idle;
        inFollowZone = false;
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();

        health = GetComponent<Health>();

    }

    void Update()
    {
        //print("enemy state=" + state);
        //print("in attack zone" + inFollowZone);
        //print("distance to player" + GetDistanceToPlayer());
        //print("anim name=" + anim.GetCurrentAnimatorClipInfo(0)[0].clip.name);



        if (state == EnemyStates.Idle)
        {
            EnemyIdle();
            return;
        }

        if (state == EnemyStates.Follow)
        {
            EnemyFollow();
            return;
        }

        if (state == EnemyStates.Attack)
        {
            EnemyAttack();
            return;
        }


    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Sword")
        {
            //inFollowZone = true;

            health.TakeDamage(33f);
            print("enemy health = " + health.currentHealth);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.tag == "Player")
        {
            inFollowZone = false;

        }

    }


    void EnemyIdle()
    {
        //check for player entering sphere
        if (inFollowZone)
        {
            state = EnemyStates.Follow;
        }
    }



    void EnemyFollow()
    {
        //check for player exiting sphere
        if (inFollowZone == false)
        {
            state = EnemyStates.Idle;
            anim.SetBool("IsWalk", false);
        }

        float speed = 1;
        Vector3 target = player.transform.position;
        target.y = transform.position.y; // Keep the y component of the target direction 
        var targetRotation = Quaternion.LookRotation(target - transform.position);

        transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 1 * Time.deltaTime);

        //move enemy in direction of player
        rb.linearVelocity = transform.forward * 2;

        if (rb.linearVelocity.magnitude > 0.1f)
        {
            anim.SetBool("IsWalk", true);
        }
        

        //check for player in attack zone
        if (GetDistanceToPlayer() < 1.9f)
        {
            state = EnemyStates.Attack;
        }


    }


    void EnemyAttack()
    {
        anim.SetBool("IsWalk", false);


        //check for player exiting attack zone
        if (GetDistanceToPlayer() > 1.9f)
        {
            state = EnemyStates.Follow;
        }
    }

    float GetDistanceToPlayer()
    {
        return Vector3.Distance(transform.position, player.transform.position);
    }

}
