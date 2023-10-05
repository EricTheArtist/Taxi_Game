using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MasterVolumeSlider : MonoBehaviour
{
    [SerializeField] private Slider _slider;

    void Start()
    {
        float savedVolume = PlayerPrefs.GetFloat("MLSavedVolume");
        _slider.value = savedVolume;
        SoundManager.Instance.ChangeMasterVolume(_slider.value);
        _slider.onValueChanged.AddListener(val => SoundManager.Instance.ChangeMasterVolume(val));
    }

    public void SaveSound()
    {
        PlayerPrefs.SetFloat("MLSavedVolume", _slider.value);
    }

}
