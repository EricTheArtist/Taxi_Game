using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class OpenWorldMissionSystem : MonoBehaviour
{
    public static OpenWorldMissionSystem OWMSinstance;

    public int[] PassengersCollected;
    public int[] CoinsCollected;
    public int[] GreenRobotsDriven;
    public int[] ConesAndLampsHit;
    public int[] PassengersDroppedOff;

    int CurrentPassengersCollected;
    int CurrentCoinsCollected;
    int CurrentGreenRobotsDrivenPast;
    int CurrentConesLampsHit;
    int CurrentPassengersDropped;

    public int missionSet;

    public TMP_Text[] MissionText;

    enum MissionTypes { PassengersCol, Coins, Robots, HitObj, PassengersDrop };

    MissionTypes[] Missions = { MissionTypes.HitObj, MissionTypes.PassengersDrop, MissionTypes.PassengersCol };

    bool AllMissionsComplete = false;

    public bool[] MissionComplete;
    private void Awake()
    {
        if (OWMSinstance == null)
        {
            OWMSinstance = this;
            
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        missionSet = PlayerPrefs.GetInt("OWMissionSet");
        loadSavedMissionProgress();
        SetMissions();
    }

    public void AddPassenger() // set from OpenWorldPassenger
    {
        if ((Missions[0] == MissionTypes.PassengersCol && MissionComplete[0] == false) || (Missions[1] == MissionTypes.PassengersCol && MissionComplete[1] == false) || (Missions[2] == MissionTypes.PassengersCol && MissionComplete[2] == false))
        {
            CurrentPassengersCollected++;
            PlayerPrefs.SetInt("OWMPassengersCol", CurrentPassengersCollected);
        }

    }

    public void AddCoins(int coins) //set from OpenWorldCurrencyManager
    {
        if ((Missions[0] == MissionTypes.Coins && MissionComplete[0] == false) || (Missions[1] == MissionTypes.Coins && MissionComplete[1] == false) || (Missions[2] == MissionTypes.Coins && MissionComplete[2] == false))
        {
            CurrentCoinsCollected += coins;
            PlayerPrefs.SetInt("OWMCoins", CurrentCoinsCollected);
        }
    }

    public void AddRobotPassed() //set from OpenWorldRobot
    {
        if ((Missions[0] == MissionTypes.Robots && MissionComplete[0] == false) || (Missions[1] == MissionTypes.Robots && MissionComplete[1] == false) || (Missions[2] == MissionTypes.Robots && MissionComplete[2] == false))
        {
            CurrentGreenRobotsDrivenPast++;
            PlayerPrefs.SetInt("OWMRobots", CurrentGreenRobotsDrivenPast);
        }
    }

    public void AddObjHit() // set from OpenWorldCrashDetection
    {
        if ((Missions[0] == MissionTypes.HitObj && MissionComplete[0] == false) || (Missions[1] == MissionTypes.HitObj && MissionComplete[1] == false) || (Missions[2] == MissionTypes.HitObj && MissionComplete[2] == false))
        {
            CurrentConesLampsHit++;
            PlayerPrefs.SetInt("OWMHitObj", CurrentConesLampsHit);
        }
    }

    public void AddPassengerDrop() // set from OpenWorldDropOff
    {
        if ((Missions[0] == MissionTypes.PassengersDrop && MissionComplete[0] == false) || (Missions[1] == MissionTypes.PassengersDrop && MissionComplete[1] == false) || (Missions[2] == MissionTypes.PassengersDrop && MissionComplete[2] == false))
        {
            CurrentPassengersDropped++;
            PlayerPrefs.SetInt("OWMPassengersDrop", CurrentPassengersDropped);
        }
    }

    void SetMissions()
    {
        if (missionSet == 0)
        {
            Missions[0] = MissionTypes.PassengersCol;
            Missions[1] = MissionTypes.Coins;
            Missions[2] = MissionTypes.Robots;
        }
        if (missionSet == 1)
        {
            Missions[0] = MissionTypes.PassengersDrop;
            Missions[1] = MissionTypes.HitObj;
            Missions[2] = MissionTypes.Robots;
        }
        if (missionSet == 2)
        {
            Missions[0] = MissionTypes.PassengersCol;
            Missions[1] = MissionTypes.Robots;
            Missions[2] = MissionTypes.HitObj;
        }
        if (missionSet == 3)
        {
            Missions[0] = MissionTypes.PassengersDrop;
            Missions[1] = MissionTypes.Coins;
            Missions[2] = MissionTypes.PassengersCol;
        }
        if (missionSet == 4)
        {
            Missions[0] = MissionTypes.HitObj;
            Missions[1] = MissionTypes.Coins;
            Missions[2] = MissionTypes.Robots;
        }
        if (missionSet == 5)
        {
            Missions[0] = MissionTypes.PassengersCol;
            Missions[1] = MissionTypes.PassengersDrop;
            Missions[2] = MissionTypes.Coins;
        }
        if (missionSet == 6)
        {
            Missions[0] = MissionTypes.Coins;
            Missions[1] = MissionTypes.HitObj;
            Missions[2] = MissionTypes.Robots;
        }
        if (missionSet == 7)
        {
            Missions[0] = MissionTypes.PassengersCol;
            Missions[1] = MissionTypes.Coins;
            Missions[2] = MissionTypes.PassengersDrop;
        }
        if (missionSet == 8)
        {
            Missions[0] = MissionTypes.PassengersDrop;
            Missions[1] = MissionTypes.HitObj;
            Missions[2] = MissionTypes.Robots;
        }
        if (missionSet == 9)
        {
            Missions[0] = MissionTypes.PassengersCol;
            Missions[1] = MissionTypes.HitObj;
            Missions[2] = MissionTypes.Coins;
        }
        if (missionSet == 10)
        {
            MissionText[0].SetText("All Missions Complete!");
            MissionText[1].SetText("");
            MissionText[2].SetText("");
            AllMissionsComplete = true;
        }
        if (AllMissionsComplete == false)
        {
            CheckProgress();
        }

    }

    public void CheckProgress()
    {
        if (AllMissionsComplete == true)
        {
            return;
        }



        for (int i = 0; i < Missions.Length; i++)
        {
            if (Missions[i] == MissionTypes.PassengersCol)
            {

                if (CurrentPassengersCollected >= PassengersCollected[missionSet])
                {
                    MissionComplete[i] = true;
                    CurrentPassengersCollected = 0;
                    MissionText[i].SetText("Mission Complete!");
                    PlayerPrefs.SetInt("OWMPassengersCol", 0);
                    PlayerLevelSystem.PLSinstance.AddXP(100);
                }
                else if (MissionComplete[i] == false)
                {
                    MissionText[i].SetText("Collect Passengers: " + CurrentPassengersCollected + "/" + PassengersCollected[missionSet]);
                }
            }
            if (Missions[i] == MissionTypes.Coins)
            {

                if (CurrentCoinsCollected >= CoinsCollected[missionSet])
                {
                    MissionComplete[i] = true;
                    CurrentCoinsCollected = 0;
                    MissionText[i].SetText("Mission Complete!");
                    PlayerPrefs.SetInt("OWMCoins", 0);
                    PlayerLevelSystem.PLSinstance.AddXP(100);
                }
                else if (MissionComplete[i] == false)
                {
                    MissionText[i].SetText("Collect Coins: " + CurrentCoinsCollected + "/" + CoinsCollected[missionSet]);
                }
            }
            if (Missions[i] == MissionTypes.Robots)
            {
                if (CurrentGreenRobotsDrivenPast >= GreenRobotsDriven[missionSet])
                {
                    MissionComplete[i] = true;
                    CurrentGreenRobotsDrivenPast = 0;
                    MissionText[i].SetText("Mission Complete!");
                    PlayerPrefs.SetInt("OWMRobots", 0);
                    PlayerLevelSystem.PLSinstance.AddXP(100);
                }
                else if (MissionComplete[i] == false)
                {
                    MissionText[i].SetText("Pass Green Robots: " + CurrentGreenRobotsDrivenPast + "/" + GreenRobotsDriven[missionSet]);
                }
            }
            if (Missions[i] == MissionTypes.HitObj)
            {
                if (CurrentConesLampsHit >= ConesAndLampsHit[missionSet])
                {
                    MissionComplete[i] = true;
                    CurrentConesLampsHit = 0;
                    MissionText[i].SetText("Mission Complete!");
                    PlayerPrefs.SetInt("OWMHitObj", 0);
                    PlayerLevelSystem.PLSinstance.AddXP(100);
                }
                else if (MissionComplete[i] == false)
                {
                    MissionText[i].SetText("Hit cones or lamps: " + CurrentConesLampsHit + "/" + ConesAndLampsHit[missionSet]);
                }
            }
            if (Missions[i] == MissionTypes.PassengersDrop)
            {
                if (CurrentPassengersDropped >= PassengersDroppedOff[missionSet])
                {
                    MissionComplete[i] = true;
                    CurrentPassengersDropped = 0;
                    MissionText[i].SetText("Mission Complete!");
                    PlayerPrefs.SetInt("OWMPassengersDrop", 0);
                    PlayerLevelSystem.PLSinstance.AddXP(100);
                }
                else if (MissionComplete[i] == false)
                {
                    MissionText[i].SetText("Drop off passengers: " + CurrentPassengersDropped + "/" + PassengersDroppedOff[missionSet]);
                }
            }
        }

        if (MissionComplete[0] == true)
        {
            PlayerPrefs.SetInt("OWMission1", 1);
        }
        if (MissionComplete[1] == true)
        {
            PlayerPrefs.SetInt("OWMission2", 1);
        }
        if (MissionComplete[2] == true)
        {
            PlayerPrefs.SetInt("OWMission3", 1);
        }

        if (MissionComplete[0] == true && MissionComplete[1] == true && MissionComplete[2] == true)
        {
            missionSet++;
            PlayerPrefs.SetInt("OWMissionSet", missionSet);


            PlayerPrefs.SetInt("OWMission1", 0);
            PlayerPrefs.SetInt("OWMission2", 0);
            PlayerPrefs.SetInt("OWMission3", 0);

            MissionComplete[0] = false;
            MissionComplete[1] = false;
            MissionComplete[2] = false;
            SetMissions();

        }



    }

    void loadSavedMissionProgress()
    {
        if (PlayerPrefs.GetInt("OWMission1") == 1)
        {
            MissionComplete[0] = true;
            MissionText[0].SetText("Mission Complete!");
        }
        if (PlayerPrefs.GetInt("OWMission2") == 1)
        {
            MissionComplete[1] = true;
            MissionText[1].SetText("Mission Complete!");
        }
        if (PlayerPrefs.GetInt("OWMission3") == 1)
        {
            MissionComplete[2] = true;
            MissionText[2].SetText("Mission Complete!");
        }

        CurrentPassengersCollected = PlayerPrefs.GetInt("OWMPassengersCol");
        CurrentCoinsCollected = PlayerPrefs.GetInt("OWMCoins");
        CurrentGreenRobotsDrivenPast = PlayerPrefs.GetInt("OWMRobots");
        CurrentConesLampsHit = PlayerPrefs.GetInt("OWMHitObj");
        CurrentPassengersDropped = PlayerPrefs.GetInt("OWMPassengersDrop");




    }
}
