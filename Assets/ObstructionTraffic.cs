using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstructionTraffic : MonoBehaviour
{

    float speed;

    private void OnEnable()
    {
        speed = -8;
    }
    private void FixedUpdate()
    {
        transform.Translate(new Vector3(0, 0, speed) * Time.deltaTime);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Obstruction")
        {
            speed = 0;
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "Coin")
        {
            other.gameObject.SetActive(false);
        }
    }

}
