using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseSystem : MonoBehaviour
{
    public bool Paused;
    public GameObject PauseMenu;
    // Start is called before the first frame update
    void Start()
    {
        Paused = false;
    }

    private void Update()
    {
        // Make sure user is on Android platform
        if (Application.platform == RuntimePlatform.Android)
        {

            // Check if Back was pressed this frame
            if (Input.GetKeyDown(KeyCode.Escape))
            {

                // Quit the application
                Application.Quit();
            }
        }
    }
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
