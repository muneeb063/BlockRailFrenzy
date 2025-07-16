using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class RewardVictoryView : BaseView
{
    public RectTransform titleTrans;

    public RectTransform rewardItemTrans;

    public RectTransform btnGroupTrans;

    public GameObject boxAnim;

    public int secondItemIndex;

    public Image secondItemSpr;

    public Sprite[] secondItemArr;

    public override void InitView()
    {

    }

    public override void ShowView()
    {
        base.ShowView();
        AudioManager.instance.openGiftSound.Play();
        secondItemIndex = Random.Range(0 , 3);

        if (secondItemIndex == 0)
            secondItemSpr.sprite = secondItemArr[0];
        else if (secondItemIndex == 1)
            secondItemSpr.sprite = secondItemArr[1];
        else if (secondItemIndex == 2)
            secondItemSpr.sprite = secondItemArr[2];

        titleTrans.gameObject.SetActive(false);
        rewardItemTrans.gameObject.SetActive(false);
        btnGroupTrans.gameObject.SetActive(false);
        boxAnim.SetActive(true);
        boxAnim.GetComponent<Animator>().SetBool("Open" , true);
        StartCoroutine(ShowItems());
    }

    IEnumerator ShowItems()
    {
        yield return new WaitForSeconds(1.5f);
        AudioManager.instance.openGiftSound.Play();
        boxAnim.SetActive(false);
        titleTrans.gameObject.SetActive(true);
        rewardItemTrans.gameObject.SetActive(true);
        btnGroupTrans.gameObject.SetActive(true);
    }

    public override void Start()
    {

    }

    public override void Update()
    {

    }

    public void Claim()
    {
        AudioManager.instance.btnSound.Play();

       // GMAdsManager.Instance.ShowBothRewarded(EarnRW);
    }

    public void EarnRW()
    {
        ClaimCB();
    }

    public void ShowRWUnityAds()
    {
    /*    GMAdsManager.Instance.ShowBothRewarded(() =>
        {
            ClaimCB();

        });*/
    }

    public void ClaimCB()
    {
        GameManager.Instance.uiManager.coinView.InitView();
        GameManager.Instance.uiManager.coinView.ShowView();
        GameManager.Instance.uiManager.coinView.UpdateCoinPanelVfx();
        GameManager.Instance.AddCoin(10);
        if (secondItemIndex == 0)
            GameManager.Instance.AddHint(1);
        else if (secondItemIndex == 1)
            GameManager.Instance.AddShuffle(1);
        else if (secondItemIndex == 2)
            GameManager.Instance.AddFreeze(1);
        StartCoroutine(ClaimIE());
    }

    IEnumerator ClaimIE()
    {
        yield return new WaitForSeconds(2.0f);
        GameManager.Instance.uiManager.coinView.HideView();
        HideView();
    }

    public void LoseIt()
    {
        AudioManager.instance.btnSound.Play();
        HideView();
    }

}
