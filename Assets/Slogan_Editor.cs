using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Slogan_Editor : MonoBehaviour
{

    public TMP_Text SloganOnCar;
    public TMP_InputField SloganInput;
    public GameObject Taxi;
    public string PlayerPrefName;
    // Start is called before the first frame update
    void Start()
    {
        LoadSlogan();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void MoveTaxiIntoView()
    {
        Taxi.transform.localEulerAngles = new Vector3(0,0,0);
    }
    public void UpdateSloganUI()
    {
        SloganOnCar.SetText(SloganInput.text);
    }

    public void SaveSlogan()
    {
        PlayerPrefs.SetString(PlayerPrefName,SloganInput.text);
    }

    public void LoadSlogan()
    {
        if(PlayerPrefs.GetString(PlayerPrefName)!= null)
        {
            SloganOnCar.SetText(PlayerPrefs.GetString(PlayerPrefName));
        }
        
    }
}
