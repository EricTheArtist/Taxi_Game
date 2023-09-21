using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityUpgrader : MonoBehaviour
{
    public TMP_Text AbilityTitle;
    public TMP_Text UpgradeCostText;
    public TMP_Text SecondsValue;
    public TMP_Text UpgradeButtonText;

    public GameObject AbilityBar;

    float MaxAbilityTime = 10;

    float CurrentAbilityValSpeed;
    float CurrentAbilityValArmour;
    float CurrentAbilityValDouble;

    float TimeToUpgrade;
    float NewTimeVal;

    int CarINDEX;

    int UpgradeCostInt;
    float UpgradeCostFloat;

    ShopUIManager SUIM;

    bool CanUpgrade;

    public GameObject[] AbilityIcons;

    
    // Start is called before the first frame update
    void Start()
    {
        SUIM = gameObject.GetComponent<ShopUIManager>();

        RefreshAbilityValues();

        CarINDEX = PlayerPrefs.GetInt("ActiveCar");

        ChangeUIforCar(CarINDEX);

        
    }

    void RefreshAbilityValues()
    {
        //Gets ability status from player prefs
        CurrentAbilityValSpeed = PlayerPrefs.GetFloat("Ability_Speed");
        CurrentAbilityValArmour = PlayerPrefs.GetFloat("Ability_Armour");
        CurrentAbilityValDouble = PlayerPrefs.GetFloat("Ability_Double");

        //for fist time play sets ability to level 1
        if (CurrentAbilityValSpeed == 0)
        {
            CurrentAbilityValSpeed = 3f;
            CurrentAbilityValArmour = 3f;
            CurrentAbilityValDouble = 6f;

            PlayerPrefs.SetFloat("Ability_Speed",CurrentAbilityValSpeed);
            PlayerPrefs.SetFloat("Ability_Armour", CurrentAbilityValArmour);
            PlayerPrefs.SetFloat("Ability_Double", CurrentAbilityValDouble);
        }
    }


    public void ChangeUIforCar(int carIndex)
    {
        CarINDEX = carIndex;
        CanUpgrade = true;
        


        if (carIndex == 2 || carIndex == 5 ||carIndex == 7 || carIndex == 9 || carIndex == 13) //golf or BMW or delorian or golf 7
        {

            //Set title text
            AbilityTitle.SetText("PENINGKATAN KELAJUAN");
            //Set Duration text
            SecondsValue.SetText(CurrentAbilityValSpeed.ToString() + "s");
            //Set Bar Scale
            AbilityBar.transform.localScale = new Vector3(CurrentAbilityValSpeed / MaxAbilityTime, 1, 1);

            TimeToUpgrade = CurrentAbilityValSpeed;

            UpgradeButtonText.SetText("NAIK TARAF");
            CanUpgrade = true;

            SetIcon(2);

        }
        if (carIndex == 4|| carIndex == 11) //police truck
        {

            AbilityTitle.SetText("TIADA POLIS");
            SecondsValue.SetText("N/A");
            AbilityBar.transform.localScale = new Vector3(MaxAbilityTime / MaxAbilityTime, 1, 1);

            UpgradeButtonText.SetText("PASIF");
            CanUpgrade = false;

            TimeToUpgrade = 10;
            SetIcon(4);
        }
        if (carIndex == 3 || carIndex == 6) //landcruiser
        {

            AbilityTitle.SetText("PERISAI");
            SecondsValue.SetText(CurrentAbilityValArmour.ToString() + "s");
            AbilityBar.transform.localScale = new Vector3(CurrentAbilityValArmour / MaxAbilityTime, 1, 1);
            TimeToUpgrade = CurrentAbilityValArmour;

            UpgradeButtonText.SetText("NAIK TARAF");
            CanUpgrade = true;

            SetIcon(3);

        }
        if (carIndex == 1 || carIndex == 8 || carIndex == 10 || carIndex == 12) //Quantum
        {

            AbilityTitle.SetText("2X SYILING");
            SecondsValue.SetText(CurrentAbilityValDouble.ToString() + "s");
            AbilityBar.transform.localScale = new Vector3(CurrentAbilityValDouble / MaxAbilityTime, 1, 1);
            TimeToUpgrade = CurrentAbilityValDouble;

            UpgradeButtonText.SetText("NAIK TARAF");
            CanUpgrade = true;

            SetIcon(1);
        }
        if (carIndex == 0)
        {
            AbilityTitle.SetText("TIADA KEMAMPUAN");
            SecondsValue.SetText("N/A");
            AbilityBar.transform.localScale = new Vector3(MaxAbilityTime/ MaxAbilityTime, 1, 1);
            TimeToUpgrade = 10;
            UpgradeButtonText.SetText("");
            CanUpgrade = false;

            SetIcon(0);
        }

        //set cost for upgrade
        UpgradeCostFloat = 50000 * (TimeToUpgrade / MaxAbilityTime);
        UpgradeCostInt = (int)UpgradeCostFloat;
        UpgradeCostText.SetText(UpgradeCostInt.ToString());

        if (TimeToUpgrade == MaxAbilityTime)
        {
            UpgradeButtonText.SetText("Maks");
            UpgradeCostText.SetText("-");
            CanUpgrade = false;
        }
    }

    public void ButtonUpgradeAbility()
    {
        if(SUIM.CheckForEnoughMoney(UpgradeCostInt) == true && CanUpgrade == true)
        {
            SUIM.DeductCoins(UpgradeCostInt);
            NewTimeVal = TimeToUpgrade + 1;

            if (CarINDEX == 2 || CarINDEX == 5 || CarINDEX == 7 || CarINDEX == 9 || CarINDEX == 13) //golf or BMW or delorian or golf 7
            {
                PlayerPrefs.SetFloat("Ability_Speed", NewTimeVal);
            }
            if (CarINDEX == 4 || CarINDEX == 11) //police truck or police golf
            {

            }
            if (CarINDEX == 3 || CarINDEX == 6) //landcruiser
            {
                PlayerPrefs.SetFloat("Ability_Armour", NewTimeVal);
            }
            if (CarINDEX == 1 || CarINDEX == 8 || CarINDEX == 10 || CarINDEX == 12) //Quantum or hance
            {
                PlayerPrefs.SetFloat("Ability_Double", NewTimeVal);
            }
            if (CarINDEX == 0)
            {

            }

            RefreshAbilityValues();
            ChangeUIforCar(CarINDEX);

        }
    }

    void SetIcon(int ActiveIcon)
    {
        foreach (GameObject Icon in AbilityIcons)
        {
            Icon.SetActive(false);
        }
        AbilityIcons[ActiveIcon].SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


}
