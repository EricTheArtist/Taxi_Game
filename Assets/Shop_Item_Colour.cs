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
    
    // Start is called before the first frame update
    void Start()
    {
        GameObject canvas;
        canvas = GameObject.Find("Canvas");
        SUIM = canvas.GetComponent<ShopUIManager>();
        ColourSample.color = Colour;
        Price_Text.SetText(Price.ToString());
        
        Owned = (PlayerPrefs.GetInt(PlayerPrefName) != 0);

        if(Owned == true)
        {
            PriceBG.SetActive(false);
        }
    }

    // Update is called once per frame
 
    public void Button_Clicked_Colour()
    {
        if (SUIM.CheckForEnoughMoney(Price) == true && Owned == false)
        {
            Owned = true;
            SUIM.DeductCoins(Price);
            PriceBG.SetActive(false);

            PlayerPrefs.SetInt(PlayerPrefName, (Owned ? 1 : 0));
        }
        if (Owned == true)
        {
            TaxiMaterial.GetComponent<Renderer>().sharedMaterial.SetColor("_Color", Colour);
        }
    }
}
