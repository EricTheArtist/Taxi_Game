using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class LevelUIManager : MonoBehaviour
{

    public GameObject LevelUIDisplay;

    public TMP_Text IconLevel;
    public TMP_Text DisplayLevel;
    public TMP_Text XPProgression;
    public GameObject ProgressBar;
    // Start is called before the first frame update


    private void OnEnable()
    {
        IconLevel.SetText(PlayerPrefs.GetInt("MLPlayerLevel").ToString());
    }
    // Update is called once per frame
    void Update()
    {
        
    }

    public void toggleLevelUIDisplay()
    {
        if (LevelUIDisplay.activeInHierarchy)
        {
            LevelUIDisplay.SetActive(false);
        }
        else
        {
            MissionsSystem.MSinstance.CheckProgress();
            IconLevel.SetText(PlayerPrefs.GetInt("MLPlayerLevel").ToString());
            LevelUIDisplay.SetActive(true);
            DisplayLevel.SetText(PlayerLevelSystem.PLSinstance.PlayerLevel.ToString());
            XPProgression.SetText(PlayerLevelSystem.PLSinstance.PlayerCurrentXP.ToString() + "/" + PlayerLevelSystem.PLSinstance.XPtoNextLevel.ToString());

            if (PlayerLevelSystem.PLSinstance.XPtoNextLevel != 0)
            {
                float barvalue = 1 - ((float)PlayerLevelSystem.PLSinstance.PlayerCurrentXP / (float)PlayerLevelSystem.PLSinstance.XPtoNextLevel);
                ProgressBar.transform.localScale = new Vector3(barvalue, 1, 1);
            }



        }
    }
}
