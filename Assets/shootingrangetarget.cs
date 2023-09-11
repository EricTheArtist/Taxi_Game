using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class shootingrangetarget : MonoBehaviour
{
    public GameObject Explosion;
    public ParticleSystem Coins;
    public GameObject CarBody;
    public GameObject Carwreck;
    public Collider ThisCol;
    bool hit = false;
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Stop" && hit ==false)
        {
            ThisCol.enabled = false;
            hit = true;
            CarBody.SetActive(false);
            Carwreck.SetActive(true);
            Coins.Play();
            ArcadeCurrencyManager.ACMInstance.IncrementCoins(20);

        }
        Instantiate(Explosion, collision.transform.position, transform.rotation);
        Destroy(collision.gameObject);
    }
}
