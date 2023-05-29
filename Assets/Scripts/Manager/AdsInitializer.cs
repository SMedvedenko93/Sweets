using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener, IUnityAdsLoadListener, IUnityAdsShowListener
{
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;

    [SerializeField] string _androidAdsInt = "Interstitial_Android";
    [SerializeField] string _iOsAdUnitIdInt = "Interstitial_iOS";

    [SerializeField] string _androidAdsRew = "Rewarded_Android";
    [SerializeField] string _iOsAdUnitIdRew = "Rewarded_iOS";
    private string _adsInt, _adsRew;

    void Awake()
    {
        InitializeAds();
    }

    public void InitializeAds()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSGameId
            : _androidGameId;
        Advertisement.Initialize(_gameId, _testMode, this);
    }

    public void LoadInterstitialAds()
    {
        _adsInt = (Application.platform == RuntimePlatform.IPhonePlayer)
        ? _iOsAdUnitIdInt
        : _androidAdsInt;
        Advertisement.Load(_adsInt, this);
    }

    public void ShowInterstitialAds()
    {
        Advertisement.Show(_adsInt, this);
    }

    public void LoadRewardedAds()
    {
        _adsRew = (Application.platform == RuntimePlatform.IPhonePlayer)
        ? _iOsAdUnitIdRew
        : _androidAdsRew;
        Advertisement.Load(_adsRew, this);
    }

    public void ShowRewardedAds()
    {
        Debug.Log("ShowRewardedAds.");
        Advertisement.Show(_adsRew, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log("OnUnityAdsAdLoaded.");
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.Log("OnUnityAdsFailedToLoad.");
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.Log("OnUnityAdsShowFailure.");
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowStart(string placementId)
    {
        Debug.Log("OnUnityAdsShowStart.");
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowClick(string placementId)
    {
        Debug.Log("OnUnityAdsShowClick.");
        //throw new System.NotImplementedException();
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        Debug.Log($"OnUnityAdsShowComplete: [{showCompletionState}]: {placementId}");
        switch (placementId)
        {
            case "Interstitial_Android":
                Debug.Log("Completed Interstitial Ad");
                GameManager.instance.GetComponent<GameManager>().LoadingLevel();
                break;
            case "Interstitial_iOS":
                Debug.Log("Completed Interstitial Ad");
                GameManager.instance.GetComponent<GameManager>().LoadingLevel();
                break;
            case "Rewarded_Android":
                if (showCompletionState == UnityAdsShowCompletionState.COMPLETED)
                {
                    Debug.Log("Completed Rewarded Ad");
                    GameManager.instance.GetComponent<GameManager>().SkipLevel();
                }
                break;
            case "Rewarded_iOS":
                if (showCompletionState == UnityAdsShowCompletionState.COMPLETED)
                {
                    Debug.Log("Completed Rewarded Ad");
                    GameManager.instance.GetComponent<GameManager>().SkipLevel();
                }
                break;
        }
        /*
        if (_adsRew.Equals("Rewarded_Android") && showCompletionState == UnityAdsShowCompletionState.COMPLETED)
        {
            Debug.Log("Completed Rewarded Ad");
        }
        else
        {
            Debug.Log("NO Rewarded Ad");
        }
        */
        //throw new System.NotImplementedException();
    }
}