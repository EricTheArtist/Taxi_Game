using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class ShopUIManager : MonoBehaviour
{
    public int Coins;
    public TMP_Text coins_amount_text;
    public TMP_Text coins_amount_shadow;
    public GameObject shopInterface;
    public GameObject PlayButton;
    public GameObject TaxiRankScene;

    public GameObject ColourPanel1;
    public GameObject ColourPanel2;

    public FlexibleColorPicker FCP;
    public Material UnderGlowMat;
    public GameObject[] Shops;
    public Button[] Tabs;

    public IS_Script ADscript;

    //public bool updatedV = true;
    //public bool updatedH = true;

    public Image colour1Sample;
    public Image colour2Sample;
    public Renderer TaxiMaterial;
    public GameObject Slogan;
    public GameObject LockIcon;
    public Texture2D[] CarBaseTex;
    public Texture2D[] CarMaskTex;

    public GameObject[] Cars;
    int ActiveCar;
    public GameObject TyresFront;
    public GameObject TyresBack;

    
    public Vector3[] TyresFrontPos;
    public Vector3[] TyresBackPos;
    

    public float[] TyresScaleX;
    public Vector3[] SloganPosition;
    public float[] SloganXrotation;
    public float[] SloganScale;

    public GameObject NumberPlate;
    public Vector3[] NumberPlatePos;

    public float StanceMax; //not currently used
    public Slider StanceBar;
    float stanceStarty;
    public GameObject CarChasisHolder;

    public GameObject[] Wheelsleft;
    public GameObject[] Wheelsright;
    public float[] WheelScale;



    RectTransform rectTransform;
  
    void Awake()
    {
        rectTransform = GetComponent<RectTransform>();

        SetOrientation();
    }
    void SetOrientation()
    {
        if (rectTransform == null) return;
        bool verticalOrientation = rectTransform.rect.width < rectTransform.rect.height ? true : false;

        if(verticalOrientation == true)
        {
            ADscript.LoadBannerAd(0);
            LayoutVertical();
        }
        if(verticalOrientation == false)
        {
            ADscript.DestroyBannerAd();
            LayoutHorizontal();
        }
    }
    void OnRectTransformDimensionsChange()
    {
        SetOrientation();
    }



    // Start is called before the first frame update
    void Start()
    {
        Screen.orientation = ScreenOrientation.AutoRotation;
        stanceStarty = CarChasisHolder.transform.localPosition.y;
        
        Coins = PlayerPrefs.GetInt("Main Amount");
        updateCoinsText();
        refreshcolouronsamples();
        ActiveCar = PlayerPrefs.GetInt("ActiveCar");
        UpdateCars(ActiveCar);

        
            

    }

    // Update is called once per frame
    void Update()
    {
        TestUILayout();
        UnderGlowMat.color = FCP.color;
        
        /* Was used to check layout
        if ( Screen.orientation == ScreenOrientation.Portrait)
        {
            if (updatedV == true)
            {
                LayoutVertical();
            }


        }
        else
        {
            if (updatedH == true)
            {
                LayoutHorizontal();
                
            }

        }
        */
        
    }

    public void updatestance()
    {
        ActiveCar = PlayerPrefs.GetInt("ActiveCar");
        float yheight = Mathf.Lerp(stanceStarty, stanceStarty - 0.1f, StanceBar.value); //some refrence seems to be missing here
        
        
        CarChasisHolder.transform.localPosition = new Vector3(CarChasisHolder.transform.localPosition.x, yheight, 
                                                        CarChasisHolder.transform.localPosition.z);
        for (int i = 0; i < Wheelsleft.Length; i++)
        {
            float wheelRotation = Mathf.Lerp(0, 20, StanceBar.value);
            Wheelsleft[i].transform.localRotation = Quaternion.Euler(0, 180, wheelRotation);

        }
        for (int i = 0; i < Wheelsright.Length; i++)
        {
            float wheelRotation = Mathf.Lerp(0, 20, StanceBar.value);
            Wheelsright[i].transform.localRotation = Quaternion.Euler(0, 0, wheelRotation);
        }

        PlayerPrefs.SetFloat("Suspension", StanceBar.value);



    }


    void LayoutVertical()
    {

        //sent ancorage
        RectTransform RT_Shop = shopInterface.GetComponent<RectTransform>();
        RT_Shop.anchorMin = new Vector2(0.5f, 0);
        RT_Shop.anchorMax = new Vector2(0.5f, 0);
        RT_Shop.pivot = new Vector2(0.5f, 0);

        //set positions
        if(TaxiRankScene!= null)
        {
            TaxiRankScene.transform.position = new Vector3(0, -0.5f, -0.8f);
        }
        

        //updatedV = false;
        //updatedH = true;
    }
    void LayoutHorizontal()
    {

        //sent ancorage
        RectTransform RT_Shop = shopInterface.GetComponent<RectTransform>();
        RT_Shop.anchorMin = new Vector2(1, 0.5f);
        RT_Shop.anchorMax = new Vector2(1, 0.5f);
        RT_Shop.pivot = new Vector2(1, 0.5f);

        //set positions
        if (TaxiRankScene != null)
        { 
            TaxiRankScene.transform.position = new Vector3(-1f, 0.3f, -4f);
        }
            

        //updatedH = false;
        //updatedV = true;
    }





    void TestUILayout()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            LayoutVertical();
        }

        if (Input.GetKeyDown(KeyCode.H))
        {
            LayoutHorizontal();
        }
    }

    public void refreshcolouronsamples()
    {
        colour1Sample.color = TaxiMaterial.sharedMaterial.GetColor("_Color");
        colour2Sample.color = TaxiMaterial.sharedMaterial.GetColor("_Color2");
    }
    public void Button_Play()
    {
        //ADscript.DestroyBannerAd();
        SceneManager.LoadScene(0, LoadSceneMode.Single);
    }

    void RefreshCoinValue()
    {
        Coins = PlayerPrefs.GetInt("Main Amount");
    }
    public void updateCoinsText()
    {
        RefreshCoinValue();
        coins_amount_text.SetText(Coins.ToString());
        coins_amount_shadow.SetText(Coins.ToString());
    }

    public void DeductCoins(int deductAmount) //public function that can be called when an item is purchased with coins
    {
        Coins = Coins - deductAmount;
        PlayerPrefs.SetInt("Main Amount", Coins);
        updateCoinsText();
    }

    public void AddCoins(int AddAmount)
    {
        Coins = Coins + AddAmount;
        PlayerPrefs.SetInt("Main Amount", Coins);
        updateCoinsText();
    }

    public bool CheckForEnoughMoney(int Cost) //Public function that must be called each time the player attempts to but something with coins
    {
        if(Cost < Coins)
        {
            return true;
        }
        else
        {
            return false;
        }
    
    }

    public void ShopTabClicked(int ButtonIndex)
    {
        for (int i = 0; i < Shops.Length; i++)
        { 
            if(i == ButtonIndex)
            {
                Shops[i].SetActive(true);
                Tabs[i].GetComponent<Image>().color = new Color32(127, 127, 127, 255); 
            }
            else
            {
                Shops[i].SetActive(false);
                Tabs[i].GetComponent<Image>().color = new Color32(255, 255, 255, 255);
            }

        }
    }


    public void UpdateCars(int CarIndex)
    {

        for (int i = 0; i < Cars.Length; i++)
        {
            if (i == CarIndex)
            {
                Cars[i].SetActive(true);
                TaxiMaterial.GetComponent<Renderer>().sharedMaterial.SetTexture("_Car_Tex",CarBaseTex[i]);
                TaxiMaterial.GetComponent<Renderer>().sharedMaterial.SetTexture("_Car_Mask", CarMaskTex[i]);
                TyresFront.transform.localPosition = TyresFrontPos[i];
                TyresBack.transform.localPosition = TyresBackPos[i];

                TyresFront.transform.localScale = new Vector3(TyresScaleX[i], WheelScale[i], WheelScale[i]);
                TyresBack.transform.localScale = new Vector3(TyresScaleX[i], WheelScale[i], WheelScale[i]);

                Slogan.transform.localPosition = new Vector3(SloganPosition[i].x, SloganPosition[i].y, SloganPosition[i].z);
                Slogan.transform.localEulerAngles = new Vector3(SloganXrotation[i], 0, 0);
                Slogan.transform.localScale = new Vector3(SloganScale[i],SloganScale[i],SloganScale[i]);

                NumberPlate.transform.localPosition = new Vector3(NumberPlatePos[i].x, NumberPlatePos[i].y, NumberPlatePos[i].z);
            }
            else
            {
                Cars[i].SetActive(false);
            }
        }

        updatestance();
    }

    public void LockControl(bool LockStatus)
    {
        LockIcon.SetActive(LockStatus);
    }

    public void ToggleColourPanel1()
    {
        ColourPanel2.SetActive(false);
        if (ColourPanel1.activeInHierarchy)
        {
            ColourPanel1.SetActive(false);
        }
        else
        {
            ColourPanel1.SetActive(true);
        }
    }

    public void ToggleColourPanel2()
    {
        ColourPanel1.SetActive(false);
        if (ColourPanel2.activeInHierarchy)
        {
            ColourPanel2.SetActive(false);
        }
        else
        {
            ColourPanel2.SetActive(true);
        }
    }
}
