using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class ShopUIManager : MonoBehaviour
{
    public int Coins;
    public TMP_Text coins_amount_text;
    public TMP_Text coins_amount_shadow;
    public GameObject shopInterface;
    public GameObject PlayButton;
    public GameObject TaxiRankScene;

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
    {
        if(Screen.orientation == ScreenOrientation.Portrait)
        {
            
            shopInterface.transform.localPosition = new Vector3(0,430,0);
            PlayButton.transform.localPosition = new Vector3(0, -800, 0);
            TaxiRankScene.transform.position = new Vector3(2.43f, 1.5f, -2.25f);
        }
        else
        {
            shopInterface.transform.localPosition = new Vector3(600, 0, 0);
            PlayButton.transform.localPosition = new Vector3(-660, -330, 0);
            TaxiRankScene.transform.position = new Vector3(0.87f,3.11f,-5.31f);
        }
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
