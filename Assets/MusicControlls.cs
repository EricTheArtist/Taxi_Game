using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicControlls : MonoBehaviour
{
    AudioSource MusicTrack;
    bool MusicPlaying = true;
    bool EffectsColour = true;

    public Color32 ActiveColor;
    public Color32 InactiveColor;
    public Image RadioBackPlate;
    public Image EffectsBackPlate;

    // check saved audio prefrences
    void Start()
    {
        MusicTrack = gameObject.GetComponent<AudioSource>();

        if (PlayerPrefs.GetInt("Music") == 1)
        {
            MusicPlaying = false;
            RadioBackPlate.color = InactiveColor;
            MusicTrack.Pause();
            Debug.Log("Start: Music Paused");
        }
        if (PlayerPrefs.GetInt("Music") == 0)
        {
            MusicPlaying = true;
            RadioBackPlate.color = ActiveColor;
            MusicTrack.Play();
            Debug.Log("Start: Music Play");
        }
        if (PlayerPrefs.GetInt("EffectsVol") == 1)
        {
            EffectsColour = false;
            EffectsBackPlate.color = InactiveColor;
        }
        if (PlayerPrefs.GetInt("EffectsVol") == 0)
        {
            EffectsColour = true;
            EffectsBackPlate.color = ActiveColor;
        }

    }


    public void ToggleMusic()
    {
        if(MusicPlaying == true)
        {
            MusicPlaying = false;
            RadioBackPlate.color = InactiveColor;
            MusicTrack.Pause();
            PlayerPrefs.SetInt("Music", 1);

            Debug.Log("Toggle: Music Paused");
        }
        else
        {
            MusicPlaying = true;
            RadioBackPlate.color = ActiveColor;
            MusicTrack.Play();
            PlayerPrefs.SetInt("Music", 0);
            Debug.Log("Toggle: Music Play");
        }
    }

    public void ToggleColour()
    {
        if(EffectsColour == true)
        {
            EffectsColour = false;
            EffectsBackPlate.color = InactiveColor;
            PlayerPrefs.SetInt("EffectsVol", 1);
        }
        else
        {
            PlayerPrefs.SetInt("EffectsVol", 0);
            EffectsColour = true;
            EffectsBackPlate.color = ActiveColor;
        }
    }
}
