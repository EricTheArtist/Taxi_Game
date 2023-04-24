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
    public GameObject welcome_ui_static;
    public GameObject welcome_ui_dynamic;
    public GameObject rank_ui_static;
    public GameObject rank_ui_dynamic;
    public GameObject settigns_ui_static;
    public GameObject settigns_ui_dynamic;
    CurrencySystem currency_system;
    public PickUpSystem PUS;

    public TMP_Text Score_Text;
    public TMP_Text run_curreny_text;

    public Test_Floating_Origin FloatingOrigin;
    public RoadSpawner RS;

    Test_HighScore THS;

    SimpleInputNamespace.TestCharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        THS = gameObject.GetComponent<Test_HighScore>();
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
        run_curreny_text.SetText(currency_system.run_amount.ToString());
        Score_Text.SetText(THS.scoreInt.ToString());
        PlayerPrefs.SetInt("Main Amount", currency_system.main_amount);

        currency_system.run_amount = 0;
        // reset the player's distance score
        THS.ResetScore();
        // here we need to add calculations for the player's distance high score and check their placement on the leaderboard
    }
    public void restart_button()
    {
        Debug.Log("Game Restarted");

        // reset posion of enviroment (this can be done using the same approach as in the floating origin system)
        FloatingOrigin.resetToOrigin();

        // reset position of player
        controller.hPosition = 0;
        this.transform.position = new Vector3(0, 0.5f, 0);
        transform.rotation = Quaternion.identity;

        // reset the current lane var in the player
        controller.MoveFromLane = 2;

        // reset the player's distance score
        //THS.ResetScore();

        // reset the momentum of the player
        controller.movementSpeed = 10f;

        // reset the obsticle spawning system
        RS.ClearRoads();

        // restart the movement in the controller
        controller.game_over = false;

        //Check and reset cop chase
        CancelInvoke("CopChase"); // stops the cops timer
        PUS.CancelCopChase(); // resets cops variables without adjusting player speed

        //open the game over UI
        endgame_ui.SetActive(false);
    }

    public void main_button()
    {
        welcome_ui_static.SetActive(true);
        welcome_ui_dynamic.SetActive(true);
        endgame_ui.SetActive(false);
    }
    public void play_button()
    {
        welcome_ui_static.SetActive(false);
        welcome_ui_dynamic.SetActive(false);
        restart_button();
    }

    public void rank_button()
    {
        //welcome_ui.SetActive(false);
        rank_ui_static.SetActive(true);
        rank_ui_dynamic.SetActive(true);
    }

    public void settings_button()
    {
        settigns_ui_static.SetActive(true);
        settigns_ui_dynamic.SetActive(true);
    }

    public void shop_button()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

}
