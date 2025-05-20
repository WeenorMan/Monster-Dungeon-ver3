using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

public class VolumeSettings : MonoBehaviour
{
    [SerializeField] AudioMixer mixer;
    [SerializeField] Slider masterSlider;
    [SerializeField] Slider musicSlider;
    [SerializeField] Slider sfxSlider;

    public const string MIXER_MASTER = "MasterVolume";
    public const string MIXER_MUSIC = "MusicVolume";
    public const string MIXER_SFX = "SFXVolume";


    private void Awake()
    {
        masterSlider.onValueChanged.AddListener(SetMasterVolume);
        musicSlider.onValueChanged.AddListener(SetMusicVolume);
        sfxSlider.onValueChanged.AddListener(SetSFXVolume);
    }

    private void Start()
    {
        masterSlider.value = PlayerPrefs.GetFloat(LevelManager.MASTER_KEY, 1f);
        musicSlider.value = PlayerPrefs.GetFloat(LevelManager.MUSIC_KEY, 1f);
        sfxSlider.value = PlayerPrefs.GetFloat(LevelManager.SFX_KEY, 1f);

        SetMasterVolume(masterSlider.value);
    }

    private void OnDisable()
    {
        PlayerPrefs.SetFloat(LevelManager.MASTER_KEY, masterSlider.value);
        PlayerPrefs.SetFloat(LevelManager.MUSIC_KEY, musicSlider.value);
        PlayerPrefs.SetFloat(LevelManager.SFX_KEY, sfxSlider.value);
    }

    void SetMasterVolume(float value)
    {
        mixer.SetFloat(MIXER_MASTER, Mathf.Log10(value) * 20);
    }
    void SetMusicVolume(float value)
    {
        mixer.SetFloat(MIXER_MUSIC, Mathf.Log10(value) * 20);
    }
    void SetSFXVolume(float value)
    {
        mixer.SetFloat(MIXER_SFX, Mathf.Log10(value) * 20);
    }
}

