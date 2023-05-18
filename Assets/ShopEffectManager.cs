using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShopEffectManager : MonoBehaviour
{

    public ParticleSystem SwitchCar;
    public ParticleSystem BuyCar;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void SwitchedCars()
    {
        SwitchCar.Play();
    }

    public void BoughtNewCar()
    {
        BuyCar.Play();
    }
}
