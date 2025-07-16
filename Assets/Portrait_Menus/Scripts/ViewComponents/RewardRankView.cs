
using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;


public class RewardRankView : BaseView
{
    public Button getCoinBtn;

    public Button getCoinX2Btn;

    public Text coinTxt;

    public override void InitView()
    {

    }

    public override void Start()
    {

    }

    public override void Update()
    {

    }

    public override void ShowView()
    {
        base.ShowView();
        AudioManager.instance.openGiftSound.Play();
        GameManager.Instance.currentState = GameManager.GAME_STATE.IN_GAME_POPUP;
        coinTxt.text = "+" + GameManager.Instance.gamePlaySetting.rankRewardBonus;
    }

    public override void HideView()
    {
        AudioManager.instance.btnSound.Play();
        base.HideView();
        GameManager.Instance.currentState = GameManager.GAME_STATE.IN_GAME;
    }

    public void GetCoin()
    {
        AudioManager.instance.btnSound.Play();
        getCoinBtn.interactable = false;
        getCoinX2Btn.interactable = false;
        GameManager.Instance.uiManager.coinView.ShowView();
        GameManager.Instance.uiManager.coinView.UpdateCoinPanel();
        GameManager.Instance.AddCoin(GameManager.Instance.gamePlaySetting.rankRewardBonus);
        GameManager.Instance.uiManager.coinView.UpdateCoinPanelVfx();
        AudioManager.instance.moreCoinSound.Play();
        StartCoroutine(GetCoinIE());
    }

    public void GetCoinX2()
    {
        AudioManager.instance.btnSound.Play();

      //  GMAdsManager.Instance.ShowBothRewarded(EarnRW);
    }

    public void GetCoinX2CB()
    {
        GameManager.Instance.uiManager.coinView.ShowView();
        GameManager.Instance.uiManager.coinView.UpdateCoinPanel();
        GameManager.Instance.AddCoin(GameManager.Instance.gamePlaySetting.rankRewardBonus * 2);
        GameManager.Instance.uiManager.coinView.UpdateCoinPanelVfx();
        StartCoroutine(GetCoinIE());
    }

    IEnumerator GetCoinIE()
    {
        yield return new WaitForSeconds(1.5f);
        getCoinBtn.interactable = true;
        getCoinX2Btn.interactable = true;
        HideView();
        GameManager.Instance.uiManager.coinView.HideView();
    }

    public void EarnRW()
    {
        GetCoinX2CB();
    }

    public void ShowRWUnityAds()
    {
      /*  GMAdsManager.Instance.ShowBothRewarded(() =>
        {
            GetCoinX2CB();

        });*/
    }
}
