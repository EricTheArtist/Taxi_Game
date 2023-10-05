using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerLevelSystem : MonoBehaviour
{
    public static PlayerLevelSystem PLSinstance;

    public int PlayerLevel;
    public int PlayerCurrentXP;
    public int XPtoNextLevel;

    public GameObject UIXPNotification;
    
    private void Awake()
    {
        if (PLSinstance == null)
        {
            PLSinstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {


        PlayerCurrentXP = PlayerPrefs.GetInt("MLCurrentXP");
        PlayerLevel = PlayerPrefs.GetInt("MLPlayerLevel");

        

        if (PlayerPrefs.HasKey("MLXPtoNextLevel"))
        {
            XPtoNextLevel = PlayerPrefs.GetInt("MLXPtoNextLevel");
        }
        else
        {
            XPtoNextLevel = 20;
            PlayerPrefs.SetInt("MLXPtoNextLevel", XPtoNextLevel);
            
        }
    }



    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            AddXP(10);
        }
        if (Input.GetKeyDown(KeyCode.P))
        {
            PlayerPrefs.SetInt("MLXPtoNextLevel", 0);
            PlayerPrefs.SetInt("MLCurrentXP", 0);
            PlayerPrefs.SetInt("MLPlayerLevel", 0);
        }
    }
    
    public void AddXP(int XP)
    {


        GameObject xpnotice = Instantiate(UIXPNotification, new Vector3(0, 0, 0), Quaternion.identity) as GameObject; 
       
        XPNotice notice = xpnotice.GetComponent<XPNotice>();
        notice.SetXPValue(XP);

        PlayerCurrentXP += XP;
        PlayerPrefs.SetInt("MLCurrentXP", PlayerCurrentXP);

        if(PlayerCurrentXP >= XPtoNextLevel)
        {
            setNewLevel(PlayerLevel + 1);
        }
    }

    void setNewLevel(int newLevel)
    {
        PlayerLevel = newLevel;
        PlayerPrefs.SetInt("MLPlayerLevel", PlayerLevel);

        PlayerCurrentXP = PlayerCurrentXP - XPtoNextLevel;
        PlayerPrefs.SetInt("MLCurrentXP", PlayerCurrentXP);

        XPtoNextLevel = (int)(50f * (Mathf.Pow(PlayerLevel + 1, 2) - (5 * (PlayerLevel + 1)) + 8));
        PlayerPrefs.SetInt("MLXPtoNextLevel", XPtoNextLevel);

    }


}
