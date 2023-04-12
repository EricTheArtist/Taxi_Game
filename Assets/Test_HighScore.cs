using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_HighScore : MonoBehaviour
{

    float score;
    float highScore;

    private SimpleInputNamespace.TestCharacterController controller;
    // Start is called before the first frame update
    void Start()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        controller = Player.GetComponent<SimpleInputNamespace.TestCharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        HighScore();
    }

    void HighScore()
    {
        if (controller.isBraking == false)
        {
            score += Time.deltaTime * controller.movementSpeed;
        }
    }

    public void ResetScore()
    {
        score = 0;
    }
}
