using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cleanUpglitches : MonoBehaviour
{
    public GameObject Explosion;
    private void OnCollisionEnter(Collision collision)
    {
        
        if(collision.transform.tag == "Player")
        {
            SceneManager.LoadScene(2);
        }
        if(collision.transform.tag == "Stop")
        {
            Instantiate(Explosion, collision.transform.position, transform.rotation);
            Debug.Log("bullet hit");
            //GameObject explosion = Instantiate(Explosion,this.transform);
            //explosion.transform.position = collision.transform.position;
        }
        Destroy(collision.gameObject);
    }
}
