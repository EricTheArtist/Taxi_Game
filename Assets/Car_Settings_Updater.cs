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

    public GameObject TyresFront;
    public GameObject TyresBack;
    public GameObject Slogan;

    public float[] TyresFrontZ;
    public float[] TyresBackZ;

    public float[] TyresScaleX;
    public Vector3[] SloganPosition;
    public float[] SloganXrotation;
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

                TyresFront.transform.localPosition = new Vector3(0, -0.3267f, TyresFrontZ[i]);
                TyresBack.transform.localPosition = new Vector3(0, -0.3268f, TyresBackZ[i]);

                TyresFront.transform.localScale = new Vector3(TyresScaleX[i], 1, 1);
                TyresBack.transform.localScale = new Vector3(TyresScaleX[i], 1, 1);

                Slogan.transform.localPosition = new Vector3(SloganPosition[i].x, SloganPosition[i].y, SloganPosition[i].z);
                Slogan.transform.localEulerAngles = new Vector3(SloganXrotation[i], 0, 0);
            }
            else
            {
                Meshes[i].SetActive(false);
            }
        }
    }
}
