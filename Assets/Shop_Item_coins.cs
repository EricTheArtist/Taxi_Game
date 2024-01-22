using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Shop_Item_coins : MonoBehaviour
{
    public int CoinsToBuy;
    int CurrentBalance;
    int NewBalance;
    public ShopUIManager SUIM;
    //public NonConsumablePurchasing NCP;
    public string ProductID;
    public Text PriceTag;
    public void CoinPurchaseRequest()
    {
       // NCP.BuyGold(ProductID,CoinsToBuy);

    }

    public void Start()
    {
      //  PriceTag.text = NCP.priceString(ProductID);
    }
}
