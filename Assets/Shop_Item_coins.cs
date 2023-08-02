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
    public NonConsumablePurchasing NCP;
    public string ProductID;
    public Text PriceTag;

    public HW_IAP_Manager HWIAPM;
    public bool HWProduct = false;

    
    public void CoinPurchaseRequest()
    {
        if(HWProduct == false)
        {
            NCP.BuyGold(ProductID,CoinsToBuy);
        }

    }

    public void Start()
    {
        if (HWProduct == true && HWIAPM == null)
        {
            HWIAPM = GameObject.Find("HuaweiIAPManager").GetComponent<HW_IAP_Manager>();
        }
        if (HWProduct == false)
        {
            PriceTag.text = NCP.priceString(ProductID);
        }
        if(HWProduct == true)
        {
            PriceTag.text = HWIAPM.HWpriceString(ProductID);
        }
    }
}
