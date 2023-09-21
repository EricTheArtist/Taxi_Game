using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using Unity.Services.Analytics;
using Unity.Services.Core;

public class ScriptedIAPListner : MonoBehaviour, IDetailedStoreListener
{

    private static IStoreController m_StoreController;          // The Unity Purchasing system.
    private static IExtensionProvider m_StoreExtensionProvider; // The store-specific Purchasing subsystems.

    IGooglePlayStoreExtensions m_GooglePlayStoreExtensions; //google extensions

    // list of product IDs for all products in the game 
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

    //refrence to purchase notice
    ExternalPurchaseNotice EPN;

    async void Start()
    {
        EPN = gameObject.GetComponent<ExternalPurchaseNotice>();

        try
        {
            await UnityServices.InitializeAsync();
            List<string> consentIdentifiers = await AnalyticsService.Instance.CheckForRequiredConsents();
        }
        catch (ConsentCheckException e)
        {

            Debug.Log("Unity Services Error: " + e);
            // Something went wrong when checking the GeoIP, check the e.Reason and handle appropriately.
        }


        InitializePurchasing();
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

    private bool IsInitialized()
    {
        return m_StoreController != null && m_StoreExtensionProvider != null;
    }

    void OnDeferredPurchase(UnityEngine.Purchasing.Product product)
    {
        //productpurchase is deffered
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        m_StoreController = controller;
        m_StoreExtensionProvider = extensions;
        m_GooglePlayStoreExtensions = extensions.GetExtension<IGooglePlayStoreExtensions>();
    }
    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs purchaseEvent) // called when a purchase is sucessfull 
    {
        EPN.externalPurchaseSucced(purchaseEvent.purchasedProduct); //grants product and activates notification
        return PurchaseProcessingResult.Complete;
    }
    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.Log("Purchasing Initialisation FAILED" + error);
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.Log("Purchasing Initialisation FAILED"+ error + message);
    }

    public void OnPurchaseFailed(UnityEngine.Purchasing.Product product, PurchaseFailureDescription failureDescription)
    {
        Debug.Log("Purchasing FAILED, Product: " + product + failureDescription);
    }

    public void OnPurchaseFailed(UnityEngine.Purchasing.Product product, PurchaseFailureReason failureReason)
    {
        Debug.Log("Purchasing FAILED, Product: " + product + failureReason);
    }





}
