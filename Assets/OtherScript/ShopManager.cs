using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using Watermelon;
public class ShopManager : MonoBehaviour
{
    // Start is called before the first frame update
    
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnBundleOneClicked()
    {
       /* if (CurrenciesController.Get(CurrencyType.Coins) >= 1500)
        {*/
            PUController.AddPowerUp(PUType.Undo, 1);
            PUController.AddPowerUp(PUType.Hint, 1);
            PUController.AddPowerUp(PUType.Shuffle, 1);
            PUController.AddPowerUp(PUType.FreezeTimer, 1);
         customManagerScript.instance.PurchaseSuccess();
        //  CurrenciesController.Substract(CurrencyType.Coins, 1500);
        // }
    }
    public void OnBundleTwoClicked()
    {
      /*  if (CurrenciesController.Get(CurrencyType.Coins) >= 2000)
        {*/
            PUController.AddPowerUp(PUType.Undo, 1);
            PUController.AddPowerUp(PUType.Hint, 1);
            PUController.AddPowerUp(PUType.Shuffle, 1);
            PUController.AddPowerUp(PUType.FreezeTimer, 1);
        Watermelon.LivesManager.AddLife();
        Watermelon.LivesManager.AddLife();
        Watermelon.LivesManager.AddLife();
        customManagerScript.instance.PurchaseSuccess();
        //  CurrenciesController.Substract(CurrencyType.Coins, 2000);
        // }
    }
    public void OnBundleThreeClicked()
    {
       /* if (CurrenciesController.Get(CurrencyType.Coins) >= 2500)
        {*/
            PUController.AddPowerUp(PUType.Undo, 3);
            PUController.AddPowerUp(PUType.Hint, 3);
            PUController.AddPowerUp(PUType.Shuffle, 3);
            PUController.AddPowerUp(PUType.FreezeTimer, 3);
            Watermelon.LivesManager.SetLifes(5);
        customManagerScript.instance.PurchaseSuccess();
        /// CurrenciesController.Substract(CurrencyType.Coins, 2500);
        // }
    }
    public void OnBundleFourClicked()
    {
      /*  if (CurrenciesController.Get(CurrencyType.Coins) >= 4000)
        {*/
            PUController.AddPowerUp(PUType.Undo, 5);
            PUController.AddPowerUp(PUType.Hint, 5);
            PUController.AddPowerUp(PUType.Shuffle, 5);
            Watermelon.LivesManager.SetLifes(8);
        PUController.AddPowerUp(PUType.FreezeTimer, 5);
        customManagerScript.instance.PurchaseSuccess();
        /*    CurrenciesController.Substract(CurrencyType.Coins, 4000);
        }*/
    }
    
    public void PurchasedFailed()
    {
        customManagerScript.instance.PurchaseFailed();
    }
    public void RemoveAds()
    {
        PlayerPrefs.SetInt("RemoveAds", 1);
       // MaxAdsManager.instance.HideBanner();
        customManagerScript.instance.PurchaseSuccess();
    }
}
