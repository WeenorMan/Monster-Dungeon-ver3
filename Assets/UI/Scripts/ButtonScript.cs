using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonScript : MonoBehaviour
{
    


    public void LoadMenu()
    {
        SceneManager.LoadScene(0);
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene(1);
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene(2);
    }

    public void CloseGame()
    {
        Application.Quit();
    }


    public void MusicSlider( float vol )
    {
        print("vol is now " + vol);

    }

    public void PlaySFX( int clip )
    {
        LevelManager.instance.PlaySFXClip(clip);
    }
}
