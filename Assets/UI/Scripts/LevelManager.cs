using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;
    public static bool isGameEnding = false;

    [SerializeField] AudioMixer mixer;

    public AudioClip[] clips;
    public AudioSource musicAudioSource;
    public AudioSource sfxAudioSource;

    public const string MASTER_KEY = "Volume";
    public const string MUSIC_KEY = "musicVolume";
    public const string SFX_KEY = "sfxVolume";

    public int enemyCount;

    private void Awake()
    {
        isGameEnding = false;

        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            print("do not destroy");
        }
        else
        {
            print("do destroy");
            Destroy(gameObject);
        }

        LoadVolume();
        PlayMusicClip(1);
    }

    public void PlaySFXClip(int clipNumber)
    {
        sfxAudioSource.PlayOneShot(clips[clipNumber]); // start clip  
    }
    public void PlayMusicClip(int clipNumber)
    {
        musicAudioSource.PlayOneShot(clips[clipNumber]); // start clip  
    }

    public void StopSFXClip()
    {
        sfxAudioSource.Stop(); //stop currently playing clip  
    }
    public void StopMusicClip()
    {
        musicAudioSource.Stop(); //stop currently playing clip  
    }
    
    public void OnPlayerDeath()
    {
        isGameEnding = true;
        Debug.Log("Player died");
        SceneManager.LoadScene(0);
    }

    public void EnemyDied()
    {
        if (isGameEnding)
        {
            return;
        }
        enemyCount--;
        Debug.Log("EnemyDied called. enemyCount: " + enemyCount);
        if (enemyCount <= 0)
        {
            CompleteLevel();
        }
    }

    private void CompleteLevel()
    {
        Debug.Log("Level Complete!");
        SceneManager.LoadScene(2);
    }

    void LoadVolume()
    {
        float masterVolume = PlayerPrefs.GetFloat(MASTER_KEY, 1f);
        float musicVolume = PlayerPrefs.GetFloat(MUSIC_KEY, 1f);
        float sfxVolume = PlayerPrefs.GetFloat(SFX_KEY, 1f);

        mixer.SetFloat(VolumeSettings.MIXER_MASTER, Mathf.Log10(masterVolume) * 20);
        mixer.SetFloat(VolumeSettings.MIXER_MUSIC, Mathf.Log10(musicVolume) * 20);
        mixer.SetFloat(VolumeSettings.MIXER_SFX, Mathf.Log10(sfxVolume) * 20);
    }
}
