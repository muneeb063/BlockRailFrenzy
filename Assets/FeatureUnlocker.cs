using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class FeatureUnlocker : MonoBehaviour
{
    [System.Serializable]
    public class Feature
    {
        public string featureName;
        public Sprite featureIcon;
        public int unlockLevel;
        public string playerPrefsKey;
    }

    [Header("UI References")]
    public GameObject featurePanel;
    public Transform featureListParent;              // Container where feature items will be added
    public GameObject featureItemPrefab;             // Prefab with Image + Text
    public GameObject runtimePrefab;
    [Header("Features")]
    public List<Feature> features = new List<Feature>();

    private void Start()
    {
        featurePanel.SetActive(false);
    }

    public void CheckAndShowFeatureUnlock()
    {
        int currentLevel = PlayerPrefs.GetInt("CurrentLevel", 1);
        bool anyFeatureUnlocked = false;

        foreach (Transform child in featureListParent)
        {
            Destroy(child.gameObject);
        }

        foreach (var feature in features)
        {
            if (currentLevel == feature.unlockLevel && PlayerPrefs.GetInt(feature.playerPrefsKey, 0) == 0)
            {
                ShowFeatureItem(feature);
                PlayerPrefs.SetInt(feature.playerPrefsKey, 1);
                anyFeatureUnlocked = true;
            }
        }

        if (anyFeatureUnlocked)
        {
            featurePanel.SetActive(true);
            PlayerPrefs.Save();
        }
        else
        {
            featurePanel.SetActive(false); // Just in case no feature is shown
        }
    }


    private void ShowFeatureItem(Feature feature)
    {
        GameObject newItem = Instantiate(featureItemPrefab, featureListParent);
        Image icon = newItem.transform.Find("Icon").GetComponent<Image>();
        Text nameText = newItem.transform.Find("Name").GetComponent<Text>();

        icon.sprite = feature.featureIcon;
        nameText.text = feature.featureName;
        customManagerScript.instance.StopTimer();
    }
}
