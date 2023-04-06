using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_LaneElements : MonoBehaviour
{
    public Vector3[] Lanes;
    GameObject[] SpawnedObsticles;
    GameObject spawnedcoins;
    GameObject spawnedpassengerpickup;
    public GameObject ObstructionPrefab;
    public GameObject CoinsRow;
    public GameObject Passengerpickup;

    public void OnTranslatePosition( int Openlane)
    {
        DestroyLast();

        for (int i = 0; i < Lanes.Length; i++) // loops through all the lanes
        {
            if(i != Openlane) // is the lane is not open spawn an obsticle
            {
                SpawnedObsticles[i] = Instantiate(ObstructionPrefab, Lanes[i], Quaternion.identity);
                SpawnedObsticles[i].transform.parent = transform;
                SpawnedObsticles[i].transform.localPosition = new Vector3(Lanes[i].x, Lanes[i].y + 0.5f, Random.Range(-0.4f,0.4f));
            }
            else if((i == 0 && i == Openlane)||(i == 4 && i == Openlane)) // is the lane is onpen and on the edge spawn a passender pickup
            {
                spawnedpassengerpickup = Instantiate(Passengerpickup, Lanes[i], Quaternion.identity);
                spawnedpassengerpickup.transform.parent = transform;
                spawnedpassengerpickup.transform.localPosition = Lanes[i];
            }
            else // if tthe lase is open spawn coins
            {
                spawnedcoins = Instantiate(CoinsRow, Lanes[i], Quaternion.identity);
                spawnedcoins.transform.parent = transform;
                spawnedcoins.transform.localPosition = Lanes[i];
            }
            
        }

    }

    public void DestroyLast() // removes as spawned objects
    {
        if (SpawnedObsticles != null)
        {
            foreach (GameObject selectedObject in SpawnedObsticles)
            {
                Destroy(selectedObject);
            }
        }
        if(spawnedcoins!= null)
        {
            Destroy(spawnedcoins);
        }
        if(spawnedpassengerpickup != null)
        {
            Destroy(spawnedpassengerpickup);
        }


        
    }
    // Start is called before the first frame update
    void Start()
    {
        SpawnedObsticles = new GameObject[5];
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
