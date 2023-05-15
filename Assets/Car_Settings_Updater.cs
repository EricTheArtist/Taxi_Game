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

    public Vector3[] TyresFrontPos;
    public Vector3[] TyresBackPos;

    public float[] TyresScaleX;
    public Vector3[] SloganPosition;
    public float[] SloganXrotation;
    public float[] SloganScale;

    float stanceStarty;
    public GameObject CarChasisHolder;
    public GameObject[] Wheelsleft;
    public GameObject[] Wheelsright;
    public float[] WheelScale;
    // Start is called before the first frame update
    void Start()
    {


            stanceStarty = CarChasisHolder.transform.localPosition.y;



        ActiveCar = PlayerPrefs.GetInt("ActiveCar");
        UpdateCarsMain(ActiveCar);
    }

    // Update is called once per frame
    void Update()
    {
        
    }


    public void UpdateCarsMain(int CarIndex)
    {
        //Sets suspension height
        float Stance = PlayerPrefs.GetFloat("Suspension");

        float yheight = Mathf.Lerp(stanceStarty, stanceStarty - 0.1f, Stance);
        CarChasisHolder.transform.localPosition = new Vector3(CarChasisHolder.transform.localPosition.x, yheight,
                                                CarChasisHolder.transform.localPosition.z);

        //sets wheel angle based on suspension height
        for (int j = 0; j < Wheelsleft.Length; j++)
        {
            float wheelRotation = Mathf.Lerp(0, 20, Stance);
            Wheelsleft[j].transform.localRotation = Quaternion.Euler(0, 180, wheelRotation);
        }
        for (int k = 0; k < Wheelsright.Length; k++)
        {
            float wheelRotation = Mathf.Lerp(0, 20, Stance);
            Wheelsright[k].transform.localRotation = Quaternion.Euler(0, 0, wheelRotation);
        }

        for (int i = 0; i < Meshes.Length; i++)
        {
            if (i == CarIndex)
            {   //activates car mesh
                Meshes[i].SetActive(true);
                //sets materials
                TaxiMaterial.GetComponent<Renderer>().sharedMaterial.SetTexture("_Car_Tex", _CarBaseTex[i]);
                TaxiMaterial.GetComponent<Renderer>().sharedMaterial.SetTexture("_Car_Mask", _CarMaskTex[i]);
                //sets tire position and scale
                TyresFront.transform.localPosition = TyresFrontPos[i];
                TyresBack.transform.localPosition = TyresBackPos[i];

                TyresFront.transform.localScale = new Vector3(TyresScaleX[i], WheelScale[i], WheelScale[i]);
                TyresBack.transform.localScale = new Vector3(TyresScaleX[i], WheelScale[i], WheelScale[i]);
                // sets slogan position and scale
                Slogan.transform.localPosition = new Vector3(SloganPosition[i].x, SloganPosition[i].y, SloganPosition[i].z);
                Slogan.transform.localEulerAngles = new Vector3(SloganXrotation[i], 0, 0);
                Slogan.transform.localScale = new Vector3(SloganScale[i], SloganScale[i], SloganScale[i]);



            }
            else
            {
                Meshes[i].SetActive(false);
            }
        }
    }
}
