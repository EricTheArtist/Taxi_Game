using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.Events;

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

    public GameObject RimsPanel;
    public GameObject StatsPanel;

    public GameObject PurchaseDialouge;

    public FlexibleColorPicker FCP;
    public Material UnderGlowMat;
    public Color DefaultUnderglow;
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
    public Slider StanceSlider;
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

    public GameObject[] RimTransform;
    public GameObject[] RimPrefabs;
    public GameObject[] LastRims;

    AbilityUpgrader AU;

    RectTransform rectTransform;
    ShopEffectManager SEM;

    public UnityEvent PurchaseSucess;
    public TMP_Text P_Text_amount;
    int P_Price;
    string P_Playerprefname;
    int P_CarIndex;
    int P_Rimindex;

    public AudioSource AudioPurchase;



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
        SEM = GameObject.FindGameObjectWithTag("ShopEffectManager").GetComponent<ShopEffectManager>();
        Screen.orientation = ScreenOrientation.AutoRotation;
        stanceStarty = CarChasisHolder.transform.localPosition.y;

        AU = gameObject.GetComponent<AbilityUpgrader>();

        Coins = PlayerPrefs.GetInt("Main Amount");
        updateCoinsText();
        refreshcolouronsamples();
        ActiveCar = PlayerPrefs.GetInt("ActiveCar");
        UpdateCars(ActiveCar);
        UpdateRims();

        // recalling the stance save
        float savedStance = PlayerPrefs.GetFloat("Suspension");
        StanceSlider.value = savedStance;
        Debug.Log("Stance save: " + savedStance);


        //recalling the underglow save
        Color Underglow;
        ColorUtility.TryParseHtmlString("#" + PlayerPrefs.GetString("UnderglowSave"), out Underglow);
        if (Underglow.r == 1 && Underglow.g == 1 && Underglow.b == 1)
        {
            FCP.color = DefaultUnderglow;
        }
        else
        {
            FCP.color = new Color(Underglow.r, Underglow.g, Underglow.b, 0.5f);
        }
        
        
    }

    // Update is called once per frame
    void Update()
    {
        TestUILayout();
        UnderGlowMat.color = FCP.color;
        

        
    }

    public void updatestance()
    {




        ActiveCar = PlayerPrefs.GetInt("ActiveCar");
        float yheight = Mathf.Lerp(stanceStarty, stanceStarty - 0.1f, StanceBar.value); //some refrence seems to be missing here
        //Debug.Log("SnanceValue: " + StanceBar.value);
        
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
        PlayerPrefs.SetFloat("Suspension", StanceBar.value);
        PlayerPrefs.SetString("UnderglowSave", ColorUtility.ToHtmlStringRGBA(FCP.color));
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
        AudioPurchase.Play();
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
        if(Cost <= Coins)
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
        AU.ChangeUIforCar(CarIndex);

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

    public void UpdateRims()
    {
        for (int j = 0; j < RimTransform.Length; j++)
        {
            Destroy(LastRims[j]);
            int activerim = PlayerPrefs.GetInt("ActiveRimIndex");
            LastRims[j] = Instantiate(RimPrefabs[activerim], RimTransform[j].transform);
        }
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
    public void ToggleRimsPanel()
    {
        ColourPanel1.SetActive(false);
        ColourPanel1.SetActive(false);
        if (RimsPanel.activeInHierarchy)
        {
            RimsPanel.SetActive(false);
        }
        else
        {
            RimsPanel.SetActive(true);
        }
    }

    public void ToggleStatsPanel()
    {
        if (StatsPanel.activeInHierarchy)
        {
            StatsPanel.SetActive(false);
        }
        else
        {
            StatsPanel.SetActive(true);
        }
    }

    public void OpenPurchaseDialouge(int price, string PlayerPrefname,int carindex, int rimindex) //if car index or rim index ar 100 they will be considered as null
    {
        PurchaseDialouge.SetActive(true);
        P_Text_amount.SetText(price.ToString());
        P_Price = price;
        P_Playerprefname = PlayerPrefname;
        P_CarIndex = carindex;
        P_Rimindex = rimindex;
    }

    public void ProcessPurchase()
    {
        DeductCoins(P_Price);
        PlayerPrefs.SetInt(P_Playerprefname, (true ? 1 : 0));

        if(P_CarIndex != 100)
        {
            PlayerPrefs.SetInt("ActiveCar", P_CarIndex);
            UpdateCars(P_CarIndex);
            SEM.BoughtNewCar();
            LockControl(false);
        }
        if(P_Rimindex != 100)
        {
            PlayerPrefs.SetInt("ActiveRimIndex", P_Rimindex);
            UpdateRims();
            SEM.BoughtNewCar();
        }
        PurchaseDialouge.SetActive(false);
        PurchaseSucess.Invoke();

    }

}
