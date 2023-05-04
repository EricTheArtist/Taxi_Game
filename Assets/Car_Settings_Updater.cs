using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car_Settings_Updater : MonoBehaviour
{
    public GameObject[] Meshes;
    public Texture2D[] _CarBaseTex;
    public Texture2D[] _CarMaskTex;
    public Renderer TaxiMaterial;
    int ActiveCar;
    // Start is called before the first frame update
    void Start()
    {
        ActiveCar = PlayerPrefs.GetInt("ActiveCar");
        UpdateCarsMain(ActiveCar);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void UpdateCarsMain(int CarIndex)
    {
        for (int i = 0; i < Meshes.Length; i++)
        {
            if (i == CarIndex)
            {
                Meshes[i].SetActive(true);
                TaxiMaterial.GetComponent<Renderer>().sharedMaterial.SetTexture("_Car_Tex", _CarBaseTex[i]);
                TaxiMaterial.GetComponent<Renderer>().sharedMaterial.SetTexture("_Car_Mask", _CarMaskTex[i]);
            }
            else
            {
                Meshes[i].SetActive(false);
            }
        }
    }
}
