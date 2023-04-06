using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public List<GameObject> roads;
    private float offset = 20f;
    int clearLane = 2;
    Test_LaneElements TLE;
    void Start()
    {
        if (roads != null && roads.Count > 0)
        {
            roads = roads.OrderBy(r => r.transform.position.z).ToList();
        }
    }

    public void MoveRoad()
    {
        //move the road
        GameObject movedRoad = roads[0];
        roads.Remove(movedRoad);
        float newZ = roads[roads.Count - 1].transform.position.z + offset;
        movedRoad.transform.position = new Vector3(0, -0.5f, newZ);
        roads.Add(movedRoad);

        //update obsticles on road
        TLE = movedRoad.GetComponent<Test_LaneElements>();
        TLE.OnTranslatePosition(clearLane);
        if(clearLane == 0) //if the clear lane is in the furthest right
        {
            int randomI = 1; //Random.Range(0, 2);
            clearLane += randomI;
            Debug.Log("Random from 0: " + randomI);

        }
        if(clearLane == 4) //if the clear lane is in the furthest right lane
        {
            int randomI = -1; //Random.Range(0, -2);
            clearLane += randomI;
            Debug.Log("Randomfrom 4: " + randomI);
        }
        else if(clearLane < 4 && clearLane > 0) // if the clear lane is in any middle 3 lanes
        {
            int randomI = Random.Range(-1, 2);
            clearLane += randomI;
            Debug.Log("Random mic: " + randomI);
        }
        Debug.Log("Clear lane: " + clearLane );

    }

    public void ClearRoads()
    {
        foreach (GameObject objet in roads)
        {
            Test_LaneElements TLM;
            TLM = objet.GetComponent<Test_LaneElements>();
            TLM.DestroyLast();
        }
    }


    public void AlignRoads() // called by the floating origin script each time all the assets are sent back to the origin
    {

        foreach (GameObject obj in roads)
        {
            obj.transform.position = new Vector3(0, -0.5f, obj.transform.position.z);
        }

    }
    
}
