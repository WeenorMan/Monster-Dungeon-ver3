using UnityEngine;

public class PlayerCombat : MonoBehaviour
{
    public Animator anim;
    public float buttonPressCount = 0;
    public GameObject GameObject;
    public bool isDamaging;

    void Start()
    {
        anim = GetComponent<Animator>();
        isDamaging = false;
    }

    void Update()
    {

        AnimatorClipInfo[] animInfo;
        animInfo = anim.GetCurrentAnimatorClipInfo(0);

        string animName = animInfo[0].clip.name;
        //print("clip name: " + animName);

        if (animName == "Idle")
        {
            //anim.SetBool("attack1", false);
            //anim.SetBool("attack2", false);
            //anim.SetBool("attack3", false);

        }

        if (Input.GetButtonDown("Attack"))
        {

            if (animName == "Idle" || animName == "Walk")
            {
                anim.SetBool("attack1", true);
                return;
            }

            if (animName == "Attack1")
            {
                print("Do attack2");
                anim.SetBool("attack1", false);
                anim.SetBool("attack2", true);

                return;
            }

            if (animName == "Attack2")
            {
                print("Do attack3");
                anim.SetBool("attack2", false);
                anim.SetBool("attack3", true);

                return;
            }

            if (animName == "Attack3")
            {
                anim.SetBool("attack2", false);

            }
        }
    }


    public void Attack1Ended()
    {
        anim.SetBool("attack1", false);

    }

    public void Attack2Ended()
    {
        anim.SetBool("attack1", false);
        anim.SetBool("attack2", false);

    }


    public void Attack3Ended()
    {
        anim.SetBool("attack1", false);
        anim.SetBool("attack2", false);
        anim.SetBool("attack3", false);

    }


    //animation events to determine when the player can inflict damage
    public void IsDamaging()
    {
        isDamaging = true;
    }
    public void NotDamaging()
    {
        isDamaging = false;
    }
}
