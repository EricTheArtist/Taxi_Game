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
            shopInterface.transform.position = new Vector3(0,-400,0);
        }
        else
        {
            shopInterface.transform.position = new Vector3(600, 0, 0);
        }
    }

    public void Button_Play()
    {
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    public void updateCoinsText()
    {
        coins_amount_text.SetText(Coins.ToString());
        coins_amount_shadow.SetText(Coins.ToString());
    }

    public void DeductCoins(int deductAmount) //public function that can be called when an item is purchased with coins
    {
        Coins = Coins - deductAmount;
        PlayerPrefs.SetInt("Main Amount", Coins);
        updateCoinsText();
    }
}
