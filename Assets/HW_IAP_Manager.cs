using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HmsPlugin;

using HuaweiMobileServices.IAP;
using HuaweiMobileServices.Utils;

using UnityEngine.Events;

using TMPro;
using UnityEngine.UI;

public class HW_IAP_Manager : MonoBehaviour
{
    public TMP_Text DebugText;
    // Please insert your products via custom editor. You can find it in Huawei > Kit Settings > IAP tab.

    //public static Action<string> IAPLog;

    List<InAppPurchaseData> consumablePurchaseRecord = new List<InAppPurchaseData>();
    List<InAppPurchaseData> activeNonConsumables = new List<InAppPurchaseData>();
    List<InAppPurchaseData> activeSubscriptions = new List<InAppPurchaseData>();

    public UnityEvent SucessfullHWPurchase;
    public string LastProductID;


    public static string ProductIDCoinsTiny = "com.vetkoekstudios.taxiranked.coinstiny";
    public static string ProductIDCoinsSmall = "com.vetkoekstudios.taxiranked.coinssmall";
    public static string ProductIDCoinsMedium = "com.vetkoekstudios.taxiranked.coinsmediumv2";
    public static string ProductIDCoinsLarge = "com.vetkoekstudios.taxiranked.coinslargev2";
    public static string ProductIDCoinsExtraLarge = "com.vetkoekstudios.taxiranked.coinsextralargev2";
    public static string ProductIDCoinsMassive = "com.vetkoekstudios.taxiranked.coinsmassivev2";

    public string ProductIDColour01 = "com.vetkoekstudios.taxiranked.colour01";
    public string ProductIDColour02 = "com.vetkoekstudios.taxiranked.colour02";
    public string ProductIDColour03 = "com.vetkoekstudios.taxiranked.colour03";
    public string ProductIDColour04 = "com.vetkoekstudios.taxiranked.colour04";

    public string ProductIDColour02Top = "com.vetkoekstudios.taxiranked.colour02top";
    public string ProductIDColour03Top = "com.vetkoekstudios.taxiranked.colour03top";
    public string ProductIDColour04Top = "com.vetkoekstudios.taxiranked.colour04top";

    public string ProductIDRims01 = "com.vetkoekstudios.taxiranked.rims01";
    public string ProductIDRims02 = "com.vetkoekstudios.taxiranked.rims02";
    public string ProductIDRims03 = "com.vetkoekstudios.taxiranked.rims03";
    public string ProductIDRims04 = "com.vetkoekstudios.taxiranked.rims04";
    public string ProductIDRims05 = "com.vetkoekstudios.taxiranked.rims05";
    public string ProductIDRims06 = "com.vetkoekstudios.taxiranked.rims06";

    public string ProductIDCar02 = "com.vetkoekstudios.taxiranked.car02";
    public string ProductIDCar03 = "com.vetkoekstudios.taxiranked.car03";
    public string ProductIDCar04 = "com.vetkoekstudios.taxiranked.car04";


    int CurrentBalance;
    int NewBalance;
    public ShopUIManager SUIM;

    void MyDebug(string message)
    {
        DebugText.SetText(message);
    }

    #region Singleton

    public static HW_IAP_Manager Instance { get; private set; }
    private void Singleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    #endregion

    #region Monobehaviour

    private void OnEnable()
    {
        HMSIAPManager.Instance.OnBuyProductSuccess += OnBuyProductSuccess;
        HMSIAPManager.Instance.OnInitializeIAPSuccess += OnInitializeIAPSuccess;
        HMSIAPManager.Instance.OnInitializeIAPFailure += OnInitializeIAPFailure;
        HMSIAPManager.Instance.OnBuyProductFailure += OnBuyProductFailure;
    }

    private void OnDisable()
    {
        HMSIAPManager.Instance.OnBuyProductSuccess -= OnBuyProductSuccess;
        HMSIAPManager.Instance.OnInitializeIAPSuccess -= OnInitializeIAPSuccess;
        HMSIAPManager.Instance.OnInitializeIAPFailure -= OnInitializeIAPFailure;
        HMSIAPManager.Instance.OnBuyProductFailure -= OnBuyProductFailure;
    }

    void Awake()
    {
        Singleton();
        //Screen.orientation = ScreenOrientation.LandscapeLeft;
    }

    void Start()
    {
        // Uncomment below if InitializeOnStart is not enabled in Huawei > Kit Settings > IAP tab.
        HMSIAPManager.Instance.InitializeIAP();
        MyDebug("Calling initialize");
    }

    #endregion

    public string HWpriceString(string ProductID)
    {
        return HMSIAPManager.Instance.GetProductInfo(ProductID).Price.ToString();
    }


    public void InitializeIAP() //not called
    {
        Debug.Log($"InitializeIAP");
        
        HMSIAPManager.Instance.InitializeIAP();
    }

    private void RestoreProducts()
    {

        HMSIAPManager.Instance.RestorePurchaseRecords((restoredProducts) =>
        {
            foreach (var item in restoredProducts.InAppPurchaseDataList)
            {
                if ((IAPProductType)item.Kind == IAPProductType.Consumable)
                {
                    Debug.Log($"Consumable: ProductId {item.ProductId} , SubValid {item.SubValid} , PurchaseToken {item.PurchaseToken} , OrderID  {item.OrderID}");
                    consumablePurchaseRecord.Add(item);
                }
            }
        });

        HMSIAPManager.Instance.RestoreOwnedPurchases((restoredProducts) =>
        {
            foreach (var item in restoredProducts.InAppPurchaseDataList)
            {
                if ((IAPProductType)item.Kind == IAPProductType.Subscription)
                {
                    Debug.Log($"Subscription: ProductId {item.ProductId} , ExpirationDate {item.ExpirationDate} , AutoRenewing {item.AutoRenewing} , PurchaseToken {item.PurchaseToken} , OrderID {item.OrderID}");
                    activeSubscriptions.Add(item);
                }

                else if ((IAPProductType)item.Kind == IAPProductType.NonConsumable)
                {
                    Debug.Log($"NonConsumable: ProductId {item.ProductId} , DaysLasted {item.DaysLasted} , SubValid {item.SubValid} , PurchaseToken {item.PurchaseToken} ,OrderID {item.OrderID}");
                    activeNonConsumables.Add(item);
                }
            }
        });

    }


    public void PurchaseProduct(string productID)
    {
        Debug.Log($"PurchaseProduct");
        MyDebug("Purchase request: " + productID);

        LastProductID = productID;
        HMSIAPManager.Instance.PurchaseProduct(productID);
    }

    #region Callbacks

    private void OnBuyProductSuccess(PurchaseResultInfo obj)
    {
        Debug.Log($"OnBuyProductSuccess");


        if (obj.InAppPurchaseData.ProductId == ProductIDCoinsTiny)
        {
            CurrentBalance = PlayerPrefs.GetInt("Main Amount");
            NewBalance = CurrentBalance + 40000;
            PlayerPrefs.SetInt("Main Amount", NewBalance);
            SUIM.updateCoinsText();
        }
        else if (obj.InAppPurchaseData.ProductId == ProductIDCoinsSmall)
        {
            CurrentBalance = PlayerPrefs.GetInt("Main Amount");
            NewBalance = CurrentBalance + 90000;
            PlayerPrefs.SetInt("Main Amount", NewBalance);
            SUIM.updateCoinsText();
        }
        else if(obj.InAppPurchaseData.ProductId == ProductIDCoinsMedium)
        {
            CurrentBalance = PlayerPrefs.GetInt("Main Amount");
            NewBalance = CurrentBalance + 200000;
            PlayerPrefs.SetInt("Main Amount", NewBalance);
            SUIM.updateCoinsText();
        }
        else if (obj.InAppPurchaseData.ProductId == ProductIDCoinsLarge)
        {
            CurrentBalance = PlayerPrefs.GetInt("Main Amount");
            NewBalance = CurrentBalance + 420000;
            PlayerPrefs.SetInt("Main Amount", NewBalance);
            SUIM.updateCoinsText();
        }
        else if (obj.InAppPurchaseData.ProductId == ProductIDCoinsExtraLarge)
        {
            CurrentBalance = PlayerPrefs.GetInt("Main Amount");
            NewBalance = CurrentBalance + 850000;
            PlayerPrefs.SetInt("Main Amount", NewBalance);
            SUIM.updateCoinsText();
        }
        else if (obj.InAppPurchaseData.ProductId == ProductIDCoinsMassive)
        {
            CurrentBalance = PlayerPrefs.GetInt("Main Amount");
            NewBalance = CurrentBalance + 1730000;
            PlayerPrefs.SetInt("Main Amount", NewBalance);
            SUIM.updateCoinsText();
        }


        else if (obj.InAppPurchaseData.ProductId == ProductIDColour01)
        {
            PlayerPrefs.SetInt("Colour01_Premium", (true ? 1 : 0));
        }
        else if (obj.InAppPurchaseData.ProductId == ProductIDColour02)
        {
            PlayerPrefs.SetInt("Colour02_Premium", (true ? 1 : 0));
        }
        else if (obj.InAppPurchaseData.ProductId == ProductIDColour03)
        {
            PlayerPrefs.SetInt("Colour03_Premium", (true ? 1 : 0));
        }
        else if (obj.InAppPurchaseData.ProductId == ProductIDColour04)
        {
            PlayerPrefs.SetInt("Colour04_Premium", (true ? 1 : 0));
        }

        else if (obj.InAppPurchaseData.ProductId == ProductIDColour02Top)
        {
            PlayerPrefs.SetInt("Colour02_PremiumTop", (true ? 1 : 0));
        }
        else if (obj.InAppPurchaseData.ProductId == ProductIDColour03Top)
        {
            PlayerPrefs.SetInt("Colour03_PremiumTop", (true ? 1 : 0));
        }
        else if (obj.InAppPurchaseData.ProductId == ProductIDColour04Top)
        {
            PlayerPrefs.SetInt("Colour04_PremiumTop", (true ? 1 : 0));
        }

        //handles rims purchase
        else if (obj.InAppPurchaseData.ProductId == ProductIDRims01)
        {
            PlayerPrefs.SetInt("Rim01Premium", (true ? 1 : 0));
        }
        else if (obj.InAppPurchaseData.ProductId == ProductIDRims02)
        {
            PlayerPrefs.SetInt("Rim02Premium", (true ? 1 : 0));
        }
        else if (obj.InAppPurchaseData.ProductId == ProductIDRims03)
        {
            PlayerPrefs.SetInt("Rim03Premium", (true ? 1 : 0));
        }
        else if (obj.InAppPurchaseData.ProductId == ProductIDRims04)
        {
            PlayerPrefs.SetInt("Rim04Premium", (true ? 1 : 0));
        }
        else if (obj.InAppPurchaseData.ProductId == ProductIDRims05)
        {
            PlayerPrefs.SetInt("Rim05Premium", (true ? 1 : 0));
        }
        else if (obj.InAppPurchaseData.ProductId == ProductIDRims06)
        {
            PlayerPrefs.SetInt("Rim06Premium", (true ? 1 : 0));
        }

        //handles cars purchase
        else if (obj.InAppPurchaseData.ProductId == ProductIDCar02)
        {
            PlayerPrefs.SetInt("Car02Premium", (true ? 1 : 0));
        }
        else if (obj.InAppPurchaseData.ProductId == ProductIDCar03)
        {
            PlayerPrefs.SetInt("Car03Premium", (true ? 1 : 0));
        }
        else if (obj.InAppPurchaseData.ProductId == ProductIDCar04)
        {
            PlayerPrefs.SetInt("Car04Premium", (true ? 1 : 0));
        }







        SucessfullHWPurchase.Invoke();
    }

    private void OnInitializeIAPFailure(HMSException obj)
    {
        MyDebug("Initialisation failed: " + obj.ToString());
    }

    private void OnInitializeIAPSuccess()
    {
        MyDebug("Initialisation Suceeded");

        RestoreProducts();
    }

    private void OnBuyProductFailure(int code)
    {
        MyDebug("Purchase failed: " + code.ToString());
    }

    #endregion
}
