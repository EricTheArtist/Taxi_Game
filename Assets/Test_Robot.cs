using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test_Robot : MonoBehaviour
{
    public GameObject RedLight;
    public GameObject GreenLight;
    public bool RedLightON;
    float ChanceOfRed = 0.5f;
    float LightDuration = 2;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void CountoGreen()
    {
        Invoke("CountToGreenComplete", LightDuration);
    }

    void CountToGreenComplete()
    {
        RedLightON = false;
        SetLightVisuals();
    }

    void SetLightVisuals()
    {
        if (RedLightON == true)
        {
            RedLight.SetActive(true);
            GreenLight.SetActive(false);
        }
        else
        {
            RedLight.SetActive(false);
            GreenLight.SetActive(true);
        }
    }

    private void OnEnable()
    {
        if(Random.value > ChanceOfRed)
        {
            RedLightON = true;
        }
        else
        {
            RedLightON = false;
        }
        SetLightVisuals();
    }
}
