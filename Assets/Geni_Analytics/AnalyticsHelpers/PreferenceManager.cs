using System;
using UnityEngine;
using System.Collections;



public class PreferenceManager
{
    #region 
    const string FirstTimeMenuForPS = "FirstTimeMenuForPS";
    const string Subscription = "Subscription";
    const string UNLOCKED_EP = "UnlockedEpisode";
    const string LEVEL_KEY = "Level";
    const string HIDE_TUTORIAL_PLAYED = "HIDE_TUTORIAL_PLAYER";
    const string LOCK_LEVEL = "LevelLocked";
    const string UNLOCK_LEVEL = "LevelUnLocked";
    const string ROTATE_TUTORIAL = "RotateTutorial";
    const string HAS_RATED = "HAS_RATED";
    const string KEYCOLLECTION_TUTORIAL = "KEYTUTORIAL";
    const string DOORUNLOCK_TUTORIAL = "DOORUNLOCK";
    public static bool labColliderChecked = false;
    const string STUN_COUNTER = "STUNCOUNTER";
    const string INVISIBLE_COUNTER = "INVISIBLECOUNTER";
    const string StunGun_TUTORIAL = "StunGun_TUTORIAL";
    const string FREE_HINT_BALANCE = "FREEHINTBALANCE";
    const string CURRENT_STARS_BALANCE = "CURRENTSTARSBALANCE";
    const string CURRENT_TICKETS_BALANCE = "CURRENTTICKETSBALANCE";
    const string FORWARD_TUTORIAL = "ForwardTutorial";
    const string BACKWARD_TUTORIAL = "BackwardTutorial";
    const string EP_UNLOCKED = "InAppLevels";
    const string MULTIPLAYER = "Multiplayer";
    const string MULTIPLAYER_UNLOCKED = "MultiplayerUnlockedFirstTime";
    const string RewardRemainingTime = "RewardRemainingTime";
    const string FREECOINS_RECEIVED = "FREECOINS_RECEIVED";
    const string PLAYED_MULTIPLAYER_COUNTER = "HAS_PLAYED_MULTIPLAYER";
    const string MULTIPLAYER_WIN_COUNTER = "PLAYER_WIN_MULTIPLAYER";
    const string ADS = "ADS";
    const string PRIVACY_POLICY_SHOWN = "is_privacy_policy_shown";
    const string PRIVACY_POLICY_ACCEPTED = "is_privacy_policy_accept";
    const string GAME_PLAYED_FIRST_TIME = "is_Game_Played_First_Time";
    const string ALL_GAME_UNLOCKED_VGP = "All_Game_Unlocked_Vgp";
    const string TEASERMODE_PLAYED_FIRST_TIME = "is_TeaserMode_Played_First_Time";
    const string lAST_GAME_CLOSE_TIME = "lastGameCloseTime";
    const string lAST_GAME_OPEN_TIME = "lasttGameOpenTime";
    const string UNLIMITED_ENERGY_TIME = "ute";
    const string PLAYER_ENERGY = "playerenergy";
    const string _itemCount = "itemCount";
    const string ISTUTORIALLEVELPLAYED = "ITLP";
    const string CURRENTPLAYERINDEX = "CURRENTPLAYERINDEX";
    const string OLD_VALUES = "OLD_VALUES";
    const string optimizationMessagesString = "OptimizationMessage";



    const string energypurchaseTime = "ept";
    const string remainingEnergyTime = "ret";

    const string perkStartTime = "startTime";
    const string perkRemainingTime = "remainingTime";
    const string seconds = "seconds";

    public static string TutorialLevelsWithBot = "TutorialLevelsWithBot";
    public static string IsAllEpisodesUnlock = "IsAllEpisodesUnlock";


    const string Footsteps_TutorialShown = "FootstepsTutotialShown";
    const string BOOSTER_TUTORIAL = "boosterTutorial";
    const string PullThePinTutorial = "PullThePinTutorial";
    const string levelCountForUnlockMinigames = "levelCountForUnlockMinigames";
    const string miniGameUnlockEffect = "unlockEffect";

    #endregion

    //Arslan Ashraf
    public static int PullThePinTut
    {
        get
        {
            //return 1;
            return PlayerPrefs.GetInt(PullThePinTutorial, 0);
        }
        set
        {
            PlayerPrefs.SetInt(PullThePinTutorial, value);
        }
    }

    public static int levelCountForMinigames
    {
        get
        {
            //return 1;
            return PlayerPrefs.GetInt(levelCountForUnlockMinigames, 0);
        }
        set
        {
            PlayerPrefs.SetInt(levelCountForUnlockMinigames, value);
        }
    }

    public static int MiniGameUnlockEffect
    {
        get
        {
            //return 1;
            return PlayerPrefs.GetInt(miniGameUnlockEffect, 0);
        }
        set
        {
            PlayerPrefs.SetInt(miniGameUnlockEffect, value);
        }
    }

    //Arslan Ashraf

    public static bool FootstepsTutotialShown
    {
        get { return PlayerPrefs.GetInt(Footsteps_TutorialShown, 0) == 1; }
        set { PlayerPrefs.SetInt(Footsteps_TutorialShown, value ? 1 : 0); Debug.Log(PlayerPrefs.GetInt(Footsteps_TutorialShown)); }
    }
    public static bool isBoostertutorialOpen
    {
        get{ return PlayerPrefs.GetInt(BOOSTER_TUTORIAL) == 1; }
        set{ PlayerPrefs.SetInt(BOOSTER_TUTORIAL, value ? 0 : 1);}
    }

    public static int GetEpRadCount(int epNum)
    {
        return PlayerPrefs.GetInt("Ep" + epNum + "RadCount", 3);
    }

    public static void DeductEpRadCount(int epNum)
    {
        PlayerPrefs.SetInt("Ep" + epNum + "RadCount", GetEpRadCount(epNum) - 1);
    }

  /*  public static int GetOlderItemsCount(ItemName itemName)
    {
        string prefsKrey = String.Format(String.Format("{0}:{1}", itemName, _itemCount));
        int count = Mathf.Clamp(PlayerPrefs.GetInt(prefsKrey), 0, 100000);
        if (PlayerPrefs.HasKey(prefsKrey))
        {
            PlayerPrefs.DeleteKey(prefsKrey);
        }
        return count;
    }*/

    public static bool OlderPrefsChecked
    {
        get
        {
            return PlayerPrefs.GetInt("OPC") == 1;
        }

        set
        {
            PlayerPrefs.SetInt("OPC", value ? 1 : 0);
        }
    }

    
    public static bool OptimizationMessageGiven
    {
        get
        {
            return PlayerPrefs.GetInt(optimizationMessagesString, 0) == 1;
        }

        set
        {
            PlayerPrefs.SetInt(optimizationMessagesString, value ? 1 : 0);
        }
    }

    public static bool IsTutorialLevelPlayed(int level)
    {
        return PlayerPrefs.GetInt(ISTUTORIALLEVELPLAYED + level.ToString()) == 1;
    }

    public static int FirstTimeMenuStatus
    {
        get
        {
            //return 1;
            return PlayerPrefs.GetInt(FirstTimeMenuForPS, 0);
        }
        set
        {
            PlayerPrefs.SetInt(FirstTimeMenuForPS, value);
        }
    }

    public static int SubscriptionStatus
    {
        get
        {
            //return 1;
            return PlayerPrefs.GetInt(Subscription, 0);
        }
        set
        {
            PlayerPrefs.SetInt(Subscription, value);
        }
    }

    public static int CurrentCharacterIndex
    {
        get
        {
            return PlayerPrefs.GetInt(CURRENTPLAYERINDEX, 0);
        }
        set
        {
            PlayerPrefs.SetInt(CURRENTPLAYERINDEX, value);
        }
    }

    public static bool IsEnergyTutorialPlayed
    {
        get
        {
            return PlayerPrefs.GetInt("ETP").Equals(1);
        }

        set
        {
            PlayerPrefs.SetInt("ETP", value ? 1 : 0);
        }
    }

    public static void SetTutorialLevelPlayed(int level)
    {
        PlayerPrefs.SetInt(ISTUTORIALLEVELPLAYED + level.ToString(), 1);
    }

    public static string Last_Game_Open_Time
    {
        get
        {
            return PlayerPrefs.GetString(lAST_GAME_OPEN_TIME);
        }
        set
        {
            PlayerPrefs.SetString(lAST_GAME_OPEN_TIME, value);
        }
    }

/*    public static void SetItemCountInStock(ItemName itemName, int Count)
    {
        PlayerPrefs.SetInt(String.Format("{0}:{1}", itemName, _itemCount), Count);
    }*/




   /* public static bool IsGivenTutorialPerformed(TutorialType type)
    {
        return PlayerPrefs.GetInt(type.ToString()).Equals(1);
    }*/

    public static bool IsObjectCollected(string name)
    {
        return PlayerPrefs.GetInt(name + "C") == 1;
    }

    public static void MarkObjectCollected(string name)
    {
        PlayerPrefs.SetInt(name + "C", 1);
    }

  /*  public static void SetGivenTutorialPerformed(TutorialType type)
    {
        PlayerPrefs.SetInt(type.ToString(), 1);
        Logger.Log(type.ToString() + " 1");
    }*/

    public static bool AllTutorialsPerformed
    {
        get
        {
            return PlayerPrefs.GetInt("ATP").Equals(1);
        }

        set
        {
            PlayerPrefs.SetInt("ATP", value ? 1 : 0);
        }
    }

  /*  public static string RemainingUnlimitedEnergyTime
    {
        get
        {
            return PlayerPrefs.GetString(PerkType.UNLIMITEDENERGY.ToString() + perkRemainingTime);
        }

        set
        {
            PlayerPrefs.SetString(PerkType.UNLIMITEDENERGY.ToString() + perkRemainingTime, value);
        }
    }*/

/*    public static string UnlimitedEnergyStartTime
    {
        get
        {
            return PlayerPrefs.GetString(PerkType.UNLIMITEDENERGY.ToString() + perkStartTime);
        }

        set
        {
            PlayerPrefs.SetString(PerkType.UNLIMITEDENERGY.ToString() + perkStartTime, value);
        }
    }
    public static string UnlimitedEnergyEndTime
    {
        get
        {
            return PlayerPrefs.GetString(PerkType.UNLIMITEDENERGY.ToString() + perkStartTime);
        }

        set
        {
            PlayerPrefs.SetString(PerkType.UNLIMITEDENERGY.ToString() + perkStartTime, value);
        }
    }
    public static string UnlimitedTeaserEndTime
    {
        get
        {
            return PlayerPrefs.GetString(PerkType.LIMITEDTEASERTIME.ToString() + perkStartTime);
        }

        set
        {
            PlayerPrefs.SetString(PerkType.LIMITEDTEASERTIME.ToString() + perkStartTime, value);
        }
    }
    public static double RemainingUnlimitedEnergyTime_InSeconds
    {
        get
        {
            return PlayerPrefs.GetInt(PerkType.UNLIMITEDENERGY.ToString() + perkRemainingTime + seconds);
        }

        set
        {
            PlayerPrefs.SetInt(PerkType.UNLIMITEDENERGY.ToString() + perkRemainingTime + seconds, (int)value);
        }
    }
    public static double RemainingUnlimitedTeaserTime_InSeconds
    {
        get
        {
            return PlayerPrefs.GetInt(PerkType.LIMITEDTEASERTIME.ToString() + perkRemainingTime + seconds);
        }

        set
        {
            PlayerPrefs.SetInt(PerkType.LIMITEDTEASERTIME.ToString() + perkRemainingTime + seconds, (int)value);
        }
    }
    public static string LimitedAppBundleVGP
    {
        get
        {
            return PlayerPrefs.GetString(PerkType.LimitedAppBunndleTime.ToString() + perkStartTime);
        }

        set
        {
            PlayerPrefs.SetString(PerkType.LimitedAppBunndleTime.ToString() + perkStartTime, value);
        }
    }
    public static double RemainingLimitedAppBundleTime
    {
        get
        {
            return PlayerPrefs.GetInt(PerkType.LimitedAppBunndleTime.ToString() + perkRemainingTime + seconds);
        }

        set
        {
            PlayerPrefs.SetInt(PerkType.LimitedAppBunndleTime.ToString() + perkRemainingTime + seconds, (int)value);
        }
    }*/

    public static string Last_Game_Close_Time
    {
        get
        {
            return PlayerPrefs.GetString(lAST_GAME_CLOSE_TIME);
        }

        set
        {
            PlayerPrefs.SetString(lAST_GAME_CLOSE_TIME, value);
        }
    }




    #region PlayerEnergyrefs
    public static float Player_Energy
    {
        get
        {
            return PlayerPrefs.GetFloat(PLAYER_ENERGY, Constant.initialEnergy);
        }

        set
        {
            PlayerPrefs.SetFloat(PLAYER_ENERGY, value);
        }
    }

    public static bool IsUnlimitedEnergyFull
    {

        get
        {
            if (SubscriptionStatus != 0)
            {
                return true;
            }
            return PlayerPrefs.GetInt("IsUnlimitedEnergyFull") == 1;
        }

        set
        {
            PlayerPrefs.SetInt("IsUnlimitedEnergyFull", value ? 1 : 0);
        }
    }
    #endregion

    #region CoinsPrefs

    private const string coinsBalanace = "COINSBALANCE";

    public static int CoinsBalance
    {
        get
        {
            return PlayerPrefs.GetInt(coinsBalanace, Constant.allLevelsUnlocked ? 1000000 : 0);
        }

        set
        {
            PlayerPrefs.SetInt(coinsBalanace, value);
        }
    }





    #endregion

    #region ItemsPrefs
    private const string itemsPrefs = "ITEMSPREFS";

    public static string PrefsItems
    {
        get
        {
            return PlayerPrefs.GetString(itemsPrefs, "");
        }

        set
        {
            PlayerPrefs.SetString(itemsPrefs, value);
        }
    }

  /*  public static bool HasItemInStock(ItemName itemName)
    {
        return InventoryList.instance.localInventory.GetItemCount(itemName) > 0;
    }*/
    #endregion

    #region StarsPrefs

    public static int StarsInPrefs
    {
        get
        {
            return PlayerPrefs.GetInt(CURRENT_STARS_BALANCE, Constant.allLevelsUnlocked ? 1000000 : 0);
        }

        set
        {
            PlayerPrefs.SetInt(CURRENT_STARS_BALANCE, value);
        }
    }

    #endregion

    #region Tickets

    public static int Tickets
    {
        get
        {
            return PlayerPrefs.GetInt(CURRENT_TICKETS_BALANCE, PlayerPrefs.HasKey(CURRENT_TICKETS_BALANCE) ? PlayerPrefs.GetInt(CURRENT_TICKETS_BALANCE) : Constant.FreeTickets);
        }

        set
        {
            PlayerPrefs.SetInt(CURRENT_TICKETS_BALANCE, value);
        }
    }

    #endregion

    #region MultiplayerPrefs
    public static void SetMultiplayerUnlocked(int val)
    {

        PlayerPrefs.SetInt(MULTIPLAYER_UNLOCKED, 1);
    }

    public static bool GetMultiplayerUnlocked()
    {
        return (PlayerPrefs.GetInt(MULTIPLAYER_UNLOCKED, 0) == 1 | Constant.allLevelsUnlocked);
    }

    public static void SetMultiplayerPlayedFirstTime(int val)
    {

        PlayerPrefs.SetInt(MULTIPLAYER, 1);
    }

    public static bool GetMultiplayerPlayedFirstTime()
    {
        return (PlayerPrefs.GetInt(MULTIPLAYER, 0) == 1);
    }

    public static void SetEP3Downloaded(int val)
    {

        PlayerPrefs.SetInt("SetEP3Downloaded", 1);
    }
    #endregion

    public static void setCurrentLevel(int currLevel)
    {

        PlayerPrefs.SetInt(LEVEL_KEY + Constant.currentEpisode.ToString(), currLevel);
    }

    public static int getCurrentLevel()
    {
        return PlayerPrefs.GetInt(LEVEL_KEY + Constant.currentEpisode.ToString(), 1);
    }

    public static int GetEpUnlockedMethod(int EP)
    {
        return PlayerPrefs.GetInt(EP_UNLOCKED + EP, 0);
    }

    public static bool getEPUnlocked(int EP)
    {
        if (Constant.allEpisodesUnlocked)
            return true;
        if (EP == 1)
            return true;

        return (PlayerPrefs.GetInt(EP_UNLOCKED + EP, 0) > 0);
    }

    public static void setEPUnlockedStatus(int EP, int value)
    {
        PlayerPrefs.SetInt(EP_UNLOCKED + EP, value);
    }

    //public static int GetEPUnlockStatus(int ep)
    //{
    //    if (SubscriptionStatus != 0)
    //    {
    //        return 2;
    //    }

    //    return PlayerPrefs.GetInt(EP_UNLOCKED + ep, 1);
    //}

    public static bool GetAdsStatus()
    {
        //0 means ads enabled and 1 means ads are disabled. so true means ads are enabled and false means ads are disabled.
        if (SubscriptionStatus != 0)
        {
            return false;
        }

        return (PlayerPrefs.GetInt(ADS, 0) == 0);
    }
    public static void SetAdsStatus(int value)
    {
        PlayerPrefs.SetInt(ADS, value);
    }

    public static int getLevelLockingIndex()
    {
        return PlayerPrefs.GetInt(LOCK_LEVEL + Constant.CurrEP, 1);
    }
    public static int GetLastEpisodePlayed()
    {
        return PlayerPrefs.GetInt("LastEpPlayed", 1);
    }
    public static void SetLastEpisodePlayed(int value)
    {
        PlayerPrefs.SetInt("LastEpPlayed", value);
    }
    public static int getLevelLockingIndexEP(int value)
    {
        //return 6;
        if (Constant.allLevelsUnlocked)
            return 20;

        return PlayerPrefs.GetInt(LOCK_LEVEL + "EP" + value, 1);
    }
    public static void setLevelLockingIndexEP(int epIndex, int value)
    {
        PlayerPrefs.SetInt(LOCK_LEVEL + "EP" + epIndex, value);
    }


    public static void setForwardTutorialValue(int value)
    {
        PlayerPrefs.SetInt(FORWARD_TUTORIAL, value);
    }
    public static int getForwardTutorialValue()
    {
        //return 0;
        return PlayerPrefs.GetInt(FORWARD_TUTORIAL, 0);
    }

    public static void setHasRated()
    {
        PlayerPrefs.SetInt(HAS_RATED, 1);
    }
    public static bool getHasRated()
    {
        return (PlayerPrefs.GetInt(HAS_RATED, 0) == 1);
    }

    public static void setKeyTutorialValue(int value)
    {
        PlayerPrefs.SetInt(KEYCOLLECTION_TUTORIAL, value);
    }
    public static int getKeyTutorialValue()
    {
        return PlayerPrefs.GetInt(KEYCOLLECTION_TUTORIAL, 1);
    }

    public static void setDoorUnlockTutorial(int value)
    {
        PlayerPrefs.SetInt(DOORUNLOCK_TUTORIAL, value);
    }



    public static void setFreeCoinsReceived(int pValue)
    {
        PlayerPrefs.SetInt(FREECOINS_RECEIVED, pValue);
    }
    public static bool getFreeCoinsReceived()
    {
        return (PlayerPrefs.GetInt(FREECOINS_RECEIVED, 0) == 1);
    }


    public static void setPlayedMultiplayer()
    {
        PlayerPrefs.SetInt(PLAYED_MULTIPLAYER_COUNTER, getPlayedMultiplayer() + 1);
    }

    public static int getPlayedMultiplayer()
    {
        return PlayerPrefs.GetInt(PLAYED_MULTIPLAYER_COUNTER, 0);
    }

    public static void setIsTutorialLevelsWithBot(int value)
    {
        PlayerPrefs.SetInt(TutorialLevelsWithBot, value);
    }

    public static int getIsTutorialLevelsWithBot()
    {
        return 3;
        return PlayerPrefs.GetInt(TutorialLevelsWithBot, 0);
    }

    public static bool getIsAllEpisodesUnlock()
    {
        return (PlayerPrefs.GetInt(IsAllEpisodesUnlock, 0) == 1);
    }

    public static void setIsAllEpisodesUnlock()
    {
        PlayerPrefs.SetInt(IsAllEpisodesUnlock, 1);
    }


    public static int getLastPlayedEP()
    {
        return PlayerPrefs.GetInt("LastPlayedEP", 1);
    }

    public static void setLastPlayedEP(int pValue)
    {
        PlayerPrefs.SetInt("LastPlayedEP", pValue);
    }

    public static string getLastPlayedLevel()
    {
        return PlayerPrefs.GetString("LastPlayedLevel", "");
    }

    public static void setLastPlayedLevel(string LValue)
    {
        PlayerPrefs.SetString("LastPlayedLevel", LValue);
    }


    public static void SetIsPrivacyPolicyShown(int val)
    {

        PlayerPrefs.SetInt(PRIVACY_POLICY_SHOWN, val);
    }

    public static bool GetIsPrivacyPolicyShown()
    {

        return (PlayerPrefs.GetInt(PRIVACY_POLICY_SHOWN, 0) == 1);
    }


    public static void SetIsPrivacyPolicyAccepted(int val)
    {

        PlayerPrefs.SetInt(PRIVACY_POLICY_ACCEPTED, val);
    }

    public static bool GetIsPrivacyPolicyAccepted()
    {
        return (PlayerPrefs.GetInt(PRIVACY_POLICY_ACCEPTED, 1) == 1);
    }


    public static void SetIsGamePlayedFirstTime(int val)
    {
        PlayerPrefs.SetInt(GAME_PLAYED_FIRST_TIME, 0);
    }

    public static bool GetIsGamePlayedFirstTime()
    {
        return (PlayerPrefs.GetInt(GAME_PLAYED_FIRST_TIME, 1) == 1);
    }

    public static void setCollectTutorial(int value)
    {
        PlayerPrefs.SetInt("CollectTutorial", value);
    }
    public static int getCollectTutorial()
    {
        return PlayerPrefs.GetInt("CollectTutorial", 0);
    }

    public static void SetDataTime(string value)
    {
        PlayerPrefs.SetString("DateTime", value);
    }
    public static string GetDateTime()
    {
        return PlayerPrefs.GetString("DateTime", Constant.ConvertDateTimeToString(DateTime.Now));
    }


    public static void SetFreeTeaserModeTry(int val)
    {
        PlayerPrefs.SetInt("FreeTeaserModeTry", val);
    }

    public static bool GetFreeTeaserModeTry()
    {
        return (PlayerPrefs.GetInt("FreeTeaserModeTry", 1) == 1);
    }


    public static void SetTeaserModeFirstEntry(int val)
    {
        PlayerPrefs.SetInt("TeaserModeFirstEntry", val);
    }

    public static bool GetTeaserModeFirstEntry()
    {
        return (PlayerPrefs.GetInt("TeaserModeFirstEntry", 1) == 1);
    }


    public static void SetTeaserModeUnlocked(int val)
    {
        PlayerPrefs.SetInt("TeaserModeUnlock", val);
    }

    public static bool GetTeaserModeUnlocked()
    {
        return (PlayerPrefs.GetInt("TeaserModeUnlock", 0) == 1);
    }
    public static void SetTeaserModeUnlockedForTime(int val)
    {
        PlayerPrefs.SetInt("TeaserModeUnlockedForTime", val);
    }
    public static bool GetTeaserModeUnlockedForTime()
    {
        return (PlayerPrefs.GetInt("TeaserModeUnlockedForTime", 0) == 1);
    }

    public static void SetOneTimeAppBundleValue(int val)
    {
        PlayerPrefs.SetInt("OneTimeAppBundleValue", val);
    }
    public static bool GetOneTimeAppBundleValue()
    {
        return (PlayerPrefs.GetInt("OneTimeAppBundleValue", 0) == 1);
    }
    public static void SetOneTimeAppBundleTeaserValue(int val)
    {
        PlayerPrefs.SetInt("OneTimeAppBundleTeaserValue", val);
    }
    public static bool GetOneTimeAppBundleTeaserValue()
    {
        return (PlayerPrefs.GetInt("OneTimeAppBundleTeaserValue", 0) == 1);
    }
    public static void SetOneTimeAppBundleChapterValue(int val)
    {
        PlayerPrefs.SetInt("OneTimeAppBundleChapterValue", val);
    }
    public static bool GetOneTimeAppBundleChapterValue()
    {
        return (PlayerPrefs.GetInt("OneTimeAppBundleChapterValue", 0) == 1);
    }

    public static void SetTeaserPropsProgress(string value)
    {
        PlayerPrefs.SetString("TeaserProgress", value);
    }

    public static string GetTeaserPropsProgress()
    {
        return PlayerPrefs.GetString("TeaserProgress", "");
    }


    public static void SetUnlimitedHintsStatus(int val)
    {
        PlayerPrefs.SetInt("UnlimitedHints", val);
    }

    public static bool GetUnlimitedHintsStatus()
    {
        return (PlayerPrefs.GetInt("UnlimitedHints", 0) == 1);
    }

    public static void SetUnlimitedReviveStatus(int val)
    {
        PlayerPrefs.SetInt("UnlimitedRevive", val);
    }

    public static bool GetUnlimitedReviveStatus()
    {
#if UNITY_EDITOR
        return true; //change by me
#endif
        return (PlayerPrefs.GetInt("UnlimitedRevive", 0) == 1);
    }




    ////////////////Daily Reward /////////////////////////

    public static void SetNextDayRewardWithVideo(int val)
    {
        PlayerPrefs.SetInt("NextDayRewardWithVideo", val);
    }

    public static int GetNextDayRewardWithVideo()
    {
        return PlayerPrefs.GetInt("NextDayRewardWithVideo", 0);
    }

    public static void SetDailyRewardDataTime(string value)
    {
        PlayerPrefs.SetString("DailyReward", value);
    }
    public static string GetDailyRewardDateTime()
    {
        return PlayerPrefs.GetString("DailyReward", "Now");
    }

    public static void SetSubscriptionDailyRewardDataTime(string value)
    {
        PlayerPrefs.SetString("SubscriptionDailyReward", value);
    }
    public static string GetSubscriptionDailyRewardDateTime()
    {
        return PlayerPrefs.GetString("SubscriptionDailyReward", "Now");
    }
    //public static void SetLatestSubscriptionDailyRewardIndex(int val)
    //{
    //    PlayerPrefs.SetInt("SubscriptionDailyRewardIndex", val);
    //}

    //public static int GetLatestSubscriptionDailyRewardIndex()
    //{
    //    return PlayerPrefs.GetInt("SubscriptionDailyRewardIndex", 0);
    //}
    public static void SetLatestDailyRewardIndex(int val)
    {
        PlayerPrefs.SetInt("DailyRewardIndex", val);
    }

    public static int GetLatestDailyRewardIndex()
    {
        return PlayerPrefs.GetInt("DailyRewardIndex", 0);
    }

    //public static void SetLatestDailyRewardOptionIndex(int val)
    //{
    //    PlayerPrefs.SetInt("DailyRewardOptionIndex", val);
    //}

    //public static int GetLatestDailyRewardOptionIndex()
    //{
    //    return PlayerPrefs.GetInt("DailyRewardOptionIndex", 0);
    //}

    //////////////////////////////////////////////////////////////

    ////////////////FortuneWheel ///////////////////

    public static void UpdateSpinCount(int val)
    {
        PlayerPrefs.SetInt("SpinCount", (val % 10) + 1);
    }

    public static int GetSpinCount()
    {
        return PlayerPrefs.GetInt("SpinCount", -1);
    }
    public static void UpdateFreeSpinCount(int val)
    {
        PlayerPrefs.SetInt("FreeSpinCount", val);
    }

    public static int GetFreeSpinCount()
    {
        return PlayerPrefs.GetInt("FreeSpinCount", -1);
    }
    ////////////////Generic Pref for Single Use///////////////////

    public static bool IsFunctionPerfomed(string functionName)
    {
        return (GetFunctionStatus(functionName) == 1);
    }
    public static void SetFunctionPerformed(string functionName, int val)
    {
        PlayerPrefs.SetInt(functionName, val);
    }
    public static int GetFunctionStatus(string functionName)
    {
        return PlayerPrefs.GetInt(functionName, 0);
    }
    //////////////////////////////////////////////////////////////

    ////////////////Player Tracking of Doors///////////////////

    public static void SetDoorsUsedForPlayerEnterance(string value)
    {
        PlayerPrefs.SetString("PlayerEnteranceDoors", value);
    }
    public static string GetDoorsUsedForPlayerEnterance()
    {
        return PlayerPrefs.GetString("PlayerEnteranceDoors", "");
    }
    //////////////////////////////////////////////////////////////

    ////////////////Player Tracking of Windows///////////////////

    public static void SetWindowsUsedForPlayerEnterance(string value)
    {
        PlayerPrefs.SetString("PlayerEnteranceWindows", value);
    }
    public static string GetWindowsUsedForPlayerEnterance()
    {
        return PlayerPrefs.GetString("PlayerEnteranceWindows", "");
    }
    //////////////////////////////////////////////////////////////

    ////////////////Enemy Tracking of Observed Items///////////////////
    public static void SetTrackOfEnemyObservedItem(string value)
    {
        PlayerPrefs.SetString("TrackOfEnemyObservedItem", value);
    }
    public static string GetTrackOfEnemyObservedItem()
    {
        return PlayerPrefs.GetString("TrackOfEnemyObservedItem", "");
    }

    #region Tracking of Level Play Iterations
    public static void SetTrackOfLevelPlayIterations(string value)
    {
        PlayerPrefs.SetString("TrackOfLevelPlayIterations", value);
    }
    public static string GetTrackOfLevelPlayIterations()
    {
        return PlayerPrefs.GetString("TrackOfLevelPlayIterations", "");
    }
    #endregion
    #region Prefs For VGPS
    public static int NoAdsVgpCount
    {
        get
        {
            return PlayerPrefs.GetInt("NoAdsVGP", 0);
        }

        set
        {
            PlayerPrefs.SetInt("NoAdsVGP", value);
        }
    }

    public static int CoinsVGPCount
    {
        get
        {
            return PlayerPrefs.GetInt("CoinsVGP", 0);
        }

        set
        {
            PlayerPrefs.SetInt("CoinsVGP", value);
        }
    }
    public static int AppBundleVGPCount
    {
        get
        {
            return PlayerPrefs.GetInt("AppBundleVGP", 0);
        }

        set
        {
            PlayerPrefs.SetInt("AppBundleVGP", value);
        }
    }


    public static int StarsVGPCount
    {
        get
        {
            return PlayerPrefs.GetInt("StarsVGP", 0);
        }

        set
        {
            PlayerPrefs.SetInt("StarsVGP", value);
        }
    }

    public static int OfferVGPCount
    {
        get
        {
            return PlayerPrefs.GetInt("OfferVGP", 0);
        }

        set
        {
            PlayerPrefs.SetInt("OfferVGP", value);
        }
    }
    //public static int CurrentStarsBalance
    //{
    //    get
    //    {
    //        return PlayerPrefs.GetInt(CURRENT_STARS_BALANCE, Constant.allLevelsUnlocked ? 10000 : 0);
    //    }

    //    set
    //    {
    //        PlayerPrefs.SetInt(CURRENT_STARS_BALANCE, value);
    //    }
    //}
    public static int InfiniteEnergyVGPCount
    {
        get
        {
            return PlayerPrefs.GetInt("InfiniteEnergyVGP", 0);
        }

        set
        {
            PlayerPrefs.SetInt("InfiniteEnergyVGP", value);
        }
    }

    public static int StarsCoinsVgpCount
    {
        get
        {
            return PlayerPrefs.GetInt("StarsCoinsVGP", 0);
        }

        set
        {
            PlayerPrefs.SetInt("StarsCoinsVGP", value);
        }
    }

    public static int UnlimitedTeaserVGPCount
    {
        get
        {
            return PlayerPrefs.GetInt("UnlimitedTeaserVGP", 0);
        }

        set
        {
            PlayerPrefs.SetInt("UnlimitedTeaserVGP", value);
        }
    }

    public static int UnlimitedReviveVGPCount
    {
        get
        {
            return PlayerPrefs.GetInt("UnlimitedReviveVGP", 0);
        }

        set
        {
            PlayerPrefs.SetInt("UnlimitedReviveVGP", value);
        }
    }

    public static int UnlimitedHintsVGPCount
    {
        get
        {
            return PlayerPrefs.GetInt("UnlimitedHintsVGP", 0);
        }

        set
        {
            PlayerPrefs.SetInt("UnlimitedHintsVGP", value);
        }
    }
    public static int FreeHintBalance
    {
        get
        {
            return PlayerPrefs.GetInt(FREE_HINT_BALANCE, Constant.allLevelsUnlocked ? 10000 : 0);
        }

        set
        {
            PlayerPrefs.SetInt(FREE_HINT_BALANCE, value);
        }
    }
    public static int mainMenuCounter
    {
        get
        {
            return PlayerPrefs.GetInt("mainMenuCounter", -1);
        }

        set
        {
            PlayerPrefs.SetInt("mainMenuCounter", value % 3);
        }
    }

    #endregion
    #region Prefs For LevelUnlocks with rAds

    public static int GetRadForUnlockLevel(int levelNum, int epNum)
    {
        return PlayerPrefs.GetInt("radUnlockForLevel" + levelNum + "ep" + epNum, 0);
    }
    public static void SetRadForUnlockLevel(int levelNum, int epNum, int value)
    {
        PlayerPrefs.SetInt("radUnlockForLevel" + levelNum + "ep" + epNum, value);
    }    
    public static int GetRadPreviousValue()
    {
        return PlayerPrefs.GetInt("unlockByRadsCountOriginalValue");
    }    
    public static void SetRadPreviousValue(int RadPreviousValue)
    {
        PlayerPrefs.SetInt("unlockByRadsCountOriginalValue", RadPreviousValue);
    }

    #endregion

    #region Prefs For LevelUnlocks with Stars

    public static int GetStarsForUnlockLevel(int levelNum, int epNum)
    {
        return PlayerPrefs.GetInt("starUnlockForLevel" + levelNum + "ep" + epNum, 0);
    }
    public static void SetStarsForUnlockLevel(int levelNum, int epNum, int value)
    {
        PlayerPrefs.SetInt("starUnlockForLevel" + levelNum + "ep" + epNum, value);
    }
    #endregion

    public static void setMultiplayerWinCountIncrement()
    {
        PlayerPrefs.SetInt(MULTIPLAYER_WIN_COUNTER, getMultiplayerWinCount() + 1);
    }

    public static int getMultiplayerWinCount()
    {
        return PlayerPrefs.GetInt(MULTIPLAYER_WIN_COUNTER, 0);
    }



    public static void setIsOldValuesCopy(int value)
    {
        PlayerPrefs.SetInt(OLD_VALUES, 1);
    }
    public static int getIsOldValuesCopy()
    {
        return PlayerPrefs.GetInt(OLD_VALUES, 0);
    }
    public static void SetEnergyRefillFlow_Rad(int val)
    {

        PlayerPrefs.SetInt("EnergyRefillFlow_Rad", val);
    }

    public static int GetEnergyRefillFlow_Rad()
    {
        return (PlayerPrefs.GetInt("EnergyRefillFlow_Rad", 0));
    }

    #region Booster
    public static bool IsBoosterTutorialShown()
    {
        return PlayerPrefs.GetInt("BoosterTutorial", 0) == 1;
    }

    public static void BoosterTutorialShown()
    {
        PlayerPrefs.SetInt("BoosterTutorial", 1);
    }
    #endregion

    #region GDPR
    public static bool CheckGDPRStatus()
    {
        return PlayerPrefs.GetInt("GDPR", 0) == 1;
    }

    public static void SetGDPRStatus(bool val)
    {
        if (val)
        {
            PlayerPrefs.SetInt("GDPR", 1);
        }
        else
        {
            PlayerPrefs.SetInt("GDPR", 0);
        }
    }
    #endregion

    #region DLC
    public static void SetChapterDownloadedStatus(int chap, bool status)
    {
        if (status)
        {
            PlayerPrefs.SetInt("Chap" + chap, 1);
        }
        else
        {
            PlayerPrefs.SetInt("Chap" + chap, 0);
        }
    }

    public static bool GetChapterDownloadedStatus(int chap)
    {
        return PlayerPrefs.GetInt("Chap" + chap, 0) == 1;
    }

    #endregion

    #region FirebaseAnalytics

    public static void SetFirebaseConsentValue(int value)
    {
        PlayerPrefs.SetInt("Firebase_consent", value);
    }
    public static bool GetFirebaseConsentValue()
    {
        return PlayerPrefs.GetInt("Firebase_consent", 0) == 1;
    }

    #endregion

    #region TouchSensitivity // © Hamza-Senpai

    public static void SetTouchSensitivityValue(float value)
    {
        PlayerPrefs.SetFloat("Touch_Sensitivity", value);
    }
    public static float GetTouchSensitivityValue()
    {
        return PlayerPrefs.GetFloat("Touch_Sensitivity", 1.5f);
    }

    #endregion
    public static bool GetLevelCompletedStatus(int chapterNo, int levelNo)
	{
        if(Constant.allLevelsUnlocked)
		{
            return true;
		}
        return (PlayerPrefs.GetInt("Chap" + chapterNo + "Level" + levelNo, 0) == 1);
	}
    public static void SetLevelCompletedStatus(int chapterNo, int levelNo)
    {
        PlayerPrefs.SetInt("Chap" + chapterNo + "Level" + levelNo, 1);
    }
    public static int GetCheckVgpValue()
    {
        return PlayerPrefs.GetInt("CheckVgpValue", 0);
    }
    public static void SetFlyingInventoryTutorialPlayed(int val)
    {
        PlayerPrefs.SetInt("FlyingInventoryTutorial", val);
    }
    public static bool GetFlyingInventoryTutorialPlayed()
    {
        return (PlayerPrefs.GetInt("FlyingInventoryTutorial", 0) == 1);
    }
    public static void SetCheckVgpValue(int value)
    {
        PlayerPrefs.SetInt("CheckVgpValue", value);
    }
    public static int Chapter5VGPcount
    {
        get
        {
            return PlayerPrefs.GetInt("Chapter5VGP", 0);
        }

        set
        {
            PlayerPrefs.SetInt("Chapter5VGP", value);
        }
    }

    public static int ChristmasOneChapterBundleVGPcount
    {
        get
        {
            return PlayerPrefs.GetInt("ChristmasOneChapterBundleVGP", 0);
        }

        set
        {
            PlayerPrefs.SetInt("ChristmasOneChapterBundleVGP", value);
        }
    }
    public static int HalloweenBundleSpecialOfferVGPcount
    {
        get
        {
            return PlayerPrefs.GetInt("ChristmasOneChapterBundleVGP", 0);
        }

        set
        {
            PlayerPrefs.SetInt("ChristmasOneChapterBundleVGP", value);
        }
    }

    public static void SetChapter5VGPbool(int val)
    {
        PlayerPrefs.SetInt("Chapter5VGPcheckforUS", val);
    }
    public static bool GetChapter5VGPbool()
    {
        return (PlayerPrefs.GetInt("Chapter5VGPcheckforUS", 0) == 1);
    }

    public static void SetAllGameVGPStatus(int val)
    {
        PlayerPrefs.SetInt(ALL_GAME_UNLOCKED_VGP, val);
    }

    public static bool GetAllGameVGPStatus()
    {
        return (PlayerPrefs.GetInt(ALL_GAME_UNLOCKED_VGP, 0) == 1);
    }
}