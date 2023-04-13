using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Test_HighScore : MonoBehaviour
{

    float score;
    int highScore;
    public int scoreInt;

    public TMP_Text CurrentScore;

    private SimpleInputNamespace.TestCharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        controller = Player.GetComponent<SimpleInputNamespace.TestCharacterController>();

        highScore = PlayerPrefs.GetInt("HighScore");
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

            if(scoreInt >= highScore)
            {
                highScore = scoreInt;
                PlayerPrefs.SetInt("HighScore", highScore);
                CurrentScore.color = new Color32(255,203,0,255);
            }
            

        }
    }

    public void ResetScore()
    {
        highScore = PlayerPrefs.GetInt("HighScore");
        CurrentScore.color = new Color32(255, 255, 255, 255);
        score = 0;
        scoreInt = 0;
    }
}
