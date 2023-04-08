using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CurrencySystem : MonoBehaviour
{
    
    public int main_amount;
    public int run_amount;
    public int multiplier;

    public TMP_Text run_amount_text;
    public TMP_Text run_amount_shadow;
    // Start is called before the first frame update
    void Start()
    {
        run_amount = 0;
        multiplier = 1;
        main_amount = PlayerPrefs.GetInt("Main Amount");
    }

    // Update is called once per frame
    void Update()
    {
        // setting 
        
        run_amount_text.SetText(main_amount.ToString());
        run_amount_shadow.SetText(main_amount.ToString());
        //PlayerPrefs.SetInt("Main Amount", main_amount);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Coin")
        {
            run_amount=Addition_Function(Multiplier_Function(multiplier), run_amount);
            // Debug.Log(Addition_Function(Multiplier_Function(multiplier), run_amount));
            //Destroy(other.gameObject);
            other.gameObject.SetActive(false);
            main_amount++;
        }
    }
    public int Addition_Function(int coins_to_add, int current_coins)
    {
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

        return result;
    }

}
