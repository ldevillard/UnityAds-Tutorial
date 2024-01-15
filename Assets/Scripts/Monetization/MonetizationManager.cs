using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.SceneManagement;

public class MonetizationManager : MonoBehaviour, IUnityAdsInitializationListener
{
    static public MonetizationManager Instance;

    [SerializeField] string androidGameId;
    [SerializeField] string iOSGameId;
    [SerializeField] bool testMode = true;

    private string gameId;

    [SerializeField] BannerAd bannerAd;
    [SerializeField] InterstitialAd interstitialAd;
    [SerializeField] RewardedAd rewardedAd;

    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;

        DontDestroyOnLoad(this);

        InitializeAds();
    }

    public void InitializeAds()
    {
#if UNITY_IOS
            gameId = iOSGameId;
#elif UNITY_ANDROID
            gameId = androidGameId;
#elif UNITY_EDITOR
            gameId = androidGameId; //Only for testing the functionality in the Editor
#endif
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(gameId, testMode, this);
        }

        rewardedAd.OnRewardedCallback += RewardedCallback;
    }

    void OnDestroy()
    {
        rewardedAd.OnRewardedCallback -= RewardedCallback;
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");

        bannerAd.Load();
        interstitialAd.Load();
        rewardedAd.Load();
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    public void ShowInterstitial()
    {
        if (interstitialAd.IsReady)
        {
            interstitialAd.Show();
        }
        else
        {
            Debug.LogWarning("Interstitial ad isn't ready to show!");
        }
    }

    public void ShowRewarded()
    {
        if (rewardedAd.IsReady)
        {
            rewardedAd.Show();
        }
        else
        {
            Debug.LogWarning("Rewarded ad isn't ready to show!");
        }
    }

    void RewardedCallback()
    {
        Debug.Log("Reward the player !");
        //For example, save the previous score to start with it !
        PlayerPrefs.SetInt("Score", ScoreManager.Instance.score);
        SceneManager.LoadSceneAsync(0);
    }
}