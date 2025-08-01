using TMPro;
using UnityEngine;

namespace Watermelon
{
    public class LivesReward : Reward
    {
        [SerializeField] float durationInMinutes = 60;

        [Space]
        [SerializeField] TextMeshProUGUI durationText;
        [SerializeField] string durationFormat = "{hh}hrs";

        public override void Init()
        {
            if (durationText != null)
            {
                durationText.text = TimeUtils.GetFormatedTime(durationInMinutes, durationFormat);
            }
        }

        public override void ApplyReward()
        {
            LivesManager.StartInfiniteLives(durationInMinutes * 60);
        }
    }
}
