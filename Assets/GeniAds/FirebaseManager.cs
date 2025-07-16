using System;
using System.Collections.Generic;
using UnityEngine;
using Firebase;
using Firebase.RemoteConfig;
using Firebase.Analytics;
using Firebase.Extensions;

public class FirebaseManager : MonoBehaviour
{
    private bool isFirebaseInitialized = false;
    public int counter; // Counter value fetched from Firebase
    public int levelThreshold; // Level threshold fetched from Firebase
    public bool isCountingDown = false;

    public void InitializeSDK()
    {
        FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
        {
            if (task.Result == DependencyStatus.Available)
            {
                isFirebaseInitialized = true;
                Debug.Log("Firebase initialized successfully.");
                SetDefaultRemoteConfigValues();
                FetchAndActivateRemoteConfig();
            }
            else
            {
                Debug.LogError($"Could not resolve all Firebase dependencies: {task.Result}");
            }
        });
    }
    #region Analytics
    public void LogNonRevenueEvent(string message)
    {
        FirebaseAnalytics.LogEvent(message);
    }
    #endregion
    #region Remote Config
    private void SetDefaultRemoteConfigValues()
    {
        // Default Values
        var defaults = new Dictionary<string, object>
        {

            { "ad_timer", 30 },
            { "level_threshold", 5 }
        };
        FirebaseRemoteConfig.DefaultInstance.SetDefaultsAsync(defaults).ContinueWithOnMainThread(task =>
        {
            if (task.IsCompleted)
            {
                Debug.Log("Default Remote Config values set.");
            }
            else
            {
                Debug.LogError("Failed to set default Remote Config values.");
            }
        });
    }

    private void FetchAndActivateRemoteConfig()
    {
        if (!isFirebaseInitialized)
        {
            Debug.LogError("Firebase is not initialized.");
            return;
        }

        FirebaseRemoteConfig.DefaultInstance.FetchAsync(TimeSpan.Zero).ContinueWithOnMainThread(fetchTask =>
        {
            if (fetchTask.IsCompleted && !fetchTask.IsFaulted && !fetchTask.IsCanceled)
            {
                FirebaseRemoteConfig.DefaultInstance.ActivateAsync().ContinueWithOnMainThread(activateTask =>
                {
                    if (activateTask.IsCompleted && !activateTask.IsFaulted && !activateTask.IsCanceled)
                    {
                        Debug.Log("Remote Config values fetched and activated.");
                        ApplyRemoteConfigValues();
                    }
                    else
                    {
                        Debug.LogError("Failed to activate Remote Config values.");
                    }
                });
            }
            else
            {
                Debug.LogError("Failed to fetch Remote Config values.");
            }
        });
    }

    private void ApplyRemoteConfigValues()
    {

        int fetchedCounter = (int)FirebaseRemoteConfig.DefaultInstance.GetValue("ad_timer").DoubleValue;
        counter = fetchedCounter;
        levelThreshold = (int)FirebaseRemoteConfig.DefaultInstance.GetValue("level_threshold").DoubleValue;

        Debug.Log("Fetched counter from Firebase: " + counter);
        Debug.Log("Fetched level threshold from Firebase: " + levelThreshold);
        // Apply these values in AdsManager
        // AdsManager.instance.RemoteShowBannerAppOpen(bannerValue, appOpenValue, showAdAfter, inventoryReward);
    }
    #endregion
}