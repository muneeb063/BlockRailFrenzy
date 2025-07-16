using System;
using UnityEngine;
using UnityEngine.UI;

using System.Collections;
using Watermelon;
public class RewardTimer : MonoBehaviour
{
    public static RewardTimer Instance;

    public Canvas rewardPanel;
    public GameObject congratesPanel;

    public GameObject[] ClaimBtns, aCTIVATEDrEWARDiMG, Claimed;


    public float timeRemaining;
    public Text timeText;

    DateTime currentDate;
    DateTime LastClaimed;
    DateTime oldDate;
    private int Day=0;
    private bool collected;


    private void Awake()
    {
        Instance = this;

        if(Instance == null)
        {
            Instance = FindObjectOfType<RewardTimer>();
        }
    }


    void Start()
    {
        timeRemaining = timeRemaining - PlayerPrefs.GetFloat("Timepassed");
        if (!string.IsNullOrEmpty(PlayerPrefs.GetString("LastClaimed")))
        {
            oldDate = System.DateTime.Parse(PlayerPrefs.GetString("LastClaimed"));
        }
        Check();
        ClaimedRewardDays();
        activeCalimedOnes();
    }
    public void back()
    {
        rewardPanel.enabled = (false);
       
    }

    private void activeCalimedOnes()
    {
        for(int i=0; i< 7; i++)
        {
            if (PlayerPrefs.GetInt("Day"+i) == 1)
            {
                Claimed[i].SetActive(true);
            }

               
        }
    }
    private void Check()
    {
        currentDate = System.DateTime.Now;
        System.TimeSpan TimeDiff = currentDate.Subtract(oldDate);

        timeRemaining = (float)(86400 - TimeDiff.TotalSeconds);

        if (timeRemaining > 0)
        {
           
            timeRemaining -= Time.deltaTime;
            DisplayTime(timeRemaining);
        }
        
        if (PlayerPrefs.GetInt("Day0") != 1)
        {
            if (TimeDiff.Days >= 1 || TimeDiff.Hours >= 24/*TimeDiff.Seconds >= 5*/)
            {
                ClaimBtns[PlayerPrefs.GetInt("lastClaimedDay")].SetActive(true);

               // aCTIVATEDrEWARDiMG[PlayerPrefs.GetInt("lastClaimedDay")].SetActive(true);
               
            }
            
        }
        else
        {
            if (TimeDiff.Days >= 1 || TimeDiff.Hours >= 24/*TimeDiff.Seconds >= 5*/)
            {
                timeRemaining = 0;
                if(PlayerPrefs.GetInt("clicked")!=1)
                {
                  
                    PlayerPrefs.SetInt("lastClaimedDay", PlayerPrefs.GetInt("lastClaimedDay") + 1);
                    if(PlayerPrefs.GetInt("lastClaimedDay")<6)
                    {
                        ClaimBtns[PlayerPrefs.GetInt("lastClaimedDay")].SetActive(true);

                      //  aCTIVATEDrEWARDiMG[PlayerPrefs.GetInt("lastClaimedDay")].SetActive(true);
                    }
                    else
                    if(PlayerPrefs.GetInt("lastClaimedDay") == 6)
                    {
                        ClaimBtns[PlayerPrefs.GetInt("lastClaimedDay")].SetActive(true);

                       // aCTIVATEDrEWARDiMG[PlayerPrefs.GetInt("lastClaimedDay")].SetActive(true);
                    }
                    
                    PlayerPrefs.SetInt("clicked", 1);
                }
            }
            else
            {
               
            }
        }
    }
    private void ClaimedRewardDays()
    {
        currentDate = System.DateTime.Now;
        System.TimeSpan TimeDiff = currentDate.Subtract(oldDate);
            if (PlayerPrefs.GetInt("lastClaimedDay") < 6)
            {
                if (TimeDiff.Days >= 1 || TimeDiff.Hours >= 24/*TimeDiff.Seconds >= 5*/)
                {
                   
                    if (PlayerPrefs.GetInt("lastClaimedDay") < 6)
                    {
                    ClaimBtns[PlayerPrefs.GetInt("lastClaimedDay")].SetActive(true);
                   // aCTIVATEDrEWARDiMG[PlayerPrefs.GetInt("lastClaimedDay")].SetActive(true);
                    rewardPanel.enabled = (true);
                    timeText.enabled = false;
                    } 
                }
            }
    }
    private void FixedUpdate()
    {
            Check();     
    }

    private void ClaimedCheck(int x)
    {
       
        oldDate = System.DateTime.Now; 
        ClaimBtns[x].SetActive( false);
    }


    private void DisplayTime(float timeToDisplay)
    {
        float hours = Mathf.FloorToInt((timeToDisplay / 3600)%48);
        float minutes = Mathf.FloorToInt((timeToDisplay/60) % 60);
        float seconds = Mathf.FloorToInt(timeToDisplay % 60);

      
       string time = hours.ToString("00") + ":" + minutes.ToString("00") + ":" + seconds.ToString("00");

        timeText.text = time;
    }
    
    IEnumerator Congrats()
    {
        congratesPanel.SetActive(true);
        yield return new WaitForSeconds(3f);
        congratesPanel.SetActive(false);
    }

    public void ClaimReward(int i)
    {
        PlayerPrefs.SetInt("Day" + i, 1);

        aCTIVATEDrEWARDiMG[i].SetActive(false);

        Claimed[i].SetActive(true);
       
        PlayerPrefs.SetInt("clicked", 0);
        LastClaimed = System.DateTime.Now;
        PlayerPrefs.SetString("LastClaimed", LastClaimed.ToString());
        timeText.enabled = true;
        timeRemaining = 86400;

        if (PlayerPrefs.GetInt("lastClaimedDay")>=6)
        {
            PlayerPrefs.SetInt("lastClaimedDay", 0);
            for (int j = 0; j <= 6; j++)
            {
                PlayerPrefs.SetInt("Day" + j, 0);
            }
        }
        else
        {
            PlayerPrefs.SetInt("lastClaimedDay", i);
        }

        ClaimedCheck(i);
        StartCoroutine(Congrats());
        

        switch (i)
            {
                case 0:
               // PlayerPrefs.SetInt("Currency", PlayerPrefs.GetInt("Currency") + 100);
                CurrenciesController.Add(CurrencyType.Coins, 10);
                /* if (FindAnyObjectByType<BeforeGameplayUiHandler>())
                     FindAnyObjectByType<BeforeGameplayUiHandler>().currency.text = PlayerPrefs.GetInt("Currency").ToString();*/
                break;
                case 1:
               // PlayerPrefs.SetInt("Currency", PlayerPrefs.GetInt("Currency") + 200);
                CurrenciesController.Add(CurrencyType.Coins, 20);
                /*  if (FindAnyObjectByType<BeforeGameplayUiHandler>())
                      FindAnyObjectByType<BeforeGameplayUiHandler>().currency.text = PlayerPrefs.GetInt("Currency").ToString();*/
                break;
                case 2:
               // PlayerPrefs.SetInt("Currency", PlayerPrefs.GetInt("Currency") + 300);
                CurrenciesController.Add(CurrencyType.Coins, 30);
                /* if (FindAnyObjectByType<BeforeGameplayUiHandler>())
                     FindAnyObjectByType<BeforeGameplayUiHandler>().currency.text = PlayerPrefs.GetInt("Currency").ToString();*/
                break;
                case 3:
              //  PlayerPrefs.SetInt("Currency", PlayerPrefs.GetInt("Currency") + 400);
                CurrenciesController.Add(CurrencyType.Coins, 40);
                /*  if (FindAnyObjectByType<BeforeGameplayUiHandler>())
                      FindAnyObjectByType<BeforeGameplayUiHandler>().currency.text = PlayerPrefs.GetInt("Currency").ToString();*/
                break;
                case 4:
              //  PlayerPrefs.SetInt("Currency", PlayerPrefs.GetInt("Currency") + 500);
                CurrenciesController.Add(CurrencyType.Coins, 50);
                /*  if (FindAnyObjectByType<BeforeGameplayUiHandler>())
                      FindAnyObjectByType<BeforeGameplayUiHandler>().currency.text = PlayerPrefs.GetInt("Currency").ToString();*/
                break;
                case 5:
                //PlayerPrefs.SetInt("Currency", PlayerPrefs.GetInt("Currency") + 700);
                CurrenciesController.Add(CurrencyType.Coins, 60);
                /* if (FindAnyObjectByType<BeforeGameplayUiHandler>())
                     FindAnyObjectByType<BeforeGameplayUiHandler>().currency.text = PlayerPrefs.GetInt("Currency").ToString();*/
                break;
                case 6:
                //PlayerPrefs.SetInt("Currency", PlayerPrefs.GetInt("Currency") + 1000);
                CurrenciesController.Add(CurrencyType.Coins, 100);
                /* if (FindAnyObjectByType<BeforeGameplayUiHandler>())
                     FindAnyObjectByType<BeforeGameplayUiHandler>().currency.text = PlayerPrefs.GetInt("Currency").ToString();*/
                break;
            }

    }  
}
