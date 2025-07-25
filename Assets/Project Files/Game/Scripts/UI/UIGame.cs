//using ByteBrewSDK;
using System;
using System.Collections.Generic;
using TMPro;
using UIAnimatorDemo;
using UnityEngine;
using UnityEngine.UI;
using Watermelon.BusStop;

namespace Watermelon
{
    public class UIGame : UIPage
    {
        [SerializeField] RectTransform safeZoneTransform;
        [SerializeField] PUUIController powerUpsUIController;
        public PUUIController PowerUpsUIController => powerUpsUIController;

        [SerializeField] Button replayButton;
        [SerializeField] UILevelQuitPopUp exitPopUp;

        [SerializeField] TextMeshProUGUI levelText;
        public Text LevelText;
        public Text winPanelText;
        public Text Gamoeover_FailedTExt;
        private UIScaleAnimation levelTextScaleAnimation;

        [Space(5f)]
        [SerializeField] GameObject devOverlay;
        public GameObject GameplaySettingPanel;
        public GameObject Mainmenu;
        public override void Init()
        {
            levelTextScaleAnimation = new UIScaleAnimation(levelText.rectTransform);

            /*devOverlay.SetActive(DevPanelEnabler.IsDevPanelDisplayed());*/

            replayButton.onClick.AddListener(OnReplayButtonClicked);

            NotchSaveArea.RegisterRectTransform(safeZoneTransform);

            exitPopUp.OnCancelExitEvent += ExitPopCloseButton;
            exitPopUp.OnConfirmExitEvent += ExitPopUpConfirmExitButton;
        }

        #region Show/Hide

        public override void PlayShowAnimation()
        {
            TutorialCanvasController.SetParent(transform);

            //UpdateLevelNumber();

            levelTextScaleAnimation.Show(scaleMultiplier: 1.05f, immediately: true);

            UIController.OnPageOpened(this);
        }

        public override void PlayHideAnimation()
        {
            levelTextScaleAnimation.Hide(scaleMultiplier: 1.05f, immediately: false);

            exitPopUp.Hide();

            UIController.OnPageClosed(this);
        }

        #endregion

        private void OnReplayButtonClicked()
        {
            exitPopUp.Show();
        }

        public void ExitPopCloseButton()
        {
            AudioController.PlaySound(AudioController.AudioClips.buttonSound);

            exitPopUp.Hide();
        }

        public void ExitPopUpConfirmExitButton()
        {
            exitPopUp.Hide();

            if (LivesManager.IsMaxLives) LivesManager.RemoveLife();

            AudioController.PlaySound(AudioController.AudioClips.buttonSound);

            UIController.HidePage<UIGame>();

            GameController.ReplayLevel();
        }

        public void UpdateLevelNumber()
        {
            /*Debug.LogError("Real Level Number " + LevelController.RealLevelNumber);
            Debug.LogError("Display Level Number " + (LevelController.DisplayLevelNumber + 1));*/
            levelText.text = string.Format("LEVEL {0}", LevelController.DisplayLevelNumber + 1);
            winPanelText.text= string.Format("LEVEL {0}", LevelController.DisplayLevelNumber + 1);
            LevelText.text=(LevelController.DisplayLevelNumber + 1).ToString();
            Mainmenu.GetComponent<UIMainMenu>().levelText.text= string.Format("LEVEL {0}", LevelController.DisplayLevelNumber + 1);
            int value = LevelController.DisplayLevelNumber + 1;
            customManagerScript.instance.LogEvent_NonRevenue("Level_" + value + "_Started");
            var EventParameters = new Dictionary<string, string>()
        {
            {
                "LevelStarted",value.ToString()
            }
        };
           // ByteBrew.NewCustomEvent("Level", EventParameters);
            customManagerScript.instance.LevelStart("TrainStation" , "Level"+value);
            Gamoeover_FailedTExt.text= string.Format("LEVEL {0}", LevelController.DisplayLevelNumber + 1);
        }
        public void UpdateJustTExt()
        {
            levelText.text = string.Format("LEVEL {0}", LevelController.DisplayLevelNumber + 1);
            LevelText.text = (LevelController.DisplayLevelNumber + 1).ToString();
            Gamoeover_FailedTExt.text = string.Format("LEVEL {0}", LevelController.DisplayLevelNumber + 1);
        }
        public void Experiment()
        {
            ;
        }
        public void OnsettingHomeClicked()
        {
            /*  if (customManagerScript.instance.noLives.activeInHierarchy)
              {
                  customManagerScript.instance.noLives.SetActive(false);
              }*/
            // Mainmenu.GetComponent<UIMainMenu>().coinsPanel.
            //PlayerPrefs.SetInt("IsFirst", 1);
            customManagerScript.instance.isRevived = false;
            if (PlayerPrefs.GetInt("Slot1Status") == 0)
            {
                FindAnyObjectByType<EnvironmentBehavior>().LockSlot1();
            }
            if (PlayerPrefs.GetInt("Slot2Status") == 0)
            {
                FindAnyObjectByType<EnvironmentBehavior>().LockSlot2();
            }
            Mainmenu.SetActive(true);
            PlayerPrefs.SetInt("BusHead", 0);
            GameplaySettingPanel.SetActive(false);
            UIController.HidePage<UIGame>();
            UIController.ShowPage<UIMainMenu>();
            customManagerScript.instance.isLevelPaused = true;
            customManagerScript.instance.levelscroller.ScrollToCurrentLevel();
          //  MaxAdsManager.instance.HideBanner();
            LivesManager.RemoveLife();
            customManagerScript.instance.PlayUIAnimator(Mainmenu);
            GameController.ReplayLevel();
            int value = LevelController.DisplayLevelNumber + 1;
            var EventParameters = new Dictionary<string, string>()
        {
            {
                "LevelLoaded",value.ToString()
            }
        };
          //  ByteBrew.NewCustomEvent("Level", EventParameters);
            customManagerScript.instance.LogEvent_NonRevenue("Level_" + value + "_Loaded");
            

        }
        public void ResumeGameplay()
        {
            if (customManagerScript.instance.isTimerPowerUpUsed == false)
            {
                customManagerScript.instance.isTimerStart = true;
                customManagerScript.instance.isLevelPaused = false;
            }
        }
        #region Development

        public void ReloadDev()
        {
            ReflectionUtils.InjectInstanceComponent<GameController>("isGameActive", false, ReflectionUtils.FLAGS_STATIC_PRIVATE);

            GameController.RefreshLevelDev();
        }

        public void HideDev()
        {
            devOverlay.SetActive(false);
        }

        public void OnLevelInputUpdatedDev(string newLevel)
        {
            int level = -1;

            if (int.TryParse(newLevel, out level))
            {
                ReflectionUtils.InjectInstanceComponent<GameController>("isGameActive", false, ReflectionUtils.FLAGS_STATIC_PRIVATE);

                LevelSave levelSave = SaveController.GetSaveObject<LevelSave>("level");
                levelSave.DisplayLevelNumber = Mathf.Clamp((level - 1), 0, int.MaxValue);
                levelSave.RealLevelNumber = levelSave.DisplayLevelNumber;

                GameController.RefreshLevelDev();
            }
        }

        public void PrevLevelDev()
        {
            ReflectionUtils.InjectInstanceComponent<GameController>("isGameActive", false, ReflectionUtils.FLAGS_STATIC_PRIVATE);

            LevelSave levelSave = SaveController.GetSaveObject<LevelSave>("level");
            levelSave.DisplayLevelNumber = Mathf.Clamp(levelSave.DisplayLevelNumber - 1, 0, int.MaxValue);
            levelSave.RealLevelNumber = levelSave.DisplayLevelNumber;

            GameController.RefreshLevelDev();
        }

        public void NextLevelDev()
        {
            ReflectionUtils.InjectInstanceComponent<GameController>("isGameActive", false, ReflectionUtils.FLAGS_STATIC_PRIVATE);

            LevelSave levelSave = SaveController.GetSaveObject<LevelSave>("level");
            levelSave.DisplayLevelNumber = levelSave.DisplayLevelNumber + 1;
            levelSave.RealLevelNumber = levelSave.DisplayLevelNumber;

            GameController.RefreshLevelDev();
        }

        #endregion
    }
}
