using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestCameraFollow : MonoBehaviour
{
    private Transform player;

    public float yOffset = 3f;
    public float zOffset = -10;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.Find("PLAYER_VEHICLE").transform;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = new Vector3(player.position.x/1.3f, player.position.y + yOffset, player.position.z + zOffset);
    }
}
