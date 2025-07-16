using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Purchasing;
using UnityEngine.Purchasing.Extension;
using UnityEngine.UI;

public class IAPScript : MonoBehaviour, IStoreListener
{
    private static IStoreController storeController;
    private static IExtensionProvider storeExtensionProvider;

    public ShopManager shopManager;

    public const string NO_ADS = "no_ads";
    public const string POWERUP_PACK = "powerup_pack";
    public const string PREMIUM_PACK = "premium_pack";
    public const string DIAMOND_PACK = "diamond_pack";
    public const string PLATINUM_PACK = "platinum_pack";

    [Header("Price Texts")]
    public Text powerupPrice_Txt;
    public Text premiumPrice_Text;
    public Text diamond_Price_Txt;
    public Text platinumPrice_Text;
    public Text removeadsPrice_Text;
    void Start()
    {
        if (storeController == null)
            InitializePurchasing();
    }

    public void InitializePurchasing()
    {
        var builder = ConfigurationBuilder.Instance(StandardPurchasingModule.Instance());

        // Add products
        builder.AddProduct(NO_ADS, ProductType.NonConsumable);
        builder.AddProduct(POWERUP_PACK, ProductType.Consumable);
        builder.AddProduct(PREMIUM_PACK, ProductType.NonConsumable);
        builder.AddProduct(DIAMOND_PACK, ProductType.Consumable);
        builder.AddProduct(PLATINUM_PACK, ProductType.Consumable);

        UnityPurchasing.Initialize(this, builder);
        Debug.Log("IAP: Initialization started.");
    }

    public void BuyProduct(string productId)
    {
        if (storeController == null) return;

        Product product = storeController.products.WithID(productId);
        if (product != null && product.availableToPurchase)
        {
            storeController.InitiatePurchase(product);
        }
        else
        {
            Debug.LogWarning("BuyProduct: Product not found or not available: " + productId);
        }
    }

    public void OnInitialized(IStoreController controller, IExtensionProvider extensions)
    {
        storeController = controller;
        storeExtensionProvider = extensions;
        Debug.Log("IAP: Initialized successfully.");

        UpdatePriceTexts();
    }

    private void UpdatePriceTexts()
    {
        if (storeController == null) return;

        // Safely set each price label from localized store metadata
        if (powerupPrice_Txt != null)
            powerupPrice_Txt.text = storeController.products.WithID(POWERUP_PACK)?.metadata.localizedPriceString;

        if (premiumPrice_Text != null)
            premiumPrice_Text.text = storeController.products.WithID(PREMIUM_PACK)?.metadata.localizedPriceString;

        if (diamond_Price_Txt != null)
            diamond_Price_Txt.text = storeController.products.WithID(DIAMOND_PACK)?.metadata.localizedPriceString;

        if (platinumPrice_Text != null)
            platinumPrice_Text.text = storeController.products.WithID(PLATINUM_PACK)?.metadata.localizedPriceString;
        if(removeadsPrice_Text!=null)
            removeadsPrice_Text.text=storeController.products.WithID(NO_ADS)?.metadata.localizedPriceString;
        Debug.Log("IAP: Price labels updated.");
    }

    public void OnInitializeFailed(InitializationFailureReason error)
    {
        Debug.LogError("IAP Initialization Failed: " + error);
        shopManager?.PurchasedFailed();
    }

    public void OnInitializeFailed(InitializationFailureReason error, string message)
    {
        Debug.LogError("IAP Initialization Failed: " + error + " | " + message);
        shopManager?.PurchasedFailed();
    }

    public PurchaseProcessingResult ProcessPurchase(PurchaseEventArgs args)
    {
        string purchasedId = args.purchasedProduct.definition.id;
        Debug.Log("IAP: Purchase successful for " + purchasedId);

        switch (purchasedId)
        {
            case NO_ADS:
                shopManager?.RemoveAds();
                break;

            case POWERUP_PACK:
                shopManager?.OnBundleOneClicked();
                break;

            case PREMIUM_PACK:
                shopManager?.OnBundleTwoClicked();
                break;

            case DIAMOND_PACK:
                shopManager?.OnBundleThreeClicked();
                break;

            case PLATINUM_PACK:
                shopManager?.OnBundleFourClicked();
                break;

            default:
                Debug.LogWarning("IAP: Unknown product ID: " + purchasedId);
                break;
        }

        return PurchaseProcessingResult.Complete;
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureReason failureReason)
    {
        Debug.LogError("IAP: Purchase failed - " + product.definition.id + " | Reason: " + failureReason);
        shopManager?.PurchasedFailed();
    }

    public void OnPurchaseFailed(Product product, PurchaseFailureDescription failureDescription)
    {
        Debug.LogError("IAP: Purchase failed - " + product.definition.id + " | Reason: " + failureDescription.reason);
        shopManager?.PurchasedFailed();
    }
}
