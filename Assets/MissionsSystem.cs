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

    int missionSet;

    public TMP_Text[] MissionText;

    enum MissionTypes { Passengers, Coins, Robots, Cops, Distance };

    MissionTypes[] Missions = {MissionTypes.Distance, MissionTypes.Cops, MissionTypes.Passengers};
    


    public bool[] MissionComplete;
    private void Awake()
    {
        if (MSinstance == null)
        {
            MSinstance = this;
            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        missionSet = PlayerPrefs.GetInt("MissionSet");
        SetMissions();
    }

    public void AddPassenger()
    {
        if ((Missions[0] == MissionTypes.Passengers && MissionComplete[1] == false) || (Missions[1] == MissionTypes.Passengers && MissionComplete[1] == false) || (Missions[2] == MissionTypes.Passengers && MissionComplete[2] == false))
        {
            CurrentPassengers++;
        }
        
    }

    public void AddCoins(int coins)
    {
        if ((Missions[0] == MissionTypes.Coins && MissionComplete[1] == false) || (Missions[1] == MissionTypes.Coins && MissionComplete[1] == false) || (Missions[2] == MissionTypes.Coins && MissionComplete[2] == false))
        {
            CurrentCoins += coins;
        }
    }

    public void AddRobotPassed()
    {
        if ((Missions[0] == MissionTypes.Robots && MissionComplete[1] == false) || (Missions[1] == MissionTypes.Robots && MissionComplete[1] == false) || (Missions[2] == MissionTypes.Robots && MissionComplete[2] == false))
        {
            CurrentRobotsDrivenPast++;
        }
    }

    public void AddCopsEscaped()
    {
        if ((Missions[0] == MissionTypes.Cops && MissionComplete[1] == false) || (Missions[1] == MissionTypes.Cops && MissionComplete[1] == false) || (Missions[2] == MissionTypes.Cops && MissionComplete[2] == false))
        {
            CurrentCopsEscaped++;
        }
    }

    public void AddDistanceDriven(int distance)
    {
        if ((Missions[0] == MissionTypes.Distance && MissionComplete[1] == false) || (Missions[1] == MissionTypes.Distance && MissionComplete[1] == false) || (Missions[2] == MissionTypes.Distance && MissionComplete[2] == false))
        {
            CurrentDistanceDriven += distance;
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
    }

    void CheckProgress()
    {
        for (int i = 0; i < Missions.Length; i++)
        {
            if (Missions[i] == MissionTypes.Passengers)
            {
                
                if(CurrentPassengers >= PassengersCollected[missionSet])
                {
                    MissionComplete[i] = true;
                    CurrentPassengers = 0;
                    MissionText[i].SetText("Mission Complete!");
                }
                else
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
                }
                else
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
                }
                else
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
                }
                else
                {
                    MissionText[i].SetText("Cops Escaped: " + CurrentCopsEscaped + "/" + CopsEscaped[missionSet]);
                }
            }
            if (Missions[i] == MissionTypes.Distance)
            {
                if (CurrentDistanceDriven >= DistanceDriven[missionSet])
                {
                    MissionComplete[i] = true;
                    CurrentDistanceDriven = 0;
                    MissionText[i].SetText("Mission Complete!");
                }
                else
                {
                    MissionText[i].SetText("Distance Traveled: " + CurrentDistanceDriven + "/" + DistanceDriven[missionSet]);
                }
            }
        }
            
        

        


    }



}
