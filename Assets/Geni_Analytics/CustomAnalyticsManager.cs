using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;
// ByteBrewSDK;
using Watermelon.BusStop;
public class CustomAnalyticsManager : AbstractSingleton<CustomAnalyticsManager>
{
    [SerializeField] GameAnalyticsManager gaManager;
    //[SerializeField] SingularManager singularManager;
    [SerializeField] FirebaseManager fbManager;
    public SolarEngineManager solarManager;
    protected override void Awake()
    {
        base.Awake();
        fbManager.InitializeSDK();
/*
        try
        {
            ByteBrew.InitializeByteBrew();
            Debug.Log("ByteBrew initialization started.");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("ByteBrew failed to initialize: " + ex.Message);
        }*/
        try
        {
            gaManager.InitializeSDK();
            Debug.Log("GameAnalytics initialization started.");
        }
        catch (System.Exception ex)
        {
            Debug.LogError("GameAnalytics failed to initialize: " + ex.Message);
        }
        //Debug.LogError("ByteBrew " + ByteBrew.IsByteBrewInitialized());
        //gaManager.InitializeSDK();
        //solarManager.InitSE();
        DontDestroyOnLoad(this.gameObject);
    }
    private void Start()
    {
        Invoke(nameof(StartSession), 3);
    }
    //Session

    public void StartSession()
    {
        GameAnalytics.NewDesignEvent("session_start");
        Debug.Log("GA Design Event: session_start");
    }

    public void GameStart(string Env,string Lvl)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, Env,Lvl);
        Debug.Log("LevelStart"+ Env+ Lvl);
       // ByteBrew.NewProgressionEvent(ByteBrewProgressionTypes.Started,"TrainStation", value);
        // singularManager.LogNonRevenueEvent(value);
        // fbManager.LogNonRevenueEvent(value);
        //  solarManager.CustomEvent(value);
    }
    public void GameWin(string Env, string Lvl)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, Env,Lvl);
        Debug.Log("LevelWin" + Env + Lvl);
        // ByteBrew.NewProgressionEvent(ByteBrewProgressionTypes.Completed, "TrainStation", value);
        // singularManager.LogNonRevenueEvent(value);
        //  fbManager.LogNonRevenueEvent(value);
        //  solarManager.CustomEvent(value);
    }
    public void GameLoose(string Env, string Lvl)
    {
        GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, Env, Lvl);
        Debug.Log("LevelLoose" + Env + Lvl);
        //ByteBrew.OnLevelFail(value);
        // ByteBrew.NewProgressionEvent(ByteBrewProgressionTypes.Failed, "TrainStation", value);
        // singularManager.LogNonRevenueEvent(value);
        //  fbManager.LogNonRevenueEvent(value);
        //  solarManager.CustomEvent(value);
    }
    public void GameRevive(string value)
    {
        LogEvent_NonRevenue(value);
    }
    public void LogEvent_NonRevenue(string value)
    {
        Debug.Log(value);
       // gaManager.LogDesignEvent(value);
        // singularManager.LogNonRevenueEvent(value);
      //  solarManager.CustomEvent(value);
        fbManager.LogNonRevenueEvent(value);
    }
  
    public void GADesignEvent(string val)
    {
        gaManager.LogDesignEvent(val);
    }
}


public enum AdLoadingStatus
{
    NotLoaded,
    Loading,
    Loaded,
    NoInventory,
}
