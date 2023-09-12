using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShootingRangeEntry : MonoBehaviour
{
    public GameObject RentTankOption;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            bool Owned = (PlayerPrefs.GetInt("Car02Premium") != 0);
            if(Owned == true)
            {
                SceneManager.LoadScene(3);
            }
            else
            {
                RentTankOption.SetActive(true);
            }

            
            
        }
    }
}
