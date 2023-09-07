using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class fruitstandVFXcontroller : MonoBehaviour
{
    public GameObject Fruit;
    public GameObject FruitVFX;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.transform.tag == "Player")
        {
            Vector3 pos = new Vector3(transform.position.x, transform.position.y + 0.3f, transform.position.z);
            if(Fruit!= null)
            {
                Destroy(Fruit);
                Instantiate(FruitVFX, pos,Quaternion.identity);
            }

        }
    }
}
