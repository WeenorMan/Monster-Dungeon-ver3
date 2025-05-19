using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

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

    public NavMeshAgent agent;
    EnemyStates state;
    [SerializeField] float damage = 10f;

    Rigidbody rb;
    Animator anim;

    public GameObject player;
    public LayerMask whatIsGround, whatIsPlayer;

    Health health;

    //attacking
    public float timeBetweenAttacks;
    bool alreadyAttacked;
    public bool isDamaging;


    void Start()
    {
        LevelManager.instance.enemyCount += 1;

        isDamaging = false;

        state = EnemyStates.Idle;
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
        if (other.tag == "Sword" && player.GetComponent<PlayerCombat>().isDamaging)
        {
            //inFollowZone = true;

            health.TakeDamage(34f);
            print("enemy health = " + health.currentHealth);
        }

        EnemyAttack();
        
    }

    private void OnTriggerExit(Collider other)
    {
        

    }


    void EnemyIdle()
    {
        //check for player entering sphere
        //if (inFollowZone)
        {
            //state = EnemyStates.Follow;
        }

        float distance = GetDistanceToPlayer();
        //print("distance to player" + distance);

        if(distance < 5.5f)
        {
           state = EnemyStates.Follow;
            anim.SetBool("IsWalk", true);
        }

    }



    void EnemyFollow()
    {
        float distance = GetDistanceToPlayer();
        //check for player exiting sphere
        if (distance > 5.5f)
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

        
        

        //check for player in attack zone
        if (GetDistanceToPlayer() < 1.9f)
        {
            state = EnemyStates.Attack;
            anim.SetBool("IsAttacking", true);
            anim.SetBool("IsWalking", false);
            alreadyAttacked = false;
        }


    }


    void EnemyAttack()
    {
        // Stop moving and face the player
        transform.LookAt(player.transform);

        if (!alreadyAttacked && GetDistanceToPlayer() < 2f)
        {
            alreadyAttacked = true;

            // Deal damage to the player
            Health playerHealth = player.GetComponent<Health>();
            if (playerHealth != null)
            {
                playerHealth.PlayerTakeDamage(damage);
                Debug.Log("Enemy attacked player! Player health: " + playerHealth.currentHealth);
            }
            else
            {
                Debug.Log("Player Health component not found.");
            }

            // Start cooldown before next attack
            Invoke(nameof(ResetAttack), timeBetweenAttacks);
        }

        // If player leaves attack range, switch to follow state
        if (GetDistanceToPlayer() > 1.9f)
        {
            anim.SetBool("IsWalk", true);
            anim.SetBool("IsAttacking", false);
            state = EnemyStates.Follow;
        }
    }

    void ResetAttack()
    {
        alreadyAttacked = false;
    }

    float GetDistanceToPlayer()
    {
        return Vector3.Distance(transform.position, player.transform.position);

    }

    public void IsDamaging()
    {
        isDamaging = true;
    }

    public void NotDamaging()
    {
        isDamaging = false;
    }

}
