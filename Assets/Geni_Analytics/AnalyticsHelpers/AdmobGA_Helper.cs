using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;

public class AdmobGA_Helper : MonoBehaviour
{
    public static void GA_Log(AdmobGAEvents log)
    {
        switch (log)
        {
            //Initalizing
            case AdmobGAEvents.Initializing:
                LogGAEvent("Admob:Initializing");
                break;
            case AdmobGAEvents.Initialized:
                LogGAEvent("Admob:Initialized");
                break;

            //Request
            case AdmobGAEvents.LoadInterstitialAd:
                LogGAEvent("Admob:iAd:Request");
                break;
            case AdmobGAEvents.LoadOnLoadingInterstitialAd:
                LogGAEvent("Admob:iAdOnLoading:Request");
                break;
            case AdmobGAEvents.LoadOnBoosterInterstitialAd:
                LogGAEvent("Admob:iAdOnBooster:Request");
                break;
            case AdmobGAEvents.LoadVideoAd:
                LogGAEvent("Admob:vAd:Request");
                break;
            case AdmobGAEvents.LoadRewardedAd:
                LogGAEvent("Admob:rAd:Request");
                break;
            case AdmobGAEvents.FootStepLoadRewardedAd:
                LogGAEvent("Admob:FootSteprAd:Request");
                break;
            case AdmobGAEvents.AppOpenAd_Load:
                LogGAEvent("Admob:AppOpenAd:Request");
                break;
            case AdmobGAEvents.LoadPullThePinInterstitialAd:
                LogGAEvent("Admob:iAdPullThePin:Request");
                break;
            case AdmobGAEvents.LoadPullThePinRewardedAd:
                LogGAEvent("Admob:rAdPullThePin:Request");
                break;
            case AdmobGAEvents.LoadInterstitialPortraitAd:
                LogGAEvent("Admob:iAdPortrait:Request");
                break;
            case AdmobGAEvents.LoadRewardedPortraitAd:
                LogGAEvent("Admob:rAdPortrait:Request");
                break;

            //LOADED
            case AdmobGAEvents.InterstitialAdLoaded:
                LogGAEvent("Admob:iAd:Loaded");
                break;
            case AdmobGAEvents.OnLoadingInterstitialAdLoaded:
                LogGAEvent("Admob:iAdOnLoading:Loaded");
                break;
            case AdmobGAEvents.OnBoosterInterstitialAdLoaded:
                LogGAEvent("Admob:iAdOnBooster:Loaded");
                break;
                
            case AdmobGAEvents.VideoAdLoaded:
                LogGAEvent("Admob:vAd:Loaded");
                break;
            case AdmobGAEvents.RewardedAdLoaded:
                LogGAEvent("Admob:rAd:Loaded");
                break;
            case AdmobGAEvents.FootStepRewardedAdLoaded:
                LogGAEvent("Admob:FootSteprAd:Loaded");
                break;
            case AdmobGAEvents.AppOpenAd_Loaded:
                LogGAEvent("Admob:AppOpenAd:Loaded");
                break;
            case AdmobGAEvents.PullThePinInterstitialAdLoaded:
                LogGAEvent("Admob:iAdPullThePin:Loaded");
                break;
            case AdmobGAEvents.PullThePinRewardedAdLoaded:
                LogGAEvent("Admob:rAdPullThePin:Loaded");
                break;
            case AdmobGAEvents.InterstitialPortraitAdLoaded:
                LogGAEvent("Admob:iAdPortrait:Loaded");
                break;
            case AdmobGAEvents.RewardedPortraitAdLoaded:
                LogGAEvent("Admob:rAdPortrait:Loaded");
                break;

            //Show Call
            case AdmobGAEvents.ShowInterstitialAd:
                LogGAEvent("Admob:iAd:ShowCall");
                break;
            case AdmobGAEvents.ShowOnLoadingInterstitialAd:
                LogGAEvent("Admob:iAdOnLoading:ShowCall");
                break;
            case AdmobGAEvents.ShowOnBoosterInterstitialAd:
                LogGAEvent("Admob:iAdOnBooster:ShowCall");
                break;
                
            case AdmobGAEvents.ShowVideoAd:
                LogGAEvent("Admob:vAd:ShowCall");
                break;
            case AdmobGAEvents.ShowRewardedAd:
                LogGAEvent("Admob:rAd:ShowCall");
                break;
            case AdmobGAEvents.FootStepShowRewardedAd:
                LogGAEvent("Admob:FootSteprAd:ShowCall");
                break;
            case AdmobGAEvents.AppOpenAd_Show:
                LogGAEvent("Admob:AppOpenAd:ShowCall");
                break;
            case AdmobGAEvents.ShowPullThePinInterstitialAd:
                LogGAEvent("Admob:iAdPullThePin:ShowCall");
                break;
            case AdmobGAEvents.ShowPullTheRewardedAd:
                LogGAEvent("Admob:rAdPullThePin:ShowCall");
                break;
            case AdmobGAEvents.ShowInterstitialPortraitAd:
                LogGAEvent("Admob:iAdPotrait:ShowCall");
                break;
            case AdmobGAEvents.ShowRewardedPortraitAd:
                LogGAEvent("Admob:rAdPotrait:ShowCall");
                break;

            //Will Display
            case AdmobGAEvents.InterstitialAdWillDisplay:
                LogGAEvent("Admob:iAd:WillDisplay");
                break;
            case AdmobGAEvents.OnLoadingInterstitialAdWillDisplay:
                LogGAEvent("Admob:iAdOnLoading:WillDisplay");
                break;
            case AdmobGAEvents.OnBoosterInterstitialAdWillDisplay:
                LogGAEvent("Admob:iAdOnBooster:WillDisplay");
                break;
                
            case AdmobGAEvents.VideoAdWillDisplay:
                LogGAEvent("Admob:vAd:WillDisplay");
                break;
            case AdmobGAEvents.RewardedAdWillDisplay:
                LogGAEvent("Admob:rAd:WillDisplay");
                break;
            case AdmobGAEvents.FootStepRewardedAdWillDisplay:
                LogGAEvent("Admob:FootSteprAd:WillDisplay");
                break;
            case AdmobGAEvents.AppOpenAd_WillDisplay:
                LogGAEvent("Admob:AppOpenAd:WillDisplay");
                break;
            case AdmobGAEvents.PullThePinInterstitialAdWillDisplay:
                LogGAEvent("Admob:iAdPullThePin:WillDisplay");
                break;
            case AdmobGAEvents.PullThePinRewardedAdWillDisplay:
                LogGAEvent("Admob:rAdPullThePin:WillDisplay");
                break;
            case AdmobGAEvents.InterstitialPortraitAdWillDisplay:
                LogGAEvent("Admob:iAdPortrait:WillDisplay");
                break;
            case AdmobGAEvents.RewardedPortraitAdWillDisplay:
                LogGAEvent("Admob:rAdPortrait:WillDisplay");
                break;

            //Displayed
            case AdmobGAEvents.InterstitialAdDisplayed:
                LogGAEvent("Admob:iAd:Displayed");
                break;
            case AdmobGAEvents.OnLoadingInterstitialAdDisplayed:
                LogGAEvent("Admob:iAdOnLoading:Displayed");
                break;
            case AdmobGAEvents.OnBoosterInterstitialAdDisplayed:
                LogGAEvent("Admob:iAdOnBooster:Displayed");
                break;
                
            case AdmobGAEvents.VideoAdDisplayed:
                LogGAEvent("Admob:vAd:Displayed");
                break;
            case AdmobGAEvents.RewardedAdDisplayed:
                LogGAEvent("Admob:rAd:Displayed");
                break;
            case AdmobGAEvents.FootStepRewardedAdDisplayed:
                LogGAEvent("Admob:FootSteprAd:Displayed");
                break;
            case AdmobGAEvents.AppOpenAd_DidDisplay:
                LogGAEvent("Admob:AppOpenAd:Displayed");
                break;
            case AdmobGAEvents.AppOpenAd_DidRecordImpression:
                LogGAEvent("Admob:AppOpenAd:DidRecordImpression");
                break;
            case AdmobGAEvents.PullThePinInterstitialAdDisplayed:
                LogGAEvent("Admob:iAdPullThePin:Displayed");
                break;
            case AdmobGAEvents.PullThePinRewardedAdDisplayed:
                LogGAEvent("Admob:rAdPullThePin:Displayed");
                break;
            case AdmobGAEvents.InterstitialPortraitAdDisplayed:
                LogGAEvent("Admob:iAdPortrait:Displayed");
                break;
            case AdmobGAEvents.RewardedPortraitAdDisplayed:
                LogGAEvent("Admob:rAdPortrait:Displayed");
                break;

            //Rewarded Ad Started
            case AdmobGAEvents.RewardedAdStarted:
                LogGAEvent("Admob:rAd:Started");
                break;
            case AdmobGAEvents.RewardedPortraitAdStarted:
                LogGAEvent("Admob:rAdPortrait:Started");
                break;

            //Rewarded Ad Give Reward
            case AdmobGAEvents.RewardedAdReward:
                LogGAEvent("Admob:rAd:Reward");
                break;
            case AdmobGAEvents.FootStepRewardedAdReward:
                LogGAEvent("Admob:FootSteprAd:Reward");
                break;
            case AdmobGAEvents.PullThePinRewardedAdReward:
                LogGAEvent("Admob:rAdPullThePin:Reward");
                break;
            case AdmobGAEvents.RewardedPortraitAdReward:
                LogGAEvent("Admob:rAdPortrait:Reward");
                break;

            //No Inventory
            case AdmobGAEvents.RewardedAdNoInventory:
                LogGAEvent("Admob:rAd:NoInventory");
                break;
            case AdmobGAEvents.FootStepRewardedAdNoInventory:
                LogGAEvent("Admob:FootSteprAd:NoInventory");
                break;
            case AdmobGAEvents.InterstitialAdNoInventory:
                LogGAEvent("Admob:iAd:NoInventory");
                break;
            case AdmobGAEvents.OnLoadingInterstitialAdNoInventory:
                LogGAEvent("Admob:iAdOnLoading:NoInventory");
                break;
            case AdmobGAEvents.OnBoosterInterstitialAdNoInventory:
                LogGAEvent("Admob:iAdOnBooster:NoInventory");
                break;
                
            case AdmobGAEvents.VideoAdNoInventory:
                LogGAEvent("Admob:vAd:NoInventory");
                break;
            case AdmobGAEvents.AppOpenAd_NoInventory:
                LogGAEvent("Admob:AppOpenAd:NoInventory");
                break;
            case AdmobGAEvents.PullThePinInterstitialAdNoInventory:
                LogGAEvent("Admob:iAdPullThePin:NoInventory");
                break;
            case AdmobGAEvents.PullThePinRewardedAdNoInventory:
                LogGAEvent("Admob:rAdPullThePin:NoInventory");
                break;
            case AdmobGAEvents.InterstitialPortraitAdNoInventory:
                LogGAEvent("Admob:iAdPortrait:NoInventory");
                break;
            case AdmobGAEvents.RewardedPortraitAdNoInventory:
                LogGAEvent("Admob:rAdPortrait:NoInventory");
                break;

            //Ad Close
            case AdmobGAEvents.InterstitialAdClosed:
                LogGAEvent("Admob:iAd:Closed");
                break;
            case AdmobGAEvents.OnLoadingInterstitialAdClosed:
                LogGAEvent("Admob:iAdOnLoading:Closed");
                break;
            case AdmobGAEvents.OnBoosterInterstitialAdClosed:
                LogGAEvent("Admob:iAdOnBooster:Closed");
                break;
                
            case AdmobGAEvents.VideoAdClosed:
                LogGAEvent("Admob:vAd:Closed");
                break;
            case AdmobGAEvents.RewardedAdClosed:
                LogGAEvent("Admob:rAd:Closed");
                break;
            case AdmobGAEvents.FootStepRewardedAdClosed:
                LogGAEvent("Admob:FootSteprAd:Closed");
                break;
            case AdmobGAEvents.AppOpenAd_Closed:
                LogGAEvent("Admob:AppOpenAd:Closed");
                break;
            case AdmobGAEvents.PullThePinInterstitialAdClosed:
                LogGAEvent("Admob:iAdPullThePin:Closed");
                break;
            case AdmobGAEvents.PullThePinRewardedAdClosed:
                LogGAEvent("Admob:rAdPullThePin:Closed");
                break;
            case AdmobGAEvents.InterstitialPortraitAdClosed:
                LogGAEvent("Admob:iAdPortrait:Closed");
                break;
            case AdmobGAEvents.RewardedPortraitAdClosed:
                LogGAEvent("Admob:rAdPortrait:Closed");
                break;

            //Ad Clicked
            case AdmobGAEvents.InterstitialAdClicked:
                LogGAEvent("Admob:iAd:Clicked");
                break;
            case AdmobGAEvents.OnLoadingInterstitialAdClicked:
                LogGAEvent("Admob:iAdOnLoading:Clicked");
                break;
            case AdmobGAEvents.OnBoosterInterstitialAdClicked:
                LogGAEvent("Admob:iAdOnBooster:Clicked");
                break;
                
            case AdmobGAEvents.VideoAdClicked:
                LogGAEvent("Admob:vAd:Clicked");
                break;
            case AdmobGAEvents.RewardedAdClicked:
                LogGAEvent("Admob:rAd:Clicked");
                break;
            case AdmobGAEvents.FootStepRewardedAdClicked:
                LogGAEvent("Admob:FootSteprAd:Clicked");
                break;
            case AdmobGAEvents.AppOpenAd_Clicked:
                LogGAEvent("Admob:AppOpenAd:Clicked");
                break;
            case AdmobGAEvents.PullThePinInterstitialAdClicked:
                LogGAEvent("Admob:iAdPullThePin:Clicked");
                break;
            case AdmobGAEvents.PullThePinRewardedAdClicked:
                LogGAEvent("Admob:rAdPullThePin:Clicked");
                break;
            case AdmobGAEvents.InterstitialPortraitAdClicked:
                LogGAEvent("Admob:iAdPortrait:Clicked");
                break;
            case AdmobGAEvents.RewardedPortraitAdClicked:
                LogGAEvent("Admob:rAdPortrait:Clicked");
                break;

            //Adapters Register
            case AdmobGAEvents.AdaptersInitialized:
                LogGAEvent("Admob:Adapters:Initialized");
                break;

            //Adapters Not Register
            case AdmobGAEvents.AdaptersNotInitialized:
                LogGAEvent("Admob:Adapters:NotInitialized");
                break;

            //Paid Event
            case AdmobGAEvents.RewardedAdPaidEvent:
                LogGAEvent("Admob:rAd:PaidEvent");
                break;
            case AdmobGAEvents.FootStepRewardedAdPaidEvent:
                LogGAEvent("Admob:FootSteprAd:PaidEvent");
                break;

            case AdmobGAEvents.InterstitialAdPaidEvent:
                LogGAEvent("Admob:iAd:PaidEvent");
                break;

            case AdmobGAEvents.LoadingInterstitialAdPaidEvent:
                LogGAEvent("Admob:iAdOnLoading:LoadingPaidEvent");
                break;
            case AdmobGAEvents.AppOpenAd_PaidEvent:
                LogGAEvent("Admob:AppOpenAd:PaidEvent");
                break;
            case AdmobGAEvents.PullThePinInterstitialAdPaidEvent:
                LogGAEvent("Admob:iAdPullThePin:PaidEvent");
                break;
            case AdmobGAEvents.PullThePinRewardedAdPaidEvent:
                LogGAEvent("Admob:rAdPullThePin:PaidEvent");
                break;
            case AdmobGAEvents.InterstitialPortraitAdPaidEvent:
                LogGAEvent("Admob:iAdPortrait:PaidEvent");
                break;
            case AdmobGAEvents.RewardedPortraitAdPaidEvent:
                LogGAEvent("Admob:rAdPortrait:PaidEvent");
                break;

            //FailedToShow
            case AdmobGAEvents.AppOpenAd_FailToShow:
                LogGAEvent("Admob:AppOpenAd:FailToShow");
                break;


            #region Banner

            case AdmobGAEvents.BannerAdLoaded:
                LogGAEvent("Admob:Banner:Loaded");
                break;

            case AdmobGAEvents.BannerAdNoInventory:
                LogGAEvent("Admob:Banner:NoInventory");
                break;

            case AdmobGAEvents.BannerAdClicked:
                LogGAEvent("Admob:Banner:Clicked");
                break;

            case AdmobGAEvents.BannerAdClosed:
                LogGAEvent("Admob:Banner:Closed");
                break;

            case AdmobGAEvents.LoadBannerAd:
                LogGAEvent("Admob:Banner:Request");
                break;

            #endregion


        }
    }

    public static void LogGAEvent(string log)
    {
        //Constant.LogDesignEvent(log);
        GameAnalytics.NewDesignEvent(log);
    }
}
