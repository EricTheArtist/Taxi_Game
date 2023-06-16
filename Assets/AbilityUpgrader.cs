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

    float MaxAbilityTime = 21;

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
        }
    }


    public void ChangeUIforCar(int carIndex)
    {
        CarINDEX = carIndex;
        CanUpgrade = true;
        


        if (carIndex == 2 || carIndex == 5) //golf or BMW
        {

            //Set title text
            AbilityTitle.SetText("SPEED BOOST");
            //Set Duration text
            SecondsValue.SetText(CurrentAbilityValSpeed.ToString() + "s");
            //Set Bar Scale
            AbilityBar.transform.localScale = new Vector3(CurrentAbilityValSpeed / MaxAbilityTime, 1, 1);

            TimeToUpgrade = CurrentAbilityValSpeed;

            UpgradeButtonText.SetText("UPGRADE");
            CanUpgrade = true;

            SetIcon(2);

        }
        if (carIndex == 4) //police truck
        {

            AbilityTitle.SetText("NO POLICE");
            SecondsValue.SetText("N/A");
            AbilityBar.transform.localScale = new Vector3(MaxAbilityTime / MaxAbilityTime, 1, 1);

            UpgradeButtonText.SetText("PASSIVE");
            CanUpgrade = false;

            TimeToUpgrade = 21;
            SetIcon(4);
        }
        if (carIndex == 3) //landcruiser
        {

            AbilityTitle.SetText("SHIELD");
            SecondsValue.SetText(CurrentAbilityValArmour.ToString() + "s");
            AbilityBar.transform.localScale = new Vector3(CurrentAbilityValArmour / MaxAbilityTime, 1, 1);
            TimeToUpgrade = CurrentAbilityValArmour;

            UpgradeButtonText.SetText("UPGRADE");
            CanUpgrade = true;

            SetIcon(3);

        }
        if (carIndex == 1) //Quantum
        {

            AbilityTitle.SetText("2X COINS");
            SecondsValue.SetText(CurrentAbilityValDouble.ToString() + "s");
            AbilityBar.transform.localScale = new Vector3(CurrentAbilityValDouble / MaxAbilityTime, 1, 1);
            TimeToUpgrade = CurrentAbilityValDouble;

            UpgradeButtonText.SetText("UPGRADE");
            CanUpgrade = true;

            SetIcon(1);
        }
        if (carIndex == 0)
        {
            AbilityTitle.SetText("NO ABILITY");
            SecondsValue.SetText("N/A");
            AbilityBar.transform.localScale = new Vector3(MaxAbilityTime/ MaxAbilityTime, 1, 1);
            TimeToUpgrade = 21;
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
            UpgradeButtonText.SetText("Maxed");
            UpgradeCostText.SetText("-");
            CanUpgrade = false;
        }
    }

    public void ButtonUpgradeAbility()
    {
        if(SUIM.CheckForEnoughMoney(UpgradeCostInt) == true && CanUpgrade == true)
        {
            SUIM.DeductCoins(UpgradeCostInt);
            NewTimeVal = TimeToUpgrade + 3;

            if (CarINDEX == 2 || CarINDEX == 5) //golf or BMW
            {
                PlayerPrefs.SetFloat("Ability_Speed", NewTimeVal);
            }
            if (CarINDEX == 4) //police truck
            {

            }
            if (CarINDEX == 3) //landcruiser
            {
                PlayerPrefs.SetFloat("Ability_Armour", NewTimeVal);
            }
            if (CarINDEX == 1) //Quantum
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
