using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
/*using GoogleMobileAds.Api;
using GoogleMobileAds.Sample;*/
[RequireComponent(typeof(UnityPaidEvent))]
public class AdsManager : AbstractSingleton<AdsManager>
{
   /* [SerializeField] SplashManager _splashHandler;
    [SerializeField] Scriptable_GameValues persistantValues;*/

    /*public BannerViewController _Banner;
    public InterstitialAdController _Interstitials;
    public RewardedAdController _Rewarded;
    public AppOpenAdController _AppOpen;

    public UnityPaidEvent UNITY_PAID_EVENTS { get; private set; }
    // ca-app-pub-8411576398660554~4943852874
    // //test

    int LoadAdAfter = 30;

    public bool shouldShowBanner { get; private set; }
    public bool shouldShowAppOpen { get; private set; }
    public int canShowAdAfter { get; private set; }
    public int inventoryRewardValue { get; private set; }

    protected override void Awake()
    {
        base.Awake();

        shouldShowBanner = false;
        shouldShowAppOpen = false;
        canShowAdAfter = 3;
        inventoryRewardValue = 3;
    }
    private void Start()
    {
        UNITY_PAID_EVENTS = GetComponent<UnityPaidEvent>();
        AdmobGA_Helper.GA_Log(AdmobGAEvents.Initializing);
        MobileAds.RaiseAdEventsOnUnityMainThread = true;
        MobileAds.Initialize(initStatus =>
        {
            AdmobGA_Helper.GA_Log(AdmobGAEvents.Initialized);
            Dictionary<string, AdapterStatus> map = initStatus.getAdapterStatusMap();
            foreach (KeyValuePair<string, AdapterStatus> keyValuePair in map)
            {
                string className = keyValuePair.Key;
                AdapterStatus status = keyValuePair.Value;

                switch (status.InitializationState)
                {
                    case AdapterState.NotReady:
                        // The adapter initialization did not complete.
                        // MonoBehaviour.print("Adapter: " + className + " not ready.");
                        break;
                    case AdapterState.Ready:
                        // The adapter was successfully initialized.
                        // MonoBehaviour.print("Adapter: " + className + " is initialized.");
                        break;
                }
            }

            _Interstitials.LoadAd();
            _Rewarded.LoadAd();
           // _splashHandler?.InitializationCompleted();
        });
    }

    public void ShowRewardedAd(Action _rewardAction)
    {
        _Rewarded.ShowAd(_rewardAction);
    }

    public void ShowInterstitialAd()
    {
       // if (persistantValues.isRemoveAds) return;
       // if (!CanShowInterstitalAd()) return;
        _Interstitials.ShowAd();
    }
    public void ShowBannerAd()
    {
       // if (persistantValues.isRemoveAds) return;
        if (!shouldShowBanner) return;
        _Banner.ShowAd();
    }
    public void ShowAppOpenAd()
    {
       /// if (persistantValues.isRemoveAds) return;
        if (!shouldShowAppOpen) return;
        _AppOpen.ShowAd();
    }
    public bool CanShowRewardedVideoAd()
    {
        return _Rewarded.CanShowRewarded();
    }

    public void RemoteShowBannerAppOpen(bool banner, bool appopen,int showAdAfter,int inventoryReward)
    {
        Debug.Log("Remote Values : " + banner + " _ " + appopen + " _ " + showAdAfter + " _ " + inventoryReward);
        shouldShowAppOpen = appopen;
        shouldShowBanner = banner;
        canShowAdAfter = showAdAfter;
        inventoryRewardValue = inventoryReward;
        if (banner == true)
        {
            _Banner.LoadAd();
            Invoke(nameof(ShowBannerAd), 2);
        }
        else
        {
            _Banner.DestroyAd();
        }
        if (appopen == true)
        {
            _AppOpen.LoadAd();
            Invoke(nameof(ShowAppOpenAd), 2);

        }
    }
   // DataManager dataManager;
   *//* bool CanShowInterstitalAd()
    {
        Debug.Log("Ads Level: " + persistantValues.unlockedLevelIndex);
        Debug.Log("Ads Index: " + canShowAdAfter);
        return (persistantValues.unlockedLevelIndex > canShowAdAfter);
    }*/
}
