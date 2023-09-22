using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameModeSelector : MonoBehaviour
{
    public bool OpenWorldMode =false;
    public Color32 ActiveColor;
    public Color32 InactiveColor;
    public Image OWplate;
    public Image Highwayplate;

    public UnityEvent SelectInfintyRun;

    void Start()
    {
        if (!PlayerPrefs.HasKey("GameModeSelectOpenW"))
        {
            PlayerPrefs.SetInt("GameModeSelectOpenW", (true ? 1 : 0));
        }
        OpenWorldMode = (PlayerPrefs.GetInt("GameModeSelectOpenW") != 0);
        toggleGameModeSelect();
    }

    public void SelectOpenWorld()
    {
        OpenWorldMode = true;
        PlayerPrefs.SetInt("GameModeSelectOpenW", (true ? 1 : 0));
        toggleGameModeSelect();
    }

    public void SelectInfiniteHighway()
    {
        OpenWorldMode = false;
        PlayerPrefs.SetInt("GameModeSelectOpenW", (false ? 1 : 0));
        toggleGameModeSelect();
        SelectInfintyRun.Invoke();
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
