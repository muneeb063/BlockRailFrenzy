using Firebase.Analytics;
//using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(AdsManager))]
public class UnityPaidEvent : MonoBehaviour
{
    AdsManager adsManager;
/*
    private void Start()
    {
        adsManager = GameObject.FindObjectOfType<AdsManager>();
    }

    public void InterstitialAd_OnPaidEvent( AdValue e)
    {
        HandleAdPaidEvent("iAd", e, adsManager._Interstitials._interstitialAd.GetResponseInfo().GetMediationAdapterClassName());
        HandleAdPaidEvent_Sigular(e, adsManager._Interstitials._interstitialAd.GetResponseInfo().GetMediationAdapterClassName());
        AdmobGA_Helper.GA_Log(AdmobGAEvents.InterstitialAdPaidEvent);
    }

    public void Banner_OnPaidEvent(AdValue e)
    {
        HandleAdPaidEvent("bAd", e, adsManager._Banner._bannerView.GetResponseInfo().GetMediationAdapterClassName());
        HandleAdPaidEvent_Sigular(e, adsManager._Banner._bannerView.GetResponseInfo().GetMediationAdapterClassName());
        AdmobGA_Helper.GA_Log(AdmobGAEvents.BannerViewPaidEvent);

    }

    public void RewardedAd_OnPaidEvent(AdValue e)
    {
        HandleAdPaidEvent("rAd", e, adsManager._Rewarded._rewardedAd.GetResponseInfo().GetMediationAdapterClassName());
        HandleAdPaidEvent_Sigular(e, adsManager._Rewarded._rewardedAd.GetResponseInfo().GetMediationAdapterClassName());
        AdmobGA_Helper.GA_Log(AdmobGAEvents.RewardedAdPaidEvent);
    }
    public void AppOpen_OnPaidEvent(AdValue e)
    {
        HandleAdPaidEvent("oAd", e, adsManager._AppOpen._appOpenAd.GetResponseInfo().GetMediationAdapterClassName());
        HandleAdPaidEvent_Sigular(e, adsManager._AppOpen._appOpenAd.GetResponseInfo().GetMediationAdapterClassName());
        AdmobGA_Helper.GA_Log(AdmobGAEvents.AppOpenAd_PaidEvent);
    }
    //Firebase
    public void HandleAdPaidEvent(string adType, AdValue args, string adNetwork)
    {
        AdValue adValue = args;

        var value = adValue.Value;
        var currency = adValue.CurrencyCode;
        var precision = adValue.Precision;
        var adunitid = adType;

        Firebase.Analytics.Parameter[] LTVParameters = {        
        new Firebase.Analytics.Parameter("value", (adValue.Value/1000000.0f)),
        new Firebase.Analytics.Parameter("currency",adValue.CurrencyCode),
        new Firebase.Analytics.Parameter("precision",(int)adValue.Precision),
        new Firebase.Analytics.Parameter("adunitid",adType),
        new Firebase.Analytics.Parameter("network",adNetwork)
        };
        Debug.Log("Value: " + (adValue.Value / 1000000.0f));
        FirebaseAnalytics.LogEvent("paid_ad_impression", LTVParameters);
    }
    //Singular
    private void HandleAdPaidEvent_Sigular(AdValue args, string AdInstance)
    {
        Debug.Log("GT >> Singular:" + AdInstance + ":PaidEvent");
        SingularAdData data = new SingularAdData("Admob", args.CurrencyCode, args.Value / 1000000.0f);
        data.WithNetworkName(AdInstance);
        SingularSDK.AdRevenue(data);
    }*/

}
