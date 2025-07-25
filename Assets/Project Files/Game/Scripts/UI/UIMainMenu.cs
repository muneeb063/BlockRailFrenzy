using System.Collections;
using TMPro;
using UnityEngine;
using Watermelon.BusStop;
using Watermelon.IAPStore;
using Watermelon.SkinStore;
using UnityEngine.UI;
//using GameAnalyticsSDK;
namespace Watermelon
{
    public class UIMainMenu : UIPage
    {
        public readonly float STORE_AD_RIGHT_OFFSET_X = 300F;

        [Space]
        [SerializeField] RectTransform safeZoneTransform;
        [SerializeField] UIScaleAnimation coinsLabelScalable;
        [SerializeField] CurrencyUIPanelSimple coinsPanel;
        public Text levelText;
        private UIScaleAnimation levelTextScaleAnimation;

        [Space]
        [SerializeField] UIMainMenuButton iapStoreButton;
        [SerializeField] UIMainMenuButton noAdsButton;
        [SerializeField] UIMainMenuButton skinsButton;

        [Space]
        [SerializeField] UINoAdsPopUp noAdsPopUp;
        public UINoAdsPopUp NoAdsPopUp => noAdsPopUp;

        [Space]
        [SerializeField] AddLivesPanel livesPanel;

        private TweenCase showHideStoreAdButtonDelayTweenCase;
        private TweenCase hideTween;
        public GameObject DailyReward;
        public GameObject LeaderBoard;
        public GameObject SpinWheel;
        public GameObject BlackImage;
        public GameObject TeamsPanel;
        public GameObject Shop;
        public BottomPanelScript bottompanelScript;
        private void OnEnable()
        {
            AdsManager.ForcedAdDisabled += ForceAdPurchased;
        }

        private void OnDisable()
        {
            AdsManager.ForcedAdDisabled -= ForceAdPurchased;
        }

        public override void Init()
        {
            levelTextScaleAnimation = new UIScaleAnimation(levelText.rectTransform);

            coinsPanel.Init();
            coinsPanel.AddButton.onClick.AddListener(IAPStoreButton);

            iapStoreButton.Init(STORE_AD_RIGHT_OFFSET_X);
            noAdsButton.Init(STORE_AD_RIGHT_OFFSET_X);
            skinsButton.Init(STORE_AD_RIGHT_OFFSET_X);

            iapStoreButton.Button.onClick.AddListener(IAPStoreButton);
            noAdsButton.Button.onClick.AddListener(NoAdButton);
            skinsButton.Button.onClick.AddListener(SkinsButton);

            NotchSaveArea.RegisterRectTransform(safeZoneTransform);
        }

        #region Show/Hide

        public override void PlayShowAnimation()
        {
            //TutorialCanvasController.SetParent(transform);

            UpdateLevelNumber();

            showHideStoreAdButtonDelayTweenCase.KillActive();
            hideTween.KillActive();

            HideAdButton(true);
            iapStoreButton.Hide(true);
            skinsButton.Hide(true);

            levelTextScaleAnimation.Show(scaleMultiplier: 1.05f, immediately: false);
            //coinsLabelScalable.Show(immediately: true);

            showHideStoreAdButtonDelayTweenCase = Tween.DelayedCall(0.05f, delegate
            {
                ShowAdButton();
                iapStoreButton.Show();
                skinsButton.Show();
            });

            UIController.OnPageOpened(this);
        }

        public override void PlayHideAnimation()
        {
            showHideStoreAdButtonDelayTweenCase.KillActive();
            hideTween.KillActive();

           // coinsLabelScalable.Hide(immediately: true);
            levelTextScaleAnimation.Hide(scaleMultiplier: 1.05f, immediately: true);

            HideAdButton();

            showHideStoreAdButtonDelayTweenCase = Tween.DelayedCall(0.1f, delegate
            {
                iapStoreButton.Hide();
                skinsButton.Hide();
            });

            hideTween = Tween.DelayedCall(0.5f, delegate
            {
                UIController.OnPageClosed(this);
            });
        }

        public void ShowAddLivesPanel()
        {
            livesPanel.Show();
        }

        private void UpdateLevelNumber()
        {
            levelText.text = string.Format("LEVEL {0}", LevelController.DisplayLevelNumber + 1);
        }

        #endregion

        #region Ad Button Label

        private void ShowAdButton(bool immediately = false)
        {
            if (AdsManager.IsForcedAdEnabled())
            {
                noAdsButton.Show(immediately);
            }
            else
            {
                noAdsButton.Hide(immediately: true);
            }
        }

        private void HideAdButton(bool immediately = false)
        {
            noAdsButton.Hide(immediately);
        }

        private void ForceAdPurchased()
        {
            HideAdButton(immediately: true);
        }

        #endregion

        #region Buttons

        public void TapToPlayButton()
        {
            if (UIController.IsDisplayed<UISettings>()) return;

            AudioController.PlaySound(AudioController.AudioClips.buttonSound);
            if (LivesManager.Lives > 0)
            {

                gameObject.GetComponent<Canvas>().enabled = false;
                GameController.StartGame();
                customManagerScript.instance.isLevelPaused = false;
                customManagerScript.instance.StartBombTimers();
                customManagerScript.instance.UpdateTxt();
                customManagerScript.instance.StartTimer();
                customManagerScript.instance.ShowFeatureUnlocked();
                //customManagerScript.instance.GADesigncallback("LevelStart");
                int value = LevelController.DisplayLevelNumber + 1;
                //customManagerScript.instance.GADesigncallback($"level_number:{value}"); // Event ID 02

                //Full Funnel
               /* GameAnalytics.NewDesignEvent($"session_start:level_load_{value}:level_start_{value}");
                GameAnalytics.NewDesignEvent($"level_start_{value}");
                Debug.Log($"GA Design Event: level_start_{value}");
                GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, $"level_{value}");*/
                // gameObject.SetActive(false);
            }
            else
            {
                ShowAddLivesPanel();
            }
        }
        IEnumerator delayplay()
        {

            BlackImage.SetActive(true);
            yield return new WaitForSeconds(1f);
            GameController.StartGame();
        }
        public void IAPStoreButton()
        {
            if (UIController.GetPage<UIStore>().IsPageDisplayed)
                return;

            UIController.HidePage<UIMainMenu>();
            UIController.ShowPage<UIStore>();

            // reopening main menu only after store page was opened throug main menu
            UIController.PageClosed += OnIapOrSkinsStoreClosed;


            AudioController.PlaySound(AudioController.AudioClips.buttonSound);
        }
        public void HeartBtnClicked()
        {
            if (LivesManager.Lives <= 0)
            {
                ShowAddLivesPanel();
            }
        }
        public void SkinsButton()
        {
            if (UIController.GetPage<UISkinStore>().IsPageDisplayed)
                return;

            UIController.HidePage<UIMainMenu>();
            UIController.ShowPage<UISkinStore>();

            // reopening main menu only after store page was opened throug main menu
            UIController.PageClosed += OnIapOrSkinsStoreClosed;

            AudioController.PlaySound(AudioController.AudioClips.buttonSound);
        }

        private void OnIapOrSkinsStoreClosed(UIPage page, System.Type pageType)
        {
            if (pageType.Equals(typeof(UIStore)) || pageType.Equals(typeof(UISkinStore)))
            {
                UIController.PageClosed -= OnIapOrSkinsStoreClosed;

                UIController.ShowPage<UIMainMenu>();
                customManagerScript.instance.levelscroller.ScrollToCurrentLevel();
               // MaxAdsManager.instance.HideBanner();
            }
        }

        public void NoAdButton()
        {
            AudioController.PlaySound(AudioController.AudioClips.buttonSound);

            noAdsPopUp.Show();
        }
        public void ExitGame()
        {
            Application.Quit();
        }
        public void DailyRewardClicked()
        {
            AudioController.PlaySound(AudioController.AudioClips.buttonSound);
            DailyReward.SetActive(true);
        }
        public void LeaderBoardClicked()
        {
            Shop.SetActive(false);
            AudioController.PlaySound(AudioController.AudioClips.buttonSound);
            LeaderBoard.SetActive(true);
            TeamsPanel.SetActive(false);
            bottompanelScript.LeaderboardClicked();
        }
        public void SpinWheelClicked()
        {
            AudioController.PlaySound(AudioController.AudioClips.buttonSound);
            SpinWheel.SetActive(true);
            gameObject.transform.parent.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceOverlay;
            SpinWheel.GetComponent<LuckyWheelView>().InitView();
            SpinWheel.GetComponent<LuckyWheelView>().ShowView();
        }
        public void OnTeamspanelClicked()
        {
            Shop.SetActive(false);
            bottompanelScript.TeamsBtnClicked();
            LeaderBoard.SetActive(false);
            TeamsPanel.SetActive(true);

        }
        public void HomeBtnClicked()
        {
            Shop.SetActive(false);
            LeaderBoard.SetActive(false);
            TeamsPanel.SetActive(false);
            bottompanelScript.onhomeBtnClicked();
        }
        public void OnShopClicked()
        {
            Shop.SetActive(true);
            LeaderBoard.SetActive(false);
            TeamsPanel.SetActive(false);
           // bottompanelScript.OnShopButtonPressed();
        }
        #endregion
    }


}
