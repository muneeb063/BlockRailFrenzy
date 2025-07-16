using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*public enum AdLoadingStatus
{
    NotLoaded,
    Loading,
    Loaded,
    NoInventory,
    
}*/

public abstract class MediationHandler : MonoBehaviour
{
    public static MediationHandler Instance;
    public BasicCallBack onAdCompleted;

    public delegate void RewardUserDelegate();

	public bool isOpenBidding = true;
    public float waitBetweenRewardedNotLoadedRequest = 3f;


	int playIteration = 0 ;
	//Waleed
	public delegate void OnAdShow();
	public static event OnAdShow onAdShow_Event;

	public delegate void OnAdClose();
	public static event OnAdClose onAdClose_Event;
	public string DefaultAndroidCPURL = "";
	public string DefaultIOSCPURL = "";


	public abstract bool IsInterstitialAdReady ();
	public abstract bool IsVideoAdReady ();
	public abstract bool IsRewardedAdReady ();

	public abstract void LoadInterstitial ();
	public abstract void LoadVideo();
	public abstract void LoadRewardedVideo ();
	public delegate void OnAdsInitialized();
	public static event OnAdsInitialized onAdsInitialized_Event;

	public abstract void LoadBannerAd();
	public abstract void HideBannerAd();

	public abstract void RemoteShowBannerAppOpen(bool banner, bool appopen);

    public abstract void ShowInterstitial ();
	public abstract void ShowVideo();
    public abstract void ShowRewardedVideo(Action _delegate, Action onAdShown, Action onRewardNotGiven, string Placement, bool shouldShowRuntimeLoading = true);

    protected AdLoadingStatus iAdStatus = AdLoadingStatus.NotLoaded , rAdStatus = AdLoadingStatus.NotLoaded,
		bAdStatus = AdLoadingStatus.NotLoaded;
    //    public abstract void ShowRewardedVideo(RewardUserDelegate _delegate );


    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        Instance = this;
    }

	#region Ads
	protected void AdsInitialized()
	{
		if (onAdsInitialized_Event != null)
		{
			onAdsInitialized_Event();
		}
	}
	public void LoadAd()
	{
		if (Constant.CurrLevel % 2 == 1 | isOpenBidding ) 
		{
			LoadInterstitial ();
		}
		else
		{
			LoadVideo ();
		}	
	}

	public void ShowAd()
	{
		if (PreferenceManager.GetAdsStatus()) 
		{
			if (Constant.CurrLevel % 2 == 1 | isOpenBidding)
			{
				ShowInterstitial ();
			}
			else 
			{
				ShowVideo ();
			}	
		}
	}
		
	public void LoadAdForMultiplayer()
	{
		if (PreferenceManager.GetAdsStatus())
		{
			if (playIteration % 2 == 1 | isOpenBidding)
			{
				LoadInterstitial ();
			}
			else
			{
				LoadVideo ();
			}
		}
	}

	public void ShowAdForMultiplayer()
	{
		if (PreferenceManager.GetAdsStatus())
		{
			if (playIteration % 2 == 1 | isOpenBidding)
			{   
				if (IsInterstitialAdReady())
					ShowInterstitial ();
				else
					ShowPHCrossPromotion();
			}
			else
			{
				if (IsVideoAdReady()) 
					ShowVideo();
				else
					ShowPHCrossPromotion();

			}
		}
	}

	public void LoadAdForTeaserMode()
	{
		if (PreferenceManager.GetAdsStatus())
		{
			if (playIteration % 2 == 1 | isOpenBidding)
			{
				LoadInterstitial ();
			}
			else
			{
				LoadVideo ();
			}
		}
			
	}
	protected void CallOnAdShowEvent()
	{
		if (onAdShow_Event != null)
		{
			onAdShow_Event();
		}
	}

	protected void CallOnAdCloseEvent()
	{
		if (onAdClose_Event != null)
		{
			onAdClose_Event();
		}
	}
	public void ShowAdForTeaserMode()
	{
		if (PreferenceManager.GetAdsStatus())
		{
			if (playIteration % 2 == 1 | isOpenBidding)
			{   
				if (IsInterstitialAdReady())
					ShowInterstitial ();
				else
					ShowPHCrossPromotion();
			}
			else
			{
				if (IsVideoAdReady()) 
					ShowVideo();
				else
					ShowPHCrossPromotion();

			}
		}
	}

	#endregion

	#region CrossPromotions
	public void ShowCrossPromotion()
	{
	
		//Constant.LogDesignEvent ("CrossPromotion:SmallCP:Show");
		////smallCP.SetActive(true);
	}

	public void ClickCrossPromotion()
	{
		//#if UNITY_ANDROID
		//	Application.OpenURL(DefaultAndroidCPURL);
		//#endif 

		//#if UNITY_IOS
		//	Application.OpenURL(DefaultIOSCPURL);
		//#endif
		//Constant.LogDesignEvent ("CrossPromotion:SmallCP:Click");
		////smallCP.SetActive (false);
	}

	public void ShowPHCrossPromotion()
	{
////		if (!GameManager.Instance.mhud.isInternetAvailableInGame)
//		if(Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork | Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork)
//		{
//            if (PreferenceManager.GetAdsStatus())
//            {
//                if (isOpenBidding && (!IsInterstitialAdReady()))
//                {
//                    Constant.LogDesignEvent("CrossPromotion:PlayHeavenCP:Show");
//                    //playHeavenCP.SetActive(true);
//                }
//            }
//        }
    }

	public void ClickPHCrossPromotion()
	{
		//Constant.LogDesignEvent ("CrossPromotion:PlayHeavenCP:Click");

		//#if UNITY_ANDROID
		//	Application.OpenURL(DefaultAndroidCPURL);
		//#endif 

		//#if UNITY_IOS
		//	Application.OpenURL(DefaultIOSCPURL);
		//#endif
		////playHeavenCP.SetActive (false);
	}

	public void ClosePHCrossPromotion()
	{
		//Constant.LogDesignEvent ("CrossPromotion:PlayHeavenCP:Close");
		////playHeavenCP.SetActive (false);
	}
		
	public void CloseCrossPromotion()
	{
		//smallCP.SetActive (false);
	}
	#endregion
}
