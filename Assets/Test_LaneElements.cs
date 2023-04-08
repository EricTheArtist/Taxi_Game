using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_LaneElements : MonoBehaviour
{
    public Vector3[] Lanes;
    public GameObject[] SpawnedObsticles;
    public GameObject spawnedcoins;
    public GameObject spawnedpassengerpickup;
    //public GameObject ObstructionPrefab;
    //public GameObject CoinsRow;
    //public GameObject Passengerpickup;


    void Start()
    {
        
    }

    public void OnTranslatePosition( int Openlane)
    {
        DestroyLast();

        for (int i = 0; i < Lanes.Length; i++) // loops through all the lanes
        {
            if(i != Openlane) // is the lane is not open spawn an obsticle
            {
                //SpawnedObsticles[i] = Instantiate(ObstructionPrefab, Lanes[i], Quaternion.identity);
                //SpawnedObsticles[i].transform.parent = transform;
                SpawnedObsticles[i].transform.localPosition = new Vector3(Lanes[i].x, Lanes[i].y + 0.5f, Random.Range(-0.4f,0.4f));
                SpawnedObsticles[i].SetActive(true);
            }
            else if((i == 0 && i == Openlane)||(i == 4 && i == Openlane)) // is the lane is onpen and on the edge spawn a passender pickup
            {
                //spawnedpassengerpickup = Instantiate(Passengerpickup, Lanes[i], Quaternion.identity);
                //spawnedpassengerpickup.transform.parent = transform;
                spawnedpassengerpickup.transform.localPosition = Lanes[i];
                spawnedpassengerpickup.SetActive(true);
            }
            else // if tthe lase is open spawn coins
            {
                //spawnedcoins = Instantiate(CoinsRow, Lanes[i], Quaternion.identity);
                //spawnedcoins.transform.parent = transform;
                spawnedcoins.transform.localPosition = Lanes[i];
                spawnedcoins.SetActive(true);
                foreach (Transform child in spawnedcoins.transform)
                {
                    child.gameObject.SetActive(true);
                }
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


        
    }
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        
    }
}
