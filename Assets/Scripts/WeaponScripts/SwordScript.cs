using UnityEngine;

public class SwordScript : MonoBehaviour
{
    Animator anim;
    public GameObject sword;

    public GameObject player;
    [SerializeField] private float damage = 10f;

    void Awake()
    {
        anim = GameObject.FindGameObjectWithTag("Player").GetComponent<Animator>();
    }

    private void Update()
    {
        print("damaging=" + player.GetComponent<PlayerCombat>().isDamaging);

    }

    public void OnTriggerEnter(Collider collision)
    {
        return;

        print("sword collided with " + collision.gameObject.name + "  damaging=" + player.GetComponent<PlayerCombat>().isDamaging);


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
