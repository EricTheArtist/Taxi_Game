using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class cleanUpglitches : MonoBehaviour
{

    private void OnCollisionEnter(Collision collision)
    {
        Destroy(collision.gameObject);
        if(collision.transform.tag == "Player")
        {
            SceneManager.LoadScene(2);
        }
    }
}
