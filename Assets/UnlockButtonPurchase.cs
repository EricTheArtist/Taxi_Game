using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnlockButtonPurchase : MonoBehaviour
{
    public int Price;
    ShopUIManager SUIM;
    public string PlayerPrefName;
    public bool Owned;

    // Start is called before the first frame update
    void Start()
    {
        GameObject canvas;
        canvas = GameObject.Find("Canvas");
        SUIM = canvas.GetComponent<ShopUIManager>();

        Owned = (PlayerPrefs.GetInt(PlayerPrefName) != 0);

        if (Owned == true) //if the player already owns the item, disables the price overlay
        {
            Destroy(gameObject);

        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ButtonUnlockUI()
    {
        if (SUIM.CheckForEnoughMoney(Price) == true && Owned == false) // if the player has enough money and does not own the item
        {
            SUIM.OpenPurchaseDialouge(Price, PlayerPrefName, 100, 100);

            //Owned = true;
            //SUIM.DeductCoins(Price);

            //PlayerPrefs.SetInt(PlayerPrefName, (Owned ? 1 : 0)); //sets ownwership in the player prefs
            //Destroy(gameObject);
        }
    }

    public void PurchaseEvent()
    {
        bool PrviousOwner = Owned;

        Owned = (PlayerPrefs.GetInt(PlayerPrefName) != 0);

        if (PrviousOwner == false && Owned == true)
        {
            Destroy(gameObject);
        }

    }
}
