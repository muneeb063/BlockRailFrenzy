using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Watermelon;
public class PowerUpUnlocked : MonoBehaviour
{
    // Start is called before the first frame update
    public Image MainSprite;
    public Sprite[] powerupsSprites;
    public GameObject[] powerupsTexts;
    protected PUSettings settings;
    public PUSettings Settings => settings;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    private void OnEnable()
    {
        MainSprite.sprite = settings.Icon;
    }
}
