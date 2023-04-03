using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_LaneElements : MonoBehaviour
{
    public Vector3[] Lanes;
    GameObject[] SpawnedObsticles;
    GameObject spawnedcoins;
    public GameObject ObstructionPrefab;
    public GameObject CoinsRow;

    public void OnTranslatePosition( int Openlane)
    {
        DestroyLast();

        for (int i = 0; i < Lanes.Length; i++)
        {
            if(i != Openlane)
            {
                SpawnedObsticles[i] = Instantiate(ObstructionPrefab, Lanes[i], Quaternion.identity);
                SpawnedObsticles[i].transform.parent = transform;
                SpawnedObsticles[i].transform.localPosition = Lanes[i];
            }
            else
            {
                spawnedcoins = Instantiate(CoinsRow, Lanes[i], Quaternion.identity);
                spawnedcoins.transform.parent = transform;
                spawnedcoins.transform.localPosition = Lanes[i];
            }
            
        }

    }

    void DestroyLast()
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
