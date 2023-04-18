using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoinAnimations : MonoBehaviour
{
    private float coin1RotationSpeed = 70f;
    private float coin2RotationSpeed = 100f;
    private float coin1BobTime = 0.30f;
    private float coin2BobTime = 0.30f;


    // Update is called once per frame
    void Update()
    {
        if (tag == "Coin")
        {
            transform.Rotate(0f, coin1RotationSpeed * Time.deltaTime, 0f, Space.World);
            //transform.position = Vector3.Lerp(transform.position, transform.position + new Vector3(0,1.5f,0), coin1BobTime);
        }

        if (tag == "Coin2")
        {
            transform.Rotate(0f, coin2RotationSpeed * Time.deltaTime, 0f, Space.World);
            //transform.position = Vector3.Lerp(transform.position, transform.position + Vector3.down, coin1BobTime);
        }
    }
}
