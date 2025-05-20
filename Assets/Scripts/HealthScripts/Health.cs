using UnityEngine;
using UnityEngine.SceneManagement;
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
            //LevelManager.instance.enemyCount -= 1;
            //print(LevelManager.instance.enemyCount);



            GameObject[] obj = GameObject.FindGameObjectsWithTag("Enemy");
            print("enemy count= " + (obj.Length-1));

            Destroy(gameObject);



        }
    }

    public void PlayerTakeDamage(float damage)
    {
        currentHealth = Mathf.Clamp(currentHealth - damage, 0, maxHealth);

        if (currentHealth <= 0 && !isDead)
        {
            isDead = true;
            Destroy(gameObject);
            SceneManager.LoadScene(0);
        }
    }
}
