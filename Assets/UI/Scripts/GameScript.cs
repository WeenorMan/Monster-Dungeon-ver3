using UnityEngine;

public class GameScript : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //stop the current music 

        LevelManager.instance.StopMusicClip();
        LevelManager.instance.PlayMusicClip(2);
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
