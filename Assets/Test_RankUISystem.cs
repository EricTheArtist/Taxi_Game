using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Test_RankUISystem : MonoBehaviour
{
    public TMP_Text MyHighScore;
    public GameObject Rank_ui_Static;
    public GameObject Rank_ui_Dynamic;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    private void OnEnable()
    {
        if(MyHighScore!= null)
        {
            int high = PlayerPrefs.GetInt("MLHighScore");
            MyHighScore.SetText(high.ToString());
        }

    }
    
        
    
    // Update is called once per frame
    void Update()
    {
        
    }

    public void Button_ColoseRank()
    {
        Rank_ui_Dynamic.SetActive(false);
        Rank_ui_Static.SetActive(false);
    }

}
