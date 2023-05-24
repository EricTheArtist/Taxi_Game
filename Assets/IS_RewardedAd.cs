using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IS_RewardedAd : MonoBehaviour
{

    public GameObject WatchADPrompt;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OpenWatchADPrompt()
    {
        WatchADPrompt.SetActive(true);
    }

    public void CloseWatchADPrompt()
    {
        WatchADPrompt.SetActive(false);
    }
}
