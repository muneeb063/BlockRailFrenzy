using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UI;
using Watermelon;
using Watermelon.BusStop;

public class RewardSlider : MonoBehaviour
{
    public Slider slider;                // The UI Slider component
    public Text rewardText;             // Text to display the reward
    public float speed = 0.5f;          // Speed of slider movement
    public bool isStopped = false;     // Whether the slider is stopped
    private bool movingRight = true;    // Direction of slider movement
    public Button RewardBtn;
    int rewardCount = 0;
    public UIComplete uIComplete;
    void Start()
    {
        // Ensure Slider and RewardText are assigned
        if (slider == null || rewardText == null)
        {
            Debug.LogError("Slider and RewardText must be assigned in the Inspector!");
            return;
        }

        // Set slider to the middle value at the start
        slider.value = 0.5f;

        // Clear the reward text
        rewardText.text = "";
    }

    void Update()
    {
        if (isStopped) return; // Stop updating if the slider is stopped

        // Adjust slider value based on direction
        if (movingRight)
        {
            slider.value += speed * Time.deltaTime; // Move slider to the right
            if (slider.value >= 1f) movingRight = false; // Reverse at max value
        }
        else
        {
            slider.value -= speed * Time.deltaTime; // Move slider to the left
            if (slider.value <= 0f) movingRight = true; // Reverse at min value
        }
        ChangingTextAtRuntime(slider.value);
    }

    public void StopSlider()
    {
   
      //  MaxAdsManager.instance.ShowRewardedVideo(GiveReward);
    }
    void GiveReward()
    {
        customManagerScript.instance.AdPlace = "MulipleRewardRV";
        if (isStopped) return; // Prevent stopping multiple times
        isStopped = true;
        RewardBtn.interactable = false;
        // Determine reward based on slider's value
        string reward = DetermineReward(slider.value);
        // Display the reward
        rewardText.text = "You got " + reward + " reward!";
        Debug.Log("Reward: " + reward);
        uIComplete.afterReward();
    }
    private string DetermineReward(float sliderValue)
    {
        // Define reward probabilities and sections
        if (sliderValue <= 0.18f || sliderValue>0.815f) // 0% to 35% range
        {
           rewardCount=LevelController.CurrentReward*2;
            CurrenciesController.Add(CurrencyType.Coins, rewardCount);
            uIComplete.rewardAmountText.text=rewardCount.ToString();
            return "2x";
        }
        else if (sliderValue > 0.18f && sliderValue<0.4f ) // 35% to 95% range
        {
            rewardCount = LevelController.CurrentReward * 3;
            CurrenciesController.Add(CurrencyType.Coins, rewardCount);
            uIComplete.rewardAmountText.text = rewardCount.ToString();
            return "3x";
        }
        else if (sliderValue > 0.6f && sliderValue < 0.815f)
        {
            rewardCount = LevelController.CurrentReward * 3;
            CurrenciesController.Add(CurrencyType.Coins, rewardCount);
            uIComplete.rewardAmountText.text = rewardCount.ToString();
            return "3x";
        }
        else if(sliderValue>=0.4f && sliderValue<=0.6f) // 95% to 100% range
        {
            rewardCount = LevelController.CurrentReward * 5;
            CurrenciesController.Add(CurrencyType.Coins, rewardCount);
            uIComplete.rewardAmountText.text = rewardCount.ToString();
            return "5x";
        }
        else
        {
            rewardCount = LevelController.CurrentReward * 2;
            CurrenciesController.Add(CurrencyType.Coins, rewardCount);
            uIComplete.rewardAmountText.text = rewardCount.ToString();
            return "2x";
        }
    }
    void ChangingTextAtRuntime(float sliderValue)
    {
        if (sliderValue <= 0.18f || sliderValue > 0.815f) // 0% to 35% range
        {
            rewardCount = LevelController.CurrentReward * 2;
            uIComplete.rewardAmountText.text = rewardCount.ToString();
        }
        else if (sliderValue > 0.18f && sliderValue < 0.4f) // 35% to 95% range
        {
            rewardCount = LevelController.CurrentReward * 3;
            uIComplete.rewardAmountText.text = rewardCount.ToString();
        }
        else if (sliderValue > 0.6f && sliderValue < 0.815f)
        {
            rewardCount = LevelController.CurrentReward * 3;
            uIComplete.rewardAmountText.text = rewardCount.ToString();
        }
        else if (sliderValue >= 0.4f && sliderValue <= 0.6f) // 95% to 100% range
        {
            rewardCount = LevelController.CurrentReward * 5;
            uIComplete.rewardAmountText.text = rewardCount.ToString();
        }
        else
        {
            rewardCount = LevelController.CurrentReward * 2;
            uIComplete.rewardAmountText.text = rewardCount.ToString();
        }
    }
}
