using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCameraFollow : MonoBehaviour
{
    private Transform player;

    private float yOffset = 1.7f;
    private float zOffset = -4;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("Taxi").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(player.position.x, player.position.y + yOffset, player.position.z + zOffset);
    }
}
