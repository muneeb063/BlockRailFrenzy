using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnalyticsMediator : MonoBehaviour
{
   // [SerializeField] DataManager dataManager;
    #region subscription
    private void OnEnable()
    {
 /*       StaticEvents.GameEvents.OnGameplay += GameStart;
        StaticEvents.GameEvents.OnGameWin += GameWin;
        StaticEvents.GameEvents.OnGameLoose += GameLoose;
        StaticEvents.GameEvents.OnRevive += GameRevive;*/
    }
    private void OnDisable()
    {
        /*StaticEvents.GameEvents.OnGameplay -= GameStart;
        StaticEvents.GameEvents.OnGameWin -= GameWin;
        StaticEvents.GameEvents.OnGameLoose -= GameLoose;
        StaticEvents.GameEvents.OnRevive -= GameRevive;*/
    }
    #endregion
    void GameStart()
    {
      //  string value = "LStart " + dataManager.persistantValues.currentLevelIndex;
       // CustomAnalyticsManager.instance?.GameStart(value);
    }
    void GameWin()
    {
       // string value = "LComplete " + dataManager.persistantValues.currentLevelIndex;
        //CustomAnalyticsManager.instance?.GameWin(value);
    }
    void GameLoose()
    {
       // string value = "LFail " + dataManager.persistantValues.currentLevelIndex +". Attempts : " + dataManager.gameData.level.GetLevelAttemptRate();
       // CustomAnalyticsManager.instance?.GameLoose(value);

    }
    void GameRevive()
    {
       // string value = "LRevive " + dataManager.persistantValues.currentLevelIndex;
        //CustomAnalyticsManager.instance?.GameRevive(value);

    }
}
