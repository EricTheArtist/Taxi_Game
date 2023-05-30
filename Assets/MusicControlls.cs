using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MusicControlls : MonoBehaviour
{
    AudioSource MusicTrack;
    bool MusicPlaying = true;

    public Color32 ActiveColor;
    public Color32 InactiveColor;
    public Image RadioBackPlate;

    // Start is called before the first frame update
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
        else
        {
            MusicPlaying = true;
            RadioBackPlate.color = ActiveColor;
            MusicTrack.Play();
            Debug.Log("Start: Music Play");
        }

    }

    // Update is called once per frame
    void Update()
    {
        
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
}
