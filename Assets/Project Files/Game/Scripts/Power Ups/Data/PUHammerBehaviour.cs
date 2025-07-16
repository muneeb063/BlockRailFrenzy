using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Watermelon
{
    public class PUHammerBehaviour : PUBehavior
    {
        private PUHammerSettings customsettings;
        private TweenCase delayTweenCase;
        public override void Init()
        {
            customsettings = (PUHammerSettings)settings;
        }

        public override bool Activate()
        {
            ApplyHammerEffect();
            return true;

        }
        public void ApplyHammerEffect()
        {
            // StartCoroutine(freeze());
            PassengerActionManager.instance.EnableFreezedPassengersColliders(true);
            customManagerScript.instance.isHammerPowerupEnabled = true;
            customManagerScript.instance.HammerUIState(true);
            
        }
       /* IEnumerator freeze()
        {
            customManagerScript.instance.FreezedEffectImage.SetActive(true);
            customManagerScript.instance.FreezedPng.SetActive(true);
            IsBusy = true;
            customManagerScript.instance.isTimerPowerUpUsed = true;
            customManagerScript.instance.StopTimer();
            yield return new WaitForSeconds(15f);
            customManagerScript.instance.ResumeTimer();
            customManagerScript.instance.isTimerPowerUpUsed = false;
            customManagerScript.instance.FreezedEffectImage.SetActive(false);
            customManagerScript.instance.FreezedPng.SetActive(false);
            IsBusy = false;
        }*/

        public override void ResetBehavior()
        {
            IsBusy = false;
            delayTweenCase.KillActive();
        }
    }
}
