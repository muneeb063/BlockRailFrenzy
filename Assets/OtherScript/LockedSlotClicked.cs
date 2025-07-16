using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Watermelon;
using Watermelon.BusStop;

public class LockedSlotClicked : MonoBehaviour
{
  /*  public bool isLeftSlot = false;
    public bool isRightSlot = false;*/
    public GameObject SlotToActivate;
    void Update()
    {
        // Check if left mouse button was clicked
        if (Input.GetMouseButtonDown(0))
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit hit;

            // Fire the ray and check if it hits a collider
            if (Physics.Raycast(ray, out hit))
            {
                // Check if the object hit is this one
                if (hit.transform == transform)
                {
                    OnModelClicked();
                }
            }
        }
    }

    void OnModelClicked()
    {
        //Debug.LogError("3D Model Clicked!");
        
        MaxAdsManager.instance.ShowRewardedVideo(UnlockSlots);
        //PUController.UsePowerUpCustom(PUType.Hint);
        // Call any custom method here
    }
    public void UnlockSlots()
    {
        customManagerScript.instance.AdPlace = "UnlockSlotRV";
        /*  if (isLeftSlot == true)
          {
              FindAnyObjectByType<EnvironmentBehavior>().Slot1Unlock();
          }
          if (isRightSlot == true)
          {
              FindAnyObjectByType<EnvironmentBehavior>().Slot2Unlock();
          }*/
        if (LevelController.DisplayLevelNumber == 3 && PlayerPrefs.GetInt("VipSlotUnlocked") == 0)
        {
            PlayerPrefs.SetInt("VipSlotUnlocked", 1);
        }
          //  FindObjectOfType<EnvironmentBehavior>().DockHand.SetActive(false);
        SlotToActivate.SetActive(true);
        SlotToActivate.gameObject.transform.GetChild(4).GetComponent<ParticleSystem>().Play();
        FindAnyObjectByType<EnvironmentBehavior>().Dock.ResetSlots();
        this.gameObject.SetActive(false);
    }
}
