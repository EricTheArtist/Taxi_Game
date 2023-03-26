using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class RoadSpawner : MonoBehaviour
{
    public List<GameObject> roads;
    private float offset = 20f;
    void Start()
    {
        if (roads != null && roads.Count > 0)
        {
            roads = roads.OrderBy(r => r.transform.position.z).ToList();
        }
    }

    public void MoveRoad()
    {
        GameObject movedRoad = roads[0];
        roads.Remove(movedRoad);
        float newZ = roads[roads.Count - 1].transform.position.z + offset;
        movedRoad.transform.position = new Vector3(0, -0.5f, newZ);
        roads.Add(movedRoad);
    }

    public void AlignRoads() // called by the floating origin script each time all the assets are sent back to the origin
    {

        foreach (GameObject obj in roads)
        {
            obj.transform.position = new Vector3(0, -0.5f, obj.transform.position.z);
        }

    }
    
}
