using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    public bool Paused;
    public GameObject WelcomeMenu;
    public GameObject PauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        Paused = false;
    }

    // Update is called once per frame
    public void TogglePause()
    {
        if(Time.timeScale == 1f)
        {
            PauseGame();
            PauseMenu.SetActive(true);
        }
        else
        {
            UnpauseGame();
            PauseMenu.SetActive(false);
        }
    }

    public void PauseGame()
    {
        Paused = true;
        Time.timeScale = 0f;
    }

    public void UnpauseGame()
    {
        Paused = false;
        Time.timeScale = 1f;
    }
}
