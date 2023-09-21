using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.UI;

using UnityEngine.Purchasing.Security;
using Unity.Services.Analytics;
using Unity.Services.Core;
using UnityEngine.Purchasing.Extension;
using UnityEngine.Events;





public class NonConsumablePurchasing : MonoBehaviour, IDetailedStoreListener
{
    private static IStoreController m_StoreController;          // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.
    private static UnityEngine.Purchasing.Product test_product = null;

    IGooglePlayStoreExtensions m_GooglePlayStoreExtensions;

    public static string ProductIDCoinsTiny = "com.vetkoekstudios.pemanduteksi.coinstiny";
    public static string ProductIDCoinsSmall = "com.vetkoekstudios.pemanduteksi.coinssmall";
    public static string ProductIDCoinsMedium = "com.vetkoekstudios.pemanduteksi.coinsmediumv2";
    public static string ProductIDCoinsLarge = "com.vetkoekstudios.pemanduteksi.coinslargev2";
    public static string ProductIDCoinsExtraLarge = "com.vetkoekstudios.pemanduteksi.coinsextralargev2";
    public static string ProductIDCoinsMassive = "com.vetkoekstudios.pemanduteksi.coinsmassivev2";

    public string ProductIDColour01 = "com.vetkoekstudios.pemanduteksi.colour01";
    public string ProductIDColour02 = "com.vetkoekstudios.pemanduteksi.colour02";
    public string ProductIDColour03 = "com.vetkoekstudios.pemanduteksi.colour03";
    public string ProductIDColour04 = "com.vetkoekstudios.pemanduteksi.colour04";

    public string ProductIDColour02Top = "com.vetkoekstudios.pemanduteksi.colour02top";
    public string ProductIDColour03Top = "com.vetkoekstudios.pemanduteksi.colour03top";
    public string ProductIDColour04Top = "com.vetkoekstudios.pemanduteksi.colour04top";

    public string ProductIDRims01 = "com.vetkoekstudios.pemanduteksi.rims01";
    public string ProductIDRims02 = "com.vetkoekstudios.pemanduteksi.rims02";
    public string ProductIDRims03 = "com.vetkoekstudios.pemanduteksi.rims03";
    public string ProductIDRims04 = "com.vetkoekstudios.pemanduteksi.rims04";
    public string ProductIDRims05 = "com.vetkoekstudios.pemanduteksi.rims05";
    public string ProductIDRims06 = "com.vetkoekstudios.pemanduteksi.rims06";

    public string ProductIDCar02 = "com.vetkoekstudios.pemanduteksi.car02";
    public string ProductIDCar03 = "com.vetkoekstudios.pemanduteksi.car03";
    public string ProductIDCar04 = "com.vetkoekstudios.pemanduteksi.car04";
    public string ProductIDCar05 = "com.vetkoekstudios.pemanduteksi.car05";
    public string ProductIDCar06 = "com.vetkoekstudios.pemanduteksi.car06";

    public string ProductIDnoads = "com.vetkoekstudios.pemanduteksi.noads";

    public string ProductIDfromButton;

    public Text myText;

    private bool return_complete = true;

    public UnityEvent SucessfullPurchase;

    int CurrentBalance;
    int NewBalance;
    public ShopUIManager SUIM;
    int payoutREF;

    async void Start()
    {
        try
        {
            await UnityServices.InitializeAsync();
            List<string> consentIdentifiers = await AnalyticsService.Instance.CheckForRequiredConsents();
        }
        catch (ConsentCheckException e)
        {
            MyDebug("Consent: :" + e.ToString());  // Something went wrong when checking the GeoIP, check the e.Reason and handle appropriately.
        }

        MyAction += myFunction;

        InitializePurchasing();

    }

    public bool CheckProductOwnership(string productID)
    {
        UnityEngine.Purchasing.Product product = m_StoreController.products.WithID(productID);

        if(product != null && !product.hasReceipt)
        {
            return false;
        }
        else
        {
            return true;
        }
    }

    public string priceString(string ProductID)
    {
        return m_StoreController.products.WithID(ProductID).metadata.localizedPriceString;
    }

    public void InitializePurchasing()
    {
        if (IsInitialized())
        {
            return;
        }

        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        builder.Configure<IGooglePlayConfiguration>().SetDeferredPurchaseListener(OnDeferredPurchase);
        builder.Configure<IGooglePlayConfiguration>().SetQueryProductDetailsFailedListener(MyAction);

        builder.AddProduct(ProductIDCoinsTiny, ProductType.Consumable);
        builder.AddProduct(ProductIDCoinsSmall, ProductType.Consumable);
        builder.AddProduct(ProductIDCoinsMedium, ProductType.Consumable);
        builder.AddProduct(ProductIDCoinsLarge, ProductType.Consumable);
        builder.AddProduct(ProductIDCoinsExtraLarge, ProductType.Consumable);
        builder.AddProduct(ProductIDCoinsMassive, ProductType.Consumable);



        builder.AddProduct(ProductIDColour01, ProductType.NonConsumable);
        builder.AddProduct(ProductIDColour02, ProductType.NonConsumable);
        builder.AddProduct(ProductIDColour03, ProductType.NonConsumable);
        builder.AddProduct(ProductIDColour04, ProductType.NonConsumable);

        builder.AddProduct(ProductIDColour02Top, ProductType.NonConsumable);
        builder.AddProduct(ProductIDColour03Top, ProductType.NonConsumable);
        builder.AddProduct(ProductIDColour04Top, ProductType.NonConsumable);

        builder.AddProduct(ProductIDRims01, ProductType.NonConsumable);
        builder.AddProduct(ProductIDRims02, ProductType.NonConsumable);
        builder.AddProduct(ProductIDRims03, ProductType.NonConsumable);
        builder.AddProduct(ProductIDRims04, ProductType.NonConsumable);
        builder.AddProduct(ProductIDRims05, ProductType.NonConsumable);
        builder.AddProduct(ProductIDRims06, ProductType.NonConsumable);

        builder.AddProduct(ProductIDCar02, ProductType.NonConsumable);
        builder.AddProduct(ProductIDCar03, ProductType.NonConsumable);
        builder.AddProduct(ProductIDCar04, ProductType.NonConsumable);
        builder.AddProduct(ProductIDCar05, ProductType.NonConsumable);
        builder.AddProduct(ProductIDCar06, ProductType.NonConsumable);

        builder.AddProduct(ProductIDnoads, ProductType.NonConsumable);

        UnityPurchasing.Initialize(this, builder);
        
    }

    private event System.Action<int> MyAction;

    void myFunction(int myInt)
    {
        MyDebug("Listener = " + myInt.ToString());
    }
    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    void OnDeferredPurchase(UnityEngine.Purchasing.Product product)
    {
        MyDebug($"Purchase of {product.definition.id} is deferred");
    }

    public void BuySubscription()
    {
        BuyProductID(ProductIDColour01);
    }

    public void BuyGold(string ProductID, int payout)
    {
        
        ProductIDfromButton = ProductID;
        UnityEngine.Purchasing.Product productREF = m_StoreController.products.WithID(ProductIDfromButton);
        payoutREF = payout;
        BuyProductID(ProductID);
    }

    public void BuyProduct(string ProductID)
    {   
        ProductIDfromButton = ProductID;
        BuyProductID(ProductID);
        
    }

    public void CompletePurchase()
    {
        if (test_product == null)
            MyDebug("Cannot complete purchase, product not initialized.");
        else
        {
            m_StoreController.ConfirmPendingPurchase(test_product);
            MyDebug("Completed purchase with " + test_product.transactionID.ToString());
        }

    }

    public void ToggleComplete()
    {
        return_complete = !return_complete;
        MyDebug("Complete = " + return_complete.ToString());

    }

    /*
    public void RestorePurchases()
    {
        m_StoreExtensionProvider.GetExtension<IAppleExtensions>().RestoreTransactions(result =>
        {

            if (result)
            {
                MyDebug("Restore purchases succeeded.");
                Dictionary<string, object> parameters = new Dictionary<string, object>()
                {
                    { "restore_success", true },
                };
                AnalyticsService.Instance.CustomData("myRestore", parameters);
            }
            else
            {
                MyDebug("Restore purchases failed.");
                Dictionary<string, object> parameters = new Dictionary<string, object>()
                {
                    { "restore_success", false },
                };
                AnalyticsService.Instance.CustomData("myRestore", parameters);
            }

            AnalyticsService.Instance.Flush();
        });

    } 
    */

    void BuyProductID(string productId)
    {
        if (IsInitialized())
        {
            UnityEngine.Purchasing.Product product = m_StoreController.products.WithID(productId);

            if (product != null && product.availableToPurchase)
            {
                MyDebug(string.Format("Purchasing product:" + product.definition.id.ToString()));
                m_StoreController.InitiatePurchase(product);
            }
            else
            {
                MyDebug("BuyProductID: FAIL. Not purchasing product, either is not found or is not available for purchase");
            }
        }
        else
        {
            MyDebug("BuyProductID FAIL. Not initialized.");
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        MyDebug("OnInitialized: PASS");

        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
        m_GooglePlayStoreExtensions = extensions.GetExtension<IGooglePlayStoreExtensions>();

    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        // Purchasing set-up has not succeeded. Check error for reason. Consider sharing this reason with the user.
        MyDebug("OnInitializeFailed InitializationFailureReason:" + error);
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        test_product = args.purchasedProduct;

#if !UNITY_EDITOR
        var validator = new CrossPlatformValidator(GooglePlayTangle.Data(), AppleTangle.Data(), Application.identifier);
        var result = validator.Validate(args.purchasedProduct.receipt); //validate puchase
        //MyDebug("Validate = " + result.ToString());
#endif
        if (m_GooglePlayStoreExtensions.IsPurchasedProductDeferred(test_product))
        {
            //The purchase is Deferred.
            //Therefore, we do not unlock the content or complete the transaction.
            //ProcessPurchase will be called again once the purchase is Purchased.
            return PurchaseProcessingResult.Pending;
        }
        if (return_complete)
        {
            

            //handles currency payout
            if(SUIM!= null)
            {
                CurrentBalance = PlayerPrefs.GetInt("Main Amount");
                NewBalance = CurrentBalance + payoutREF;
                PlayerPrefs.SetInt("Main Amount", NewBalance);
                SUIM.updateCoinsText();
            }

                payoutREF = 0;

            //handles colour purchase
            if(ProductIDfromButton == ProductIDColour01)
            {
                PlayerPrefs.SetInt("Colour01_Premium", (true ? 1 : 0));
            }
            if (ProductIDfromButton == ProductIDColour02)
            {
                PlayerPrefs.SetInt("Colour02_Premium", (true ? 1 : 0));
            }
            if (ProductIDfromButton == ProductIDColour03)
            {
                PlayerPrefs.SetInt("Colour03_Premium", (true ? 1 : 0));
            }
            if (ProductIDfromButton == ProductIDColour04)
            {
                PlayerPrefs.SetInt("Colour04_Premium", (true ? 1 : 0));
            }

            if (ProductIDfromButton == ProductIDColour02Top)
            {
                PlayerPrefs.SetInt("Colour02_PremiumTop", (true ? 1 : 0));
            }
            if (ProductIDfromButton == ProductIDColour03Top)
            {
                PlayerPrefs.SetInt("Colour03_PremiumTop", (true ? 1 : 0));
            }
            if (ProductIDfromButton == ProductIDColour04Top)
            {
                PlayerPrefs.SetInt("Colour04_PremiumTop", (true ? 1 : 0));
            }

            //handles rims purchase
            if (ProductIDfromButton == ProductIDRims01)
            {
                PlayerPrefs.SetInt("Rim01Premium", (true ? 1 : 0));
            }
            if (ProductIDfromButton == ProductIDRims02)
            {
                PlayerPrefs.SetInt("Rim02Premium", (true ? 1 : 0));
            }
            if (ProductIDfromButton == ProductIDRims03)
            {
                PlayerPrefs.SetInt("Rim03Premium", (true ? 1 : 0));
            }
            if (ProductIDfromButton == ProductIDRims04)
            {
                PlayerPrefs.SetInt("Rim04Premium", (true ? 1 : 0));
            }
            if (ProductIDfromButton == ProductIDRims05)
            {
                PlayerPrefs.SetInt("Rim05Premium", (true ? 1 : 0));
            }
            if (ProductIDfromButton == ProductIDRims06)
            {
                PlayerPrefs.SetInt("Rim06Premium", (true ? 1 : 0));
            }

            //handles cars purchase
            if (ProductIDfromButton == ProductIDCar02)
            {
                PlayerPrefs.SetInt("Car02Premium", (true ? 1 : 0));
            }
            if (ProductIDfromButton == ProductIDCar03)
            {
                PlayerPrefs.SetInt("Car03Premium", (true ? 1 : 0));
            }
            if (ProductIDfromButton == ProductIDCar04)
            {
                PlayerPrefs.SetInt("Car04Premium", (true ? 1 : 0));
            }
            if (ProductIDfromButton == ProductIDCar05)
            {
                PlayerPrefs.SetInt("Car05Premium", (true ? 1 : 0));
            }
            if (ProductIDfromButton == ProductIDCar06)
            {
                PlayerPrefs.SetInt("Car06Premium", (true ? 1 : 0));
            }

            if (ProductIDfromButton == ProductIDnoads)
            {
                PlayerPrefs.SetInt("NoAds", (true ? 1 : 0));
            }


            SucessfullPurchase.Invoke();
            PlayerLevelSystem.PLSinstance.AddXP(50);
            MyDebug(string.Format("ProcessPurchase: Complete. Product:" + args.purchasedProduct.definition.id + " - " + test_product.transactionID.ToString()));
            return PurchaseProcessingResult.Complete;
            
        }
        else
        {
            MyDebug(string.Format("ProcessPurchase: Pending. Product:" + args.purchasedProduct.definition.id + " - " + test_product.transactionID.ToString()));
            return PurchaseProcessingResult.Pending;
        }

    }

    public void OnPurchaseFailed(UnityEngine.Purchasing.Product product, PurchaseFailureReason failureReason)
    {
        MyDebug(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureReason));
    }

    public void ListPurchases()
    {
        foreach (UnityEngine.Purchasing.Product item in m_StoreController.products.all)
        {
            if (item.hasReceipt)
            {
                MyDebug("In list for  " + item.receipt.ToString());
            }
            else
                MyDebug("No receipt for " + item.definition.id.ToString());
        }
    }
    private void MyDebug(string debug)
    {
        Debug.Log(debug);
        if(myText != null)
        {
            myText.text += "\r\n" + debug;
        }
        
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new System.NotImplementedException();
    }

    public void OnPurchaseFailed(UnityEngine.Purchasing.Product product, PurchaseFailureDescription failureDescription)
    {
        MyDebug(string.Format("OnPurchaseFailed: FAIL. Product: '{0}', PurchaseFailureReason: {1}", product.definition.storeSpecificId, failureDescription));
    }
}
