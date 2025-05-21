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

    
    
}
