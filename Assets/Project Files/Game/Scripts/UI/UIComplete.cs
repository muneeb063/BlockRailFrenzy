using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using System;
using System.Collections.Generic;
using TMPro;
using Watermelon.BusStop;
using UIAnimatorCore;
//using GameAnalyticsSDK;
namespace Watermelon
{
    public class UIComplete : UIPage
    {
        [SerializeField] RectTransform safeZone;
        [SerializeField] UIFadeAnimation backgroundFade;

        [Space]
        [SerializeField] UIScaleAnimation levelCompleteLabel;

        [Space]
        [SerializeField] UIScaleAnimation rewardLabel;
        [SerializeField] Image rewardIconImage;
        public Text rewardAmountText;

        [Header("Coins Label")]
        [SerializeField] UIScaleAnimation coinsPanelScalable;
        [SerializeField] CurrencyUIPanelSimple coinsPanelUI;

        [Space]
        [SerializeField] UIFadeAnimation multiplyRewardButtonFade;
        [SerializeField] Button multiplyRewardButton;
        [SerializeField] UIFadeAnimation noThanksButtonFade;
        [SerializeField] Button noThanksButton;
        [SerializeField] Text noThanksText;

        private TweenCase noThanksAppearTween;
        private int coinsHash = CurrencyType.Coins.ToString().GetHashCode();

        private readonly string NO_THANKS_TEXT = "NO THANKS";
        private readonly string CONTINUE_TEXT = "CONTINUE";

        private int currentReward;
        public RewardSlider multiplesliderScript;
        public GameObject WellDoneSprite;
        public Button bgButtun;
        public bool isCoroutineStarted=false;
        public override void Init()
        {
            multiplyRewardButton.onClick.AddListener(MultiplyRewardButton);
            noThanksButton.onClick.AddListener(NoThanksButton);
            bgButtun.onClick.AddListener(NoThanksButton);
            coinsPanelUI.Init();

            Currency currency = CurrenciesController.GetCurrency(CurrencyType.Coins);
            rewardIconImage.sprite = currency.Icon;

            NotchSaveArea.RegisterRectTransform(safeZone);
        }

        #region Show/Hide
        public override void PlayShowAnimation()
        {
            /*    if (PlayerPrefs.GetInt("Slot1Status") == 0)
                {
                    FindAnyObjectByType<EnvironmentBehavior>().LockSlot1();
                }
                if (PlayerPrefs.GetInt("Slot2Status") == 0)
                {
                    FindAnyObjectByType<EnvironmentBehavior>().LockSlot2();
                }*/
          //  FindAnyObjectByType<EnvironmentBehavior>().LockAllSlots();
            rewardLabel.Hide(immediately: true);
            multiplyRewardButtonFade.Hide(immediately: true);
            noThanksButtonFade.Hide(immediately: true);
            noThanksButton.interactable = false;
            coinsPanelScalable.Hide(immediately: true);

            noThanksText.text = NO_THANKS_TEXT;

            backgroundFade.Show(duration: 2f);
            levelCompleteLabel.Show();

            coinsPanelScalable.Show();
            noThanksAppearTween = Tween.DelayedCall(0.5f, delegate
            {
                noThanksButtonFade.Show();
                noThanksButton.interactable = true;
            });
            /*        currentReward = LevelController.CurrentReward;
                    //WellDoneSprite.GetComponent<UIAnimator>().PlayAnimation(AnimSetupType.Intro);
                    ShowRewardLabel(currentReward, false, 2f, delegate // update reward here
                    {
                        rewardLabel.Transform.DOPushScale(Vector3.one * 1.1f, Vector3.one, 0.2f, 0.2f).OnComplete(delegate
                        {
                            FloatingCloud.SpawnCurrency(coinsHash, (RectTransform)rewardLabel.Transform, (RectTransform)coinsPanelScalable.Transform, 10, "", () =>
                            {
                                CurrenciesController.Add(CurrencyType.Coins, currentReward);

                                multiplyRewardButtonFade.Show();
                                multiplyRewardButton.interactable = true;

                                noThanksAppearTween = Tween.DelayedCall(0.5f, delegate
                                {
                                    noThanksButtonFade.Show();
                                    noThanksButton.interactable = true;
                                });
                            });
                        });
                    });*/
        }

        public override void PlayHideAnimation()
        {
            if (!isPageDisplayed)
                return;

            backgroundFade.Hide(0.25f);
            coinsPanelScalable.Hide();

            Tween.DelayedCall(0.25f, delegate
            {
                canvas.enabled = false;
                isPageDisplayed = false;

                UIController.OnPageClosed(this);
            });
        }


        #endregion

        #region RewardLabel

        public void ShowRewardLabel(float rewardAmounts, bool immediately = false, float duration = 0.3f, Action onComplted = null)
        {
            rewardLabel.Show(immediately: immediately);

            if (immediately)
            {
                rewardAmountText.text = "+" + rewardAmounts;
                onComplted?.Invoke();

                return;
            }

            rewardAmountText.text = "+" + 0;

            Tween.DoFloat(0, rewardAmounts, duration, (float value) =>
            {
                rewardAmountText.text = "+" + (int)value;
            }).OnComplete(delegate
            {

                onComplted?.Invoke();
            });
        }

        #endregion

        #region Buttons

        public void MultiplyRewardButton()
        {
            AudioController.PlaySound(AudioController.AudioClips.buttonSound);
            PlayerPrefs.SetInt("BusHead", 0);
            if (noThanksAppearTween != null && noThanksAppearTween.IsActive)
            {
                noThanksAppearTween.Kill();
            }

            noThanksButton.interactable = false;
            multiplyRewardButton.interactable = false;

            AdsManager.ShowRewardBasedVideo((bool success) =>
            {
                if (success)
                {
                    int rewardMult = 3;

                    noThanksButtonFade.Hide(immediately: true);
                    multiplyRewardButtonFade.Hide(immediately: true);

                    ShowRewardLabel(currentReward * rewardMult, false, 0.3f, delegate
                    {
                        FloatingCloud.SpawnCurrency(coinsHash, (RectTransform)rewardLabel.Transform, (RectTransform)coinsPanelScalable.Transform, 10, "", () =>
                        {
                            CurrenciesController.Add(CurrencyType.Coins, currentReward * rewardMult);

                            noThanksText.text = CONTINUE_TEXT;

                            noThanksButton.interactable = true;
                            noThanksButton.gameObject.SetActive(true);
                            noThanksButtonFade.Show();
                        });
                    });

                    LivesManager.AddLife();
                }
                else
                {
                    NoThanksButton();
                }
            });
        }

        public void NoThanksButton()
        {
            if (isCoroutineStarted == false)
            {
                RaycastController.Enable();
                StartCoroutine(delay());
            }
        }
        IEnumerator delay()
        {
            customManagerScript.instance.Timer.color = Color.white;
            //CoinsSafeZone.SetActive(true);
            isCoroutineStarted = true;
            currentReward = LevelController.CurrentReward;
            //WellDoneSprite.GetComponent<UIAnimator>().PlayAnimation(AnimSetupType.Intro);
            ShowRewardLabel(currentReward, false, 0.1f, delegate // update reward here
            {
                FloatingCloud.SpawnCurrency(coinsHash, (RectTransform)rewardLabel.Transform, (RectTransform)coinsPanelScalable.Transform, 10, "", () =>
                {
                    CurrenciesController.Add(CurrencyType.Coins, currentReward);

                    multiplyRewardButtonFade.Show();
                    multiplyRewardButton.interactable = true;

                    noThanksAppearTween = Tween.DelayedCall(0.5f, delegate
                    {
                        noThanksButtonFade.Show();
                        noThanksButton.interactable = true;
                    });
                });
            });
            yield return new WaitForSeconds(2f);
            customManagerScript.instance.isRevived = false;
            /*customManagerScript.count++;
            if (customManagerScript.count % 3 == 0)
            {*/
            customManagerScript.instance.ShowInterstitial();
            //}
            PlayerPrefs.SetInt("BusHead", 0);
            AudioController.PlaySound(AudioController.AudioClips.buttonSound);
            
            UIController.HidePage<UIComplete>();
            UIController.ShowPage<UIGame>();
            //MaxAdsManager.instance.ShowBanner();
            customManagerScript.instance.PlayUIAnimator(customManagerScript.instance.uiGames.gameObject);
            GameController.LoadNextLevel();
            //GameController.LoadNextLevel();
            customManagerScript.instance.UpdateLevelData();


            // LivesManager.AddLife();
            multiplesliderScript.isStopped = false;
            multiplesliderScript.RewardBtn.interactable = true;
            multiplesliderScript.rewardText.text = null;
            isCoroutineStarted = false;
            customManagerScript.instance.ShowFeatureUnlocked();
            customManagerScript.instance.DocksStates();
            if (LevelController.DisplayLevelNumber > 4)
            {
                FindAnyObjectByType<DockBehavior>().UnlockVipSlot();
            }
            int value = LevelController.DisplayLevelNumber + 1;

            //Full Funnel
            /*GameAnalytics.NewDesignEvent($"session_start:level_load_{value}:level_start_{value}");
            GameAnalytics.NewDesignEvent($"level_start_{value}");
            Debug.Log($"GA Design Event: level_start_{value}");
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, $"level_{value}");*/

            //customManagerScript.instance.GADesigncallback($"level_number:{value}"); // Event ID 02
            customManagerScript.instance.StartBombTimers();
        }
        public void afterReward()
        {
            if (isCoroutineStarted == false)
            {
                RaycastController.Enable();
                StartCoroutine(DelayMultiple());
            }
        }
        IEnumerator DelayMultiple()
        {
            customManagerScript.instance.Timer.color = Color.white;
            isCoroutineStarted = true;
            currentReward = LevelController.CurrentReward;
            //WellDoneSprite.GetComponent<UIAnimator>().PlayAnimation(AnimSetupType.Intro);
            ShowRewardLabel(currentReward, false, 0.1f, delegate // update reward here
            {
                FloatingCloud.SpawnCurrency(coinsHash, (RectTransform)rewardLabel.Transform, (RectTransform)coinsPanelScalable.Transform, 10, "", () =>
                {
                    CurrenciesController.Add(CurrencyType.Coins, currentReward);

                    multiplyRewardButtonFade.Show();
                    multiplyRewardButton.interactable = true;

                    noThanksAppearTween = Tween.DelayedCall(0.5f, delegate
                    {
                        noThanksButtonFade.Show();
                        noThanksButton.interactable = true;
                    });
                });
            });
            yield return new WaitForSeconds(2f);
            customManagerScript.instance.isRevived = false;
            PlayerPrefs.SetInt("BusHead", 0);
            AudioController.PlaySound(AudioController.AudioClips.buttonSound);
           // FindAnyObjectByType<EnvironmentBehavior>().UnlockSlot();
            UIController.HidePage<UIComplete>();
            UIController.ShowPage<UIGame>();
           // MaxAdsManager.instance.ShowBanner();
            customManagerScript.instance.PlayUIAnimator(customManagerScript.instance.uiGames.gameObject);
            GameController.LoadNextLevel();
            customManagerScript.instance.UpdateLevelData();


            LivesManager.AddLife();
            multiplesliderScript.isStopped = false;
            multiplesliderScript.RewardBtn.interactable = true;
            multiplesliderScript.rewardText.text = null;
            isCoroutineStarted = false;
            customManagerScript.instance.ShowFeatureUnlocked();
            customManagerScript.instance.DocksStates();
            if (LevelController.DisplayLevelNumber > 4)
            {
                FindAnyObjectByType<DockBehavior>().UnlockVipSlot();
            }
            int value = LevelController.DisplayLevelNumber + 1;

            //Full Funnel
          /*  GameAnalytics.NewDesignEvent($"session_start:level_load_{value}:level_start_{value}");
            GameAnalytics.NewDesignEvent($"level_start_{value}");
            Debug.Log($"GA Design Event: level_start_{value}");
            GameAnalytics.NewProgressionEvent(GAProgressionStatus.Start, $"level_{value}");*/

            //customManagerScript.instance.GADesigncallback($"level_number:{value}"); // Event ID 02
            customManagerScript.instance.StartBombTimers();
        }
        public void HomeButton()
        {
            AudioController.PlaySound(AudioController.AudioClips.buttonSound);
            customManagerScript.instance.Timer.color = Color.white;
        }

        #endregion
    }
}
