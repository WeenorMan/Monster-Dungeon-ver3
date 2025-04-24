using UnityEngine;

public class Health : MonoBehaviour
{
    [SerializeField] private float maxHealth;
    public float currentHealth;
    bool isDead;
    private Animator anim;

    void Awake()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    void Update()
    {

    }

    public void TakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);

        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            Destroy(gameObject);
        }
    }
}
