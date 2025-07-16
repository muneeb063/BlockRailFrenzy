using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;
using Watermelon;

public class ComboSpriteManager : MonoBehaviour
{
    public GameObject[] comboSprites;
    public float comboResetTime = 1.5f;

    public float fadeDuration = 1f;
    public float scaleStart = 1.5f;
    public float scaleEnd = 1f;

    private int comboCount = 0;
    private float lastActionTime;

    void Start()
    {
        foreach (GameObject obj in comboSprites)
        {
            obj.SetActive(false);
            Image img = obj.GetComponent<Image>();
            if (img != null)
            {
                img.color = new Color(1, 1, 1, 0); // Start invisible
            }
            obj.transform.localScale = Vector3.one;
        }

       
    }

    public void RegisterAction()
    {
       /* float currentTime = Time.time;

        if (currentTime - lastActionTime <= comboResetTime)
        {
            comboCount++;
        }
        else
        {
            comboCount = 1;
        }

        lastActionTime = currentTime;

        if (comboCount >= 2)
        {
            ShowCombo(comboCount);
        }*/
    }

    public void ShowCombo(/*int comboLevel*/)
    {
      //  int index = Mathf.Clamp(comboLevel - 1, 0, comboSprites.Length - 1);
      int index=Random.Range(0, comboSprites.Length);
        // Deactivate all first
        for (int i = 0; i < comboSprites.Length; i++)
        {
            comboSprites[i].SetActive(false);
        }

        GameObject currentSprite = comboSprites[index];
        currentSprite.SetActive(true);

        Transform trans = currentSprite.transform;
        Image img = currentSprite.GetComponent<Image>();

        if (trans != null) trans.DOKill(true);
        if (img != null) img.DOKill(true);

        // Reset transform and color
        trans.localScale = Vector3.one * 0.5f;
        if (img != null) img.color = new Color(1, 1, 1, 1);

        // Tween sequence
        Sequence popSequence = DOTween.Sequence();

        popSequence.Append(trans.DOScale(2.5f, 0.15f).SetEase(DG.Tweening.Ease.OutQuad))
                   .Append(trans.DOScale(2f, 0.15f).SetEase(DG.Tweening.Ease.OutBack));

        if (img != null)
        {
            popSequence.Append(img.DOFade(0f, fadeDuration).SetEase(DG.Tweening.Ease.InQuad));
        }

        popSequence.OnComplete(() => currentSprite.SetActive(false));

        // Play sound
        if (AudioController.AudioClips.CombosClips != null &&
            index < AudioController.AudioClips.CombosClips.Length &&
            AudioController.AudioClips.CombosClips[index] != null)
        {
            AudioController.PlaySound(AudioController.AudioClips.CombosClips[index], 0.5f);
        }
    }
}
