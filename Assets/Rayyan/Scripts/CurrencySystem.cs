using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class CurrencySystem : MonoBehaviour
{
    
    public int main_amount; // READ ONLY, DO NOT SET
    public int run_amount;
    public int multiplier;
    int Coins;

    public TMP_Text run_amount_text;
    public TMP_Text run_amount_shadow;

    
    public ParticleSystem Spark_CoinPickup;

    [SerializeField] private GameObject coinIcon;
    [SerializeField] private AudioClip _coinCollectClip;

    // Start is called before the first frame update
    void Start()
    {
        run_amount = 0;
        multiplier = 1;
        main_amount = PlayerPrefs.GetInt("Main Amount");
        RefreshCoinAmountDisplay();
        
    }

    // Update is called once per frame
    void Update()
    {
        // setting 
        

        //PlayerPrefs.SetInt("Main Amount", main_amount);
    }

    void RefreshCoinAmountDisplay()
    {
        main_amount = PlayerPrefs.GetInt("Main Amount");
        run_amount_text.SetText(main_amount.ToString());
        run_amount_shadow.SetText(main_amount.ToString());
    }

    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin" || other.tag == "Coin2")
        {
            run_amount=Addition_Function(Multiplier_Function(multiplier), run_amount); //not sure why we need such a complex function for this but run amount is only used for the display when the player crashes and should not be used for adding anyting to the saves coins
            
            SoundManager.Instance.PlaySound(_coinCollectClip);
            Spark_CoinPickup.Play();
            other.gameObject.SetActive(false);
            Eric_AddCoins(1*multiplier); //adds the coins to the saved coins
            RefreshCoinAmountDisplay();
        }
    }

    public void Eric_DeductCoins(int deductAmount) //all coin deduction in the game scene must run through this function
    {
        Coins = PlayerPrefs.GetInt("Main Amount");
        if (deductAmount > Coins)
        {
            Coins = 0;
        }
        else
        {
            Coins = Coins - deductAmount;
        }
        
        PlayerPrefs.SetInt("Main Amount", Coins);
        RefreshCoinAmountDisplay();
    }

    public void Eric_AddCoins(int AddAmount) //all coin addition in the game scene must run through this function
    {
        Coins = PlayerPrefs.GetInt("Main Amount");
        Coins = Coins + AddAmount;
        PlayerPrefs.SetInt("Main Amount", Coins);
        RefreshCoinAmountDisplay();
    }




    public int Addition_Function(int coins_to_add, int current_coins)
    {
        coinIcon.GetComponent<Animator>().SetTrigger("isRotating");
        ///Debug.Log(coins_to_add + "  " + current_coins);
        current_coins += coins_to_add;
        return current_coins;
    }
    public int Subtraction_Function(int coins_to_sub, int current_coins) 
    {
        current_coins -= coins_to_sub;
        return current_coins;
    }
    public int Multiplier_Function(int coin_multiplier)
    {
        //if a coin multiplier is active, each coin earned will be multiuplied by that said multiplier.
        int coin_value = 1;
        int result = coin_value * coin_multiplier;
        //Debug.Log(result);
        return result;
    }

}
