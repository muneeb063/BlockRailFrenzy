using UnityEngine;

[CreateAssetMenu(menuName = "Feature Unlock Data")]
public class FeatureUnlockData : ScriptableObject
{
    public FeatureUnlockEntry[] featureUnlocks;
}

[System.Serializable]
public class FeatureUnlockEntry
{
    public int unlockLevel;           // The level at which this feature unlocks
    public Sprite featureIcon;        // The icon to display for this feature
}
