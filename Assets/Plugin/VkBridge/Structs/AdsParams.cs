#region VKNativeAds
public struct VKWebAppShowNativeAdsStruct
{
    /// <summary>
    /// ������ �������� �������
    /// </summary>
    public AdFormat ad_format { get; set; }
    /// <summary>
    /// ������ ��� ad_format = reward. ������������ �� �������� ������ interstitial ������� � ������ ���������� rewarded video
    /// </summary>
    public bool? use_waterfall { get; set; }
}
#endregion

#region VkBannerAd
public struct VKWebAppShowBannerAdStruct
{
    /// <summary>
    /// ������������ �������. ��������� ��������: "top" ��� "bottom".
    /// </summary>
    public BannerAdFormat banner_location { get; set; }

    /// <summary>
    /// ��� ���� �������. ��������� ��������: "resize" ��� "overlay".
    /// </summary>
    public string layout_type { get; set; }

    /// <summary>
    /// ���������� � ���, ���������� �� ������ �������� � �������.
    /// </summary>
    public bool? can_close { get; set; }
   
}
#endregion

public struct VKWebAppShowNativeAdsResultStruct
{
    /// <summary>
    /// ���� �� �������� ������� ������������.
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
