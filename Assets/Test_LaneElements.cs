using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_LaneElements : MonoBehaviour
{
    public Vector3[] Lanes;
    public GameObject[] SpawnedObsticles;
    public GameObject spawnedcoins;
    public GameObject spawnedpassengerpickup;

    float ChanceToSawn;
    private SimpleInputNamespace.TestCharacterController controller;

    public GameObject RobotComponent;
    public bool RobotEnabled;
    float RobotSpawnChance = 0.8f;


    void Start()
    {
        GameObject Player = GameObject.FindGameObjectWithTag("Player");
        controller = Player.GetComponent<SimpleInputNamespace.TestCharacterController>();
        RobotComponent.SetActive(false);
    }

    public void OnTranslatePosition( int Openlane)
    {
        DestroyLast();

        for (int i = 0; i < Lanes.Length; i++) // loops through all the lanes
        {
            if(i != Openlane) // is the lane is not open set an obsticle to that lane
            {
                SpawnedObsticles[i].transform.localPosition = new Vector3(Lanes[i].x, Lanes[i].y + 0.5f, Random.Range(-0.4f,0.4f));

                
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
                foreach (Transform child in spawnedpassengerpickup.transform)
                {
                    child.gameObject.SetActive(true);
                }
            }
            else // if tthe lase is open spawn coins
            {

                spawnedcoins.transform.localPosition = Lanes[i];
                spawnedcoins.SetActive(true);
                foreach (Transform child in spawnedcoins.transform)
                {
                    child.gameObject.SetActive(true);
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
        if(spawnedcoins!= null)
        {
            foreach (Transform child in spawnedcoins.transform)
            {
                child.gameObject.SetActive(false);
            }


            spawnedcoins.SetActive(false);
        }
        if(spawnedpassengerpickup != null)
        {
            spawnedpassengerpickup.SetActive(false);
        }

        RobotComponent.SetActive(false);

    }

}
