using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Test_RankUISystem : MonoBehaviour
{
    public TMP_Text MyHighScore;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        int high = PlayerPrefs.GetInt("HighScore");
        MyHighScore.SetText(high.ToString());
    }
    
        
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Button_ColoseRank()
    {
        gameObject.SetActive(false);
    }
}
