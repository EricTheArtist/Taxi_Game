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
        if(Notice!= null)
        {
            Notice.SetActive(true);
        }
        
        
        CurrentBalance = PlayerPrefs.GetInt("MLMain Amount");

        if (product.definition.id == "com.vetkoekstudios.pemanduteksi.promocoin5000") //5k coins promo
        {
            ProductName.SetText("5000 Coins Promo");
            var payout = product.definition.payout.quantity; // Assuming a single payout, retrieve the payout quantity
            NewBalance = CurrentBalance + (int)payout;
            PlayerPrefs.SetInt("MLMain Amount", NewBalance);

        }
        if (product.definition.id == "com.vetkoekstudios.pemanduteksi.preregister10000") //10k coins promo
        {
            ProductName.SetText("10000 Coins Pre-Registration Reward");
            var payout = product.definition.payout.quantity; // Assuming a single payout, retrieve the payout quantity
            NewBalance = CurrentBalance + (int)payout;
            PlayerPrefs.SetInt("MLMain Amount", NewBalance);

        }

        if (product.definition.id == "com.vetkoekstudios.pemanduteksi.coinstiny") //40k coins
        {
            ProductName.SetText("40 000 Syiling");
            NewBalance = CurrentBalance + 40000;
            PlayerPrefs.SetInt("MLMain Amount", NewBalance);
        }

        if (product.definition.id == "com.vetkoekstudios.pemanduteksi.coinssmall") //90k coins
        {
            ProductName.SetText("90 000 Syiling");
            NewBalance = CurrentBalance + 90000;
            PlayerPrefs.SetInt("MLMain Amount", NewBalance);
        }

        if (product.definition.id == "com.vetkoekstudios.pemanduteksi.coinsmediumv2") //200k coins
        {
            ProductName.SetText("200 000 Syiling");
            NewBalance = CurrentBalance + 200000;
            PlayerPrefs.SetInt("MLMain Amount", NewBalance);
        }

        if (product.definition.id == "com.vetkoekstudios.pemanduteksi.coinslargev2") //420k coins
        {
            ProductName.SetText("420 000 Syiling");
            NewBalance = CurrentBalance + 420000;
            PlayerPrefs.SetInt("MLMain Amount", NewBalance);
        }

        if (product.definition.id == "com.vetkoekstudios.pemanduteksi.coinsextralargev2") //850k coins
        {
            ProductName.SetText("850 000 Syiling");
            NewBalance = CurrentBalance + 850000;
            PlayerPrefs.SetInt("MLMain Amount", NewBalance);
        }

        if (product.definition.id == "com.vetkoekstudios.pemanduteksi.coinsmassivev2") //1750k coins
        {
            ProductName.SetText("1 730 000 Syiling");
            NewBalance = CurrentBalance + 1730000;
            PlayerPrefs.SetInt("MLMain Amount", NewBalance);
        }


        if (product.definition.id == "com.vetkoekstudios.pemanduteksi.colour01") //colour 1 (not used)
        {
            PlayerPrefs.SetInt("MLColour01_Premium", (true ? 1 : 0));
            ProductName.SetText("Produk warna Dipulihkan");
        }
        if (product.definition.id == "com.vetkoekstudios.pemanduteksi.colour02") //colour 2
        {
            PlayerPrefs.SetInt("MLColour02_Premium", (true ? 1 : 0));
            ProductName.SetText("Produk warna Dipulihkan");
        }
        if (product.definition.id == "com.vetkoekstudios.pemanduteksi.colour03") //colour 3
        {
            PlayerPrefs.SetInt("MLColour03_Premium", (true ? 1 : 0));
            ProductName.SetText("Produk warna Dipulihkan");
        }
        if (product.definition.id == "com.vetkoekstudios.pemanduteksi.colour04") //colour 4
        {
            PlayerPrefs.SetInt("MLColour04_Premium", (true ? 1 : 0));
            ProductName.SetText("Produk warna Dipulihkan");
        }
        if (product.definition.id == "com.vetkoekstudios.pemanduteksi.colour02top") //colour 2 top
        {
            PlayerPrefs.SetInt("MLColour02_PremiumTop", (true ? 1 : 0));
            ProductName.SetText("Produk warna Dipulihkan");
        }
        if (product.definition.id == "com.vetkoekstudios.pemanduteksi.colour03top") //colour 3 top
        {
            PlayerPrefs.SetInt("MLColour03_PremiumTop", (true ? 1 : 0));
            ProductName.SetText("Produk warna Dipulihkan");
        }
        if (product.definition.id == "com.vetkoekstudios.pemanduteksi.colour04top") //colour 4 top
        {
            PlayerPrefs.SetInt("MLColour04_PremiumTop", (true ? 1 : 0));
            ProductName.SetText("Produk warna Dipulihkan");
        }
        if (product.definition.id == "com.vetkoekstudios.pemanduteksi.rims01") //rims 1
        {
            PlayerPrefs.SetInt("MLRim01Premium", (true ? 1 : 0));
            ProductName.SetText("Produk Rims Dipulihkan");
        }
        if (product.definition.id == "com.vetkoekstudios.pemanduteksi.rims02") //rims 2
        {
            PlayerPrefs.SetInt("MLRim02Premium", (true ? 1 : 0));
            ProductName.SetText("Produk Rims Dipulihkan");
        }
        if (product.definition.id == "com.vetkoekstudios.pemanduteksi.rims03") //rims 3
        {
            PlayerPrefs.SetInt("MLRim03Premium", (true ? 1 : 0));
            ProductName.SetText("Produk Rims Dipulihkan");
        }
        if (product.definition.id == "com.vetkoekstudios.pemanduteksi.rims04") //rims 4
        {
            PlayerPrefs.SetInt("MLRim04Premium", (true ? 1 : 0));
            ProductName.SetText("Produk Rims Dipulihkan");
        }
        if (product.definition.id == "com.vetkoekstudios.pemanduteksi.rims05") //rims 5
        {
            PlayerPrefs.SetInt("MLRim05Premium", (true ? 1 : 0));
            ProductName.SetText("Produk Rims Dipulihkan");
        }
        if (product.definition.id == "com.vetkoekstudios.pemanduteksi.rims06") //rims 2
        {
            PlayerPrefs.SetInt("MLRim06Premium", (true ? 1 : 0));
            ProductName.SetText("Produk Rims Dipulihkan");
        }
        if (product.definition.id == "com.vetkoekstudios.pemanduteksi.car01") //car 1 (not used)
        {
            PlayerPrefs.SetInt("MLCar01Premium", (true ? 1 : 0));
            ProductName.SetText("Produk kereta dipulihkan");
        }
        if (product.definition.id == "com.vetkoekstudios.pemanduteksi.car02") //car 2
        {
            PlayerPrefs.SetInt("MLCar02Premium", (true ? 1 : 0));
            ProductName.SetText("Produk kereta dipulihkan");
        }
        if (product.definition.id == "com.vetkoekstudios.pemanduteksi.car03") //car 3
        {
            PlayerPrefs.SetInt("MLCar03Premium", (true ? 1 : 0));
            ProductName.SetText("Produk kereta dipulihkan");
        }
        if (product.definition.id == "com.vetkoekstudios.pemanduteksi.car04") //car 4
        {
            PlayerPrefs.SetInt("MLCar04Premium", (true ? 1 : 0));
            ProductName.SetText("Produk kereta dipulihkan");
        }
        if (product.definition.id == "com.vetkoekstudios.pemanduteksi.car05") //car 5
        {
            PlayerPrefs.SetInt("MLCar05Premium", (true ? 1 : 0));
            ProductName.SetText("Produk kereta dipulihkan");
        }
        if (product.definition.id == "com.vetkoekstudios.pemanduteksi.car06") //car 6
        {
            PlayerPrefs.SetInt("MLCar06Premium", (true ? 1 : 0));
            ProductName.SetText("Produk kereta dipulihkan");
        }

        if (product.definition.id == "com.vetkoekstudios.pemanduteksi.noads") //no ads
        {
            PlayerPrefs.SetInt("MLNoAds", (true ? 1 : 0));
            ProductName.SetText("Iklan Dialih Keluar");
        }


        // updates text on screen
        if (run_amount_shadow != null && run_amount_text != null)
        {
            run_amount_text.SetText(NewBalance.ToString());
            run_amount_shadow.SetText(NewBalance.ToString());
        }

        /*
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
        */
    }

    public void CloseExternalPurchaseNotice()
    {
        Notice.SetActive(false);
    }


}
