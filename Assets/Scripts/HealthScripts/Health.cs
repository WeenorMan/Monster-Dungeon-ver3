using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class Health : MonoBehaviour
{
    public Slider healthSlider;
    public float maxHealth = 100f;
    public float currentHealth;
    bool isDead;
    private Animator anim;
    private bool isInvulnerable = false;
    [SerializeField] private float invulnerabilityDuration = 1f;
     

    void Start()
    {
        currentHealth = maxHealth;
        anim = GetComponent<Animator>();
    }

    void Update()
    {
        if (healthSlider.value != currentHealth)
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
            anim.SetTrigger("Die");

            var agent = GetComponent<UnityEngine.AI.NavMeshAgent>();
            if (agent != null) agent.enabled = false;
            var rb = GetComponent<Rigidbody>();
            if (rb != null) rb.linearVelocity = Vector3.zero;
            var collider = GetComponent<Collider>();
            if (collider != null) collider.enabled = false;

            StartCoroutine(EnemyDeathAnim());
        }
    }

    public void PlayerTakeDamage(float amount)
    {
        if (isInvulnerable) return;
        currentHealth -= amount;
        StartCoroutine(InvulnerabilityCoroutine());

        if (currentHealth <= 0 && !isDead)
        {
            LevelManager.instance.PlaySFXClip(6);
            isDead = true;
            anim.SetTrigger("Die");
            StartCoroutine(PlayerDeathAnim());
        }
    }

    private IEnumerator InvulnerabilityCoroutine()
    {
        isInvulnerable = true;
        yield return new WaitForSeconds(invulnerabilityDuration);
        isInvulnerable = false;
    }

    private IEnumerator PlayerDeathAnim()
    {
        float animTime = 3.9f;
        yield return new WaitForSeconds(animTime);

        LevelManager.instance.OnPlayerDeath();
        Destroy(gameObject);
    }

    private IEnumerator EnemyDeathAnim()
    {
        float animTime = 1.5f;
        yield return new WaitForSeconds(animTime);

        Destroy(gameObject);
    }
    public bool IsDead => isDead;

}
