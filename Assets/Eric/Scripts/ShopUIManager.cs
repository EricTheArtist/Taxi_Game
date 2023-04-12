using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

public class ShopUIManager : MonoBehaviour
{
    public int Coins;
    public TMP_Text coins_amount_text;
    public TMP_Text coins_amount_shadow;
    public GameObject shopInterface;
    public GameObject PlayButton;
    public GameObject TaxiRankScene;
    public GameObject ModRidesBTNS;

    public GameObject Colour1Shop;
    public GameObject Colour2Shop;
    // Start is called before the first frame update
    void Start()
    {
        Coins = PlayerPrefs.GetInt("Main Amount");
        updateCoinsText();
    }

    // Update is called once per frame
    void Update()
    {/*
        if(Screen.orientation == ScreenOrientation.Portrait)
        {


            //sent ancorage
            RectTransform RT_Shop = shopInterface.GetComponent<RectTransform>();
            RT_Shop.anchorMin = new Vector2(0.5f, 1);
            RT_Shop.anchorMax = new Vector2(0.5f, 1);
            RT_Shop.pivot = new Vector2(0.5f, 1);

            RectTransform RT_BackBTN = PlayButton.GetComponent<RectTransform>();
            RT_BackBTN.anchorMin = new Vector2(0.5f, 0);
            RT_BackBTN.anchorMax = new Vector2(0.5f, 0);
            RT_BackBTN.pivot = new Vector2(0.5f, 0);

            RectTransform RT_ModRides = ModRidesBTNS.GetComponent<RectTransform>();
            RT_ModRides.anchorMin = new Vector2(0.5f, 0);
            RT_ModRides.anchorMax = new Vector2(0.5f, 0);
            RT_ModRides.pivot = new Vector2(0.5f, 0);
            
            //set positions
            RT_Shop.position = new Vector3(0, -10, 0);
            //shopInterface.transform.position = new Vector3(0,-10,0);
            PlayButton.transform.position = new Vector3(0, 20, 0);
            TaxiRankScene.transform.position = new Vector3(2.43f, 1f, -2.25f);
            ModRidesBTNS.transform.position = new Vector3(0, -1040, 0);

        }
        else
        {
            //set positions
            shopInterface.transform.localPosition = new Vector3(-10, -10, 0);
            PlayButton.transform.localPosition = new Vector3(20, 20, 0);
            TaxiRankScene.transform.position = new Vector3(0.87f,3.11f,-5.31f);
            ModRidesBTNS.transform.localPosition = new Vector3(850, 50, 0);
            //sent ancorage
            RectTransform RT_Shop = shopInterface.GetComponent<RectTransform>();
            RT_Shop.anchorMin = new Vector2(1, 1);
            RT_Shop.anchorMax = new Vector2(1, 1);
            RT_Shop.pivot = new Vector2(1, 1);

            RectTransform RT_BackBTN = PlayButton.GetComponent<RectTransform>();
            RT_BackBTN.anchorMin = new Vector2(0,0);
            RT_BackBTN.anchorMax = new Vector2(0,0);
            RT_BackBTN.pivot = new Vector2(0,0);

            RectTransform RT_ModRides = ModRidesBTNS.GetComponent<RectTransform>();
            RT_ModRides.anchorMin = new Vector2(0, 0);
            RT_ModRides.anchorMax = new Vector2(0, 0);
            RT_ModRides.pivot = new Vector2(0, 0);
       }*/ 
    }

    public void Button_Play()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    void RefreshCoinValue()
    {
        Coins = PlayerPrefs.GetInt("Main Amount");
    }
    public void updateCoinsText()
    {
        RefreshCoinValue();
        coins_amount_text.SetText(Coins.ToString());
        coins_amount_shadow.SetText(Coins.ToString());
    }

    public void DeductCoins(int deductAmount) //public function that can be called when an item is purchased with coins
    {
        Coins = Coins - deductAmount;
        PlayerPrefs.SetInt("Main Amount", Coins);
        updateCoinsText();
    }

    public bool CheckForEnoughMoney(int Cost) //Public function that must be called each time the player attempts to but something with coins
    {
        if(Cost < Coins)
        {
            return true;
        }
        else
        {
            return false;
        }
    
    }

    public void Button_OpenColour1()
    {
        Colour1Shop.SetActive(true);
    }

    public void Button_OpenColour2()
    {
        Colour2Shop.SetActive(true);
    }

    public void Button_ReturnToShopMain()
    {
        Colour1Shop.SetActive(false);
        Colour2Shop.SetActive(false);
    }
}
