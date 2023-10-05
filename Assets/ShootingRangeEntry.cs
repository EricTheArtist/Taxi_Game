using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShootingRangeEntry : MonoBehaviour
{
    public GameObject RentTankOption;
    public IS_MainScript ISMAIN;

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Player")
        {
            bool Owned = (PlayerPrefs.GetInt("MLCar02Premium") != 0);
            if(Owned == true)
            {
                ISMAIN.DestroyBannerAd();
                SceneManager.LoadScene(3);
            }
            else
            {
                RentTankOption.SetActive(true);
            }

            
            
        }
    }
}
