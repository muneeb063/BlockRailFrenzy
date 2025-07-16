#pragma warning disable 0067

using UnityEngine;
using Watermelon.IAPStore;
using Watermelon.SkinStore;

namespace Watermelon.BusStop
{
    public class RaycastController : MonoBehaviour
    {
        private UIStore iapStorePage;
        private UIMainMenu mainMenuPage;
        private UISkinStore storePage;

        private static bool isActive;

        public static event SimpleCallback OnInputActivated;
        public static event SimpleCallback OnMovementInputActivated;
        public bool shouldClick = false;
        public void Init()
        {
            isActive = true;

            iapStorePage = UIController.GetPage<UIStore>();
            mainMenuPage = UIController.GetPage<UIMainMenu>();
            storePage = UIController.GetPage<UISkinStore>();
        }

        private void Update()
        {
            if (!isActive) return;
            //Debug.LogError("Calling");/*
          /*  if (PlayerPrefs.GetInt("IsFirst") == 0)
            {*/
                if (Input.GetMouseButtonDown(0) && !IsRaycastBlockedByUI() && !UIController.IsPopupOpened /*&&shouldClick==true*/)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit))
                    {
                        IClickableObject clickableObject = hit.transform.GetComponent<IClickableObject>();
                        if (clickableObject != null)
                        {
                            if (!GameController.IsGameActive)
                            {
                                if (LivesManager.Lives > 0)
                                {
                                    GameController.StartGame();
                                    clickableObject.OnObjectClicked();
                                }
                                else
                                {
                                    /* mainMenuPage.ShowAddLivesPanel();
                                     customManagerScript.instance.noLives.SetActive(true);*/
                                    customManagerScript.instance.uiGames.OnsettingHomeClicked();
                                    // Debug.LogError("Calling");
                                }
                            }
                            else
                            {
                                clickableObject.OnObjectClicked();
                            }
                        }
                    }
                }
           // }
           /* else if(PlayerPrefs.GetInt("IsFirst") == 1)
            {
                if (Input.GetMouseButtonDown(0) && !MainMenuBlocked() && !UIController.IsPopupOpened *//*&&shouldClick==true*//*)
                {
                    Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
                    RaycastHit hit;

                    if (Physics.Raycast(ray, out hit))
                    {
                        IClickableObject clickableObject = hit.transform.GetComponent<IClickableObject>();
                        if (clickableObject != null)
                        {
                            if (!GameController.IsGameActive)
                            {
                                if (LivesManager.Lives > 0)
                                {
                                    GameController.StartGame();
                                    clickableObject.OnObjectClicked();
                                }
                                else
                                {
                                    *//* mainMenuPage.ShowAddLivesPanel();
                                     customManagerScript.instance.noLives.SetActive(true);*//*
                                    customManagerScript.instance.uiGames.OnsettingHomeClicked();
                                    // Debug.LogError("Calling");
                                }
                            }
                            else
                            {
                                clickableObject.OnObjectClicked();
                            }
                        }
                    }
                }
            }*/
        }

        private bool IsRaycastBlockedByUI()
        {
            return iapStorePage.IsPageDisplayed || storePage.IsPageDisplayed || mainMenuPage.IsPageDisplayed;
        }
        private bool MainMenuBlocked()
        {
            return mainMenuPage.IsPageDisplayed;
        }
        public static void Enable()
        {
            isActive = true;
            OnInputActivated?.Invoke();
        }

        public static void Disable()
        {
            isActive = false;
        }
    }
}
