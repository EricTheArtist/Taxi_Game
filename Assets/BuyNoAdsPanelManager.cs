using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BuyNoAdsPanelManager : MonoBehaviour
{
    public IS_MainScript IS_MAIN;
    public NonConsumablePurchasing NCP;
    public GameObject BuynoadsButton;

    private void Start()
    {
        bool Owned = (PlayerPrefs.GetInt("NoAds") != 0);
        if (Owned == true)
        {
            BuynoadsButton.SetActive(false);
        }
    }
    public void ClickNoAdsButton()
    {
        bool Owned = (PlayerPrefs.GetInt("NoAds") != 0);
        if (Owned == true)
        {
            IS_MAIN.DestroyBannerAd();
            BuynoadsButton.SetActive(false);
        }
        else
        {
            NCP.BuyProduct("com.vetkoekstudios.pemanduteksi.noads");
        }
        
    }
    public void VoidONnoAdsPurchase()
    {
        IS_MAIN.DestroyBannerAd();
        BuynoadsButton.SetActive(false);
    }
}
