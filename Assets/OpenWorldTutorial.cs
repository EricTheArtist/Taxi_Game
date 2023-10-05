using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class OpenWorldTutorial : MonoBehaviour
{
    public Image PromptImage;
    public Sprite C_TaxiBoss;
    public Sprite C_ShopOwner;
    public Sprite C_Police;
    public TMP_Text DialougeText;
    public GameObject NarritiveInterface;

    public string OWFirstOpen;
    public string OWFirstPickup;
    public string OWFirstDropOff;
    public string OWFirstCrash;

    void Start()
    {

        if (PlayerPrefs.GetInt("MLNI_OWFirstOpen") == 0)
        {
            PlayerPrefs.SetInt("MLNI_OWFirstOpen", 1);
            OpenInterface(OWFirstOpen, C_TaxiBoss, 1);
        }
    }

    public void OWFirstPickUpEvent()
    {
        if (PlayerPrefs.GetInt("MLNI_OWFirstPickUp") == 0)
        {
            OpenInterface(OWFirstPickup, C_TaxiBoss, 0);
            PlayerPrefs.SetInt("MLNI_OWFirstPickUp", 1);
        }
    }

    public void OWFirstDropOffEvent()
    {
        if (PlayerPrefs.GetInt("MLNI_OWFirstDropOff") == 0)
        {
            OpenInterface(OWFirstDropOff, C_TaxiBoss, 0);
            PlayerPrefs.SetInt("MLNI_OWFirstDropOff", 1);
        }
    }

    public void OWFirstCrashEvent()
    {
        if (PlayerPrefs.GetInt("MLNI_OWFirstCrash") == 0)
        {
            OpenInterface(OWFirstCrash, C_Police, 1);
            PlayerPrefs.SetInt("MLNI_OWFirstCrash", 1);
        }
    }

    public void OpenInterface(string Text, Sprite Character, float timescale)
    {
        Time.timeScale = timescale;
        NarritiveInterface.SetActive(true);
        DialougeText.SetText(Text);
        PromptImage.sprite = Character;
    }

    public void CloseInterface()
    {
        Time.timeScale = 1;
        NarritiveInterface.SetActive(false);
    }



}
