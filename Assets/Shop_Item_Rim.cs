using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class Shop_Item_Rim : MonoBehaviour
{
    public int Price;
    public bool Owned;
    public TMP_Text Price_Text;
    ShopUIManager SUIM;
    public GameObject PriceBG;
    public string PlayerPrefName;
    public string MyRimProductID;
    public Text RealMoneyPrice;
    public bool Premium = false;
    public int RimIndex;

    ShopEffectManager SEM;
    NonConsumablePurchasing NCS;

    void Start()
    {
        GameObject canvas;
        canvas = GameObject.Find("Canvas");
        SUIM = canvas.GetComponent<ShopUIManager>();
        SEM = GameObject.FindGameObjectWithTag("ShopEffectManager").GetComponent<ShopEffectManager>();
        NCS = canvas.GetComponent<NonConsumablePurchasing>();
        if (PlayerPrefName == "Rim01") //just for the starter vhecle
        {
            PlayerPrefs.SetInt(PlayerPrefName, (Owned ? 1 : 0));
        }

        Owned = (PlayerPrefs.GetInt(PlayerPrefName) != 0); // checks to see if the player already owns the rims



        if (Premium == false) // if the item is not costing real money it sets the price in coins
        {
            Price_Text.SetText(Price.ToString());
        }
        if (Owned == true) //if the player already owns the item, disables the price overlay
        {
            PriceBG.SetActive(false);
            if (RimIndex == PlayerPrefs.GetInt("ActiveRimIndex")) // if the this rim is the same as the active rim update to show it in the shop 
            {
                SUIM.UpdateRims();
            }
        }
        if (Premium == true)
        {
            RealMoneyPrice.text = NCS.priceString(MyRimProductID);
        }
    }


    public void Button_Clicked_Rims()
    {
        if(Premium == true && Owned == false)
        {
            NCS.BuyProduct(MyRimProductID);
        }
        if (SUIM.CheckForEnoughMoney(Price) == true && Owned == false && Premium == false) // if the player has enough money and does not own the item
        {

            SUIM.OpenPurchaseDialouge(Price, PlayerPrefName, 100, RimIndex);
            //Owned = true;
            //SUIM.DeductCoins(Price);
            //PriceBG.SetActive(false);

            //PlayerPrefs.SetInt(PlayerPrefName, (Owned ? 1 : 0)); //sets ownwership in the player prefs

            //SEM.BoughtNewCar();

            //PlayerPrefs.SetInt("ActiveRimIndex",RimIndex);
            //SUIM.UpdateRims();
            //SUIM.LockControl(false);
        }
        else if (Owned == true) // if the player owns the item sets it to the active car
        {
            PlayerPrefs.SetInt("ActiveRimIndex", RimIndex);
            SUIM.UpdateRims();

            //SEM.SwitchedCars();
            //SUIM.LockControl(false);
        }
        else if (Owned == false) //if they player does not own the car 
        {
            Debug.Log("Too poor for Item");
            //SUIM.LockControl(true);
        }
    }

    public void RealCurrencyRimsPurchaseSucess() // called by the IAP button when a payment is sucessful
    {

        Owned = (PlayerPrefs.GetInt(PlayerPrefName) != 0);
        if(Owned == true && NCS.ProductIDfromButton == MyRimProductID)
        {
            PriceBG.SetActive(false);
            PlayerPrefs.SetInt("ActiveRimIndex", RimIndex);
            SUIM.UpdateRims();

            SEM.BoughtNewCar();

        }
        
        
        
        


        //SUIM.LockControl(false);
    }


    public void PurchaseEvent()
    {
        bool PrviousOwner = Owned;

        Owned = (PlayerPrefs.GetInt(PlayerPrefName) != 0);

        if (PrviousOwner == false && Owned == true)
        {
            PriceBG.SetActive(false);
        }

    }


}
