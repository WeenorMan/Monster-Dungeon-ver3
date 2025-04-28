using UnityEngine;

public class SwordScript : MonoBehaviour
{
    Animator anim;
    public GameObject sword;

    public GameObject player;

    void Awake()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    private void Update()
    {
        print("damaging=" + player.GetComponent<PlayerCombat>().isDamaging);

    }

    public void OnCollisionEnter(Collision collision)
    {



        if (collision.gameObject.tag == "Enemy" && player.GetComponent<PlayerCombat>().isDamaging)

        {
            print("Hit enemy");

            Health health = collision.gameObject.GetComponent<Health>();
            if (health != null)
            {
                health.TakeDamage(33f);
                print("enemy health = " + health.currentHealth);
            }
            else
            {
                print("health script is null");
            }
        }
    }
}
