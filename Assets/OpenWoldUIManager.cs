using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenWoldUIManager : MonoBehaviour
{
    public GameObject OWsettigns_ui_static;
    public GameObject OWsettigns_ui_dynamic;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void settings_button()
    {
        OWsettigns_ui_static.SetActive(true);
        OWsettigns_ui_dynamic.SetActive(true);
    }

    public void Close_Settings()
    {
        OWsettigns_ui_static.SetActive(false);
        OWsettigns_ui_dynamic.SetActive(false);
    }


    public void shop_button()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(1, LoadSceneMode.Single);
    }

    public void InfinteRunner_button()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }
}
