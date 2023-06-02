using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using TMPro;

public class ExternalPurchaseNotice : MonoBehaviour //IStoreListener
{
    public GameObject Notice;

    //int CoinsToBuy = 5000;
    int CurrentBalance;
    int NewBalance;
    public TMP_Text ProductName;
    public TMP_Text run_amount_text;
    public TMP_Text run_amount_shadow;
    /*
    private IStoreController storeController;

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        storeController = controller;
    }
    */
    // Start is called before the first frame update
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
            
        }
        if (product.definition.id == "com.vetkoekstudios.taxiranked.preregister10000")
        {
            ProductName.SetText("10000 Coins Pre-Registration Reward");
        }
        if (product.definition.id == "com.vetkoekstudios.taxiranked.colour01")
        {
            PlayerPrefs.SetInt("Colour01_Premium", (true ? 1 : 0));
            ProductName.SetText("Colour product Restored");
        }
        if (product.definition.id == "com.vetkoekstudios.taxiranked.rims01")
        {
            PlayerPrefs.SetInt("Rim05Premium", (true ? 1 : 0));
            ProductName.SetText("Rims product Restored");
        }
        if (product.definition.id == "com.vetkoekstudios.taxiranked.car01")
        {
            PlayerPrefs.SetInt("Car01Premium", (true ? 1 : 0));
            ProductName.SetText("Car product Restored");
        }


        var payout = product.definition.payout.quantity; // Assuming a single payout, retrieve the payout quantity
        NewBalance = CurrentBalance + (int)payout;
        PlayerPrefs.SetInt("Main Amount", NewBalance);

        // updates text on screen
        if (run_amount_shadow != null && run_amount_text != null)
        {
            run_amount_text.SetText(NewBalance.ToString());
            run_amount_shadow.SetText(NewBalance.ToString());
        }
        //Notice.SetActive(true);
        //ProductName.SetText("First script ran");
        if (product.hasReceipt) // checks to see if the product is looged as owned
        {
            //Notice.SetActive(true);
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
        /*
        if (product != null && product.availableToPurchase) // buys the product if it is avalable to purchaase
        {
            storeController.ConfirmPendingPurchase(product);
            storeController.InitiatePurchase("restored: "+ product.definition.id);
        }
        if (!product.hasReceipt)
        {
            Notice.SetActive(true);
            ProductName.SetText(product.definition.id.ToString());
            storeController.InitiatePurchase(product.definition.id);
        }
        */
    }

    public void ExternalCoinsProduct5000(Product product)
    {

        //ProductName.SetText(product.definition.id.ToString());
        CurrentBalance = PlayerPrefs.GetInt("Main Amount");

        if (product.definition.id == "com.vetkoekstudios.taxiranked.promocoin5000")
        {
            ProductName.SetText("5000 Coins Promo");
        }
        if (product.definition.id == "com.vetkoekstudios.taxiranked.preregister10000")
        {
            ProductName.SetText("10000 Coins Pre-Registration Reward");
        }
        if (product.definition.id == "com.vetkoekstudios.taxiranked.colour01")
        {
            PlayerPrefs.SetInt("Colour01_Premium", (true ? 1 : 0));
            ProductName.SetText("Colour product Restored");
        }
        if (product.definition.id == "com.vetkoekstudios.taxiranked.rims01")
        {
            PlayerPrefs.SetInt("Rim05Premium", (true ? 1 : 0));
            ProductName.SetText("Rims product Restored");
        }
        if (product.definition.id == "com.vetkoekstudios.taxiranked.car01")
        {
            PlayerPrefs.SetInt("Car01Premium", (true ? 1 : 0));
            ProductName.SetText("Car product Restored");
        }


        var payout = product.definition.payout.quantity; // Assuming a single payout, retrieve the payout quantity
        NewBalance = CurrentBalance + (int)payout;
        PlayerPrefs.SetInt("Main Amount", NewBalance);

        // updates text on screen
        if(run_amount_shadow!=null && run_amount_text != null)
        {
           run_amount_text.SetText(NewBalance.ToString());
           run_amount_shadow.SetText(NewBalance.ToString());
        }
        Notice.SetActive(true);
        ProductName.SetText("Second script ran");
    }

    public void CloseExternalPurchaseNotice()
    {
        Notice.SetActive(false);
    }

  /*  void IStoreListener.OnInitializeFailed(InitializationFailureReason error)
    {
        throw new System.NotImplementedException();
    }

    void IStoreListener.OnInitializeFailed(InitializationFailureReason error, string message)
    {
        throw new System.NotImplementedException();
    }

    PurchaseProcessingResult IStoreListener.ProcessPurchase(PurchaseEventArgs purchaseEvent)
    {
        return PurchaseProcessingResult.Complete;
    }

    void IStoreListener.OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        throw new System.NotImplementedException();
    }
*/
  //  void IStoreListener.OnInitialized(IStoreController controller, IExtensionProvider extensions)
  //  {
  //      throw new System.NotImplementedException();
  //  }
}
