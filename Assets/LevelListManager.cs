using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.UI;
using Watermelon.BusStop;
using System.Collections.Generic;
public class LevelListManager : MonoBehaviour
{
    [Header("Level Settings")]
    public int totalLevels = 50;
    public int currentLevel = 4;

    [Header("References")]
    public GameObject levelButtonPrefab;
    public Transform contentParent;
    public ScrollRect scrollRect;

    [Header("Scroll Settings")]
    [SerializeField] private float scrollSpeed = 5f;
    private List<GameObject> levelButtons = new List<GameObject>();
    void Start()
    {
        //currentLevel = LevelController.DisplayLevelNumber + 1;
        GenerateLevelButtons();
        ScrollToCurrentLevel();
    }

    void GenerateLevelButtons()
    {
        //levelButtons.Clear();
        currentLevel = LevelController.DisplayLevelNumber + 1;
      //  Debug.LogError("Current LEvel " + currentLevel);
        for (int i = 1; i <= totalLevels; i++)
        {
            GameObject levelButton = Instantiate(levelButtonPrefab, contentParent);
            levelButton.transform.SetAsFirstSibling(); // For bottom-to-top order

            // Set level number text
            Text levelText = levelButton.GetComponentInChildren<Text>();
            if (levelText != null)
                levelText.text = i.ToString();

            // Enable current level icon only for current level
            Transform currentIcon = levelButton.transform.Find("CurrentLevelIcon");
            if (currentIcon != null)
                currentIcon.gameObject.SetActive(i == currentLevel);

            levelButtons.Add(levelButton);
        }
    }
    private void UpdateCurrentLevelIcon()
    {
        for (int i = 0; i < levelButtons.Count; i++)
        {
            Transform icon = levelButtons[i].transform.Find("CurrentLevelIcon");
            if (icon != null)
                icon.gameObject.SetActive((i + 1) == currentLevel); // +1 because level 1 = index 0
        }
    }
    public void ScrollToCurrentLevel()
    {
        currentLevel = LevelController.DisplayLevelNumber + 1;
        // Debug.LogError("Current LEvel " + currentLevel);
        
        StartCoroutine(ScrollToCurrentLevelCoroutine());
    }

    System.Collections.IEnumerator ScrollToCurrentLevelCoroutine()
    {
        UpdateCurrentLevelIcon();
        // Wait for UI layout to finish
        yield return new WaitForEndOfFrame();

        // Since reverse arrangement is ON, do not use 1 - formula
        float targetPosition = (float)(currentLevel - 1) / (totalLevels - 1);
        float startPosition = scrollRect.verticalNormalizedPosition;

        float t = 0f;
        while (t < 1f)
        {
            t += Time.deltaTime * scrollSpeed;
            scrollRect.verticalNormalizedPosition = Mathf.Lerp(startPosition, targetPosition, t);
            yield return null;
        }

        scrollRect.verticalNormalizedPosition = targetPosition;
    }
}
