﻿using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Watermelon
{
    public class PUUIUnlockPanel : MonoBehaviour, IPopupWindow
    {
        public static PUUIUnlockPanel Instance { get; private set; }

        [SerializeField] GameObject powerUpPanel;

        [Space(5)]
        [SerializeField] Image powerUpPurchasePreview;
        [SerializeField] Text powerUpPurchaseDescriptionText;
        [SerializeField] RawImage rawImageVideo;
        [Space(5)]
        [SerializeField] Button closeButton;

        private bool isOpened;
        public bool IsOpened => isOpened;

        private List<PUSettings> unlockedPowerUps;
        private int pageIndex = 0;

        private Canvas canvas;

        private void Awake()
        {
            Instance = this;

            closeButton.onClick.AddListener(ClosePanel);

            canvas = GetComponent<Canvas>();
            canvas.enabled = false;
        }

        public void Show(List<PUSettings> unlockedPowerUps)
        {
            this.unlockedPowerUps = unlockedPowerUps;

            canvas.enabled = true;

            pageIndex = 0;

            PreparePage(pageIndex);

            UIController.OnPopupWindowOpened(this);
        }

        private void PreparePage(int index)
        {
            if (!unlockedPowerUps.IsInRange(index)) return;

            PUSettings settings = unlockedPowerUps[index];

            powerUpPurchasePreview.sprite = settings.Icon;
            powerUpPurchaseDescriptionText.text = settings.Description;
            customManagerScript.instance.videoPlayer.clip = settings.powerupVideo;
            customManagerScript.instance.videoPlayer.Play();
            //powerUpPurchasePreview.transform.localScale = new Vector3(0.6f, 0.6f, 0.6f);
            //powerUpPurchasePreview.transform.DOScale(1.0f, 0.25f).SetEasing(Ease.Type.BackOut);
        }

        public void ClosePanel()
        {
            customManagerScript.instance.videoPlayer.clip = null;
            pageIndex++;
            customManagerScript.instance.isTimerStart = true;
            if(pageIndex >= unlockedPowerUps.Count)
            {
                canvas.enabled = false;

                UIController.OnPopupWindowClosed(this);

                foreach(PUSettings unlockerPowerUp in unlockedPowerUps)
                {
                    PUController.UnlockPowerUp(unlockerPowerUp.Type);
                }
            }
            else
            {
                PreparePage(pageIndex);
            }
            if (PlayerPrefs.GetInt("EnableUndoTutorial") == 0)
            {
                PlayerPrefs.SetInt("EnableUndoTutorial", 1);
                PlayerPrefs.SetInt("UndoPowerUp", 1);
            }
        }
    }
}