using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsLinks : MonoBehaviour
{
    public void LinkToIG()
    {
        Application.OpenURL("https://www.instagram.com/vetkoek_studios/");
    }

    public void LinkToTikTok()
    {
        Application.OpenURL("https://www.tiktok.com/@vetkoek_studios");
    }

    public void LinkToPlaystore()
    {
        Application.OpenURL("https://play.google.com/store/apps/details?id=com.vetkoekstudios.TaxiRanked");
    }

    public void LinktoMerchStore()
    {
        Application.OpenURL("https://www.etsy.com/shop/vetkoekstudiosmerch/?etsrc=sdt&coupon=SAVE50");
    }
    
}
