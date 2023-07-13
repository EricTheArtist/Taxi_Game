using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayButtonSound : MonoBehaviour
{
    [SerializeField] private AudioClip ButtonSound1;


    public void PlayButtonSound1()
    {
        SoundManager.Instance.PlaySound(ButtonSound1);
    }

}
