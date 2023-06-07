using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Shop_Item_Colour : MonoBehaviour
{
    public int Price;
    public Color Colour;
    public bool Owned;
    public TMP_Text Price_Text;
    ShopUIManager SUIM;
    public GameObject PriceBG;

    public Image ColourSample;
    public string PlayerPrefName;

    public GameObject TaxiMaterial;

    public string shaderInput = "_Color";
    public string productID;

    public bool Premium = false;
    public NonConsumablePurchasing NCP;


    // Start is called before the first frame update
    void Start()
    {   
        GameObject canvas;
        canvas = GameObject.Find("Canvas");
        SUIM = canvas.GetComponent<ShopUIManager>();
        ColourSample.color = Colour;
        Owned = (PlayerPrefs.GetInt(PlayerPrefName) != 0);



        if (Premium == false)
        {
            Price_Text.SetText(Price.ToString());
        }
        if(Owned == true)
        {
            PriceBG.SetActive(false);

        }
    }

    // Update is called once per frame
 
    public void Button_Clicked_Colour()
    {
        if (SUIM.CheckForEnoughMoney(Price) == true && Owned == false && Premium == false)
        {
            Owned = true;
            SUIM.DeductCoins(Price);
            PriceBG.SetActive(false);

            PlayerPrefs.SetInt(PlayerPrefName, (Owned ? 1 : 0));
        }
        if (Owned == true)
        {
            TaxiMaterial.GetComponent<Renderer>().sharedMaterial.SetColor(shaderInput, Colour);
        }

        SUIM.refreshcolouronsamples();
    }

    public void RealCurrencyColourPurchaseSucess()
    {
        Owned = true;
        PriceBG.SetActive(false);
        PlayerPrefs.SetInt(PlayerPrefName, (Owned ? 1 : 0));
        SUIM.refreshcolouronsamples();
        NCP.BuyProduct(productID);
    }



}
