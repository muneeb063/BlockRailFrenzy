using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watermelon;

public class BottomPanelScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject TeamsBtn,LeaderBoardBtn,HomeBtn,shopBtn;
    void Start()
    {
        HomeBtn.transform.GetChild(1).gameObject.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void onhomeBtnClicked()
    {
        AudioController.PlaySound(AudioController.AudioClips.buttonSound);
        TeamsBtn.transform.GetChild(1).gameObject.SetActive(false);
        LeaderBoardBtn.transform.GetChild(1).gameObject.SetActive(false);
        HomeBtn.transform.GetChild(1).gameObject.SetActive(true);
        shopBtn.transform.GetChild(1).gameObject.SetActive(false);
    }
    public void LeaderboardClicked()
    {
       
        TeamsBtn.transform.GetChild(1).gameObject.SetActive(false);
        LeaderBoardBtn.transform.GetChild(1).gameObject.SetActive(true);
        HomeBtn.transform.GetChild(1).gameObject.SetActive(false);
        shopBtn.transform.GetChild(1).gameObject.SetActive(false);
    }
    public void TeamsBtnClicked()
    {
        AudioController.PlaySound(AudioController.AudioClips.buttonSound);
        TeamsBtn.transform.GetChild(1).gameObject.SetActive(true);
        LeaderBoardBtn.transform.GetChild(1).gameObject.SetActive(false);
        HomeBtn.transform.GetChild(1).gameObject.SetActive(false);
        shopBtn.transform.GetChild(1).gameObject.SetActive(false);
    }
    public void OnShopButtonPressed()
    {
        AudioController.PlaySound(AudioController.AudioClips.buttonSound);
        TeamsBtn.transform.GetChild(1).gameObject.SetActive(false);
        LeaderBoardBtn.transform.GetChild(1).gameObject.SetActive(false);
        HomeBtn.transform.GetChild(1).gameObject.SetActive(false);
        shopBtn.transform.GetChild(1).gameObject.SetActive(true);
    }
}
