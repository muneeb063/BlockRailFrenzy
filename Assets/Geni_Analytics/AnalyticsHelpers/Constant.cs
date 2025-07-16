
using UnityEngine;
using System.Collections;
using GameAnalyticsSDK ;

using System.Collections.Generic;
using Firebase.Analytics;
using System;
using System.Globalization;
//using Facebook.Unity;

public delegate void BasicCallBackWithParams();
public delegate void BasicCallBack();

public enum DevicePerformance
{
    Low,
    Medium,
    High,
}
public enum SceneNames
{
   SplashScene,
   IntermediateToMenu,
   Menu,
   MenuStore,
   FortuneWheel,
   LevelSelectionScreen,
   GamePlay_OptimizeEnv,
   GamePlay_Halloween,
   GamePlayTeaserMode,
   LoadingScene,
   WhackATeacher,
   PullThePin,
   LevelCompleteFail
}
public enum RewardedAdStatus
{
    rewarded_available,
    rewarded_opt_in,
    rewarded_completed,
    rewarded_failed,

}
public enum LevelProgressionStatus
{
    level_start,
    level_completed,
    level_fail,
    level_retry
}

public enum ChapterUnlockingMethods
{
    Rads,
    InApp,
}

public static class Constant
{

    public const int christmasEpisodeNo = 3;

   // public static FPSFixingState fpsState;
    public static int HalloweenChaptersBundleCount = 0;
    public static int ChristmasChaptersBundleCount = 0;
    public static bool canLog = true;
    public static bool canNotify = true;
    public static bool UsePaidEvent = true;
    public static int LevelCompleteCoinsDivider = 2;
    public static int ChapterUnlockOnDailyGift = 9;
    public static int FreeTickets = 3;
    public static int MinutesForTeaserMode = 0;
    public static int SecondsForTeaserMode = 0;
    public static int DailyGiftCycleIndex = 0;
    public static bool ValentineLevelandCoinsVGPS = false;
    public static bool isAdmobInitialized = false;
    public static bool showAdOnLoadScreen = true;
    public static bool allLevelsUnlocked = false;
    public static bool allEpisodesUnlocked = true;
    public static bool useSeperateMusicForHalloween = false;
    public static float storeTutorialOffset = 0.015f;
    // check either user intiate an inapp and should log or it is restore Process. True = shouldLogInapp
    public static bool shouldLogOnInApp = false;
    public static int totalEpisodes = 3;
    public static DevicePerformance myPerformance = DevicePerformance.Low;
    public static string PlacementforAds;
    public static float explicitPlayerCameraFar = -100;
    public static float explicitSecondaryCameraFar = -100;
    public static bool isRestored;
    public static bool isLowEndDevice = false;
    public static bool isLowRamDevice = false;
    public static int LevelProgressionTime;
    public static bool isLvlProgressionStart = false;
    public static string EndCinmaticVideoURL = "";
    public static string FlyingInventory = "1,60";
    public static string VideoFolder = "";
    public static bool isVideoBaseChapter = false;
    public static bool IS_FIREBASE_INITIALIZE = false;
    public static bool showAdOnBoosterClose = true;
    public static bool miniGameEnabled = true;
    public static float HoursForAppBundleVGP = 0;
    public static float MinutesForAppBundleVGP = 0;
    public static float SecondsForAppBundleVGP = 0;
    public static float FlyingInventoryTime = 60;
    public static bool isAppOpenAd_NoDisplay = false;
    public static bool IS_OPEN_AD = true;
    public static int APP_OPEN_AD_SHOW_COUNT=10;
    public static bool dontShowAppOpenAd = false;
    public static bool IsSmartSegmentation = false;
    public static bool isIntegrityEnabled = true;
    public static bool IsFlyingInventoryEnable = true;
    public static bool IS_LEVEL_COMPLETE = false;
    public static string[] OpenAdAndSegmentation = new string[2];
    public static string[] RadValuesForUnlockingLevels = new string[16];
    public static string[] StarValuesForUnlockingLevels = new string[16];
    public static string ChaptersToDisableOnLowEnd = "";

    //FootSteps
    public static bool arrowenable = true;
    public static float footstepTimer = 35f;
    public static bool footstepenable = true;
    public static bool miniGameSelect = true;
    public static MiniGames miniGamesCurrentMode = MiniGames.MiniGamesMode;
    public static bool FootStepRad = true;
    public static int miniGameEnableLevelCount = 5;
    public static int noAdVGPCount = 0;

    //for optimzation
    public enum DeviceCalibre
    {
        Normal,
        LowTier,
        Potato
    }

    public enum MiniGames
    {
        pullThePinMode,
        MiniGamesMode
    }

    public static class DeviceData
    {
        private static float deviceScore = 25f;
        public static float lowTierDeviceScore = 65;
        public static float potatoTierDeviceScore = 40;

        private static DeviceCalibre deviceCalibre;

        public static float DeviceScore
        {
            get
            {
                return deviceScore;
            }

            set
            {
                deviceScore = value;
                SetDeviceTier();
            }
        }

        public static void SetDeviceTier()
        {
            deviceCalibre = DeviceCalibre.Normal;

            if (deviceScore <= potatoTierDeviceScore)
            {
                deviceCalibre = DeviceCalibre.Potato;
            }

            else if (deviceScore <= lowTierDeviceScore)
            {
                deviceCalibre = DeviceCalibre.LowTier;
            }
        }

        public static DeviceCalibre DeviceCalibre
        {
            get
            {
                return deviceCalibre;
            }

            set
            {
                deviceCalibre = value;
            }
        }
    }





    public static float initialEnergy = 100f;
    public static bool isInternetReachable => Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork || Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork;

    public static class Items
    {
        public static float energyDrinkReplinish = 30f;
    }

    #region orientationStates
    public static void SetLandscapeOnly()
    {
        // Allow both LandscapeLeft and LandscapeRight orientations
        Screen.orientation = ScreenOrientation.LandscapeLeft;
        Screen.orientation = ScreenOrientation.AutoRotation;

        Screen.autorotateToLandscapeLeft = true;
        Screen.autorotateToLandscapeRight = true;
        Screen.autorotateToPortrait = false;
        Screen.autorotateToPortraitUpsideDown = false;
    }

    public static void SetPortriat()
    {
        Screen.orientation = ScreenOrientation.Portrait;
       // Screen.orientation = ScreenOrientation.AutoRotation;
        // Lock the orientation to Portrait
        Screen.autorotateToLandscapeLeft = false;
        Screen.autorotateToLandscapeRight = false;
        Screen.autorotateToPortrait = true;
        Screen.autorotateToPortraitUpsideDown = true;
    }
    #endregion

    public static class Layers
    {
        public static int doorsLayer = 10;
    }

    public static class TAG
    {

        public static string PLAYER = "Player";
        public static string ENEMY = "Enemy";
        public static string POOL_HANDLER = "PoolHandler";
        public static string HUD = "HUD";
        public static string WayPoint = "WayPoint";
        public static string CineCam = "CineCam";
        public static string COLLECTABLE = "Collectable";
        public static string DOOR = "Door";
        public static string KEYUNLOCKDOOR = "KeyUnlockDoor";
        public static string HIDE_COl = "HideCollider";
        public static string PLAYER_CAM = "PlayerCam";
        public static string MANAGER = "Manager";
        public static string PlayerTrigger = "PlayerTrigger";
        public static string NeighbourSecondaryCam = "NeighbourSecondaryCamera";
        public static string ThiefSecondaryCam = "ThiefSecondaryCamera";
        public static string BODY = "Body";
        public static string FACE = "Face";
        public static string LADDER = "Ladder";
        public static string LEFTLEG = "LeftLeg";
        public static string RIGHTLEG = "RightLeg";
        public static string LEFTSLAP = "LeftSlap";
        public static string RIGHTSLAP = "RightSlap";
        public static string ItemObservingPoint = "ItemObservingPoint";
        public static string SOUNDRANGE = "SoundRange";
        public static string TRAP = "TRAP";
        public static string Francis = "Francis";
    }

    public static class REWARDS
    {
        public static int LEVELCOMPLETECOINS = 50;
        public static int FOOTSTEPCOINS = 40;
    }

    public static class PRIMITIVES
    {
        public static string POSITION = "position";
        public static string ROTATION = "rotation";
        public static string SCALE = "scale";
        public static string TIME = "time";
    }

    public static class LAYER
    {
        public static string INTERACTABLE = "interactable";
        public static string INTERACTABLEWITHVIEW = "InteractableWithView";
        public static string DUMMYITEMS = "DummyItems";
        public static string SOUNDRANGE = "SoundRange";
        public static string DOOR = "Door";
        public const string PLAYERVISIBLELAYER = "ItemsForCameraCulling";
        public const string PLAYERNOTVISIBLELAYER = "NonVisibleBodyParts";
    }

    public static int objectsCounter = 0;
    public static int Count = 1;
    public static int level_demo = 11;
    public static int HintCount = 0;
    public static int InstructionsCount = 0;
    public static int previousInstructionIndex = 0;
    public static bool onGameLaunch = true;

   

    public static bool LevelWinFailDone = false;

    /// <summary>
    /// Logs the business event.
    /// </summary>
    /// <param name="amount">Amount of Purchaseable in Cents i.e. 0.99$ = 99cents .</param>
    /// <param name="itemId">Give your inApp item Key..cncoins1 , cncoins2 etc </param>
   /* public static void LogBusinessEvent(string inAppType, int amount, InAppItemName itemType, string itemId, string receipt, string signature)
    {

#if UNITY_IPHONE
            GameAnalytics.NewBusinessEventIOS("USD", amount, "CoinsPack", itemId, "InAPP" , receipt);

    	//	LogResourceEvent (GameAnalyticsSDK.GAResourceFlowType.Source, Constant.Currency , GetCoinsForInApp(itemId) , InAPP , itemId);
#endif
#if UNITY_ANDROID
        Logger.Log("Business Is:: " + inAppType + ":" + amount + ":" + itemId);
        GameAnalytics.NewBusinessEventGooglePlay("USD", amount, itemType.ToString(), itemId, "InAPP", receipt, signature);

        // }
#endif

    }*/

    public static string GetLevelObjective(int epNum, int levelNum)
    {
        return "";
    }

    public static int GetCoinsForInApp(string inAppID)
    {

        //switch (inAppID) {

        //          case InAppHandler.PACKAGE_COINS_PKG1:
        //	return 1000;

        //          case InAppHandler.PACKAGE_COINS_PKG2:
        //	return 3000;

        //          case InAppHandler.PACKAGE_COINS_PKG3:
        //	return 6000;

        //          case InAppHandler.PACKAGE_COINS_PKG4:
        //	return 12000;

        //          case InAppHandler.PACKAGE_COINS_PKG8:
        //	return 5000;

        //    case InAppHandler.PACKAGE_COINS_PKG9:
        //	return 15000;

        //          case InAppHandler.PACKAGE_COINS_PKG10:
        //	return 30000;

        //default:
        //	break;
        //}
        return 0;

    }

    public static void LogDesignEvent(string eventName)
    {
        GameAnalytics.NewDesignEvent(eventName);
    }

    public static void FacebookLogDesignEvent(string eventName, string eventKey, string eventValue)
    {

#if UNITY_IPHONE
Logger.Log("event Is:: "+eventName); 
FB.LogAppEvent(
eventName,
null,
new Dictionary<string, object>()
{
{ "Facebook_Deeplink", eventKey }
});

#endif

#if UNITY_ANDROID

//        if (!Constant.isAmazonBuild)
//        {
//            Logger.Log("facebook logs :: " + eventName + ": Key:" + eventKey + ": Value:" + eventValue);
//            FB.LogAppEvent(
//            eventName,
//            null,
//            new Dictionary<string, object>()
//            {
//{ eventKey, eventValue }
//            });


//        }
#endif

    }

    public static void LogProgressionEvent(GAProgressionStatus status, string world, string point, string phase, int prize)
    {

#if UNITY_IPHONE
    		//  Logger.Log("event Is:: "+eventName);   
    		//GameAnalytics.NewProgressionEvent(status, world, point, phase, prize);
    		Logger.Log("Prog:: "+status.ToString() +":"+ point+ CurrEP + ":"+ phase);   

    		GameAnalytics.NewProgressionEvent(status, world, point + currentEpisode, phase, prize);
#endif
#if UNITY_ANDROID
        if (!Constant.isAmazonBuild)
        {
        //    Logger.Log("Prog:: " + status.ToString() + ":" + point + currentEpisode + ":" + phase);
            GameAnalytics.NewProgressionEvent(status, world, point + currentEpisode, phase, prize);
        }

#endif

    }

    /// <summary>
    /// Logs the info of Virtual currency(Coins) Gained ( source ) or spent (sink) resource event.
    /// </summary>
    /// <param name="flowType">Flow type can be GAResourceFlowType.Sink in case of spending or GAResourceFlowType.Source in case of earning ..</param>
    /// <param name="currency">Currency = Pass Constant.currency </param>
    /// <param name="amount">Amount = coins amount earned from RewardedVid,LevelComplete,InAPP OR Spent on Buying store Items.   </param>
    /// <param name="itemType"> for Earning = RewardedVid,LevelComplete,InAPP : For Spending = Store </param>
    /// <param name="itemId"> for earning it can be empty For spending it should be in store = Pack1 , Pack2 , Pack3 , pack4..  </param>
    public static void LogResourceEvent(GAResourceFlowType flowType, string currency, float amount, string itemType, string itemId)
    {
#if UNITY_IPHONE
    		Logger.Log("Prog: : "+  GAResourceFlowType.Sink.ToString() +":"+ currency+":"+amount.ToString() + ":"+  itemType + ":"+ itemId ) ;   
    		GameAnalytics.NewResourceEvent(flowType  , currency, amount,   itemType , itemId );

#endif
#if UNITY_ANDROID
        if (!Constant.isAmazonBuild)
        {
         //   Logger.Log("resource:: " + flowType.ToString() + ":" + ":" + currency + ":" + amount.ToString() + ":" + itemType + ":" + itemId);

            GameAnalytics.NewResourceEvent(flowType, currency, amount, itemType, itemId);


        }

#endif

    }

	public static string ConvertDateTimeToString(DateTime dateTime)
	{
		return dateTime.ToString("R", CultureInfo.InvariantCulture);
	}

    //Arslan

    public static bool IsminigameUnlockEffectActive()
    {
        if (PreferenceManager.levelCountForMinigames >= Constant.miniGameEnableLevelCount && PreferenceManager.MiniGameUnlockEffect == 0)
            return false;

         return true;
    }
    //Arslan

	public static DateTime ConverStringToDateTime(string dateTime)
	{
		try
		{
			return DateTime.ParseExact(dateTime, "R", CultureInfo.InvariantCulture);
		}
		catch (System.FormatException)
		{
			return DateTime.Now;
		}
	}

	#region FirebaseAnalytics Events
	public static void FirebaseEarnCurrencyEvent(string currencyName,int value,string placement, string source, int chapter, string sub_chapter )
    {

        //if (!UsePaidEvent)
        //    return;

        if (IS_FIREBASE_INITIALIZE)
        {
            FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventEarnVirtualCurrency,
                    new Parameter[] {
                new Parameter(FirebaseAnalytics.ParameterVirtualCurrencyName, currencyName),
                new Parameter(FirebaseAnalytics.ParameterValue, value),
                new Parameter("placement", placement),
                new Parameter("source", source),
                new Parameter("chapter", chapter),
                new Parameter("sub_chapter", sub_chapter)});

         //   Logger.Log("Firebase:EarnCurrency:" + currencyName + ":" + value + ":" + placement + ":" + source + ":" + chapter + ":" + sub_chapter);
        }
    }

    public static void FirebaseSpendCurrencyEvent(string currencyName, int value,string itemName, string placement, int chapter, string sub_chapter)
    {
        //if (!UsePaidEvent)
        //    return;

        if (IS_FIREBASE_INITIALIZE)
        {
            FirebaseAnalytics.LogEvent(FirebaseAnalytics.EventSpendVirtualCurrency,
              new Parameter[] {
            new Parameter(FirebaseAnalytics.ParameterVirtualCurrencyName, currencyName),
            new Parameter(FirebaseAnalytics.ParameterValue, value),
            new Parameter(FirebaseAnalytics.ParameterItemName, itemName),
            new Parameter("chapter", chapter),
            new Parameter("sub_chapter", sub_chapter)
              });

           // Logger.Log("Firebase:SpendCurrency:" + currencyName + ":" + value + ":" + itemName + ":" + placement + ":" + chapter + ":" + sub_chapter);
        }
    }
    //public static void FirebaseLogRadsEvent(RewardedAdStatus rewardedAdStatus, string placement, int chapter, string sub_chapter)
    //{
    //    //if (!UsePaidEvent)
    //    //    return;

    //    if (IS_FIREBASE_INITIALIZE)
    //    {
    //        FirebaseAnalytics.LogEvent(rewardedAdStatus.ToString(),
    //                         new Parameter[] {
    //                    new Parameter("placement", placement),
    //                    new Parameter("chapter",chapter),
    //                    new Parameter("sub_chapter", sub_chapter)
    //                         });

    //        //  Logger.Log("Firebase:" + rewardedAdStatus.ToString() + ":" + placement + ":" + chapter + ":" + sub_chapter);
    //    }
    //}
    public static void FirebaseLevelProgressionEvent(LevelProgressionStatus levelProgressionStatus, int chapter, string sub_chapter)
    {
        //if (!UsePaidEvent)
        //    return;

        if (IS_FIREBASE_INITIALIZE)
        {
            FirebaseAnalytics.LogEvent(levelProgressionStatus.ToString(),
                new Parameter[]{
            new Parameter("chapter", chapter),
            new Parameter("sub_chapter", sub_chapter)
                });

           // Logger.Log("Firebase:" + levelProgressionStatus.ToString() + ":" + chapter + ":" + sub_chapter);
        }
    }
    public static void FirebaseLevelProgressionEvent(LevelProgressionStatus levelProgressionStatus, int chapter, string sub_chapter, int levelDuration)
    {
        //if (!UsePaidEvent)
        //    return;

        if (IS_FIREBASE_INITIALIZE)
        {
            FirebaseAnalytics.LogEvent(levelProgressionStatus.ToString(),
            new Parameter[]{
            new Parameter("chapter", chapter),
            new Parameter("sub_chapter", sub_chapter),
            new Parameter("level_duration", levelDuration)
            });

         //   Logger.Log("Firebase:" + levelProgressionStatus.ToString() + ":" + chapter + ":" + sub_chapter + ":" + levelDuration);
        }
    }
    #endregion

    public const string World = "Neighbour";
    public const string Point = "Episode";
    public const string Phase = "Mission";

    // For resource event
    public const string Currency = "Coins";
    public const string Currency_Star = "Stars";
    public const string Currency_Tickets = "Tickets";
    public const string RewardedVid = "RewardedVid";
    public const string StarInGameplay = "StarInGameplay";
    public const string LevelUnlock = "LevelUnlock";
    public const string HintReveal = "HintReveal";
    public const string FootSteps = "FootSteps";


    public const string RewardedSpecialOffer = "RewardedSpecialOffer"; //by me
    public const string MultiplayerFee = "MultiplayerFees";
    public const string TeaserModeFee = "TeaserModeFee";
    public const string PullTheStringModeFee = "PullThePinModeFee";
    public const string TeaserItem = "TeaserItem";
    public const string InventoryItem = "InventoryItem";

    public const string MultiplayerRewardForTeacher = "MultiplayerRewardForTeacher";

    // for different Gift Boxes

    public const string MultiplayerRewardForTheif = "MultiplayerRewardForTheif";

    // 1 means yes and 0 means no
    public const int isTestBuild = 0; //0 for live and 1 for sandbox

    public const int RewardedforVid = 50; //by me
    public const int Rewarded_StarforVid = 1; //by me
    public const int StarCollectGameplay = 1; //by me
    public const int RewardedforSpecialOffer = 2000; //by me

    public const string LevelComplete = "LevelComplete";
    public const string LevelComplete_rAd = "LevelComplete_rAd";
    public const string InAPP = "InAPP";
    public const string VGP = "VGP";
    public const string Store = "Store";
    public const string Tutorial = "Tutorial";
    public const string FootStep = "Tutorial";

    public const int MultiplayerFees = 50;
    public const int TeaserModeFees = 100;
    public const int PullTheStringModeFees = 100;
    public static int MultiplayerWinRewardForTeacher = 70;
    public static int MultiplayerWinRewardForTheif = 100;

    public static int MultiplayerWinRewardForTheifSilverBox = 100;
    public static int MultiplayerWinRewardForTheifGoldBox = 100;
    public static int MultiplayerWinRewardForTheifPlatinumBox = 100;
    public static int[] GiftBoxLocationIndex = { -1, -1, -1 };
    public static int GiftBoxLocationIndexForTutorialLevel1 = -1;
    public static bool IsGiftBoxAlreadyCollected = false;

    public static int totalNumberOfLevels = 29;
    public const int SilverBoxId = 1;
    public const int GoldBoxId = 2;
    public const int PlatinumBoxId = 3;

    public static bool IsSilverBoxCollected = false;
    public static bool IsGoldBoxCollected = false;
    public static bool IsPlatinumBoxCollected = false;

    public static int DailyReward = 50;

    public static bool IsInternetReacheable
    {
        get
        {
            return Application.internetReachability == NetworkReachability.ReachableViaCarrierDataNetwork | Application.internetReachability == NetworkReachability.ReachableViaLocalAreaNetwork;
        }
    }


    public static bool InAppCallFromMultiplayer = false;
    public static bool InAppCallFromMenu = false;
    public static bool InAppSuccesseedForMultiplayer = false;
    public static int ReplayLevel = 0;
    public static int CurrLevel = 0;
    public static int CurrEP = 1;
    public static int currentEpisode = 1;
    public static int playIteration = 0;
    public static int levelValue = 1;
    public static int levelValueEP2 = 1;
    public static bool isAmazonBuild = false;
    public static bool isCinematicEnd = false;
    public static string Prefix = "";
    public static bool ShouldInitializeGA = false;


   // public const string EnterTeaserModeByRad = "EnterTeaserModeByRad";
  //  public const string EnterTeaserModeByCoins = "EnterTeaserModeByCoins";
    public static string teaserModeEnterance = "";
    public static string pullThePinModeEnterance = "";

    public static AssetBundle currentAssetBundle;
    public static int assetChapterNo = 0;
    public static bool loadFromAssetBundle = false;

    #region Boosters
    public static int bonusHintsMultiplier = 1;
    public static bool isUnlimitedEnergyBoosterActive = false;

    public static string[] chaptersName = { "Episode 1", "Valentine's Special", "Episode 2", "Winter Season" };
    public static bool isGameLaunch, boosterShownInLevel = false;

    public static int[] boostersOccurence = { };


    #endregion

    public static string DEEP_LINK_HOST = "requestepisode";
    public static int HintIndexBeforeBooster;

    // For cat level booster at the end
    public static bool isCatInHand = false;
   // public static EnableTeacherChase cat;

    public static int DLCDownloadingChapterNo = 0;
    public static List<int> DLCEpisodeLoadedAssets = new List<int>();
}