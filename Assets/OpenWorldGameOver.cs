using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenWorldGameOver : MonoBehaviour
{
    public GameObject GreyscaleEffect;
    public GameObject GameOverUI;
    public static OpenWorldGameOver OWGOInstance;
    public GameObject CarAudio;
    [SerializeField] private AudioClip _crashSoundClip;
    // Start is called before the first frame update
    void Start()
    {
        if (OWGOInstance == null)
        {
            OWGOInstance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public void GameOver()
    {
        if(SoundManager.Instance!= null)
        {
            SoundManager.Instance.PlaySound(_crashSoundClip);
        }
        Invoke("End", 0.5f);


    }

    void End()
    {
        Time.timeScale = 0;
        GreyscaleEffect.SetActive(true);
        GameOverUI.SetActive(true);
        CarAudio.SetActive(false);
    }

    public void RestartScene()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(2);
    }

}
