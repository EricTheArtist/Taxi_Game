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
        if (PlayerPrefs.GetInt("EffectsVol") == 1)
        {
            _effectsSource.mute = true;
        }
        if (PlayerPrefs.GetInt("EffectsVol") == 0)
        {
            _effectsSource.mute = false;
        }

        float savedVolume = PlayerPrefs.GetFloat("SavedVolume");
        ChangeMasterVolume(savedVolume);

    }

    public void PlaySound(AudioClip clip)
    {
        _effectsSource.PlayOneShot(clip);
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
