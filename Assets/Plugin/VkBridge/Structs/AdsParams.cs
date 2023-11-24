#region VKNativeAds
public struct VKWebAppShowNativeAdsStruct
{
    /// <summary>
    /// Формат нативной рекламы
    /// </summary>
    public AdFormat ad_format { get; set; }
    /// <summary>
    /// Только для ad_format = reward. Использовать ли механизм показа interstitial рекламы в случае отсутствия rewarded video
    /// </summary>
    public bool? use_waterfall { get; set; }
}
#endregion

#region VkBannerAd
public struct VKWebAppShowBannerAdStruct
{
    /// <summary>
    /// Расположение баннера. Возможные значения: "top" или "bottom".
    /// </summary>
    public BannerAdFormat banner_location { get; set; }

    /// <summary>
    /// Тип фона баннера. Возможные значения: "resize" или "overlay".
    /// </summary>
    public string layout_type { get; set; }

    /// <summary>
    /// Информация о том, отображать ли кнопку закрытия в баннере.
    /// </summary>
    public bool? can_close { get; set; }
   
}
#endregion

public struct VKWebAppShowNativeAdsResultStruct
{
    /// <summary>
    /// Была ли показана реклама пользователю.
    /// </summary>
    public bool result { get; set; }
}
public enum AdFormat
{
    interstitial,
    reward
}
public enum BannerAdFormat
{
    top,
    bottom
}
public enum BannerLayoutType
{
    resize,
    overlay
}
