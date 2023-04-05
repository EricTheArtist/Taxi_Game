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
    public GameObject welcome_ui;
    CurrencySystem currency_system;

    public TMP_Text main_currency_text;
    public TMP_Text run_curreny_text;

    public Test_Floating_Origin FloatingOrigin;

    SimpleInputNamespace.TestCharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        currency_system = GetComponent<CurrencySystem>();
        controller = GetComponent<SimpleInputNamespace.TestCharacterController>();
        controller.game_over = true;
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

        // here we need to add calculations for the player's distance high score and check their placement on the leaderboard
    }
    public void restart_button()
    {
        Debug.Log("Game Restarted");
        //SceneManager.LoadScene(SceneManager.GetActiveScene().name); // reloading the whole scene is might cause big peformance hit as our game becomes more complex
        // instead we shoud do the following:

        // reset posion of enviroment (this can be done using the same approach as in the floating origin system)
        FloatingOrigin.resetToOrigin();

        // reset position of player
        controller.hPosition = 0;
        this.transform.position = new Vector3(0, 0.5f, 0);
        transform.rotation = Quaternion.identity;

        // reset the current lane var in the player
        controller.MoveFromLane = 2;

        // reset the player's distance score

        // reset the momentum of the player

        // reset the obsticle spawning system

        // restart the movement in the controller
        controller.game_over = false;

        endgame_ui.SetActive(false);
    }

    public void main_button()
    {
        welcome_ui.SetActive(true);
        endgame_ui.SetActive(false);
    }
    public void play_button()
    {
        welcome_ui.SetActive(false);
        restart_button();
    }

    public void shop_button()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

}