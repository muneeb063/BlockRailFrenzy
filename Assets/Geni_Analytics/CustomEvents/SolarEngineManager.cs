using System;
using System.Collections.Generic;
using Firebase.Analytics;
using SolarEngine;
using UnityEngine;

public class SolarEngineManager : MonoBehaviour
{
    String AppKey = "178afe5f8085c16d";
    public void InitSE()
    {
        MuneebBhaiMethod();
        return;
        Debug.Log("Initializing SolarEngine SDK...");

        SEConfig seConfig = new()
        {
            logEnabled = true,
            isDebugModel = true // Enable debug mode
        };

        Analytics.SESDKInitCompletedCallback initCallback = initSuccessCallback;
        seConfig.initCompletedCallback = initCallback;

        SolarEngine.Analytics.preInitSeSdk(AppKey);
        SolarEngine.Analytics.initSeSdk(AppKey, seConfig);

        Debug.Log("SolarEngine SDK initialization called.");
    }
    void MuneebBhaiMethod()
    {
        //Initialization
        SolarEngine.Analytics.preInitSeSdk(AppKey);    //PreInit should be called first.
        SEConfig seConfig = new SEConfig();
        seConfig.logEnabled = true;
        seConfig.initCompletedCallback = onInitCallback;     //Here put initCallback into SEConfig.
        SolarEngine.Analytics.initSeSdk("appkey", seConfig);

    }
    //InitCallback
    private void onInitCallback(int code)
    {
        ///please refer to the codes below
        Debug.LogError("Solar SDK Init Sucess"+code);
    }

    public void initSuccessCallback(int code)
    {
        Debug.LogError("Solar SDK Init Sucess"+ code);
    }

    public void LogLevelStartedEvent(string level)
    {

        // Log the level start event using Solar Engine Analytics
        SolarEngine.Analytics.track(FirebaseAnalytics.ParameterLevel, new Dictionary<string, object>
            {
                { "level", level.ToString() }  // Convert level to string if needed
            });
    }

    public void LogLevelCompleteEvent(string level)
    {
        SolarEngine.Analytics.track(FirebaseAnalytics.EventLevelUp, new Dictionary<string, object>
            {
                { "level", level.ToString() }  // Convert level to string if neededs
            });
    }


    public void LogLevelFailedEvent(string level)
    {

        SolarEngine.Analytics.track(FirebaseAnalytics.EventLevelEnd, new Dictionary<string, object>
            {
                { "level", level.ToString() }  // Convert level to string if needed
            });
    }


    public void CustomEvent(string s)
    {
        SolarEngine.Analytics.track("_custom_Event", new Dictionary<string, object>
        {
            {"custom_event",s}
        });
    }
   
}