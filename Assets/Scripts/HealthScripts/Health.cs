using UnityEngine;
using UnityEngine.UI;

public class Health : MonoBehaviour
{
    public Slider healthSlider;
    public float maxHealth = 100f;
    public float currentHealth;
    bool isDead;
    private Animator anim;

    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if(healthSlider.value != currentHealth)
        {
          healthSlider.value = currentHealth;
        }

        
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
