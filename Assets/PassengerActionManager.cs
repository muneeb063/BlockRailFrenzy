using System.Collections.Generic;
using UnityEngine;
using Watermelon;
using Watermelon.BusStop;

public class PassengerActionManager : MonoBehaviour
{
    [Header("Passengers and Level Info")]
    public List<HumanoidCharacterBehavior> passengers;
    public List<HumanoidCharacterBehavior> FreezedPassengers;
    public List<HumanoidCharacterBehavior> BombPassengers;
    public List<HumanoidCharacterBehavior> LockedCharacters;
    public List<HumanoidCharacterBehavior> KeyPassengers;
    public int currentLevel = 1;

    [Header("Configs")]
    public LevelPassengerActionConfigSO levelConfig;

    [Tooltip("ScriptableObject that implements freeze action.")]
    public ScriptableObject freezeActionScriptableObject;

    [Tooltip("ScriptableObject that implements bomb action.")]
    public ScriptableObject bombActionScriptableObject;

    [Tooltip("ScriptableObject that implements LockedPassenger action.")]
    public ScriptableObject LockActionScriptableObject;

    [Tooltip("ScriptableObject that implements KeyPassenger action.")]
    public ScriptableObject KeyActionScriptableObject;

    private IPassengerAction freezeAction;
    private IPassengerAction bombAction;
    private IPassengerAction LockedAction;
    private IPassengerAction KeyAction;
    public static PassengerActionManager instance;
    public GameObject KeyPrefab;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        freezeAction = freezeActionScriptableObject as IPassengerAction;
        bombAction = bombActionScriptableObject as IPassengerAction;
        LockedAction=LockActionScriptableObject as IPassengerAction;
        KeyAction=KeyActionScriptableObject as IPassengerAction;

        if (freezeAction == null)
            Debug.LogError("Assigned freeze ScriptableObject does not implement IPassengerAction.");

        if (bombAction == null)
            Debug.LogError("Assigned bomb ScriptableObject does not implement IPassengerAction.");

        if(LockedAction==null)
            Debug.LogError("Assigned Lock ScriptableObject does not implement IPassengerAction.");

        if (KeyAction == null)
            Debug.LogError("Assigned Key ScriptableObject does not implement IPassengerAction.");
    }

    public void ApplyAllActionsForLevel()
    {
        int levelToApply = LevelController.DisplayLevelNumber + 1;
        ApplyActionToPassengers(levelToApply);
    }

    public void ApplyActionToPassengers(int level)
    {
        if (levelConfig == null || freezeAction == null || bombAction == null)
        {
            Debug.LogWarning("Missing levelConfig or one of the action ScriptableObjects.");
            return;
        }

        if (level < 1 || level > levelConfig.levelPassengerActions.Count)
        {
            Debug.LogWarning("Level out of range.");
            return;
        }

        var levelData = levelConfig.levelPassengerActions[level - 1];

        // Freeze Passengers
        foreach (var data in levelData.passengerFreezeData)
        {
            int index = data.passengerIndex;
            int freezeCount = data.freezeCount;

            if (index >= 0 && index < passengers.Count)
            {
                HumanoidCharacterBehavior passenger = passengers[index];
                passenger.freezeCounter = freezeCount;
                passenger.FreezeCounterText.text = freezeCount.ToString();
                freezeAction.Apply(passenger);
                if (!FreezedPassengers.Contains(passenger))
                    FreezedPassengers.Add(passenger);
            }
            else
            {
                Debug.LogWarning($"Freeze index {index} is out of bounds.");
            }
        }

        // Bomb Passengers
        foreach (var data in levelData.passengerBombData)
        {
            int index = data.passengerIndex;
            float timer = data.blastTime;

            if (index >= 0 && index < passengers.Count)
            {
                HumanoidCharacterBehavior passenger = passengers[index];
                passenger.BombTime = timer; // Ensure you have this field in your passenger script
                passenger.BombTimerText.text = timer.ToString();
                bombAction.Apply(passenger);
                if (!BombPassengers.Contains(passenger))
                    BombPassengers.Add(passenger);
                passenger.ResetBombTimer();
            }
            else
            {
                Debug.LogWarning($"Bomb index {index} is out of bounds.");
            }
        }
        // Locked Passengers
        foreach (var data in levelData.passengerLockedData)
        {
            int index = data.passengerIndex;
            int LockCounts = data.LockCount;

            if (index >= 0 && index < passengers.Count)
            {
                HumanoidCharacterBehavior passenger = passengers[index];
                passenger.LockCount = LockCounts;
                passenger.LockCountText.text = LockCounts.ToString();
                LockedAction.Apply(passenger);
                if (!LockedCharacters.Contains(passenger))
                    LockedCharacters.Add(passenger);
            }
            else
            {
                Debug.LogWarning($"Locked index {index} is out of bounds.");
            }
        }
        // Key Passengers
        foreach (var data in levelData.passengerKeyData)
        {
            int index = data.passengerIndex;

            if (index >= 0 && index < passengers.Count)
            {
                HumanoidCharacterBehavior passenger = passengers[index];
                KeyAction.Apply(passenger);
                if (!KeyPassengers.Contains(passenger))
                    KeyPassengers.Add(passenger);
            }
            else
            {
                Debug.LogWarning($"Key index {index} is out of bounds.");
            }
        }
    }

    public void ClearLists()
    {
        passengers.Clear();
        FreezedPassengers.Clear();
        BombPassengers.Clear();
        LockedCharacters.Clear();
        KeyPassengers.Clear();
    }

    public void EnableFreezedPassengersColliders(bool state)
    {
        if (FreezedPassengers == null || FreezedPassengers.Count == 0)
            return;

        foreach (var passenger in FreezedPassengers)
        {
            if (passenger != null)
            {
                BoxCollider boxCollider = passenger.GetComponent<BoxCollider>();
                if (boxCollider != null)
                {
                    boxCollider.enabled = state;
                }
            }
        }
    }
    public void StartBombTimers()
    {
        if (BombPassengers == null || BombPassengers.Count == 0)
            return;

        foreach (var passenger in BombPassengers)
        {
            if (passenger != null)
            {
                passenger.ResetBombTimer();
               passenger.StartBombTimer();
            }
        }
    }
    public void DiffuseBombs()
    {
        if (BombPassengers == null || BombPassengers.Count == 0)
            return;
       
        foreach (var passenger in BombPassengers)
        {
            if (passenger != null)
            {
                passenger.DiffuseBomb();
            }
        }
    }
}
