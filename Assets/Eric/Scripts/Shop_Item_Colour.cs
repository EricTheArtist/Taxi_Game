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
    public string MyproductID;

    public bool Premium = false;
    public NonConsumablePurchasing NCP;

    public bool TestWithoutIAPButton = false;

    public Text RealCurrencyPrice;
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
        if (Premium == true)
        {
            RealCurrencyPrice.text = NCP.priceString(MyproductID);
        }
    }

    // Update is called once per frame
 
    public void Button_Clicked_Colour()
    {
        if (SUIM.CheckForEnoughMoney(Price) == true && Owned == false && Premium == false)
        {
            SUIM.OpenPurchaseDialouge(Price,PlayerPrefName,100,100);
            //Owned = true;
            //SUIM.DeductCoins(Price);
            //PriceBG.SetActive(false);

            //PlayerPrefs.SetInt(PlayerPrefName, (Owned ? 1 : 0));
        }
        if (Owned == true)
        {
            ApplyColour();

            SaveMyColor();
            
        }
        if(Premium == true && TestWithoutIAPButton == true && Owned == false)
        {
            NCP.BuyProduct(MyproductID);
        }


        SUIM.refreshcolouronsamples();
    }

    public void RealCurrencyColourPurchaseSucess()
    {
        Owned = (PlayerPrefs.GetInt(PlayerPrefName) != 0);
        Debug.Log("Owned Check" + PlayerPrefName + Owned);
        if(Owned == true && NCP.ProductIDfromButton == MyproductID)
        {
            PriceBG.SetActive(false);
            SUIM.refreshcolouronsamples();
            ApplyColour();
            SaveMyColor();

        }

    }

    void SaveMyColor()
    {
        if (shaderInput == "_Color")
        {
            PlayerPrefs.SetString("ColourBottomSave", ColorUtility.ToHtmlStringRGB(Colour));
        }
        else
        {
            PlayerPrefs.SetString("ColourTopSave", ColorUtility.ToHtmlStringRGB(Colour));
        }
    }

    void ApplyColour()
    {
        TaxiMaterial.GetComponent<Renderer>().sharedMaterial.SetColor(shaderInput, Colour);
    }

    public void PurchaseEvent()
    {
        bool PrviousOwner = Owned;

        Owned = (PlayerPrefs.GetInt(PlayerPrefName) != 0);
        
        if(PrviousOwner == false && Owned == true)
        {
            PriceBG.SetActive(false);
        }

    }


}
