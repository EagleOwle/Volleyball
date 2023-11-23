using Unity.VisualScripting.FullSerializer;
using UnityEngine;

public class WorkingWithVkBridge : MonoBehaviour
{
    public VkBridgeController bridge;

    public void Start()
    {
        bridge.VKWebAppInit();
        ShowBannerAd();
        ShowAdsReward();
    }

    public void ShowBannerAd()
    {
        bridge.VKWebAppShowBannerAd(new VKWebAppShowBannerAdStruct
        {
            banner_location = BannerAdFormat.bottom,
            layout_type = "overlay",
            can_close = false
        }, BannerAdResult);
    }
    public void ShowAdsReward()
    {
        bridge.VKWebAppShowNativeAds(new VKWebAppShowNativeAdsStruct
        {
            ad_format = AdFormat.reward
        }, NativeAdsResult);

    }

    public void BannerAdResult(VKWebAppShowNativeAdsResultStruct result)
    {
        Debug.Log("Banner Ad Result: " + result.result);
        // Дополнительная обработка результата показа баннера, если необходимо
    }

    public void NativeAdsResult(VKWebAppShowNativeAdsResultStruct result)
    {
        Debug.Log("Native Ads Result: " + result.result);
        // Дополнительная обработка результата просмотра рекламы, если необходимо
    }
}
