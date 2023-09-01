using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class MissionsSystem : MonoBehaviour
{
    public static MissionsSystem MSinstance;

    public int[] PassengersCollected;
    public int[] CoinsCollected;
    public int[] RobotsDrivenPast;
    public int[] CopsEscaped;
    public int[] DistanceDriven;

    int CurrentPassengers;
    int CurrentCoins;
    int CurrentRobotsDrivenPast;
    int CurrentCopsEscaped;
    int CurrentDistanceDriven;

    public int missionSet;

    public TMP_Text[] MissionText;

    enum MissionTypes { Passengers, Coins, Robots, Cops, Distance };

    MissionTypes[] Missions = {MissionTypes.Distance, MissionTypes.Cops, MissionTypes.Passengers};

    bool AllMissionsComplete = false;

    public bool[] MissionComplete;
    private void Awake()
    {
        if (MSinstance == null)
        {
            MSinstance = this;
            //DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        missionSet = PlayerPrefs.GetInt("MissionSet");
        loadSavedMissionProgress();
        SetMissions();
    }

    public void AddPassenger() // set from pick up system
    {
        if ((Missions[0] == MissionTypes.Passengers && MissionComplete[0] == false) || (Missions[1] == MissionTypes.Passengers && MissionComplete[1] == false) || (Missions[2] == MissionTypes.Passengers && MissionComplete[2] == false))
        {
            CurrentPassengers++;
            PlayerPrefs.SetInt("MissionPassengers", CurrentPassengers);
        }
        
    }

    public void AddCoins(int coins) //set from game end system
    {
        if ((Missions[0] == MissionTypes.Coins && MissionComplete[0] == false) || (Missions[1] == MissionTypes.Coins && MissionComplete[1] == false) || (Missions[2] == MissionTypes.Coins && MissionComplete[2] == false))
        {
            CurrentCoins += coins;
            PlayerPrefs.SetInt("MissionCoins", CurrentCoins);
        }
    }

    public void AddRobotPassed() //set from pick up system
    {
        if ((Missions[0] == MissionTypes.Robots && MissionComplete[0] == false) || (Missions[1] == MissionTypes.Robots && MissionComplete[1] == false) || (Missions[2] == MissionTypes.Robots && MissionComplete[2] == false))
        {
            CurrentRobotsDrivenPast++;
            PlayerPrefs.SetInt("MissionRobots", CurrentRobotsDrivenPast);
        }
    }

    public void AddCopsEscaped() // set from pick up system
    {
        if ((Missions[0] == MissionTypes.Cops && MissionComplete[0] == false) || (Missions[1] == MissionTypes.Cops && MissionComplete[1] == false) || (Missions[2] == MissionTypes.Cops && MissionComplete[2] == false))
        {
            CurrentCopsEscaped++;
            PlayerPrefs.SetInt("MissionCops", CurrentCopsEscaped);
        }
    }

    public void AddDistanceDriven(int distance) // set from game end system
    {
        if ((Missions[0] == MissionTypes.Distance && MissionComplete[0] == false) || (Missions[1] == MissionTypes.Distance && MissionComplete[1] == false) || (Missions[2] == MissionTypes.Distance && MissionComplete[2] == false))
        {
            CurrentDistanceDriven += distance;
            PlayerPrefs.SetInt("MissionDistance", CurrentDistanceDriven);
        }
    }

    void SetMissions()
    {
        if (missionSet == 0)
        {
            Missions[0] = MissionTypes.Passengers;
            Missions[1] = MissionTypes.Coins;
            Missions[2] = MissionTypes.Robots;
        }
        if(missionSet == 1)
        {
            Missions[0] = MissionTypes.Distance;
            Missions[1] = MissionTypes.Cops;
            Missions[2] = MissionTypes.Robots;
        }
        if (missionSet == 2)
        {
            Missions[0] = MissionTypes.Passengers;
            Missions[1] = MissionTypes.Robots;
            Missions[2] = MissionTypes.Cops;
        }
        if (missionSet == 3)
        {
            Missions[0] = MissionTypes.Distance;
            Missions[1] = MissionTypes.Coins;
            Missions[2] = MissionTypes.Passengers;
        }
        if (missionSet == 4)
        {
            Missions[0] = MissionTypes.Cops;
            Missions[1] = MissionTypes.Coins;
            Missions[2] = MissionTypes.Robots;
        }
        if (missionSet == 5)
        {
            Missions[0] = MissionTypes.Passengers;
            Missions[1] = MissionTypes.Distance;
            Missions[2] = MissionTypes.Coins;
        }
        if (missionSet == 6)
        {
            Missions[0] = MissionTypes.Coins;
            Missions[1] = MissionTypes.Cops;
            Missions[2] = MissionTypes.Robots;
        }
        if (missionSet == 7)
        {
            Missions[0] = MissionTypes.Passengers;
            Missions[1] = MissionTypes.Coins;
            Missions[2] = MissionTypes.Distance;
        }
        if (missionSet == 8)
        {
            Missions[0] = MissionTypes.Distance;
            Missions[1] = MissionTypes.Cops;
            Missions[2] = MissionTypes.Robots;
        }
        if (missionSet == 9)
        {
            Missions[0] = MissionTypes.Passengers;
            Missions[1] = MissionTypes.Cops;
            Missions[2] = MissionTypes.Coins;
        }
        if (missionSet == 10)
        {
            MissionText[0].SetText("All Missions Complete!");
            MissionText[1].SetText("");
            MissionText[2].SetText("");
            AllMissionsComplete = true;
        }
        if(AllMissionsComplete == false)
        {
            CheckProgress();
        }

    }

    public void CheckProgress()
    {
        if(AllMissionsComplete == true)
        {
            return;
        }



        for (int i = 0; i < Missions.Length; i++)
        {
            if (Missions[i] == MissionTypes.Passengers)
            {
                
                if(CurrentPassengers >= PassengersCollected[missionSet])
                {
                    MissionComplete[i] = true;
                    CurrentPassengers = 0;
                    MissionText[i].SetText("Mission Complete!");
                    PlayerPrefs.SetInt("MissionPassengers", 0);
                    PlayerLevelSystem.PLSinstance.AddXP(100);
                }
                else if(MissionComplete[i] == false)
                {
                    MissionText[i].SetText("Collect Passengers: " + CurrentPassengers +"/"+ PassengersCollected[missionSet]);
                }
            }
            if(Missions[i] == MissionTypes.Coins)
            {
                
                if (CurrentCoins >= CoinsCollected[missionSet])
                {
                    MissionComplete[i] = true;
                    CurrentCoins = 0;
                    MissionText[i].SetText("Mission Complete!");
                    PlayerPrefs.SetInt("MissionCoins", 0);
                    PlayerLevelSystem.PLSinstance.AddXP(100);
                }
                else if (MissionComplete[i] == false)
                {
                    MissionText[i].SetText("Collect Coins: " + CurrentCoins + "/" + CoinsCollected[missionSet]);
                }
            }
            if (Missions[i] == MissionTypes.Robots)
            {
                if (CurrentRobotsDrivenPast >= RobotsDrivenPast[missionSet])
                {
                    MissionComplete[i] = true;
                    CurrentRobotsDrivenPast = 0;
                    MissionText[i].SetText("Mission Complete!");
                    PlayerPrefs.SetInt("MissionRobots", 0);
                    PlayerLevelSystem.PLSinstance.AddXP(100);
                }
                else if (MissionComplete[i] == false)
                {
                    MissionText[i].SetText("Pass Robots: " + CurrentRobotsDrivenPast + "/" + RobotsDrivenPast[missionSet]);
                }
            }
            if (Missions[i] == MissionTypes.Cops)
            {
                if (CurrentCopsEscaped >= CopsEscaped[missionSet])
                {
                    MissionComplete[i] = true;
                    CurrentCopsEscaped = 0;
                    MissionText[i].SetText("Mission Complete!");
                    PlayerPrefs.SetInt("MissionCops", 0);
                    PlayerLevelSystem.PLSinstance.AddXP(100);
                }
                else if (MissionComplete[i] == false)
                {
                    MissionText[i].SetText("Escape Cops: " + CurrentCopsEscaped + "/" + CopsEscaped[missionSet]);
                }
            }
            if (Missions[i] == MissionTypes.Distance)
            {
                if (CurrentDistanceDriven >= DistanceDriven[missionSet])
                {
                    MissionComplete[i] = true;
                    CurrentDistanceDriven = 0;
                    MissionText[i].SetText("Mission Complete!");
                    PlayerPrefs.SetInt("MissionDistance", 0);
                    PlayerLevelSystem.PLSinstance.AddXP(100);
                }
                else if (MissionComplete[i] == false)
                {
                    MissionText[i].SetText("Travel Distance: " + CurrentDistanceDriven + "/" + DistanceDriven[missionSet]);
                }
            }
        }

        if (MissionComplete[0] == true)
        {
            PlayerPrefs.SetInt("Mission1", 1);
        }
        if (MissionComplete[1] == true)
        {
            PlayerPrefs.SetInt("Mission2", 1);
        }
        if (MissionComplete[2] == true)
        {
            PlayerPrefs.SetInt("Mission3", 1);
        }

        if (MissionComplete[0] == true && MissionComplete[1] == true && MissionComplete[2] == true)
        {
            missionSet++;
            PlayerPrefs.SetInt("MissionSet", missionSet);
            

            PlayerPrefs.SetInt("Mission1", 0);
            PlayerPrefs.SetInt("Mission2", 0);
            PlayerPrefs.SetInt("Mission3", 0);

            MissionComplete[0] = false;
            MissionComplete[1] = false;
            MissionComplete[2] = false;
            SetMissions();

        }
        


    }

    void loadSavedMissionProgress()
    {
        if (PlayerPrefs.GetInt("Mission1") == 1)
        {
            MissionComplete[0] = true;
            MissionText[0].SetText("Mission Complete!");
        }
        if (PlayerPrefs.GetInt("Mission2") == 1)
        {
            MissionComplete[1] = true;
            MissionText[1].SetText("Mission Complete!");
        }
        if (PlayerPrefs.GetInt("Mission3") == 1)
        {
            MissionComplete[2] = true;
            MissionText[2].SetText("Mission Complete!");
        }

        CurrentPassengers = PlayerPrefs.GetInt("MissionPassengers");
        CurrentCoins = PlayerPrefs.GetInt("MissionCoins");
        CurrentRobotsDrivenPast = PlayerPrefs.GetInt("MissionRobots");
        CurrentCopsEscaped = PlayerPrefs.GetInt("MissionCops");
        CurrentDistanceDriven = PlayerPrefs.GetInt("MissionDistance");
        



    }


}
