using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FeatureUnlockUI : MonoBehaviour
{
    [Header("References")]
    public FeatureUnlockData unlockData;
    public Image progressBar;
    public Image featureIconImage;

    [Header("Progress Settings")]
    public float fillSpeed = 1.5f;

    [Header("Debug")]
    public int currentLevel = 0;

    private int currentFeatureIndex = 0;
    private bool isFilling = false;

    void Start()
    {
        UpdateUI();
    }

    public void SetLevel(int level)
    {
        currentLevel = level;
        UpdateUI();
    }

    void UpdateUI()
    {
        if (currentFeatureIndex >= unlockData.featureUnlocks.Length)
            return;

        FeatureUnlockEntry currentFeature = unlockData.featureUnlocks[currentFeatureIndex];
        featureIconImage.sprite = currentFeature.featureIcon;

        int prevLevel = currentFeatureIndex == 0 ? 0 : unlockData.featureUnlocks[currentFeatureIndex - 1].unlockLevel;
        int targetLevel = currentFeature.unlockLevel;

        float progress = Mathf.InverseLerp(prevLevel, targetLevel, currentLevel);

        if (currentLevel >= targetLevel)
        {
            StartCoroutine(FillToCompleteThenAdvance(progress));
        }
        else
        {
            StartCoroutine(FillProgressBarSmooth(progress));
        }
    }

    IEnumerator FillProgressBarSmooth(float target)
    {
        if (isFilling) yield break;
        isFilling = true;

        float start = progressBar.fillAmount;
        float t = 0f;

        while (t < 1f)
        {
            t += Time.deltaTime * fillSpeed;
            progressBar.fillAmount = Mathf.Lerp(start, target, t);
            yield return null;
        }

        progressBar.fillAmount = target;
        isFilling = false;
    }

    IEnumerator FillToCompleteThenAdvance(float currentProgress)
    {
        if (isFilling) yield break;
        isFilling = true;

        // Fill to 100% first
        yield return StartCoroutine(FillProgressBarSmooth(1f));
        yield return new WaitForSeconds(0.2f);

        // Unlock current feature
        OnFeatureUnlocked();

        // If no more features, just reset bar
        if (currentFeatureIndex >= unlockData.featureUnlocks.Length)
        {
            progressBar.fillAmount = 0f;
            isFilling = false;
            yield break;
        }

        // Show next feature icon
        FeatureUnlockEntry nextFeature = unlockData.featureUnlocks[currentFeatureIndex];
        featureIconImage.sprite = nextFeature.featureIcon;

        // Reset progress bar
        progressBar.fillAmount = 0f;
        yield return new WaitForSeconds(0.1f);

        // Calculate new progress for current level against the new unlock target
        int prevLevel = currentFeatureIndex == 0 ? 0 : unlockData.featureUnlocks[currentFeatureIndex - 1].unlockLevel;
        int targetLevel = nextFeature.unlockLevel;
        float newProgress = Mathf.InverseLerp(prevLevel, targetLevel, currentLevel);

        // Fill again based on same current level
        yield return StartCoroutine(FillProgressBarSmooth(newProgress));

        isFilling = false;
    }

    void OnFeatureUnlocked()
    {
        Debug.Log("Feature Unlocked: " + unlockData.featureUnlocks[currentFeatureIndex].featureIcon.name);
        currentFeatureIndex++;
    }
}
