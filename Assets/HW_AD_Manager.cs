using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using HmsPlugin;
using HuaweiMobileServices.Ads;

public class HW_AD_Manager : MonoBehaviour
{
    //private Toggle testAdStatusToggle;
    private readonly string TAG = "[HMS] HW_AD_Manager: ";
    public ShopUIManager SUIM;
    public GameObject RrewardDisplay;

    #region Singleton

    public static HW_AD_Manager Instance { get; private set; }
    private void Singleton()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    #endregion

    private void Awake()
    {
        Singleton();
    }

    private void Start()
    {
        HMSAdsKitManager.Instance.OnRewarded = OnRewarded;
        HMSAdsKitManager.Instance.OnInterstitialAdClosed = OnInterstitialAdClosed;

        HMSAdsKitManager.Instance.ConsentOnFail = OnConsentFail;
        HMSAdsKitManager.Instance.ConsentOnSuccess = OnConsentSuccess;
        HMSAdsKitManager.Instance.RequestConsentUpdate();

        //testAdStatusToggle = GameObject.FindGameObjectWithTag("Toggle").GetComponent<Toggle>();
        //testAdStatusToggle.isOn = HMSAdsKitSettings.Instance.Settings.GetBool(HMSAdsKitSettings.UseTestAds);


        #region SetNonPersonalizedAd , SetRequestLocation

        var builder = HwAds.RequestOptions.ToBuilder();

        builder
            .SetConsent("tcfString")
            .SetNonPersonalizedAd((int)NonPersonalizedAd.ALLOW_ALL)
            .Build();

        bool requestLocation = true;
        var requestOptions = builder.SetConsent("testConsent").SetRequestLocation(requestLocation).Build();

        Debug.Log($"{TAG}RequestOptions NonPersonalizedAds:  {requestOptions.NonPersonalizedAd}");
        Debug.Log($"{TAG}Consent: {requestOptions.Consent}");

        #endregion

    }

    private void OnConsentSuccess(ConsentStatus consentStatus, bool isNeedConsent, IList<AdProvider> adProviders)
    {
        Debug.Log($"{TAG}OnConsentSuccess consentStatus:{consentStatus} isNeedConsent:{isNeedConsent}");
        foreach (var AdProvider in adProviders)
        {
            Debug.Log($"{TAG}OnConsentSuccess adproviders: Id:{AdProvider.Id} Name:{AdProvider.Name} PrivacyPolicyUrl:{AdProvider.PrivacyPolicyUrl} ServiceArea:{AdProvider.ServiceArea}");
        }
    }

    private void OnConsentFail(string desc)
    {
        Debug.LogError($"{TAG}OnConsentFail:{desc}");
    }

    public void ShowBannerAd()
    {
        Debug.Log($"{TAG}ShowBannerAd");

        HMSAdsKitManager.Instance.ShowBannerAd();
    }

    public void HideBannerAd()
    {
        Debug.Log($"{TAG}HideBannerAd");

        HMSAdsKitManager.Instance.HideBannerAd();
    }

    public void ShowRewardedAd()
    {
        Debug.Log($"{TAG}ShowRewardedAd");
        HMSAdsKitManager.Instance.ShowRewardedAd();
    }

    public void ShowInterstitialAd()
    {
        Debug.Log($"{TAG}ShowInterstitialAd");
        HMSAdsKitManager.Instance.ShowInterstitialAd();
    }

    public void ShowSplashImage()
    {
        Debug.Log($"{TAG}ShowSplashImage!");

        HMSAdsKitManager.Instance.LoadSplashAd("testq6zq98hecj", SplashAd.SplashAdOrientation.PORTRAIT);
    }

    public void ShowSplashVideo()
    {
        Debug.Log($"{TAG}ShowSplashVideo!");

        HMSAdsKitManager.Instance.LoadSplashAd("testd7c5cewoj6", SplashAd.SplashAdOrientation.PORTRAIT);
    }

    public void OnRewarded(Reward reward)
    {
        SUIM.AddCoins(500);
        RrewardDisplay.SetActive(true);
    }

    public void OnInterstitialAdClosed()
    {
        Debug.Log($"{TAG}interstitial ad closed");
    }

    public void SetTestAdStatus()
    {
        // HMSAdsKitManager.Instance.SetTestAdStatus(testAdStatusToggle.isOn);
        HMSAdsKitManager.Instance.SetTestAdStatus(HMSAdsKitSettings.Instance.Settings.GetBool(HMSAdsKitSettings.UseTestAds));
        HMSAdsKitManager.Instance.DestroyBannerAd();
        HMSAdsKitManager.Instance.LoadAllAds();
    }
}
