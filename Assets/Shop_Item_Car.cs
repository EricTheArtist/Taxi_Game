using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class Shop_Item_Car : MonoBehaviour
{
    public int Price;
    public bool Owned;
    public TMP_Text Price_Text;
    ShopUIManager SUIM;
    public GameObject PriceBG;
    public string PlayerPrefName;
    public bool Premium = false;
    public GameObject car;
    public int CarIndex;

    public string MyCarProductID;
    public Text LocalisedPrice;

    ShopEffectManager SEM;
    NonConsumablePurchasing NCP;

    void Start()
    {
        GameObject canvas;
        canvas = GameObject.Find("Canvas");
        SUIM = canvas.GetComponent<ShopUIManager>();
        SEM = GameObject.FindGameObjectWithTag("ShopEffectManager").GetComponent<ShopEffectManager>();
        NCP = canvas.GetComponent<NonConsumablePurchasing>();

        if(PlayerPrefName == "Car00") //just for the starter vhecle
        {
            PlayerPrefs.SetInt(PlayerPrefName, (Owned ? 1 : 0));
        }
        
        Owned = (PlayerPrefs.GetInt(PlayerPrefName) != 0); // checks to see if the player already owns the car


        if(Premium == true)
        {
            LocalisedPrice.text = NCP.priceString(MyCarProductID);
        }
        if (Premium == false) // if the item is not costing real money it sets the price in coins
        {
            Price_Text.SetText(Price.ToString());   
        }
        if (Owned == true) //if the player already owns the item, disables the price overlay
        {
            PriceBG.SetActive(false);
            if (CarIndex == PlayerPrefs.GetInt("ActiveCar")) // if the this car is the same as the active car update to show it in the shop 
            {
                SUIM.UpdateCars(CarIndex);
            }
        }
    }


    public void Button_Clicked_Car()
    {

        if (Premium == true && Owned == false)
        {
            NCP.BuyProduct(MyCarProductID);
        }
        if (SUIM.CheckForEnoughMoney(Price) == true && Owned == false && Premium == false) // if the player has enough money and does not own the item
        {
            SUIM.OpenPurchaseDialouge(Price, PlayerPrefName, CarIndex, 100);

            //Owned = true;
            //SUIM.DeductCoins(Price);
            //PriceBG.SetActive(false); 

            //PlayerPrefs.SetInt(PlayerPrefName, (Owned ? 1 : 0)); //sets ownwership in the player prefs

            //SEM.BoughtNewCar();

            //PlayerPrefs.SetInt("ActiveCar", CarIndex);
            //SUIM.UpdateCars(CarIndex);
            //SUIM.LockControl(false);
        }
        else if (Owned == true) // if the player owns the item sets it to the active car
        {
            PlayerPrefs.SetInt("ActiveCar", CarIndex);
            SUIM.UpdateCars(CarIndex);

            SEM.SwitchedCars();
            SUIM.LockControl(false);
        }
        else if (Owned == false) //if they player does not own the car 
        {
            SUIM.UpdateCars(CarIndex);
            SEM.SwitchedCars();
            SUIM.LockControl(true);
        }
    }

    public void RealCurrencyCarPurchaseSucess() // called by the IAP button when a payment is sucessful
    {
        Owned = (PlayerPrefs.GetInt(PlayerPrefName) != 0);
        if (Owned == true && NCP.ProductIDfromButton == MyCarProductID)
        {
            PriceBG.SetActive(false);
        
            PlayerPrefs.SetInt("ActiveCar", CarIndex);

            SUIM.UpdateCars(CarIndex);

            SEM.BoughtNewCar();
            SUIM.LockControl(false);
        }

        

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
