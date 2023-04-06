using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shop_Item_coins : MonoBehaviour
{
    public int CoinsToBuy;
    int CurrentBalance;
    int NewBalance;
    public ShopUIManager SUIM;
    public void CoinPurchaseSucess()
    {
        CurrentBalance = PlayerPrefs.GetInt("Main Amount");
        NewBalance = CurrentBalance + CoinsToBuy;
        PlayerPrefs.SetInt("Main Amount", NewBalance);
        SUIM.updateCoinsText();
    }
}
