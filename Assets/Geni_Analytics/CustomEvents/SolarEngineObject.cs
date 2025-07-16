using SolarEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using AppLovinMax;

public class SolarEngineObject : MonoBehaviour
{

    public static SolarEngineObject instance;
    
    private void Awake()
    {
        instance = this;
    }

    public bool isSolarEngineInitialized = false;



    private void Start()
    {
        Debug.Log("in START");
        InitSolarSDK();
    }


    public void InitSolarSDK()
    {
        Debug.Log("in initSolarSDK");
        if (!isSolarEngineInitialized)
        {
            Debug.Log("GT >> InitSolarSDK");

            // depends upon your consent status saving flow
            bool isGDPRConsentGiven = false;
            //if (ConsentInformation.ConsentStatus == ConsentStatus.Obtained)// || Userprefs.GDPRConsentStatus == 1)
            //{
            //    isGDPRConsentGiven = true;
            //    Debug.Log("CheckGDPRStatus() is true");
            //}

            SolarEngine.Analytics.preInitSeSdk("178afe5f8085c16d");

            SEConfig seConfig = new SEConfig();

            seConfig.logEnabled = false;
            seConfig.isGDPRArea = true;
//            seConfig.adpersonal
  //          seConfig.adPersonalizationEnabled = true;
    //        seConfig.adUserDataEnabled = true;

            SolarEngine.Analytics.SESDKInitCompletedCallback initCallback = OnSolarInitCallback;
            seConfig.initCompletedCallback = initCallback;

            //SolarEngine.Analytics.initSeSdk(GameKeys.SOLARENGINE_APPKEY, seConfig);
            SolarEngine.Analytics.initSeSdk("178afe5f8085c16d", seConfig);
        }
    }

    private void OnSolarInitCallback(int code)
    {
        Debug.Log("GT >> OnSolarInitCallback  code : " + code + "Recieved call back from solar atleast!"); //watchout for this debug in g
        if (code == 0)
        {
            isSolarEngineInitialized = true;
            Debug.Log("Solar engine inti is sucess");
           
               // Debug.Log(PrefManager.IsFirstGameLaunch + "        PrefManager.IsFirstGameLaunch");
                CustomAttributes customAttributes = new CustomAttributes();
                customAttributes.custom_event_name = "firstAppOpen";

                Dictionary<string, object> dict = new Dictionary<string, object>();
                dict.Add("isInit", "true");

                SolarEngine.Analytics.trackCustom("firstOpen", dict);
                SolarEngine.Analytics.trackFirstEvent(customAttributes);
            
        }
    }

    public void TrackSolarEngineAdImpression(string adType, string adUnitId, double revenue, string platform)
    {
        if (isSolarEngineInitialized)
        {
            SolarEngine.ImpressionAttributes impressionAttributes = new SolarEngine.ImpressionAttributes
            {
                ad_platform = platform,                       // Set to AppLovin or any other platform
                mediation_platform = "applovin",              // Use "applovin" for mediation platform
                ad_id = adUnitId,                             // Use ad unit ID
                ad_type = GetAdFormat(adType),                // Convert the ad type
                ad_ecpm = revenue * 1000,                     // Revenue should be provided in correct units (USD)
                currency_type = "USD",                        // Specify currency type as USD
                is_rendered = true                            // Ad has been rendered successfully
            };

            SolarEngine.Analytics.trackIAI(impressionAttributes);

            Debug.Log("GT >> TrackSolarEngineAdImpression Sent");
            Debug.Log("GT >> ad platform: " + platform);
            Debug.Log("GT >> mediation_platform: applovin");
            Debug.Log("GT >> ad_type: " + impressionAttributes.ad_type);
            Debug.Log("GT >> ad_ecpm: " + impressionAttributes.ad_ecpm);
        }
    }





    private int GetAdFormat(string adType)
    {
        Debug.Log("GT >> adType: " + adType);
        switch (adType)
        {
            case "Rewarded":
                return 1;  // Rewarded Video

            case "AppOpen":
                return 2;  // AppOpen

            case "Interstitial":
                return 3;  // Interstitial

            case "Banner":
                return 8;  // Banner

            default:
                return 0;  // Other
        }
    }





}
