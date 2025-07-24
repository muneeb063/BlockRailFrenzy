//using ByteBrewSDK;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watermelon;
using Watermelon.BusStop;
using Watermelon.IAPStore;
using UnityEngine.UI;
using UIAnimatorCore;
using UIAnimatorDemo;
using DG.Tweening;
using UnityEngine.Video;
using System;
using GameAnalyticsSDK;

public class customManagerScript : MonoBehaviour
{
    // Start is called before the first frame update
    public static customManagerScript instance;
    public static int count;
    public static bool isAppOpenShown = false;
    public UIMainMenu mainmenu;
    public GameObject Settings;
    public GameObject LevelCompleteui;
    public Camera mainCamera;
    public GameObject noLives;
    public UIGame uiGames;
    public bool isRevived = false;
    public bool shouldClick = false;
    public HumanoidCharacterBehavior lastcharacter;
    public List<HumanoidCharacterBehavior> characterBehaviors;
    public GameObject SitParticle;
    public GameObject Confeti;
    public float remainingTime;
    public Text Timer;
    public bool isTimerZero = false;
    public bool isTimerStart = false;
    public float[] LevelsTimes;
    public Canvas TutorialCanvas;
    public float AnimationCounter=10f;
    public GameObject container;
    private bool animationStarted = false;
    public bool isTimerPowerUpUsed = false;
    public ComboSpriteManager comboSpriteManager;
    public List<HumanoidCharacterBehavior> passangers;
    public int Count = 0;
    public GameObject FreezedEffectImage, FreezedPng;
    public GameObject LevelArtData;
    public GameObject TutorialBlackScreen, UndoBtnHand;
    public FeatureUnlocker featureUnlocker;
    public List<LockedSlotClicked> lockedSlots;
    public VideoPlayer videoPlayer;
    public LevelListManager levelscroller;
    public GameObject purchaseFailPanel, PurchaseCompletePanel;
    public FeatureUnlockUI unlockUIFeature;
    public string AdPlace = "Null";
    public bool isHammerPowerupEnabled = false;
    public GameObject HammerUI;
    public bool isLevelPaused = false;
    public bool isLevelFailed = false;
    public float countdownTime = 15f; // In seconds
    private Coroutine countdownCoroutine;

    public Text timerText; // Optional: assign in Inspector to show countdown
    public GameObject FeatureSliderObj;
    public int TrainFillCount;
    public int TrainCount = 0;
    void Start()
    {
        //PassengerActionManager.instance.ClearLists();
        MaxAdsManager.instance.ShowBanner();
        if (instance == null)
        {
            instance = this;
        }
        TutorialCanvasController.SetParent(uiGames.transform);
        //customManagerScript.instance.GADesigncallback("SessionStart");
        if (PlayerPrefs.GetInt("IsFirst") == 0)
        {
            MaxAdsManager.instance.ShowBanner();
            PlayerPrefs.SetInt("IsFirst", 1);
            UIController.ShowPage<UIGame>();
            customManagerScript.instance.PlayUIAnimator(uiGames.gameObject);
            UIController.HidePage<UIMainMenu>();
            mainmenu.TapToPlayButton();
            MaxAdsManager.instance.Log_GA_Event("UniqueEvent");
            int value = LevelController.DisplayLevelNumber + 1;
            customManagerScript.instance.LevelStart("TrainStation","Level"+value);
            /*var EventParameters = new Dictionary<string, string>()
        {
            {
                "LevelStarted",value.ToString()
            }
        };
            ByteBrew.NewCustomEvent("Level", EventParameters);*/
            customManagerScript.instance.LogEvent_NonRevenue("Level_" + value + "_Started");
            //customManagerScript.instance.GADesigncallback("LevelStart");
            //customManagerScript.instance.GADesigncallback($"level_number:{value}"); // Event ID 02

            GameAnalytics.NewDesignEvent($"session_start:level_load_{value}:level_start_{value}");
            GameAnalytics.NewDesignEvent($"level_start_{value}");
            Debug.Log($"GA Design Event: level_start_{value}");
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, $"level_{value}");

        }
        else if (PlayerPrefs.GetInt("IsFirst") == 1)
        {
            customManagerScript.instance.isLevelPaused = true;
            UIController.ShowPage<UIMainMenu>();
            MaxAdsManager.instance.HideBanner();
            customManagerScript.instance.PlayUIAnimator(mainmenu.gameObject);
            customManagerScript.instance.levelscroller.ScrollToCurrentLevel();
            int value = LevelController.DisplayLevelNumber + 1;
            var EventParameters = new Dictionary<string, string>()
        {
            {
                "LevelLoaded",value.ToString()
            }
        };
            //ByteBrew.NewCustomEvent("Level", EventParameters);
            customManagerScript.instance.LogEvent_NonRevenue("Level_" + value + "_Loaded");

            GameAnalytics.NewDesignEvent($"session_start:level_load_{value}");

            GameAnalytics.NewDesignEvent($"level_load_{value}");

            if (isAppOpenShown == false)
            {
                isAppOpenShown = true;
                // MaxAdsManager.instance.ShowAppOpen();
            }
        }
        
    }
    public void StartBombTimers()
    {
        //Invoke(nameof(BombTimerDelay), 2f);
        BombTimerDelay();
    }
    void BombTimerDelay()
    {
        
        PassengerActionManager.instance.StartBombTimers();
    }
    public void HammerUIState(bool state)
    {
        HammerUI.SetActive(state);
    }
    public void GADesigncallback(string s)
    {
        MaxAdsManager.instance.GADesignEvent(s);
    }
    public void PurchaseSuccess()
    {
        StartCoroutine(PurchaseCallback(PurchaseCompletePanel));
    }
    public void PurchaseFailed()
    {
        StartCoroutine(PurchaseCallback(purchaseFailPanel));
    }
    IEnumerator PurchaseCallback(GameObject purchasestatus)
    {
        purchasestatus.SetActive(true);
        yield return new WaitForSeconds(3f);
        purchasestatus.SetActive(false);
    }
    public void DocksStates()
    {
        if (PlayerPrefs.GetInt("Slot1Status") == 0)
        {
            FindAnyObjectByType<EnvironmentBehavior>().LockSlot1();
        }
        if (PlayerPrefs.GetInt("Slot2Status") == 0)
        {
            FindAnyObjectByType<EnvironmentBehavior>().LockSlot2();
        }
        FindAnyObjectByType<EnvironmentBehavior>().LockAllSlots();
        customManagerScript.instance.FreezedEffectImage.SetActive(false);
        customManagerScript.instance.FreezedPng.SetActive(false);
        
        customManagerScript.instance.lockedSlots.Clear();
        LockedSlotClicked[] lockedslotss = GameObject.FindObjectsOfType<LockedSlotClicked>();
        foreach (LockedSlotClicked item in lockedslotss)
        {
            customManagerScript.instance.lockedSlots.Add(item);
        }
    }
    public void EnableUndoTutorial()
    {
        TutorialBlackScreen.SetActive(true);
        UndoBtnHand.SetActive(true);
        HumanoidCharacterBehavior[] passangers = FindObjectsOfType<HumanoidCharacterBehavior>();
        foreach (HumanoidCharacterBehavior passanger in passangers)
        {
            passanger.gameObject.GetComponent<BoxCollider>().enabled = false;
        }
    }
    public void DisableUndoTutorial()
    {
        TutorialBlackScreen.SetActive(false);
        UndoBtnHand.SetActive(false);
        HumanoidCharacterBehavior[] passangers = FindObjectsOfType<HumanoidCharacterBehavior>();
        foreach (HumanoidCharacterBehavior passanger in passangers)
        {
            passanger.gameObject.GetComponent<BoxCollider>().enabled = true;
        }
    }
    void AnimateDefaultElement()
    {
        PUUIBehavior[] behaviors = container.GetComponentsInChildren<PUUIBehavior>(true);

        if (behaviors.Length == 0)
        {
            Debug.LogWarning("No PUUIBehavior components found under container.");
            return;
        }

        GameObject defaultObj = behaviors[0].defaultElementsObjects;

        if (defaultObj != null)
        {
            Debug.Log("Countdown complete. Animating 'defaultElementsObjects'...");

            defaultObj.transform.DOScale(1.2f, 0.5f)
                .SetLoops(-1, LoopType.Yoyo)
                .SetEase(DG.Tweening.Ease.InOutSine);
        }
        else
        {
            Debug.LogWarning("defaultElementsObjects reference is null in PUUIBehavior.");
        }
    }
    public void AnimatePowerUps()
    {
       // if (animationStarted || container == null) return;

        AnimationCounter -= Time.deltaTime;

        // Debug: print time in whole seconds (optional)
        int secondsLeft = Mathf.CeilToInt(AnimationCounter);
        Debug.Log("Time left: " + secondsLeft + "s");

        if (AnimationCounter <= 0f)
        {
          //  animationStarted = true;
            AnimateDefaultElement();
        }
    }
    public void StartTimer()
    {
        if (LevelController.DisplayLevelNumber + 1 >= 50)
        {
            remainingTime = 300;

        }
        else
        {
            remainingTime = LevelsTimes[LevelController.DisplayLevelNumber];
        }
        isTimerStart = true;
    }
    public void DisableTimer()
    {
       /* if (*//*TutorialCanvas.enabled == true &&*//* LevelController.DisplayLevelNumber + 1 <= 5 )
        {
            Timer.gameObject.transform.parent.gameObject.SetActive(false);
            return;    
        }
        else
        {
            Timer.gameObject.transform.parent.gameObject.SetActive(true);
        }*/
    }
    public void StopTimer()
    {
        isTimerStart = false;
        customManagerScript.instance.isLevelPaused = true;
    }
    public void ResumeTimer()
    {
        isTimerStart = true;
    }
    void TimerWorking()
    {
        if (isTimerStart == true)
        {
            if (remainingTime > 0)
            {
                remainingTime -= Time.deltaTime;

                int minutes = Mathf.FloorToInt(remainingTime / 60f);
                int seconds = Mathf.FloorToInt(remainingTime % 60f);

                Timer.text = string.Format("{0:00}:{1:00}", minutes, seconds);
                if (remainingTime < 10)
                {
                    Timer.color = Color.red;
                }
            }
            else
            {
                Timer.text = "00:00";
                isTimerZero = true;
                GameController.LoseGame();
                //Debug.LogError("LevelFailed");
                /* TimeUpScreen.SetActive(true);
                 SoundManager.Instance.PlayLevelFail();*/
            }
        }
    }
    public void ResetClick()
    {
        Debug.LogError("CanClickNow");
        shouldClick = false;
    }
    public void ClickCallback()
    {
        // Invoke(nameof(ResetClick), 2f);
    }
    public void PlayUIAnimator(GameObject obj)
    {
        if (obj.GetComponent<UIAnimator>())
        {
            obj.GetComponent<UIAnimator>().PlayAnimation(AnimSetupType.Intro);
        }
    }
    void setcam()
    {
      
    }
    // Update is called once per frame
    void Update()
    {
        if (LevelController.DisplayLevelNumber > 29)
        {
            FeatureSliderObj.SetActive(false);
        }
        DisableTimer();
        TimerWorking();
       // ViewHandle();
        //  AnimatePowerUps();
        //Debug.LogError("Lives " + Watermelon.LivesManager.Lives);
        // Debug.LogError("LevelElements " + LevelController.levelElements.Count);
        if (isTimerPowerUpUsed && countdownCoroutine == null)
        {
            countdownCoroutine = StartCoroutine(StartCountdown());
        }

        // Stop timer if flag becomes false
        if (!isTimerPowerUpUsed && countdownCoroutine != null)
        {
            StopCoroutine(countdownCoroutine);
            countdownCoroutine = null;

            if (timerText != null)
                timerText.text = "00:00"; // Reset text on stop
        }
    }
    IEnumerator StartCountdown()
    {
        float timer = countdownTime;

        while (isTimerPowerUpUsed)
        {
            while (timer > 0f)
            {
                if (timerText != null)
                {
                    TimeSpan time = TimeSpan.FromSeconds(timer);
                    timerText.text = time.ToString(@"mm\:ss");
                }

                Debug.Log("Timer: " + Mathf.CeilToInt(timer));
                yield return new WaitForSeconds(1f);
                timer -= 1f;
            }

            timer = countdownTime; // Reset timer to 15 again
        }

        countdownCoroutine = null;
    }
    void CheckLives()
    {
        if (FindAnyObjectByType<LivesManager>().lives == 0)
        {

        }
    }
    public void UpdateLevelData()
    {
        uiGames.UpdateLevelNumber();
    }
    public void UpdateTxt()
    {
        uiGames.UpdateJustTExt();
    }
    void ViewHandle()
    {
        if ((LevelController.DisplayLevelNumber + 1) == 13 || (LevelController.DisplayLevelNumber + 1) == 21 || (LevelController.DisplayLevelNumber + 1) == 27 || (LevelController.DisplayLevelNumber + 1) == 31 || (LevelController.DisplayLevelNumber + 1) == 54|| (LevelController.DisplayLevelNumber + 1) == 64|| (LevelController.DisplayLevelNumber + 1) == 69|| (LevelController.DisplayLevelNumber + 1) == 70|| (LevelController.DisplayLevelNumber + 1) == 87|| (LevelController.DisplayLevelNumber + 1) == 88 || (LevelController.DisplayLevelNumber + 1) == 71 || (LevelController.DisplayLevelNumber + 1) == 104 || (LevelController.DisplayLevelNumber + 1) == 109)
        {
            mainCamera.fieldOfView = 53;
        }
        else
        {
            mainCamera.fieldOfView = 45;
        }
    }
    public void LoadRewardedVideo()
    {
        MaxAdsManager.instance.LoadRewardedAd();
    }
    public void AdDelayInter()
    {
        Invoke(nameof(ShowInterstitial), 2f);
    }
    public void ShowInterstitial()
    {
        MaxAdsManager.instance.ShowInterstitial();
    }
    public void ShowRewardVideo()
    {
        //MaxAdsManager.instance.ShowRewardedVideo();
    }
    public void LevelComplete(string s,string lvl)
    {
        MaxAdsManager.instance.CustomAnalytics.GameWin(s,lvl);
    }
    public void LevelFailed(string s, string lvl)
    {
        MaxAdsManager.instance.CustomAnalytics.GameLoose(s,lvl);
    }
    public void LevelStart(string s, string lvl)
    {
        MaxAdsManager.instance.CustomAnalytics.GameStart(s,lvl);
    }
    public void LogEvent_NonRevenue(string s)
    {
        MaxAdsManager.instance.CustomAnalytics.LogEvent_NonRevenue(s);
    }
    public void ShowFeatureUnlocked()
    {
        int level = LevelController.DisplayLevelNumber + 1;
        PlayerPrefs.SetInt("CurrentLevel", level);
        featureUnlocker.CheckAndShowFeatureUnlock();
    }
}
