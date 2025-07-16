using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;
public class GameAnalyticsManager : MonoBehaviour
{
    public void InitializeSDK()
    {
        GameAnalytics.Initialize();
        GameAnalyticsILRD.SubscribeMaxImpressions();
    }
    public void LogDesignEvent(string value)
    {
        GameAnalytics.NewDesignEvent(value, 0);
    }
}
