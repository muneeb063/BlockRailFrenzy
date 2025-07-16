using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static UnityEngine.Rendering.DebugUI;
public class ProfilePanelScript : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] ProfileButtons;
    public Image MainSprite;
    public Sprite[] Characters_Sprites;
    public InputField NameField;
    void Start()
    {
        string savedText = PlayerPrefs.GetString("SavedText", ""); // default is empty
        NameField.text = savedText;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnProfileButtonClicked()
    {
        for (int i = 0; i < ProfileButtons.Length; i++)
        {
            ProfileButtons[i].transform.GetChild(1).gameObject.SetActive(false);
        }
       
    }
    public void CancelBtnClicked()
    {
        
        SaveText();
        gameObject.SetActive(false);
    }
    public void SaveText()
    {
        string inputText = NameField.text;
        PlayerPrefs.SetString("SavedText", inputText);
        PlayerPrefs.Save(); // Optional, forces save immediately
        Debug.Log("Saved Text: " + inputText);
    }
}
