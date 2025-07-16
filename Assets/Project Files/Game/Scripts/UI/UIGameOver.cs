//using ByteBrewSDK;
using System.Collections.Generic;
using UIAnimatorCore;
using UIAnimatorDemo;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;
using Watermelon.BusStop;
using GameAnalyticsSDK;
namespace Watermelon
{
    public class UIGameOver : UIPage
    {
        [SerializeField] UIScaleAnimation levelFailedScalable;
        [SerializeField] UIScaleAnimation heartScalable;

        [SerializeField] UIFadeAnimation backgroundFade;

        [SerializeField] UIScaleAnimation replayButtonScalable;
        [SerializeField] Button replayButton;

        private TweenCase replayPingPongCase;
        public GameObject MainMenu, OutofMoves, FailedPanel, OutOfTime;
        public override void Init()
        {
            replayButton.onClick.AddListener(ReplayButton);
        }

        #region Show/Hide

        public override void PlayShowAnimation()
        {

            levelFailedScalable.Hide(immediately: true);
            heartScalable.Hide(immediately: true);
            replayButtonScalable.Hide(immediately: true);

            float fadeDuration = 0.3f;
            backgroundFade.Show(fadeDuration);

            Tween.DelayedCall(fadeDuration * 0.8f, delegate {

                levelFailedScalable.Show(scaleMultiplier: 1.1f);
                heartScalable.Show(scaleMultiplier: 1.1f);

                replayButtonScalable.Show(scaleMultiplier: 1.05f);

                replayPingPongCase = replayButtonScalable.Transform.DOPingPongScale(1.0f, 1.05f, 0.9f, Ease.Type.QuadIn, Ease.Type.QuadOut, unscaledTime: true);

                UIController.OnPageOpened(this);
            });
        }

        public override void PlayHideAnimation()
        {
            backgroundFade.Hide(0.3f);

            Tween.DelayedCall(0.3f, delegate {

                if (replayPingPongCase != null && replayPingPongCase.IsActive) replayPingPongCase.Kill();

                UIController.OnPageClosed(this);
            });
        }

        #endregion

        #region Buttons 
        public void GiveExtraTime()
        {

            MaxAdsManager.instance.ShowRewardedVideo(TimeReward);
        }
        void TimeReward()
        {
            customManagerScript.instance.Timer.color = Color.white;
            customManagerScript.instance.isLevelPaused = false;
            customManagerScript.instance.AdPlace = "ExtraTimeRV";
            LivesManager.AddLife();
            customManagerScript.instance.remainingTime += 20f;
            customManagerScript.instance.isTimerStart = true;
            customManagerScript.instance.isTimerZero = false;
            UIController.HidePage<UIGameOver>();
            UIController.ShowPage<UIGame>();
            customManagerScript.instance.PlayUIAnimator(customManagerScript.instance.uiGames.gameObject);
            RaycastController.Enable();
        }
        public void ReduceLife()
        {
            LivesManager.RemoveLife();
            int value = LevelController.DisplayLevelNumber + 1;
            //customManagerScript.instance.GADesigncallback("level_fail"); // Event ID 01

            //customManagerScript.instance.GADesigncallback($"level_number:{value}"); // Event ID 02

            //customManagerScript.instance.GADesigncallback($"level_fail_time:{value}:{customManagerScript.instance.remainingTime:F2}"); // Event ID 03

            //Full Funnel
            GameAnalytics.NewDesignEvent($"session_start:level_load_{value}:level_start_{value}:result:fail");
            GameAnalytics.NewDesignEvent($"level_{value}:result:fail");
            GameAnalytics.NewDesignEvent($"fail");
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Fail, $"level_{value}");
            Debug.Log($"GA Design Event: level_{value}:result:fail");
        }
        public void ReplayButton()
        {
            customManagerScript.instance.Timer.color = Color.white;
            customManagerScript.instance.isRevived = false;
            AudioController.PlaySound(AudioController.AudioClips.buttonSound);
            //FindAnyObjectByType<EnvironmentBehavior>().LockAllSlots();
            customManagerScript.instance.DocksStates();
            // UIController.HidePage<UIGameOver>();
            
            /*if (LivesManager.IsMaxLives)
                LivesManager.RemoveLife();

            GameController.ReplayLevel();*/
            if (LivesManager.Lives > 0)
            {
                customManagerScript.instance.isLevelPaused = false;
                UIController.HidePage<UIGameOver>();
                UIController.ShowPage<UIGame>();
                MaxAdsManager.instance.ShowBanner();
                customManagerScript.instance.PlayUIAnimator(customManagerScript.instance.uiGames.gameObject);
                customManagerScript.instance.StartTimer();
                //GameController.ReplayLevel();
                GameController.RefreshLevelDev();
                customManagerScript.instance.isTimerZero = false;
                /* if (customManagerScript.instance.isTimerZero == false)
                 {
                     OutofMoves.SetActive(true);
                 }
                 else
                 {
                     OutOfTime.SetActive(true);
                 }*/
                customManagerScript.instance.StartBombTimers();
                FailedPanel.SetActive(false);
                //customManagerScript.instance.GADesigncallback("LevelStart");
                int value = LevelController.DisplayLevelNumber + 1;
                //customManagerScript.instance.GADesigncallback($"level_number:{value}"); // Event ID 02
                GameAnalytics.NewDesignEvent($"session_start:level_load_{value}");
                GameAnalytics.NewDesignEvent($"level_load_{value}");
                Debug.Log($"GA Design Event: level_load_{value}");

                GameAnalytics.NewDesignEvent($"session_start:level_load_{value}:level_start_{value}");
                GameAnalytics.NewDesignEvent($"level_start_{value}");
                Debug.Log($"GA Design Event: level_start_{value}");
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, $"level_{value}");

            }
            else 
            {
                MainMenu.SetActive(true);
                UIController.HidePage<UIGameOver>();
                //gameObject.transform.GetChild(1).gameObject.SetActive(true);
                OutofMoves.SetActive(false);
                OutOfTime.SetActive(false);
                FailedPanel.SetActive(false);
                UIController.ShowPage<UIMainMenu>();
                customManagerScript.instance.isLevelPaused = true;
                customManagerScript.instance.levelscroller.ScrollToCurrentLevel();
                MaxAdsManager.instance.HideBanner();
                customManagerScript.instance.PlayUIAnimator(MainMenu);
                GameController.ResetStuff();
                int value = LevelController.DisplayLevelNumber + 1;
                var EventParameters = new Dictionary<string, string>()
        {
            {
                "LevelLoaded",value.ToString()
            }
        };
               // ByteBrew.NewCustomEvent("Level", EventParameters);
                customManagerScript.instance.LogEvent_NonRevenue("Level_" + value + "_Loaded");
                GameAnalytics.NewDesignEvent($"session_start:level_load_{value}");

                GameAnalytics.NewDesignEvent($"level_load_{value}");

                // PlayerPrefs.SetInt("IsFirst", 1);
            }
        }
        void GiveReward()
        {
            customManagerScript.instance.isLevelPaused = false;
            customManagerScript.instance.isTimerStart = true;
            customManagerScript.instance.AdPlace = "UnlockSlotsPanelRV";
            /* if (customManagerScript.instance.Count == 0)
             {
              *//*   FindAnyObjectByType<EnvironmentBehavior>().Slot1Unlock();
                 FindAnyObjectByType<EnvironmentBehavior>().Slot2Unlock();*//*
                 LockedSlotClicked[] lockedSlotsActive = FindObjectsOfType<LockedSlotClicked>();
                 foreach (LockedSlotClicked slot in lockedSlotsActive)
                 {
                     slot.UnlockSlots();
                 }
                 customManagerScript.instance.Count++;
             }
             else
             {
                 LockedSlotClicked[] lockedSlotsActive = FindObjectsOfType<LockedSlotClicked>();
                 foreach (LockedSlotClicked slot in lockedSlotsActive)
                 {
                     slot.UnlockSlots();
                 }
                 customManagerScript.instance.Count = 0;

             }*/
            if (customManagerScript.instance.lockedSlots.Count >= 2)
            {
                customManagerScript.instance.lockedSlots[0].UnlockSlots();
                customManagerScript.instance.lockedSlots[1].UnlockSlots();
                FindAnyObjectByType<EnvironmentBehavior>().Dock.ShiftAllLeft();
                // Remove the higher index first to avoid shifting
                customManagerScript.instance.lockedSlots.RemoveAt(1);
                customManagerScript.instance.lockedSlots.RemoveAt(0);
            }
            else if (customManagerScript.instance.lockedSlots.Count == 1)
            {
                customManagerScript.instance.lockedSlots[0].UnlockSlots();
                customManagerScript.instance.lockedSlots.RemoveAt(0);
                FindAnyObjectByType<EnvironmentBehavior>().Dock.ShiftAllLeft();
            }
            else
            {
                return;
            }
            PassengerActionManager.instance.DiffuseBombs();
            PUController.UsePowerUpCustom(PUType.Hint);
            RaycastController.Enable();
            LivesManager.AddLife();
            UIController.HidePage<UIGameOver>();
            UIController.ShowPage<UIGame>();
            customManagerScript.instance.PlayUIAnimator(customManagerScript.instance.uiGames.gameObject);
            FindAnyObjectByType<EnvironmentBehavior>().CheckMatch();
            LevelController.ResetLevelElementsPosition();
        }
        void placeElemet()
        {
            /* for (int i = 0; i < LevelController.levelElements.Count; i++)
             {

                 LevelController.PlaceElementOnMap(LevelController.levelElements[i], LevelController.levelElements[i].ElementPosition);
                 // Debug.LogError(i);
             }*/
        }
        void enableRayCast()
        {
            RaycastController.Enable();
        }
        public void GiveExtraLife()
        {
            // count++;
            MaxAdsManager.instance.ShowRewardedVideo(GiveReward);
            /*  LivesManager.AddLife();
              UIController.HidePage<UIGameOver>();
              UIController.ShowPage<UIGame>();
              PUController.UsePowerUpCustom(PUType.Hint);
              //GameController.ResetGame();
              RaycastController.Enable();
              FindAnyObjectByType<EnvironmentBehavior>().Slot1Unlock();
              FindAnyObjectByType<EnvironmentBehavior>().Slot2Unlock();
              FindAnyObjectByType<EnvironmentBehavior>().CheckMatch();*/
            /*   if (count == 1)
               {
                   FindAnyObjectByType<EnvironmentBehavior>().Slot1Unlock();
               }
               if (count == 2) {
                   FindAnyObjectByType<EnvironmentBehavior>().Slot2Unlock();
                   count = 0;
               }*/
            //GameController.ReplayLevel();
        }
        #endregion
    }
}