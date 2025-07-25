using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using Facebook.Unity;
public class FacebookManager : MonoBehaviour
{
    void Awake()
    {
      /*  if (!FB.IsInitialized)
        {
            // Initialize the Facebook SDK
            FB.Init(InitCallback, OnHideUnity);
        }
        else
        {
            // Already initialized, signal an app activation App Event
            FB.ActivateApp();
        }*/
    }

    private void InitCallback()
    {
       /* if (FB.IsInitialized)
        {
            FB.ActivateApp();
        }
        else
        {
            Debug.Log("Failed to Initialize the Facebook SDK");
        }*/
    }

    private void OnHideUnity(bool isGameShown)
    {
        //if (!isGameShown)
        //{
        //    Time.timeScale = 0;
        //}
        //else
        //{
        //    Time.timeScale = 1;
        //}
    }
}