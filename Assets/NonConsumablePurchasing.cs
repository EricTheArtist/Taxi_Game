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

    public string ProductIDRims01 = "com.vetkoekstudios.taxiranked.rims01";


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

        builder.AddProduct(ProductIDRims01, ProductType.NonConsumable);

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
        BuyProductID(ProductID);
        ProductIDfromButton = ProductID;
        UnityEngine.Purchasing.Product productREF = m_StoreController.products.WithID(ProductIDfromButton);
        payoutREF = payout;
    }

    public void BuyProduct(string ProductID)
    {
        BuyProductID(ProductID);
        ProductIDfromButton = ProductID;
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

        //var validator = new CrossPlatformValidator(GooglePlayTangle.Data(), AppleTangle.Data(), Application.identifier);
        //var result = validator.Validate(args.purchasedProduct.receipt);
        //MyDebug("Validate = " + result.ToString());

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
                CurrentBalance = PlayerPrefs.GetInt("Main Amount");
                NewBalance = CurrentBalance + payoutREF;
                PlayerPrefs.SetInt("Main Amount", NewBalance);
                SUIM.updateCoinsText();
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

            //handles rims purchase
            if (ProductIDfromButton == ProductIDRims01)
            {
                PlayerPrefs.SetInt("Rim01Premium", (true ? 1 : 0));
            }


            SucessfullPurchase.Invoke();
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
        myText.text += "\r\n" + debug;
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
