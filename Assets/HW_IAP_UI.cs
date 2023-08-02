using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using HmsPlugin;
using UnityEngine.UI;

public class HW_IAP_UI : MonoBehaviour
{

    public Button Btn_ItemCoins40k;
    public Button Btn_ItemCoins90k;
    public Button Btn_ItemCoins200k;
    public Button Btn_ItemCoins420k;
    public Button Btn_ItemCoins850k;
    public Button Btn_ItemCoins1730k;

    public Button Btn_ItemCar1;
    public Button Btn_ItemCar2;
    public Button Btn_ItemCar3;

    public Button Btn_ItemColour1Bottom;
    public Button Btn_ItemColour2Bottom;
    public Button Btn_ItemColour3Bottom;

    public Button Btn_ItemColour1Top;
    public Button Btn_ItemColour2Top;
    public Button Btn_ItemColour3Top;

    public Button Btn_ItemRims1;
    public Button Btn_ItemRims2;
    public Button Btn_ItemRims3;
    public Button Btn_ItemRims4;
    public Button Btn_ItemRims5;
    public Button Btn_ItemRims6;


    private Text Txt_Log;

    #region Monobehaviour

    private void Awake()
    {
        //Btn_ItemCoins100 = GameObject.Find("ItemBuyButtonC100").GetComponent<Button>();
        //Btn_ItemCoins1000 = GameObject.Find("ItemBuyButtonC1000").GetComponent<Button>();
        //Btn_ItemRemoveAds = GameObject.Find("ItemBuyButtonRemoveAds").GetComponent<Button>();
        //Btn_ItemPremium = GameObject.Find("ItemBuyButtonPremium").GetComponent<Button>();
        //Btn_Init = GameObject.Find("InitButton").GetComponent<Button>();

        //Btn_ManageSubscriptions = GameObject.Find("ManageSubscription").GetComponent<Button>();
        //Btn_EditSubscriptions = GameObject.Find("EditSubscription").GetComponent<Button>();

        //Txt_Log = GameObject.Find("StatusText").GetComponent<Text>();
    }

    private void OnEnable()
    {
        Btn_ItemCoins40k.onClick.AddListener(ButtonClick_BuyItemCoins40k);
        Btn_ItemCoins90k.onClick.AddListener(ButtonClick_BuyItemCoins90k);
        Btn_ItemCoins200k.onClick.AddListener(ButtonClick_BuyItemCoins200k);
        Btn_ItemCoins420k.onClick.AddListener(ButtonClick_BuyItemCoins420k);
        Btn_ItemCoins850k.onClick.AddListener(ButtonClick_BuyItemCoins850k);
        Btn_ItemCoins1730k.onClick.AddListener(ButtonClick_BuyItemCoins1730k);


        //IapDemoManager.IAPLog += OnIAPLog;
    }

    private void OnDisable()
    {
        Btn_ItemCoins40k.onClick.RemoveListener(ButtonClick_BuyItemCoins40k);
        Btn_ItemCoins90k.onClick.RemoveListener(ButtonClick_BuyItemCoins90k);
        Btn_ItemCoins200k.onClick.RemoveListener(ButtonClick_BuyItemCoins200k);
        Btn_ItemCoins420k.onClick.RemoveListener(ButtonClick_BuyItemCoins420k);
        Btn_ItemCoins850k.onClick.RemoveListener(ButtonClick_BuyItemCoins850k);
        Btn_ItemCoins1730k.onClick.RemoveListener(ButtonClick_BuyItemCoins1730k);


        //IapDemoManager.IAPLog -= OnIAPLog;
    }

    #endregion

    #region Callbacks

    private void OnIAPLog(string log)
    {
        Txt_Log.text = log;
    }

    #endregion

    #region Button Events

    private void ButtonClick_BuyItemCoins40k()
    {
        HW_IAP_Manager.Instance.PurchaseProduct("com.vetkoekstudios.taxiranked.coinstiny");
    }

    private void ButtonClick_BuyItemCoins90k()
    {
        HW_IAP_Manager.Instance.PurchaseProduct("com.vetkoekstudios.taxiranked.coinssmall");
    }

    private void ButtonClick_BuyItemCoins200k()
    {
        HW_IAP_Manager.Instance.PurchaseProduct("com.vetkoekstudios.taxiranked.coinsmediumv2");
    }

    private void ButtonClick_BuyItemCoins420k()
    {
        HW_IAP_Manager.Instance.PurchaseProduct("com.vetkoekstudios.taxiranked.coinslargev2");
    }

    private void ButtonClick_BuyItemCoins850k()
    {
        HW_IAP_Manager.Instance.PurchaseProduct("com.vetkoekstudios.taxiranked.coinsextralargev2");
    }

    private void ButtonClick_BuyItemCoins1730k()
    {
        HW_IAP_Manager.Instance.PurchaseProduct("com.vetkoekstudios.taxiranked.coinsmassivev2");
    }


    private void ButtonClick_InitializeIAP()
    {
        HW_IAP_Manager.Instance.InitializeIAP();
    }

    private void OpenSubscriptionEditingScreen()
    {
        //HW_IAP_Manager.Instance.RedirectingtoSubscriptionEditingScreen("premium");
    }

    private void OpenSubscriptionManagementScreen()
    {
        //HW_IAP_Manager.Instance.RedirectingtoSubscriptionManagementScreen();
    }

    #endregion

}
