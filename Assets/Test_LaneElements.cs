using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_LaneElements : MonoBehaviour
{
    public Vector3[] Lanes;
    public GameObject[] SpawnedObsticles;
    public GameObject spawnedcoins;
    public GameObject spawnedpassengerpickup;
    public GameObject abilityObject;

    float ChanceToSawn;
    private SimpleInputNamespace.TestCharacterController controller;

    public GameObject RobotComponent;
    public bool RobotEnabled;
    public bool AbilityEnabled;
    public bool LuncboxEnabled;
    bool CanCollectLunchbox;
    float LunchboxSpawnChance = 0.8f;
    float RobotSpawnChance = 0.8f;
    float AbilitySpawnChance = 0.8f;
    AbilitySystem AS;
    BasicDailyReward BDR;
    Test_HighScore THS;

    public GameObject AbilityObjectSpeedBoost;
    public GameObject AbilityObjectShield;
    public GameObject AbilityObject2X;
    public GameObject AbilityObjectCooldrink;
    public GameObject ObjectCoolerbox;

    public GameObject SceneryParentObject;
    public GameObject[] SceneryPrefabs;
    int ScoreThreshold = 100;
    int SceneryPrefabIndex = 0;


    void Start()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        controller = Player.GetComponent<SimpleInputNamespace.TestCharacterController>();
        AS = Player.GetComponent<AbilitySystem>();
        THS = Player.GetComponent<Test_HighScore>();
        BDR = GameObject.FindGameObjectWithTag("RewardCheck").GetComponent<BasicDailyReward>();
        CanCollectLunchbox = BDR.CanClaimReward();
        RobotComponent.SetActive(false);

        if (AbilityEnabled == false)
        {
            Destroy(abilityObject);
        }
        if (RobotEnabled == false)
        {
            Destroy(RobotComponent);
        }
        if (LuncboxEnabled == false)
        {
            Destroy(ObjectCoolerbox);
            CanCollectLunchbox = false;
        }
        if(AbilityEnabled == true)
        {
            int Carindex = PlayerPrefs.GetInt("ActiveCar");

            if (Carindex == 2 || Carindex == 5) //golf or BMW
            {
                AbilityObjectSpeedBoost.SetActive(true);
            }
            if (Carindex == 4) //police truck
            {
                AbilityObjectCooldrink.SetActive(true);
            }
            if (Carindex == 3) //landcruiser
            {
                AbilityObjectShield.SetActive(true);
            }
            if (Carindex == 1) //Quantum
            {
                AbilityObject2X.SetActive(true);
            }
            if (Carindex == 0)
            {

            }
        }

    }

    public void OnTranslatePosition( int Openlane)
    {
        DestroyLast();

        if(LuncboxEnabled == true)
        {
            CanCollectLunchbox = BDR.CanClaimReward(); //checks to see if lunchbox has been collected yet
        }
        

        for (int i = 0; i < Lanes.Length; i++) // loops through all the lanes
        {
            if(i != Openlane) // is the lane is not open set an obsticle to that lane
            {
                SpawnedObsticles[i].transform.localPosition = new Vector3(Lanes[i].x, Lanes[i].y + 0.5f, Random.Range(-4f,7f)); //gives the obsticel a random position on the lane

                
                if(controller.isBraking == true) //The chance of an obsticle to spawn is based on the percent ratio of the current speed to max speed, meaning that at max speed 4 lanes will always have obsticles
                {
                    ChanceToSawn = 1 - (controller._movementSpeed / controller.maxMovementSpeed);
                }
                if(controller.isBraking == false)
                {
                    ChanceToSawn = 1 - (controller.movementSpeed / controller.maxMovementSpeed);
                }
                //Debug.Log("Obsticle spawn probability: " + ChanceToSawn);
                if (Random.value > ChanceToSawn) 
                {
                    SpawnedObsticles[i].SetActive(true);
                } 
            }
            else if((i == 0 && i == Openlane)||(i == 4 && i == Openlane)) // is the lane is onpen and on the edge place a passender pickup
            {

                spawnedpassengerpickup.transform.localPosition = Lanes[i];
                spawnedpassengerpickup.SetActive(true);
                spawnedcoins.SetActive(false);
                foreach (Transform child in spawnedpassengerpickup.transform)
                {
                    child.gameObject.SetActive(true);
                }
            }
            else // if tthe lase is open spawn coins or ability pickup
            {
                if(Random.value > AbilitySpawnChance && AS.CanSpawnPickup == true && AbilityEnabled == true)
                {
                    abilityObject.transform.localPosition = Lanes[i];
                    abilityObject.SetActive(true);
                }
                else if(Random.value > LunchboxSpawnChance && CanCollectLunchbox == true)
                {
                    ObjectCoolerbox.transform.localPosition = Lanes[i];
                    ObjectCoolerbox.SetActive(true);
                }
                else
                {
                    spawnedcoins.transform.localPosition = Lanes[i];
                    spawnedcoins.SetActive(true);
                    foreach (Transform child in spawnedcoins.transform)
                    {
                        child.gameObject.SetActive(true);
                    }
                }

            }
            
        }

        if(RobotEnabled== true)
        {
            
            if(Random.value > RobotSpawnChance && spawnedpassengerpickup.activeInHierarchy == false)
            {
                RobotComponent.SetActive(true);
            }
        }

        if (THS.scoreInt > ScoreThreshold && SceneryPrefabs.Length>0)
        {
            int NumOfPrefabs = SceneryPrefabs.Length;

            foreach (Transform child in SceneryParentObject.transform)
            {
                Destroy(child.gameObject);
            }

            Instantiate(SceneryPrefabs[SceneryPrefabIndex], new Vector3(0, 0, 0), Quaternion.identity, SceneryParentObject.transform);
            

            if (SceneryPrefabIndex < NumOfPrefabs)
            {
                SceneryPrefabIndex++;
            }
            else
            {
                SceneryPrefabIndex = 0;
            }
            
            ScoreThreshold += 100;
        }
        

        

    }

    public void DestroyLast() // deactivatets all pickup and obsticle objects
    {
        if (SpawnedObsticles != null)
        {
            foreach (GameObject selectedObject in SpawnedObsticles)
            {
                selectedObject.SetActive(false);
            }
        }
        /*
        if(spawnedcoins!= null)
        {
            foreach (Transform child in spawnedcoins.transform)
            {
                child.gameObject.SetActive(false);
            }


            spawnedcoins.SetActive(false);
        }
        */
        if(spawnedpassengerpickup != null)
        {
            spawnedpassengerpickup.SetActive(false);
        }

        if (AbilityEnabled == true)
        {
            abilityObject.SetActive(false);
        }
        if (RobotEnabled == true)
        {
            RobotComponent.SetActive(false);
        }
        if (LuncboxEnabled == true)
        {
            ObjectCoolerbox.SetActive(false);
        }



        

        

    }

}
