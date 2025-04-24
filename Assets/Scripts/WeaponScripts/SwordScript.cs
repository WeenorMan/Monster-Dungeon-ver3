using UnityEngine;

public class SwordScript : MonoBehaviour
{
    public GameObject sword;

    void Start()
    {
        

    }

    
    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Enemy")
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
