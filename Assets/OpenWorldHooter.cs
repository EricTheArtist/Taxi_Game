using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWorldHooter : MonoBehaviour
{
    [SerializeField] private AudioClip HootClip;
    public void hoot()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlaySound(HootClip);
        }
    }
}
