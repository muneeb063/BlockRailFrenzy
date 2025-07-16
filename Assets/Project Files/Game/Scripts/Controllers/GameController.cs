using UnityEngine;
using Watermelon.BusStop;
using Watermelon.SkinStore;
using UnityEngine.SceneManagement;
using System.Collections;
using Unity.VisualScripting;
//using ByteBrewSDK;
using System.Collections.Generic;
using GameAnalyticsSDK;
using System;
namespace Watermelon
{
    public class GameController : MonoBehaviour
    {
        private static GameController gameController;
        public GameObject Tunnelpanel, PassangerPanel;
        [DrawReference]
        [SerializeField] GameData data;

        [Space]
        [SerializeField] UIController uiController;
        [SerializeField] MusicSource musicSource;

        private ParticlesController particlesController;
        private LevelController levelController;
        private TutorialController tutorialController;
        private PUController powerUpsController;
        private SkinsController skinController;
        private SkinStoreController skinStoreController;

        private static bool isGameActive;
        public static bool IsGameActive => isGameActive;

        public static event SimpleCallback OnLevelChangedEvent;
        private static LevelSave levelSave;
        public static bool RemoveTrain = false;
        public static GameData Data => gameController.data;

        [Button]
        public void Reload()
        {
            SceneManager.LoadScene("Game");
        }

        private void Awake()
        {
            gameController = this;
            PassengerActionManager.instance.ClearLists();
            levelSave = SaveController.GetSaveObject<LevelSave>("level");

            // Cache components
            CacheComponent(out particlesController);
            CacheComponent(out levelController);
            CacheComponent(out tutorialController);
            CacheComponent(out powerUpsController);
            CacheComponent(out skinController);
            CacheComponent(out skinStoreController);

            uiController.Init();

            particlesController.Init();

            tutorialController.Init();
            powerUpsController.Init();

            skinController.Init();
            skinStoreController.Init(skinController);

            musicSource.Init();
            musicSource.Activate();

            uiController.InitPages();

            // Add raycast controller component
            RaycastController raycastController = gameObject.AddComponent<RaycastController>();
            raycastController.Init();

            levelController.Init();
        }
        public static void ResetStuff()
        {
            isGameActive = false;
            levelSave.ReplayingLevelAgain = true;

            LoadLevel();
        }
        private void Start()
        {
            PlayerPrefs.SetInt("BusHead", 0);
               // UIController.ShowPage<UIMainMenu>();

            LoadLevel(() =>
            {
                GameLoading.MarkAsReadyToHide();
            });
        }

        private void OnDestroy()
        {
            EnvironmentBehavior.UnloadSpawnedBusses();
        }

        private static void LoadLevel(System.Action OnComplete = null)
        {
            gameController.levelController.LoadLevel(() =>
            {
                OnComplete?.Invoke();
            });
            
            OnLevelChangedEvent?.Invoke();
        }
        public static void ResetGame()
        {
            isGameActive = false;
            levelSave.ReplayingLevelAgain = true;
        }
        public static void StartGame()
        {
            // On Level is loaded
            isGameActive = true;

            UIController.HidePage<UIMainMenu>();
            UIController.ShowPage<UIGame>();
            MaxAdsManager.instance.ShowBanner();
            customManagerScript.instance.PlayUIAnimator(customManagerScript.instance.uiGames.gameObject);
            //Tween.DelayedCall(2f, LivesManager.RemoveLife);
        }
       
        public static void LoseGame()
        {
            if (!isGameActive)
                return;

            isGameActive = false;

            RaycastController.Disable();
            int value = LevelController.DisplayLevelNumber + 1;
            var EventParameters = new Dictionary<string, string>()
        {
            {
                "LevelFailed",value.ToString()
            }
        };
          //  ByteBrew.NewCustomEvent("Level", EventParameters);
            customManagerScript.instance.LogEvent_NonRevenue("Level_" + value + "_failed");
            customManagerScript.instance.LevelFailed("TrainStation", "Level" + value);
            UIController.HidePage<UIGame>();
            UIController.ShowPage<UIGameOver>();
            PassengerActionManager.instance.ClearLists();
            customManagerScript.instance.isLevelPaused = true;
            customManagerScript.instance.isTimerStart = false;
            AudioController.PlaySound(AudioController.AudioClips.failSound);

            levelSave.ReplayingLevelAgain = true;
            if (customManagerScript.instance.isTimerZero == true)
            {
                FindAnyObjectByType<UIGameOver>().OutOfTime.SetActive(true);
                FindAnyObjectByType<UIGameOver>().OutofMoves.SetActive(false);
            }
            else if (customManagerScript.instance.isLevelFailed == true)
            {
                customManagerScript.instance.isLevelFailed = false;
                FindAnyObjectByType<UIGameOver>().ReduceLife();
                FindAnyObjectByType<UIGameOver>().FailedPanel.SetActive(true);
            }
            else
            {
                FindAnyObjectByType<UIGameOver>().OutofMoves.SetActive(true);
                FindAnyObjectByType<UIGameOver>().OutOfTime.SetActive(false);
            }
        }
        public static void WinGame()
        {
            if (!isGameActive)
                return;
            //PlayerPrefs.SetInt("IsFirst", 1);
            isGameActive = false;
            RemoveTrain = true;
            RaycastController.Disable();
            int value = LevelController.DisplayLevelNumber + 1;
            var EventParameters = new Dictionary<string, string>()
        {
            {
                "LevelCompleted",value.ToString()
            }
        };
            //ByteBrew.NewCustomEvent("Level", EventParameters);
            customManagerScript.instance.LogEvent_NonRevenue("Level_" +value + "_Completed");
            customManagerScript.instance.LevelComplete("TrainStation", "Level" + value);
            levelSave.ReplayingLevelAgain = false;

            Watermelon.BusStop.LevelData completedLevel = LevelController.LoadedStageData;
            /*  customManagerScript.count++;
              if (customManagerScript.count % 3 == 0)
              {
                  customManagerScript.instance.AdDelayInter();
              }*/
            customManagerScript.instance.Confeti.GetComponent<ParticleSystem>().Play();
            customManagerScript.instance.isTimerStart = false;
            UIController.HidePage<UIGame>();
            UIController.ShowPage<UIComplete>();
            //customManagerScript.instance.GADesigncallback("level_complete"); // Event ID 01
            //customManagerScript.instance.GADesigncallback($"level_number:{value}"); // Event ID 02

            GameAnalytics.NewDesignEvent($"session_start:level_load_{value}:level_start_{value}:result:win");
            GameAnalytics.NewDesignEvent($"level_{value}:result:win");
            GameAnalytics.NewDesignEvent($"win");
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Complete, $"level_{value}");
            Debug.Log($"GA Progression Event: COMPLETE level_{value}");

            //customManagerScript.instance.GADesigncallback($"level_complete_time:{value}:{customManagerScript.instance.remainingTime:F2}");
            //customManagerScript.instance.GADesigncallback($"level_{value}_CT_{value}_{customManagerScript.instance.remainingTime:F2}");
            //Debug.LogError(value);
            customManagerScript.instance.unlockUIFeature.SetLevel(value);
            customManagerScript.instance.PlayUIAnimator(customManagerScript.instance.LevelCompleteui);
            AudioController.PlaySound(AudioController.AudioClips.completeSound);
            SaveController.Save();
            PassengerActionManager.instance.ClearLists();
        }
       
      
        public static void LoadNextLevel()
        {
            if (isGameActive)
                return;

            gameController.levelController.AdjustLevelNumber();
            customManagerScript.instance.StartTimer();
            //UIController.ShowPage<UIMainMenu>();

            levelSave.ReplayingLevelAgain = false;

            //AdsManager.ShowInterstitial(null);

            LoadLevel();
        }

        public static void ReplayLevel()
        {
            isGameActive = false;

            //UIController.ShowPage<UIMainMenu>();

            levelSave.ReplayingLevelAgain = true;

            //AdsManager.ShowInterstitial(null);

            LoadLevel();
        }

        public static void RefreshLevelDev()
        {
            UIController.ShowPage<UIGame>();
            customManagerScript.instance.PlayUIAnimator(customManagerScript.instance.uiGames.gameObject);
            levelSave.ReplayingLevelAgain = true;
            MaxAdsManager.instance.ShowBanner();
            LoadLevel();
        }

        private void OnApplicationQuit()
        {
            // to make sure we will load similar level next time game launched (in case we outside level bounds)
            levelSave.ReplayingLevelAgain = true;
        }

        #region Extensions
        public bool CacheComponent<T>(out T component) where T : Component
        {
            Component unboxedComponent = gameObject.GetComponent(typeof(T));

            if (unboxedComponent != null)
            {
                component = (T)unboxedComponent;

                return true;
            }

            Debug.LogError(string.Format("Scripts Holder doesn't have {0} script added to it", typeof(T)));

            component = null;

            return false;
        }
        #endregion
    }
}