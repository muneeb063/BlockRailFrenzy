using DG.Tweening;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;
using Watermelon;
using DG;
using Ease = DG.Tweening.Ease;
public class LuckyWheelView : BaseView
{
    public RectTransform rootItemTrans;

    public GameObject resultObject;

    public Text rewardValueTxt;

    public Image rewardIcon;

    public Sprite coinSpr;

    public Sprite hintSpr;

    public Sprite shuffleSpr;

    public Sprite freezeSpr;

    int randomRound;

    private List<int> rewardIndexList = new List<int>();

    public GameObject[] selectObjList;

    public GameObject freeBtn;

    public GameObject adsBtn;

    public GameObject closeBtn;

    public GameObject closeTextBtn;

    private int spinCount;

    private void StartSpin()
    {
        for (int i = 0; i < 6; i++)
        {
            selectObjList[i].SetActive(false);
        }
        // resultObject.SetActive(false);
        // rewardIndexList.Clear();
        AudioController.PlaySound(AudioController.AudioClips.buttonSound);
        freeBtn.SetActive(false);
        adsBtn.SetActive(false);
        closeBtn.SetActive(false);
        closeTextBtn.SetActive(false);
        spinCount--;

        randomRound = rewardIndexList[0];
        rewardIndexList.RemoveAt(0);

        rootItemTrans.DORotate(new Vector3(0f , 0f , 6 * 360 + 60 * randomRound) , 5f , RotateMode.FastBeyond360).SetEase(Ease.InOutExpo).OnComplete(() =>
        {
            ShowReward();
        });


    }

    public override void Start()
    {
      
    }

    public override void Update()
    {

    }

    public override void InitView()
    {
        //AdsControl.Instance.HideBannerAd();
        randomRound = 0;
        spinCount = 6;
        resultObject.SetActive(false);
        rewardIndexList.Clear();
        if (PlayerPrefs.GetInt("IsOnce") == 0)
        {
            PlayerPrefs.SetInt("IsOnce", 1);
            freeBtn.SetActive(true);
            adsBtn.SetActive(false);
        }
        else
        {
            freeBtn.SetActive(false);
            adsBtn.SetActive(true);
        }
        /*freeBtn.SetActive(true);
        adsBtn.SetActive(false);*/
        closeBtn.SetActive(false);
        closeTextBtn.SetActive(true);

        List<int> tempIndexList = new List<int>();

        for (int i = 0; i < 6; i++)
        {
            tempIndexList.Add(i);
            selectObjList[i].SetActive(false);
        }


        for (int i = 0; i < 6; i++)
        {
            int randomIndex = Random.Range(0 , tempIndexList.Count);
            rewardIndexList.Add(tempIndexList[randomIndex]);
            tempIndexList.RemoveAt(randomIndex);
        }
    }

    private void ShowReward()
    {
        AudioController.PlaySound(AudioController.AudioClips.completeSound);

        resultObject.SetActive(true);
        selectObjList[randomRound].SetActive(true);

        switch (randomRound)
        {
            case 0:
                rewardIcon.sprite = hintSpr;
                rewardValueTxt.text = "+10";
                GameManager.Instance.AddHint(1);
                CurrenciesController.Add(CurrencyType.Coins, 10);
                break;

            case 1:
                rewardIcon.sprite = coinSpr;
                rewardValueTxt.text = "+20";
                CurrenciesController.Add(CurrencyType.Coins, 20);
                break;

            case 2:
                rewardIcon.sprite = shuffleSpr;
                rewardValueTxt.text = "+25";
               // GameManager.Instance.AddShuffle(1);
                CurrenciesController.Add(CurrencyType.Coins, 25);
                break;

            case 3:
                rewardIcon.sprite = coinSpr;
                rewardValueTxt.text = "+30";
               // GameManager.Instance.AddCoin(10);
                CurrenciesController.Add(CurrencyType.Coins, 30);
                break;

            case 4:
                rewardIcon.sprite = freezeSpr;
                rewardValueTxt.text = "+40";
               // GameManager.Instance.AddFreeze(1);
                CurrenciesController.Add(CurrencyType.Coins, 40);
                break;

            case 5:
                rewardIcon.sprite = coinSpr;
                rewardValueTxt.text = "+50";
               // GameManager.Instance.AddCoin(5);
                CurrenciesController.Add(CurrencyType.Coins, 50);
                break;
        }

        if (spinCount > 0)
        {
            freeBtn.SetActive(false);
            adsBtn.SetActive(true);
            closeBtn.SetActive(false);
            closeTextBtn.SetActive(true);
        }

        else
        {
            freeBtn.SetActive(false);
            adsBtn.SetActive(false);
            closeBtn.SetActive(true);
            closeTextBtn.SetActive(false);
        }
    }

    public void Close()
    {
        gameObject.transform.parent.GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
        AudioController.PlaySound(AudioController.AudioClips.buttonSound);
        HideView();
        gameObject.SetActive(false);
    }

    public void FreeSpin()
    {
        AudioController.PlaySound(AudioController.AudioClips.buttonSound);
        StartSpin();

    }

    public void WatchAdsSpin()
    {
        AudioController.PlaySound(AudioController.AudioClips.buttonSound);

        //GMAdsManager.Instance.ShowBothRewarded(EarnFreeSpin);
       // MaxAdsManager.instance.ShowRewardedVideo(EarnFreeSpin);
       // EarnFreeSpin();
    }

    public void EarnFreeSpin()
    {
        customManagerScript.instance.AdPlace = "SpinWheelPanelRV";
        StartSpin();
    }

    public void ShowRWUnityAds()
    {
      /*  GMAdsManager.Instance.ShowBothRewarded(() =>
        {

            StartSpin();

        });*/
        //AdsManager.Instance.ShowRewarded();
      //  MaxAdsManager.instance.ShowRewardedVideo(StartSpin);
        //StartSpin();
    }
}
