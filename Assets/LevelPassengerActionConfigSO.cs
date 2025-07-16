using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "LevelPassengerActionConfig", menuName = "Game Data/Level Passenger Action Config")]
public class LevelPassengerActionConfigSO : ScriptableObject
{
    [Tooltip("Level-wise passenger indices to affect. Index 0 = Level 1, etc.")]
    public List<LevelPassengerActionEntry> levelPassengerActions = new();
}

[System.Serializable]
public class LevelPassengerActionEntry
{
    public string label = "Level X";

    [Tooltip("Passenger index and desired freeze count.")]
    public List<PassengerFreezeData> passengerFreezeData;
    [Tooltip("Passenger index and desired bomb blast time.")]
    public List<PassengerBombData> passengerBombData;
    /*[Tooltip("How many of the above should be affected for this level.")]
    public int countToAffect;*/
    [Tooltip("Passenger index and desired Locked Count.")]
    public List<PassengerLockedData> passengerLockedData;
    [Tooltip("Passenger index and desired Key Count.")]
    public List<PassengerKeyData> passengerKeyData;
}

[System.Serializable]
public class PassengerFreezeData
{
    public int passengerIndex;
    public int freezeCount = 3;
}
[System.Serializable]
public class PassengerBombData
{
    public int passengerIndex;
    public float blastTime = 5f;
}
[System.Serializable]
public class PassengerLockedData
{
    public int passengerIndex;
    public int LockCount = 0;
}
[System.Serializable]
public class PassengerKeyData
{
    public int passengerIndex;
}