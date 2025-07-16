using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watermelon.BusStop;

namespace Watermelon
{
    public class PUFreezeTimerBehaviour : PUBehavior
    {
        private PUFreezeTimerSetting customsettings;
        private TweenCase delayTweenCase;
        public override void Init()
        {
            customsettings = (PUFreezeTimerSetting)settings;
        }

        public override bool Activate()
        {
            FreezeTimerNow();
            return true;
                
        }
        public void FreezeTimerNow()
        {
            StartCoroutine(freeze());
        }
        IEnumerator freeze()
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
        }
        public override void ResetBehavior()
        {
            IsBusy = false;
            delayTweenCase.KillActive();
        }
    }
}
