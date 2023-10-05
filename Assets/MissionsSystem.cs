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
        missionSet = PlayerPrefs.GetInt("MLMissionSet");
        loadSavedMissionProgress();
        SetMissions();
    }

    public void AddPassenger() // set from pick up system
    {
        if ((Missions[0] == MissionTypes.Passengers && MissionComplete[0] == false) || (Missions[1] == MissionTypes.Passengers && MissionComplete[1] == false) || (Missions[2] == MissionTypes.Passengers && MissionComplete[2] == false))
        {
            CurrentPassengers++;
            PlayerPrefs.SetInt("MLMissionPassengers", CurrentPassengers);
        }
        
    }

    public void AddCoins(int coins) //set from game end system
    {
        if ((Missions[0] == MissionTypes.Coins && MissionComplete[0] == false) || (Missions[1] == MissionTypes.Coins && MissionComplete[1] == false) || (Missions[2] == MissionTypes.Coins && MissionComplete[2] == false))
        {
            CurrentCoins += coins;
            PlayerPrefs.SetInt("MLMissionCoins", CurrentCoins);
        }
    }

    public void AddRobotPassed() //set from pick up system
    {
        if ((Missions[0] == MissionTypes.Robots && MissionComplete[0] == false) || (Missions[1] == MissionTypes.Robots && MissionComplete[1] == false) || (Missions[2] == MissionTypes.Robots && MissionComplete[2] == false))
        {
            CurrentRobotsDrivenPast++;
            PlayerPrefs.SetInt("MLMissionRobots", CurrentRobotsDrivenPast);
        }
    }

    public void AddCopsEscaped() // set from pick up system
    {
        if ((Missions[0] == MissionTypes.Cops && MissionComplete[0] == false) || (Missions[1] == MissionTypes.Cops && MissionComplete[1] == false) || (Missions[2] == MissionTypes.Cops && MissionComplete[2] == false))
        {
            CurrentCopsEscaped++;
            PlayerPrefs.SetInt("MLMissionCops", CurrentCopsEscaped);
        }
    }

    public void AddDistanceDriven(int distance) // set from game end system
    {
        if ((Missions[0] == MissionTypes.Distance && MissionComplete[0] == false) || (Missions[1] == MissionTypes.Distance && MissionComplete[1] == false) || (Missions[2] == MissionTypes.Distance && MissionComplete[2] == false))
        {
            CurrentDistanceDriven += distance;
            PlayerPrefs.SetInt("MLMissionDistance", CurrentDistanceDriven);
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
            MissionText[0].SetText("Semua Misi Selesai!");
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
                    MissionText[i].SetText("Misi selesai!");
                    PlayerPrefs.SetInt("MLMissionPassengers", 0);
                    PlayerLevelSystem.PLSinstance.AddXP(100);
                }
                else if(MissionComplete[i] == false)
                {
                    MissionText[i].SetText("Kumpul Penumpang: " + CurrentPassengers +"/"+ PassengersCollected[missionSet]);
                }
            }
            if(Missions[i] == MissionTypes.Coins)
            {
                
                if (CurrentCoins >= CoinsCollected[missionSet])
                {
                    MissionComplete[i] = true;
                    CurrentCoins = 0;
                    MissionText[i].SetText("Misi selesai!");
                    PlayerPrefs.SetInt("MLMissionCoins", 0);
                    PlayerLevelSystem.PLSinstance.AddXP(100);
                }
                else if (MissionComplete[i] == false)
                {
                    MissionText[i].SetText("Kumpul Syiling: " + CurrentCoins + "/" + CoinsCollected[missionSet]);
                }
            }
            if (Missions[i] == MissionTypes.Robots)
            {
                if (CurrentRobotsDrivenPast >= RobotsDrivenPast[missionSet])
                {
                    MissionComplete[i] = true;
                    CurrentRobotsDrivenPast = 0;
                    MissionText[i].SetText("Misi selesai!");
                    PlayerPrefs.SetInt("MLMissionRobots", 0);
                    PlayerLevelSystem.PLSinstance.AddXP(100);
                }
                else if (MissionComplete[i] == false)
                {
                    MissionText[i].SetText("Lulus lampu isyarat: " + CurrentRobotsDrivenPast + "/" + RobotsDrivenPast[missionSet]);
                }
            }
            if (Missions[i] == MissionTypes.Cops)
            {
                if (CurrentCopsEscaped >= CopsEscaped[missionSet])
                {
                    MissionComplete[i] = true;
                    CurrentCopsEscaped = 0;
                    MissionText[i].SetText("Misi selesai!");
                    PlayerPrefs.SetInt("MLMissionCops", 0);
                    PlayerLevelSystem.PLSinstance.AddXP(100);
                }
                else if (MissionComplete[i] == false)
                {
                    MissionText[i].SetText("Polis melarikan diri: " + CurrentCopsEscaped + "/" + CopsEscaped[missionSet]);
                }
            }
            if (Missions[i] == MissionTypes.Distance)
            {
                if (CurrentDistanceDriven >= DistanceDriven[missionSet])
                {
                    MissionComplete[i] = true;
                    CurrentDistanceDriven = 0;
                    MissionText[i].SetText("Misi selesai!");
                    PlayerPrefs.SetInt("MLMissionDistance", 0);
                    PlayerLevelSystem.PLSinstance.AddXP(100);
                }
                else if (MissionComplete[i] == false)
                {
                    MissionText[i].SetText("Jarak Perjalanan: " + CurrentDistanceDriven + "/" + DistanceDriven[missionSet]);
                }
            }
        }

        if (MissionComplete[0] == true)
        {
            PlayerPrefs.SetInt("MLMission1", 1);
        }
        if (MissionComplete[1] == true)
        {
            PlayerPrefs.SetInt("MLMission2", 1);
        }
        if (MissionComplete[2] == true)
        {
            PlayerPrefs.SetInt("MLMission3", 1);
        }

        if (MissionComplete[0] == true && MissionComplete[1] == true && MissionComplete[2] == true)
        {
            missionSet++;
            PlayerPrefs.SetInt("MLMissionSet", missionSet);
            

            PlayerPrefs.SetInt("MLMission1", 0);
            PlayerPrefs.SetInt("MLMission2", 0);
            PlayerPrefs.SetInt("MLMission3", 0);

            MissionComplete[0] = false;
            MissionComplete[1] = false;
            MissionComplete[2] = false;
            SetMissions();

        }
        


    }

    void loadSavedMissionProgress()
    {
        if (PlayerPrefs.GetInt("MLMission1") == 1)
        {
            MissionComplete[0] = true;
            MissionText[0].SetText("Misi selesai!");
        }
        if (PlayerPrefs.GetInt("MLMission2") == 1)
        {
            MissionComplete[1] = true;
            MissionText[1].SetText("Misi selesai!");
        }
        if (PlayerPrefs.GetInt("MLMission3") == 1)
        {
            MissionComplete[2] = true;
            MissionText[2].SetText("Misi selesai!");
        }

        CurrentPassengers = PlayerPrefs.GetInt("MLMissionPassengers");
        CurrentCoins = PlayerPrefs.GetInt("MLMissionCoins");
        CurrentRobotsDrivenPast = PlayerPrefs.GetInt("MLMissionRobots");
        CurrentCopsEscaped = PlayerPrefs.GetInt("MLMissionCops");
        CurrentDistanceDriven = PlayerPrefs.GetInt("MLMissionDistance");
        



    }


}
