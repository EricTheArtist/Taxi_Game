using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OpenWorldCarDisplay : MonoBehaviour
{

    bool Owned;
    public string PlayerPrefName;
    public GameObject Self;
    private void Start()
    {
        
        Owned = (PlayerPrefs.GetInt(PlayerPrefName) != 0);
        if(Owned == true)
        {
            Destroy(Self);
        }
    }

}
