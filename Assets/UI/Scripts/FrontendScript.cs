using UnityEngine;

public class FrontendScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {

        //stop the current clip
        LevelManager.instance.StopMusicClip();
        LevelManager.instance.PlayMusicClip(1);



    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
