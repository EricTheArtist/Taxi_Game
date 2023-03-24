using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class GameEndSystem : MonoBehaviour
{
    /// <summary>
    /// the players meets an end game under three conditions:
    /// 1. crashing headon into a collision
    /// 2. being stunned while already stunned
    /// 3. quitting
    /// 
    /// each condition will have its own function that when met will call the end game state
    /// the quit function will be used and created in a different system
    /// </summary>
   
    public bool endgame;
    public GameObject endgame_ui;
    CurrencySystem currency_system;

    public TMP_Text main_currency_text;
    public TMP_Text run_curreny_text;

    SimpleInputNamespace.TestCharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        currency_system = GetComponent<CurrencySystem>();
        controller = GetComponent<SimpleInputNamespace.TestCharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
       
        
    }
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.tag =="Obstruction")
        {
            Debug.Log("Hit and Endgame");
            endgame = true;
            controller.game_over = true;
            Endgame();
        }
    }
    void Collison_Crash()
    {

    }
    void Collision_Stun()
    {

    }
    void Stun_Timer()
    {

    }
    void Endgame()
    {
        endgame_ui.SetActive(true);
        Endgame_Calculations();
        //call endgame calculations here
        //save players current amount
    }
    void Endgame_Calculations()
    {
        run_curreny_text.SetText(currency_system.run_amount.ToString()); // = currency_system.run_amount_text; removed this cause it wasn't updating properly
        currency_system.main_amount=PlayerPrefs.GetInt("Main Amount");
        currency_system.main_amount = currency_system.main_amount + currency_system.run_amount;
        main_currency_text.SetText(currency_system.main_amount.ToString());
        PlayerPrefs.SetInt("Main Amount", currency_system.main_amount);

        currency_system.run_amount = 0;
    }
    public void restart_button()
    {
        Debug.Log("Game Restarted");
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

}
