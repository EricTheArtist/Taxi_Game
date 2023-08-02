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
    public bool HWProduct = false;
    public GameObject car;
    public int CarIndex;

    public string MyCarProductID;
    public Text LocalisedPrice;

    ShopEffectManager SEM;
    NonConsumablePurchasing NCP;
    public HW_IAP_Manager HWIAPM;

    void Start()
    {
        GameObject canvas;
        canvas = GameObject.Find("Canvas");
        SUIM = canvas.GetComponent<ShopUIManager>();
        SEM = GameObject.FindGameObjectWithTag("ShopEffectManager").GetComponent<ShopEffectManager>();
        if (HWProduct == false)
        {
            NCP = canvas.GetComponent<NonConsumablePurchasing>();
        }
        if (HWProduct == true && HWIAPM == null)
        {
            HWIAPM = GameObject.Find("HuaweiIAPManager").GetComponent<HW_IAP_Manager>();
        }

        if(PlayerPrefName == "Car00") //just for the starter vhecle
        {
            PlayerPrefs.SetInt(PlayerPrefName, (Owned ? 1 : 0));
        }
        
        Owned = (PlayerPrefs.GetInt(PlayerPrefName) != 0); // checks to see if the player already owns the car


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
        if (Premium == true &&HWProduct==false)
        {
            LocalisedPrice.text = NCP.priceString(MyCarProductID);
        }
        if(Premium == true && HWProduct == true)
        {
            LocalisedPrice.text = HWIAPM.HWpriceString(MyCarProductID);
        }
    }


    public void Button_Clicked_Car()
    {
        Owned = (PlayerPrefs.GetInt(PlayerPrefName) != 0);
        if (Premium == true && Owned == false)
        {
            if(HWProduct == false)
            {
                NCP.BuyProduct(MyCarProductID);
            }
            else if(HWProduct == true)
            {
                HWIAPM.PurchaseProduct(MyCarProductID);
            }
            
        }
        if (SUIM.CheckForEnoughMoney(Price) == true && Owned == false && Premium == false && CarIndex!= 8) // if the player has enough money and does not own the item
        {

            SUIM.OpenPurchaseDialouge(Price, PlayerPrefName, CarIndex, 100);

        }
        if (SUIM.CheckForEnoughPassengers(Price) == true && Owned == false && Premium == false && CarIndex == 8)
        {
            SUIM.OpenPurchaseDialouge(Price, PlayerPrefName, CarIndex, 100);

        }
        if (Owned == false) //if they player does not own the car 
        {
            SUIM.UpdateCars(CarIndex);
            SEM.SwitchedCars();
            SUIM.LockControl(true);
        }
        else if (Owned == true) // if the player owns the item sets it to the active car
        {
            PlayerPrefs.SetInt("ActiveCar", CarIndex);
            SUIM.UpdateCars(CarIndex);

            SEM.SwitchedCars();
            SUIM.LockControl(false);
        }

    }

    public void RealCurrencyCarPurchaseSucess() // subscribed to event called by NonConsumablePurchasing when a payment is sucessful
    {
        if (isActiveAndEnabled)
        {
            string ProductIDfromPurchaser = "0";
            if(HWProduct == false)
            {
                ProductIDfromPurchaser = NCP.ProductIDfromButton;
            }
            if(HWProduct == true)
            {
                ProductIDfromPurchaser = HWIAPM.LastProductID;
            }
            
            Owned = (PlayerPrefs.GetInt(PlayerPrefName) != 0);
            if (Owned == true && ProductIDfromPurchaser == MyCarProductID)
            {
                PriceBG.SetActive(false);
        
                PlayerPrefs.SetInt("ActiveCar", CarIndex);

                SUIM.UpdateCars(CarIndex);

                SEM.BoughtNewCar();
                SUIM.LockControl(false);
            }
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
