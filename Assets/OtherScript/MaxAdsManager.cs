using System;
using System.Drawing;
using UnityEngine;
//using static MaxSdkCallbacks;
using UnityEngine.UIElements;
using static MaxAdsManager;
//using GameAnalyticsSDK;
using Watermelon.BusStop;
using static Watermelon.UnityAdsLegacyContainer;
//using static MaxSdkBase;
//using ByteBrewSDK;
//using static MaxEvents;
using static UnityEngine.Rendering.DebugUI;
using System.Collections.Generic;
//using Firebase.Analytics;

public class MaxAdsManager : MonoBehaviour
{
    /*public string SDK_KEY = "YOUR_SDK_KEY_HERE";
    public string RewardedadUnitId = "«Android-ad-unit-ID»";
    public string InterstitialadUnitId = "«Android-ad-unit-ID»";
    public string bannerAdUnitId = "«Android-ad-unit-ID»";
    public string AppOpenAdUnitId = "«Android-ad-unit-ID»";
    int retryAttempt_Rewarded;
    int retryAttempt_Inter;
    public static MaxAdsManager instance;
    public delegate void RewardAfterAd();
    private static RewardAfterAd notifyReward;
    public CustomAnalyticsManager CustomAnalytics;
    public FirebaseManager firebaseManager;
    private bool canShowIAD=false;
    public const string InterstitialPlacement = "InterstitialPlacement";
    public const string BannerPlacement = "BannerPlacement";
    public const string RewardedPlacement = "RewardedPlacement";
    private void Awake()
    {
        if(instance == null)
        {
            instance = this;
        }
        DontDestroyOnLoad(instance);
    }
    private void Update()
    {
        if (PlayerPrefs.GetInt("LevelExceeds") == 0)
        {
            CheckLevelThreshold();
        }
    }
    public void Log_GA_Event(string s)
    {
       
        CustomAnalytics.LogEvent_NonRevenue(s);

    }
    public void GADesignEvent(string s) { 

    CustomAnalyticsManager.instance.GADesignEvent(s);
    }
    void EnableIADShowing()
    {
        canShowIAD = true;
    }
    void Start()
    {
       
        MaxSdkCallbacks.OnSdkInitializedEvent += sdkConfiguration =>
        {
           // MaxSdk.ShowMediationDebugger();
            Debug.Log("AppLovin MAX SDK Initialized");
            // Optional: Enable verbose logging for debugging
            MaxSdk.SetVerboseLogging(true);
            InitializeBannerAds();
            InitializeInterstitialAds();
            InitializeRewardedAds();
            InitializeAppOpenEvents();
            MaxSdk.LoadAppOpenAd(AppOpenAdUnitId);
            canShowIAD = true;
            // Load an ad (example)

        };

        MaxSdk.SetSdkKey(SDK_KEY);
        MaxSdk.InitializeSdk();
    }
    #region Rewarded

    public void InitializeRewardedAds()
    {
        // Attach callback
        MaxSdkCallbacks.Rewarded.OnAdLoadedEvent += OnRewardedAdLoadedEvent;
        MaxSdkCallbacks.Rewarded.OnAdLoadFailedEvent += OnRewardedAdLoadFailedEvent;
        MaxSdkCallbacks.Rewarded.OnAdDisplayedEvent += OnRewardedAdDisplayedEvent;
        MaxSdkCallbacks.Rewarded.OnAdClickedEvent += OnRewardedAdClickedEvent;
        MaxSdkCallbacks.Rewarded.OnAdRevenuePaidEvent += OnRewardedAdRevenuePaidEvent;
        MaxSdkCallbacks.Rewarded.OnAdHiddenEvent += OnRewardedAdHiddenEvent;
        MaxSdkCallbacks.Rewarded.OnAdDisplayFailedEvent += OnRewardedAdFailedToDisplayEvent;
        MaxSdkCallbacks.Rewarded.OnAdReceivedRewardEvent += OnRewardedAdReceivedRewardEvent;
       
        // Load the first rewarded ad
        LoadRewardedAd();
    }
    public void ShowRewardedVideo(RewardAfterAd r)
    {
        if (MaxSdk.IsRewardedAdReady(RewardedadUnitId))
        {
            notifyReward = r;
            MaxSdk.ShowRewardedAd(RewardedadUnitId);
        }
    }
    public void LoadRewardedAd()
    {
        MaxSdk.LoadRewardedAd(RewardedadUnitId);
    }

    private void OnRewardedAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad is ready for you to show. MaxSdk.IsRewardedAdReady(adUnitId) now returns 'true'.
        Log_GA_Event("Max:RewardedVideo:Loaded");
        int levelValue = 0;

        if (FindAnyObjectByType<LevelController>())
        {
            levelValue = LevelController.DisplayLevelNumber + 1;
        }
        GameAnalytics.NewAdEvent(GAAdAction.Loaded, GAAdType.RewardedVideo, "MAX", RewardedPlacement);
        // Reset retry attempt
        retryAttempt_Rewarded = 0;
        ////ByteBrew Call
        var EventParameters = new Dictionary<string, string>()
{
    { "EventName", "RewardedVideoLoaded" },
    { "AdNetwork", "MAX" },
    { "AdUnitId", adUnitId },
    { "Revenue", adInfo.Revenue.ToString() },
    { "Platform", GetPlatformString() },
    {"LevelNumber",levelValue.ToString() }
};

        //ByteBrew.NewCustomEvent("Ads", EventParameters);
    }

    private void OnRewardedAdLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        // Rewarded ad failed to load
        // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds).
        Log_GA_Event("Max:RewardedVideo:Failed");
        int levelValue = 0;

        if (FindAnyObjectByType<LevelController>())
        {
            levelValue = LevelController.DisplayLevelNumber + 1;
        }
        string Adplace = null;
        if (FindAnyObjectByType<customManagerScript>())
        {
            Adplace = customManagerScript.instance.AdPlace;
        }
        GameAnalytics.NewAdEvent(GAAdAction.FailedShow, GAAdType.RewardedVideo, "MAX", RewardedPlacement);
        retryAttempt_Rewarded++;
        double retryDelay = Math.Pow(2, Math.Min(6, retryAttempt_Rewarded));
        var EventParameters = new Dictionary<string, string>()
{
    { "EventName", "RewardedVideoFailed" },
    { "AdNetwork", "MAX" },
    { "AdUnitId", adUnitId },
    { "ErrorMessage", errorInfo.Message },
    { "Platform", GetPlatformString() },
    {"LevelNumber",levelValue.ToString() },
    {"AdPlace",Adplace }
};

      //  ByteBrew.NewCustomEvent("Ads", EventParameters);
        Invoke("LoadRewardedAd", (float)retryDelay);
    }

    private void OnRewardedAdDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) {
        Log_GA_Event("Max:RewardedVideo:Shown");
        string Adplace = null;
        if (FindAnyObjectByType<customManagerScript>())
        {
            Adplace = customManagerScript.instance.AdPlace;
        }
        int levelValue = 0;

        if (FindAnyObjectByType<LevelController>())
        {
            levelValue = LevelController.DisplayLevelNumber + 1;
        }
        GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.RewardedVideo, "MAX", RewardedPlacement);
       
        SolarEngineObject.instance.TrackSolarEngineAdImpression("Rewarded", adUnitId, adInfo.Revenue, GetPlatformString());
       // ByteBrew.TrackAdEvent(ByteBrewAdTypes.Reward, "applovin", adUnitId, adInfo.Revenue);
        var EventParameters = new Dictionary<string, string>()
{
    { "EventName", "RewardedVideoDisplayed" },
    { "AdNetwork", "MAX" },
    { "AdUnitId", adUnitId },
    { "Revenue", adInfo.Revenue.ToString() },
    { "Platform", GetPlatformString() },
    {"LevelNumber",levelValue.ToString() },
    {"AdPlace",Adplace }
};

        //ByteBrew.NewCustomEvent("Ads", EventParameters);
    }


    private string GetPlatformString()
    {
        // Determine the platform
        switch (Application.platform)
        {
            case RuntimePlatform.Android:
                return "android";
            case RuntimePlatform.IPhonePlayer:
                return "ios";
            case RuntimePlatform.WindowsPlayer:
                return "windows";
            case RuntimePlatform.OSXPlayer:
                return "mac";
            case RuntimePlatform.WebGLPlayer:
                return "webgl";
            default:
                return "unknown";
        }
    }

    private void OnRewardedAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        // Rewarded ad failed to display. AppLovin recommends that you load the next ad.
        LoadRewardedAd();
    }

    private void OnRewardedAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) {

        string Adplace = null;
        if (FindAnyObjectByType<customManagerScript>())
        {
            Adplace = customManagerScript.instance.AdPlace;
        }
        int levelValue = 0;

        if (FindAnyObjectByType<LevelController>())
        {
            levelValue = LevelController.DisplayLevelNumber + 1;
        }
        var EventParameters = new Dictionary<string, string>()
{
    { "EventName", "RewardedVideoClicked" },
    { "AdNetwork", "MAX" },
    { "AdUnitId", adUnitId },
    { "Revenue", adInfo.Revenue.ToString() },
    { "Platform", GetPlatformString() },
    {"LevelNumber",levelValue.ToString() },
     {"AdPlace",Adplace }
};

      //  ByteBrew.NewCustomEvent("Ads", EventParameters);
    }

    private void OnRewardedAdHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        string Adplace = null;
        if (FindAnyObjectByType<customManagerScript>())
        {
            Adplace = customManagerScript.instance.AdPlace;
        }
        // Rewarded ad is hidden. Pre-load the next ad
        int levelValue = 0;

        if (FindAnyObjectByType<LevelController>())
        {
            levelValue = LevelController.DisplayLevelNumber + 1;
        }
        var EventParameters = new Dictionary<string, string>()
{
    { "EventName", "RewardedVideoHidden" },
    { "AdNetwork", "MAX" },
    { "AdUnitId", adUnitId },
    { "Revenue", adInfo.Revenue.ToString() },
    { "Platform", GetPlatformString() },
    {"LevelNumber",levelValue.ToString() },
    {"AdPlace",Adplace }
};

        // ByteBrew.NewCustomEvent("Ads", EventParameters);
        if (adUnitId == RewardedadUnitId)
        {
            notifyReward = null;
        }
        LoadRewardedAd();
    }

    private void OnRewardedAdReceivedRewardEvent(string adUnitId, MaxSdk.Reward reward, MaxSdkBase.AdInfo adInfo)
    {
        // The rewarded ad displayed and the user should receive the reward.
        if (adUnitId == RewardedadUnitId && notifyReward != null)
        {
            notifyReward.Invoke(); // or notifyReward?.Invoke();
            notifyReward = null;
        }
    }

    private void OnRewardedAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        string Adplace = null;
        if (FindAnyObjectByType<customManagerScript>())
        {
            Adplace = customManagerScript.instance.AdPlace;
        }
        int levelValue = 0;

        if (FindAnyObjectByType<LevelController>())
        {
            levelValue = LevelController.DisplayLevelNumber + 1;
        }
        // Ad revenue paid. Use this callback to track user revenue.
        var EventParameters = new Dictionary<string, string>()
{
    { "EventName", "RewardedVideoRevenuePaid" },
    { "AdNetwork", "MAX" },
    { "AdUnitId", adUnitId },
    { "Revenue", adInfo.Revenue.ToString() },
    { "Platform", GetPlatformString() },
    {"LevelNumber",levelValue.ToString() },
    {"AdPlace",Adplace }
};
        // ByteBrew.NewCustomEvent("Ads", EventParameters);
        Firebase.Analytics.FirebaseAnalytics.LogEvent(
        "ad_impression",
        new Firebase.Analytics.Parameter[] {
            new Firebase.Analytics.Parameter("ad_platform", GetPlatformString()),
            new Firebase.Analytics.Parameter("ad_source", adInfo.NetworkName),
            new Firebase.Analytics.Parameter("AdUnitId", adUnitId),
            new Firebase.Analytics.Parameter("ad_format", adInfo.AdFormat),
            new Firebase.Analytics.Parameter("value", adInfo.Revenue.ToString()),
            new Firebase.Analytics.Parameter("currency", "USD")
        }
    );

    }
    #endregion
    #region Interstial
    public void ShowInterstitial()
    {
        if (PlayerPrefs.GetInt("RemoveAds") == 1) return;
        if (MaxSdk.IsInterstitialReady(InterstitialadUnitId)&& canShowIAD && PlayerPrefs.GetInt("LevelExceeds")==1)
        {
            canShowIAD = false;
            Invoke(nameof(EnableIADShowing), firebaseManager.counter);
            MaxSdk.ShowInterstitial(InterstitialadUnitId);
             

        }
    }
    public void InitializeInterstitialAds()
    {
        // Attach callback
        MaxSdkCallbacks.Interstitial.OnAdLoadedEvent += OnInterstitialLoadedEvent;
        MaxSdkCallbacks.Interstitial.OnAdLoadFailedEvent += OnInterstitialLoadFailedEvent;
        MaxSdkCallbacks.Interstitial.OnAdDisplayedEvent += OnInterstitialDisplayedEvent;
        MaxSdkCallbacks.Interstitial.OnAdClickedEvent += OnInterstitialClickedEvent;
        MaxSdkCallbacks.Interstitial.OnAdHiddenEvent += OnInterstitialHiddenEvent;
        MaxSdkCallbacks.Interstitial.OnAdDisplayFailedEvent += OnInterstitialAdFailedToDisplayEvent;
        MaxSdkCallbacks.Interstitial.OnAdRevenuePaidEvent += OnInterstitialAdRevenuePaidEvent;
        // Load the first interstitial
        LoadInterstitial();
    }

    private void LoadInterstitial()
    {
        MaxSdk.LoadInterstitial(InterstitialadUnitId);
    }

    private void OnInterstitialLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad is ready for you to show. MaxSdk.IsInterstitialReady(adUnitId) now returns 'true'
        Log_GA_Event("Max:Interstitial:Loaded");
        GameAnalytics.NewAdEvent(GAAdAction.Loaded, GAAdType.Interstitial, "MAX", InterstitialPlacement);
        // Reset retry attempt
        int levelValue = 0;

        if (FindAnyObjectByType<LevelController>())
        {
            levelValue = LevelController.DisplayLevelNumber + 1;
        }
        var EventParameters = new Dictionary<string, string>()
{
    { "EventName", "InterstitialLoaded" },
    { "AdNetwork", "MAX" },
    { "AdUnitId", adUnitId },
    { "Revenue", adInfo.Revenue.ToString() },
    { "Platform", GetPlatformString() },
    {"LevelNumber",levelValue.ToString() }
};

       // ByteBrew.NewCustomEvent("Ads", EventParameters);
        retryAttempt_Inter = 0;
    }

    private void OnInterstitialLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo)
    {
        int levelValue = 0;

        if (FindAnyObjectByType<LevelController>())
        {
            levelValue = LevelController.DisplayLevelNumber + 1;
        }
        // Interstitial ad failed to load
        // AppLovin recommends that you retry with exponentially higher delays, up to a maximum delay (in this case 64 seconds)
        Log_GA_Event("Max:Interstitial:Failed");
        GameAnalytics.NewAdEvent(GAAdAction.FailedShow, GAAdType.Interstitial, "MAX", InterstitialPlacement);
        retryAttempt_Inter++;
        double retryDelay = Math.Pow(2, Math.Min(6, retryAttempt_Inter));
        var EventParameters = new Dictionary<string, string>()
{
    { "EventName", "InterstitialFailed" },
    { "AdNetwork", "MAX" },
    { "AdUnitId", adUnitId },
    { "ErrorMessage", errorInfo.Message },
    { "Platform", GetPlatformString() },
    {"LevelNumber",levelValue.ToString() }
};

       // ByteBrew.NewCustomEvent("Ads", EventParameters);
        Invoke("LoadInterstitial", (float)retryDelay);
    }

    private void OnInterstitialDisplayedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        int levelValue = 0;

        if (FindAnyObjectByType<LevelController>())
        {
            levelValue = LevelController.DisplayLevelNumber + 1;
        }
        Log_GA_Event("Max:Interstitial:Shown");
        GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.Interstitial, "MAX", InterstitialPlacement);
        SolarEngineObject.instance.TrackSolarEngineAdImpression("Interstitial", adUnitId, adInfo.Revenue, GetPlatformString());
        // ByteBrew.TrackAdEvent(ByteBrewAdTypes.Interstitial, "applovin", adUnitId, adInfo.Revenue);
        var EventParameters = new Dictionary<string, string>()
{
    { "EventName", "InterstitialDisplayed" },
    { "AdNetwork", "MAX" },
    { "AdUnitId", adUnitId },
    { "Revenue", adInfo.Revenue.ToString() },
    { "Platform", GetPlatformString() },
    {"LevelNumber",levelValue.ToString() }
};

      //  ByteBrew.NewCustomEvent("Ads", EventParameters);

    }

    private void OnInterstitialAdFailedToDisplayEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo, MaxSdkBase.AdInfo adInfo)
    {
        // Interstitial ad failed to display. AppLovin recommends that you load the next ad.
        LoadInterstitial();
    }
    private void OnInterstitialAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        Firebase.Analytics.FirebaseAnalytics.LogEvent(
       "ad_impression",
       new Firebase.Analytics.Parameter[] {
            new Firebase.Analytics.Parameter("ad_platform", GetPlatformString()),
            new Firebase.Analytics.Parameter("ad_source", adInfo.NetworkName),
            new Firebase.Analytics.Parameter("AdUnitId", adUnitId),
            new Firebase.Analytics.Parameter("ad_format", adInfo.AdFormat),
            new Firebase.Analytics.Parameter("value", adInfo.Revenue.ToString()),
            new Firebase.Analytics.Parameter("currency", "USD")
       }
   );
    }
    private void OnInterstitialClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) {
        int levelValue = 0;

        if (FindAnyObjectByType<LevelController>())
        {
            levelValue = LevelController.DisplayLevelNumber + 1;
        }
        var EventParameters = new Dictionary<string, string>()
{
    { "EventName", "InterstitialClicked" },
    { "AdNetwork", "MAX" },
    { "AdUnitId", adUnitId },
    { "Revenue", adInfo.Revenue.ToString() },
    { "Platform", GetPlatformString() },
    {"LevelNumber",levelValue.ToString() }
};

       // ByteBrew.NewCustomEvent("Ads", EventParameters);
    }

    private void OnInterstitialHiddenEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        int levelValue = 0;

        if (FindAnyObjectByType<LevelController>())
        {
            levelValue = LevelController.DisplayLevelNumber + 1;
        }
        // Interstitial ad is hidden. Pre-load the next ad.
        var EventParameters = new Dictionary<string, string>()
{
    { "EventName", "InterstitialHidden" },
    { "AdNetwork", "MAX" },
    { "AdUnitId", adUnitId },
    { "Revenue", adInfo.Revenue.ToString() },
    { "Platform", GetPlatformString() },
    {"LevelNumber",levelValue.ToString() }
};

      //  ByteBrew.NewCustomEvent("Ads", EventParameters);
        LoadInterstitial();
    }
    #endregion
    #region Banner
    public void ShowBanner()
    {
        if (PlayerPrefs.GetInt("RemoveAds") == 1) return;
        MaxSdk.ShowBanner(bannerAdUnitId);
       // GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.Banner, "MAX", BannerPlacement);
        Log_GA_Event("Max:Banner:Shown");
    }
    public void HideBanner()
    {
        MaxSdk.HideBanner(bannerAdUnitId);
    }
    public void InitializeBannerAds()
    {
        // Banners are automatically sized to 320×50 on phones and 728×90 on tablets
        // You may call the utility method MaxSdkUtils.isTablet() to help with view sizing adjustments
        MaxSdk.CreateBanner(bannerAdUnitId, MaxSdkBase.BannerPosition.BottomCenter);

        // Set background color for banners to be fully functional
        MaxSdk.SetBannerBackgroundColor(bannerAdUnitId, UnityEngine.Color.clear);

        MaxSdkCallbacks.Banner.OnAdLoadedEvent += OnBannerAdLoadedEvent;
        MaxSdkCallbacks.Banner.OnAdLoadFailedEvent += OnBannerAdLoadFailedEvent;
        MaxSdkCallbacks.Banner.OnAdClickedEvent += OnBannerAdClickedEvent;
        MaxSdkCallbacks.Banner.OnAdRevenuePaidEvent += OnBannerAdRevenuePaidEvent;
        MaxSdkCallbacks.Banner.OnAdExpandedEvent += OnBannerAdExpandedEvent;
        MaxSdkCallbacks.Banner.OnAdCollapsedEvent += OnBannerAdCollapsedEvent;
    }

    private void OnBannerAdLoadedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) {
        Log_GA_Event("Max:Banner:Loaded");
        GameAnalytics.NewAdEvent(GAAdAction.Loaded, GAAdType.Banner, "MAX", BannerPlacement);
        int levelValue = 0;

        if (FindAnyObjectByType<LevelController>())
        {
            levelValue = LevelController.DisplayLevelNumber + 1;
        }
        var EventParameters = new Dictionary<string, string>()
{
    { "EventName", "BannerLoaded" },
    { "AdNetwork", "MAX" },
    { "AdUnitId", adUnitId },
    { "Revenue", adInfo.Revenue.ToString() },
    { "Platform", GetPlatformString() },
    {"LevelNumber",levelValue.ToString() }
};

       // ByteBrew.NewCustomEvent("Ads", EventParameters);
    }

    private void OnBannerAdLoadFailedEvent(string adUnitId, MaxSdkBase.ErrorInfo errorInfo) {
        Log_GA_Event("Max:Banner:Failed");
        int levelValue = 0;

        if (FindAnyObjectByType<LevelController>())
        {
            levelValue = LevelController.DisplayLevelNumber + 1;
        }
        GameAnalytics.NewAdEvent(GAAdAction.FailedShow, GAAdType.Banner, "MAX", BannerPlacement);
        var EventParameters = new Dictionary<string, string>()
{
    { "EventName", "BannerFailed" },
    { "AdNetwork", "MAX" },
    { "AdUnitId", adUnitId },
    { "ErrorMessage", errorInfo.Message },
    { "Platform", GetPlatformString() },
    {"LevelNumber",levelValue.ToString() }
};

       // ByteBrew.NewCustomEvent("Ads", EventParameters);
        InitializeBannerAds();
    }

    private void OnBannerAdClickedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) {
        int levelValue = 0;

        if (FindAnyObjectByType<LevelController>())
        {
            levelValue = LevelController.DisplayLevelNumber + 1;
        }
        var EventParameters = new Dictionary<string, string>()
{
    { "EventName", "BannerClicked" },
    { "AdNetwork", "MAX" },
    { "AdUnitId", adUnitId },
    { "Revenue", adInfo.Revenue.ToString() },
    { "Platform", GetPlatformString() },
    {"LevelNumber",levelValue.ToString() }
};

       // ByteBrew.NewCustomEvent("Ads", EventParameters);
    }

    private void OnBannerAdRevenuePaidEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) 
    {
        int levelValue = 0;

        if (FindAnyObjectByType<LevelController>())
        {
            levelValue = LevelController.DisplayLevelNumber + 1;
        }
        GameAnalytics.NewAdEvent(GAAdAction.Show, GAAdType.Banner, "MAX", BannerPlacement);
        SolarEngineObject.instance.TrackSolarEngineAdImpression("Banner", adUnitId, adInfo.Revenue, GetPlatformString());
        // ByteBrew.TrackAdEvent(ByteBrewAdTypes.Banner, "applovin", adUnitId, adInfo.Revenue);
        var EventParameters = new Dictionary<string, string>()
{
    { "EventName", "BannerDisplayed" },
    { "AdNetwork", "MAX" },
    { "AdUnitId", adUnitId },
    { "Revenue", adInfo.Revenue.ToString() },
    { "Platform", GetPlatformString() },
    {"LevelNumber",levelValue.ToString() }
};

        //  ByteBrew.NewCustomEvent("Ads", EventParameters);
        Firebase.Analytics.FirebaseAnalytics.LogEvent(
       "ad_impression",
       new Firebase.Analytics.Parameter[] {
            new Firebase.Analytics.Parameter("ad_platform", GetPlatformString()),
            new Firebase.Analytics.Parameter("ad_source", adInfo.NetworkName),
            new Firebase.Analytics.Parameter("AdUnitId", adUnitId),
            new Firebase.Analytics.Parameter("ad_format", adInfo.AdFormat),
            new Firebase.Analytics.Parameter("value", adInfo.Revenue.ToString()),
            new Firebase.Analytics.Parameter("currency", "USD")
       }
   );
    }

    private void OnBannerAdExpandedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) {
    
    
    }

    private void OnBannerAdCollapsedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo) { }
    #endregion
    #region AppOpen
    public void OnAppOpenDismissedEvent(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        MaxSdk.LoadAppOpenAd(AppOpenAdUnitId);
    }

    private void OnApplicationPause(bool pauseStatus)
    {
        if (!pauseStatus)
        {
            ShowAppOpen();
        }
    }
    public void ShowAppOpen()
    {
        ShowInterstitial();
       *//* if (MaxSdk.IsAppOpenAdReady(AppOpenAdUnitId))
        {
            MaxSdk.ShowAppOpenAd(AppOpenAdUnitId);
        }
        else
        {
            MaxSdk.LoadAppOpenAd(AppOpenAdUnitId);
        }*//*
    }
   public void InitializeAppOpenEvents()
    {
        MaxSdkCallbacks.AppOpen.OnAdDisplayedEvent += AppOpenShown;
        MaxSdkCallbacks.AppOpen.OnAdLoadedEvent += AppOpenLoaded;
        MaxSdkCallbacks.AppOpen.OnAdLoadFailedEvent += (string adUnitId, MaxSdkBase.ErrorInfo error) =>
        {
            Debug.LogWarning($"Failed to load App Open Ad: {error.Code} - {error.Message}");
            Log_GA_Event("Max:AppOpen:Failed");
        };
        MaxSdkCallbacks.AppOpen.OnAdHiddenEvent += AppOpenClosed;
    }
    void AppOpenShown(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        Log_GA_Event("Max:AppOpen:Shown");
    }
    void AppOpenLoaded(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        Log_GA_Event("Max:AppOpen:Loaded");
    }
    void AppOpenClosed(string adUnitId, MaxSdkBase.AdInfo adInfo)
    {
        Log_GA_Event("Max:AppOpen:Closed");
    }
    #endregion
    public void LevelComplete(string s,string l)
    {
        CustomAnalytics.GameWin(s,l);
    }
    public void LevelFailed(string s,string l)
    {
        CustomAnalytics.GameLoose(s,l);
    }
    public void LevelStart(string s,string l)
    {
        CustomAnalytics.GameStart(s,l);
    }
    public void LogEvent_NonRevenue(string s)
    {
        CustomAnalytics.LogEvent_NonRevenue(s);
    }
    public void CheckLevelThreshold()
    {
        if(FindAnyObjectByType<LevelController>())
        if (LevelController.DisplayLevelNumber+1 >= firebaseManager.levelThreshold)
        {
                PlayerPrefs.SetInt("LevelExceeds", 1);
                Debug.Log("called");
            
        }
    }*/
}
