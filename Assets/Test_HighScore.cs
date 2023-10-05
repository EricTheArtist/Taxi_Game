using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Test_HighScore : MonoBehaviour
{

    float score;
    int highScoreSwiping;
    //int highScoreSteering;
    public int scoreInt;

    public TMP_Text CurrentScore;
    public ParticleSystem HighRunEffect;
    public HorizontalModeManager HMM;

    private SimpleInputNamespace.TestCharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        controller = Player.GetComponent<SimpleInputNamespace.TestCharacterController>();

        highScoreSwiping = PlayerPrefs.GetInt("MLHighScore");
        //highScoreSteering = PlayerPrefs.GetInt("HighScoreSteering");
        
        //HighRunEffect.
    }

    // Update is called once per frame
    void Update()
    {
        HighScore();
        CurrentScore.SetText(scoreInt.ToString());
    }

    void HighScore()
    {
        if (controller.game_over == false)
        {
            score += (Time.deltaTime * controller.movementSpeed) /10; //calculates score based in how long the player has been alve for and what spped they atre going at
            scoreInt = (int)score;

            if(scoreInt >= highScoreSwiping)
            {

                if(HighRunEffect.isPlaying== false)
                {
                    HighRunEffect.Play();
                    PlayerLevelSystem.PLSinstance.AddXP(50);
                }

                highScoreSwiping = scoreInt;
                PlayerPrefs.SetInt("MLHighScore", highScoreSwiping);
                CurrentScore.color = new Color32(255,203,0,255);
            }
            /*
            else if ((scoreInt >= highScoreSteering) && HMM.SteeringWheelActive == true)
            {
                if (HighRunEffect.isPlaying == false)
                {
                    HighRunEffect.Play();
                    PlayerLevelSystem.PLSinstance.AddXP(50);
                }

                highScoreSteering = scoreInt;
                PlayerPrefs.SetInt("HighScoreSteering", highScoreSteering);
                CurrentScore.color = new Color32(255, 203, 0, 255);


            }
            */

        }
    }

    public void ResetScore()
    {
        //highScoreSteering = PlayerPrefs.GetInt("HighScoreSteering");
        highScoreSwiping = PlayerPrefs.GetInt("MLHighScore");
        CurrentScore.color = new Color32(255, 255, 255, 255);
        HighRunEffect.Stop();
        score = 0;
        scoreInt = 0;
    }
}
