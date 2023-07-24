using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using TMPro;
using UnityEngine.SceneManagement;

public class ExternalPurchaseNotice : MonoBehaviour
{
    public static ExternalPurchaseNotice NoticeInstance;
    public GameObject Notice;

    
    int CurrentBalance;
    int NewBalance;
    public TMP_Text ProductName;
    public TMP_Text run_amount_text;
    public TMP_Text run_amount_shadow;

    private void Start()
    {
        SceneManager.activeSceneChanged += ChangedActiveScene;

    }

    private void ChangedActiveScene(Scene current, Scene next)
    {
        if(SceneManager.GetActiveScene().name == "Game_Scene")
        {   
            
            run_amount_text = GameObject.FindGameObjectWithTag("CurrencyText").GetComponent<TMP_Text>(); //do not worry of null refrence
        
            run_amount_shadow = GameObject.FindGameObjectWithTag("CurrencyShadow").GetComponent<TMP_Text>();

        }
 
    }


    private void Awake()
    {

        if (NoticeInstance == null)
        {
            NoticeInstance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void externalPurchaseSucced(Product product)
    {
        Notice.SetActive(true);
        
        CurrentBalance = PlayerPrefs.GetInt("Main Amount");

        if (product.definition.id == "com.vetkoekstudios.taxiranked.promocoin5000") //5k coins promo
        {
            ProductName.SetText("5000 Coins Promo");
            var payout = product.definition.payout.quantity; // Assuming a single payout, retrieve the payout quantity
            NewBalance = CurrentBalance + (int)payout;
            PlayerPrefs.SetInt("Main Amount", NewBalance);

        }
        if (product.definition.id == "com.vetkoekstudios.taxiranked.preregister10000") //10k coins promo
        {
            ProductName.SetText("10000 Coins Pre-Registration Reward");
            var payout = product.definition.payout.quantity; // Assuming a single payout, retrieve the payout quantity
            NewBalance = CurrentBalance + (int)payout;
            PlayerPrefs.SetInt("Main Amount", NewBalance);

        }
        if (product.definition.id == "com.vetkoekstudios.taxiranked.colour01") //colour 1 (not used)
        {
            PlayerPrefs.SetInt("Colour01_Premium", (true ? 1 : 0));
            ProductName.SetText("Colour product Restored");
        }
        if (product.definition.id == "com.vetkoekstudios.taxiranked.colour02") //colour 2
        {
            PlayerPrefs.SetInt("Colour02_Premium", (true ? 1 : 0));
            ProductName.SetText("Colour product Restored");
        }
        if (product.definition.id == "com.vetkoekstudios.taxiranked.colour03") //colour 3
        {
            PlayerPrefs.SetInt("Colour03_Premium", (true ? 1 : 0));
            ProductName.SetText("Colour product Restored");
        }
        if (product.definition.id == "com.vetkoekstudios.taxiranked.colour04") //colour 4
        {
            PlayerPrefs.SetInt("Colour04_Premium", (true ? 1 : 0));
            ProductName.SetText("Colour product Restored");
        }
        if (product.definition.id == "com.vetkoekstudios.taxiranked.colour02top") //colour 2 top
        {
            PlayerPrefs.SetInt("Colour02_PremiumTop", (true ? 1 : 0));
            ProductName.SetText("Colour product Restored");
        }
        if (product.definition.id == "com.vetkoekstudios.taxiranked.colour03top") //colour 3 top
        {
            PlayerPrefs.SetInt("Colour03_PremiumTop", (true ? 1 : 0));
            ProductName.SetText("Colour product Restored");
        }
        if (product.definition.id == "com.vetkoekstudios.taxiranked.colour04top") //colour 4 top
        {
            PlayerPrefs.SetInt("Colour04_PremiumTop", (true ? 1 : 0));
            ProductName.SetText("Colour product Restored");
        }
        if (product.definition.id == "com.vetkoekstudios.taxiranked.rims01") //rims 1
        {
            PlayerPrefs.SetInt("Rim01Premium", (true ? 1 : 0));
            ProductName.SetText("Rims product Restored");
        }
        if (product.definition.id == "com.vetkoekstudios.taxiranked.rims02") //rims 2
        {
            PlayerPrefs.SetInt("Rim02Premium", (true ? 1 : 0));
            ProductName.SetText("Rims product Restored");
        }
        if (product.definition.id == "com.vetkoekstudios.taxiranked.rims03") //rims 3
        {
            PlayerPrefs.SetInt("Rim03Premium", (true ? 1 : 0));
            ProductName.SetText("Rims product Restored");
        }
        if (product.definition.id == "com.vetkoekstudios.taxiranked.rims04") //rims 4
        {
            PlayerPrefs.SetInt("Rim04Premium", (true ? 1 : 0));
            ProductName.SetText("Rims product Restored");
        }
        if (product.definition.id == "com.vetkoekstudios.taxiranked.rims05") //rims 5
        {
            PlayerPrefs.SetInt("Rim05Premium", (true ? 1 : 0));
            ProductName.SetText("Rims product Restored");
        }
        if (product.definition.id == "com.vetkoekstudios.taxiranked.rims06") //rims 2
        {
            PlayerPrefs.SetInt("Rim06Premium", (true ? 1 : 0));
            ProductName.SetText("Rims product Restored");
        }
        if (product.definition.id == "com.vetkoekstudios.taxiranked.car01") //car 1 (not used)
        {
            PlayerPrefs.SetInt("Car01Premium", (true ? 1 : 0));
            ProductName.SetText("Car product Restored");
        }
        if (product.definition.id == "com.vetkoekstudios.taxiranked.car02") //car 2
        {
            PlayerPrefs.SetInt("Car02Premium", (true ? 1 : 0));
            ProductName.SetText("Car product Restored");
        }
        if (product.definition.id == "com.vetkoekstudios.taxiranked.car03") //car 3
        {
            PlayerPrefs.SetInt("Car03Premium", (true ? 1 : 0));
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
