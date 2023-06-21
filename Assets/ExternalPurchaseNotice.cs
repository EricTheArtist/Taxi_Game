using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using TMPro;

public class ExternalPurchaseNotice : MonoBehaviour
{
    public GameObject Notice;

    
    int CurrentBalance;
    int NewBalance;
    public TMP_Text ProductName;
    public TMP_Text run_amount_text;
    public TMP_Text run_amount_shadow;

    void Start()
    {
        
    }

    public void externalPurchaseSucced(Product product)
    {
        Notice.SetActive(true);
        
        CurrentBalance = PlayerPrefs.GetInt("Main Amount");

        if (product.definition.id == "com.vetkoekstudios.taxiranked.promocoin5000")
        {
            ProductName.SetText("5000 Coins Promo");
            var payout = product.definition.payout.quantity; // Assuming a single payout, retrieve the payout quantity
            NewBalance = CurrentBalance + (int)payout;
            PlayerPrefs.SetInt("Main Amount", NewBalance);

        }
        if (product.definition.id == "com.vetkoekstudios.taxiranked.preregister10000")
        {
            ProductName.SetText("10000 Coins Pre-Registration Reward");
            var payout = product.definition.payout.quantity; // Assuming a single payout, retrieve the payout quantity
            NewBalance = CurrentBalance + (int)payout;
            PlayerPrefs.SetInt("Main Amount", NewBalance);

        }
        if (product.definition.id == "com.vetkoekstudios.taxiranked.colour01")
        {
            PlayerPrefs.SetInt("Colour01_Premium", (true ? 1 : 0));
            ProductName.SetText("Colour product Restored");
        }
        if (product.definition.id == "com.vetkoekstudios.taxiranked.colour02")
        {
            PlayerPrefs.SetInt("Colour02_Premium", (true ? 1 : 0));
            ProductName.SetText("Colour product Restored");
        }
        if (product.definition.id == "com.vetkoekstudios.taxiranked.colour03")
        {
            PlayerPrefs.SetInt("Colour03_Premium", (true ? 1 : 0));
            ProductName.SetText("Colour product Restored");
        }
        if (product.definition.id == "com.vetkoekstudios.taxiranked.colour04")
        {
            PlayerPrefs.SetInt("Colour04_Premium", (true ? 1 : 0));
            ProductName.SetText("Colour product Restored");
        }
        if (product.definition.id == "com.vetkoekstudios.taxiranked.rims01")
        {
            PlayerPrefs.SetInt("Rim01Premium", (true ? 1 : 0));
            ProductName.SetText("Rims product Restored");
        }
        if (product.definition.id == "com.vetkoekstudios.taxiranked.car01")
        {
            PlayerPrefs.SetInt("Car01Premium", (true ? 1 : 0));
            ProductName.SetText("Car product Restored");
        }


        


        // updates text on screen
        if (run_amount_shadow != null && run_amount_text != null)
        {
            run_amount_text.SetText(NewBalance.ToString());
            run_amount_shadow.SetText(NewBalance.ToString());
        }

        if (product.hasReceipt) // checks to see if the product is looged as owned
        {
            
            if (product.definition.id == "com.vetkoekstudios.taxiranked.colour01")
            {
                bool owned = (PlayerPrefs.GetInt("Colour01_Premium") != 0);
                if (owned == false)
                {
                    Notice.SetActive(true);
                    
                    PlayerPrefs.SetInt("Colour01_Premium", (true ? 1 : 0));
                    ProductName.SetText("Colour product Restored");
                }

            }
        }

    }

    public void CloseExternalPurchaseNotice()
    {
        Notice.SetActive(false);
    }


}
