using UnityEngine;

public class AnimationStateController : MonoBehaviour
{
    Animator anim;
    float velocityZ = 0.0f;
    float velocityX = 0.0f;
    public float acceleration = 2f;
    public float deceleration = 2f;
    int velocityHash;
    void Start()
    {
        anim = GetComponent<Animator>();

        velocityHash = Animator.StringToHash("Velocity");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
