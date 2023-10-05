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

        if (PlayerPrefs.GetInt("MLMusic") == 1)
        {
            MusicPlaying = false;
            RadioBackPlate.color = InactiveColor;
            MusicTrack.Pause();
            Debug.Log("Start: Music Paused");
        }
        if (PlayerPrefs.GetInt("MLMusic") == 0)
        {
            MusicPlaying = true;
            RadioBackPlate.color = ActiveColor;
            MusicTrack.Play();
            Debug.Log("Start: Music Play");
        }
        if (PlayerPrefs.GetInt("MLEffectsVol") == 1)
        {
            EffectsColour = false;
            if (EffectsBackPlate != null) 
            {
                EffectsBackPlate.color = InactiveColor;
            }
            
        }
        if (PlayerPrefs.GetInt("MLEffectsVol") == 0)
        {
            EffectsColour = true;
            if (EffectsBackPlate != null) 
            {
                EffectsBackPlate.color = ActiveColor;
            }
            
        }

    }


    public void ToggleMusic()
    {
        if(MusicPlaying == true)
        {
            MusicPlaying = false;
            RadioBackPlate.color = InactiveColor;
            MusicTrack.Pause();
            PlayerPrefs.SetInt("MLMusic", 1);

            Debug.Log("Toggle: Music Paused");
        }
        else
        {
            MusicPlaying = true;
            RadioBackPlate.color = ActiveColor;
            MusicTrack.Play();
            PlayerPrefs.SetInt("MLMusic", 0);
            Debug.Log("Toggle: Music Play");
        }
    }

    public void ToggleColour()
    {
        if(EffectsColour == true)
        {
            EffectsColour = false;
            EffectsBackPlate.color = InactiveColor;
            PlayerPrefs.SetInt("MLEffectsVol", 1);
        }
        else
        {
            PlayerPrefs.SetInt("MLEffectsVol", 0);
            EffectsColour = true;
            EffectsBackPlate.color = ActiveColor;
        }
    }
}
