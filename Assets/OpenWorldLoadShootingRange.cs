using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenWorldLoadShootingRange : MonoBehaviour
{
    //public IS_MainScript IS_MAIN;
    public GameObject BuynoadsButton;

    private void Start()
    {
        if (PlayerPrefs.GetInt("Ads_OWFirstOpen") == 0)
        {
            BuynoadsButton.SetActive(false);
        }
        if (PlayerPrefs.GetInt("Ads_OWFirstOpen") == 1)
        {
            bool Owned = (PlayerPrefs.GetInt("NoAds") != 0);
            if(Owned == false)
            {
                //IS_MAIN.LoadBannerAd(0);   
            }
            
        }
        PlayerPrefs.SetInt("Ads_OWFirstOpen", 1);

    }
    public void LoadShootingRange()
    {
        //IS_MAIN.DestroyBannerAd();
        SceneManager.LoadScene(3);
    }
}
