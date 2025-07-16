using GLTFast.Schema;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class ProfileIdentity : MonoBehaviour
{
    public int Identity;
    // Start is called before the first frame update
    public ProfilePanelScript panelScript;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnProfilebuttonClicked()
    {
        panelScript.OnProfileButtonClicked();
        panelScript.ProfileButtons[Identity].transform.GetChild(1).gameObject.SetActive(true);
        panelScript.MainSprite.sprite = panelScript.Characters_Sprites[Identity];
    }
}
