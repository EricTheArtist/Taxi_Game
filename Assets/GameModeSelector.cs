using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameModeSelector : MonoBehaviour
{
    public bool OpenWorldMode =false;
    public Color32 ActiveColor;
    public Color32 InactiveColor;
    public Image OWplate;
    public Image Highwayplate;

    void Start()
    {
        OpenWorldMode = (PlayerPrefs.GetInt("MLGameModeSelectOpenW") != 0);
        toggleGameModeSelect();
    }

    public void SelectOpenWorld()
    {
        OpenWorldMode = true;
        PlayerPrefs.SetInt("MLGameModeSelectOpenW", (true ? 1 : 0));
        toggleGameModeSelect();
    }

    public void SelectInfiniteHighway()
    {
        OpenWorldMode = false;
        PlayerPrefs.SetInt("MLGameModeSelectOpenW", (false ? 1 : 0));
        toggleGameModeSelect();
    }

    void toggleGameModeSelect()
    {
        if (OpenWorldMode == true)
        {
            Highwayplate.color = InactiveColor;
            OWplate.color = ActiveColor;
        }
        else
        {
            Highwayplate.color = ActiveColor;
            OWplate.color = InactiveColor;
        }
    }

}
