//using GoogleMobileAds.Api;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//public delegate void AdmobRewardUserDelegate(object sender, Reward args);

public enum AdmobGAEvents
{
    Initializing,
    Initialized,

    LoadOnLoadingInterstitialAd,
    OnLoadingInterstitialAdLoaded,
    ShowOnLoadingInterstitialAd,
    OnLoadingInterstitialAdWillDisplay,
    OnLoadingInterstitialAdDisplayed,
    OnLoadingInterstitialAdNoInventory,
    OnLoadingInterstitialAdClicked,
    OnLoadingInterstitialAdClosed,

    #region PullThePinEvents
    //PullThePin All Events
    LoadPullThePinInterstitialAd,
    PullThePinInterstitialAdLoaded,
    ShowPullThePinInterstitialAd,
    PullThePinInterstitialAdWillDisplay,
    PullThePinInterstitialAdDisplayed,
    PullThePinInterstitialAdNoInventory,
    PullThePinInterstitialAdClicked,
    PullThePinInterstitialAdClosed,
    PullThePinInterstitialAdPaidEvent,

    LoadPullThePinRewardedAd,
    PullThePinRewardedAdLoaded,
    ShowPullTheRewardedAd,
    PullThePinRewardedAdWillDisplay,
    PullThePinRewardedAdDisplayed,
    PullThePinRewardedAdNoInventory,
    PullThePinRewardedAdClicked,
    PullThePinRewardedAdClosed,
    PullThePinRewardedAdReward,
    PullThePinRewardedAdPaidEvent,

    #endregion

    #region Booster
    LoadOnBoosterInterstitialAd,
    OnBoosterInterstitialAdLoaded,
    ShowOnBoosterInterstitialAd,
    OnBoosterInterstitialAdWillDisplay,
    OnBoosterInterstitialAdDisplayed,
    OnBoosterInterstitialAdNoInventory,
    OnBoosterInterstitialAdClicked,
    OnBoosterInterstitialAdClosed,
    #endregion

    #region PortriatAds

    //interstital Ad
    LoadInterstitialPortraitAd,
    InterstitialPortraitAdLoaded,
    ShowInterstitialPortraitAd,
    InterstitialPortraitAdWillDisplay,
    InterstitialPortraitAdDisplayed,
    InterstitialPortraitAdNoInventory,
    InterstitialPortraitAdClicked,
    InterstitialPortraitAdClosed,
    InterstitialPortraitAdPaidEvent,
    InterstitialAdFailedToShow,

    //Rewarded Ad
    LoadRewardedPortraitAd,
    RewardedPortraitAdLoaded,
    ShowRewardedPortraitAd,
    RewardedPortraitAdWillDisplay,
    RewardedPortraitAdDisplayed,
    RewardedPortraitAdNoInventory,
    RewardedPortraitAdStarted,
    RewardedPortraitAdReward,
    RewardedPortraitAdClicked,
    RewardedPortraitAdClosed,
    RewardedPortraitAdPaidEvent,
    RewardedAdDidRecordImpression,

    #endregion

    LoadInterstitialAd,
    LoadVideoAd,
    LoadRewardedAd,
    FootStepLoadRewardedAd,

    InterstitialAdLoaded,
    VideoAdLoaded,
    RewardedAdLoaded,
    FootStepRewardedAdLoaded,

    ShowInterstitialAd,
    ShowVideoAd,
    ShowRewardedAd,
    FootStepShowRewardedAd,

    InterstitialAdWillDisplay,
    VideoAdWillDisplay,
    RewardedAdWillDisplay,
    FootStepRewardedAdWillDisplay,

    InterstitialAdDisplayed,
    VideoAdDisplayed,
    RewardedAdDisplayed,
    FootStepRewardedAdDisplayed,

    InterstitialAdNoInventory,
    VideoAdNoInventory,
    RewardedAdNoInventory,
    FootStepRewardedAdNoInventory,

    RewardedAdStarted,

    RewardedAdReward,
    FootStepRewardedAdReward,

    InterstitialAdClicked,
    VideoAdClicked,
    RewardedAdClicked,
    FootStepRewardedAdClicked,

    InterstitialAdClosed,
    VideoAdClosed,
    RewardedAdClosed,
    FootStepRewardedAdClosed,

    AdaptersInitialized,
    AdaptersNotInitialized,

    RewardedAdPaidEvent,
    FootStepRewardedAdPaidEvent,
    InterstitialAdPaidEvent,
    LoadingInterstitialAdPaidEvent,

    #region Banner

    BannerAdLoaded,
    BannerAdClosed,
    BannerAdClicked,
    BannerAdDisplayed,
    BannerAdNoInventory,
    LoadBannerAd,
    BannerViewPaidEvent,


    #endregion

    #region AppOpen

    AppOpenAd_Load,
    AppOpenAd_Show,
    AppOpenAd_Loaded,
    AppOpenAd_WillDisplay,
    AppOpenAd_DidDisplay,
    AppOpenAd_DidRecordImpression,
    AppOpenAd_PaidEvent,
    AppOpenAd_NoInventory,
    AppOpenAd_Closed,
    AppOpenAd_Clicked,
    AppOpenAd_FailToShow,

    #endregion
}
