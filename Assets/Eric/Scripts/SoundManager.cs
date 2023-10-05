using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [SerializeField] private AudioSource _musicSource, _effectsSource;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        if (PlayerPrefs.GetInt("MLEffectsVol") == 1)
        {
            _effectsSource.mute = true;
        }
        if (PlayerPrefs.GetInt("MLEffectsVol") == 0)
        {
            _effectsSource.mute = false;
        }
        if (!PlayerPrefs.HasKey("MLSavedVolume"))
        {
            PlayerPrefs.SetFloat("MLSavedVolume", 1);
        }

        float savedVolume = PlayerPrefs.GetFloat("MLSavedVolume");
        ChangeMasterVolume(savedVolume);

    }

    public void PlaySound(AudioClip clip)
    {
        _effectsSource.PlayOneShot(clip);
    }

    public void StopSound()
    {
        _effectsSource.Stop();
    }

    public void ChangeMasterVolume(float value)
    {
        AudioListener.volume = value;
    }

    public void ToggleEffects()
    {
        _effectsSource.mute = !_effectsSource.mute;
    }


}
