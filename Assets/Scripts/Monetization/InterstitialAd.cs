using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class InterstitialAd : Ad, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public override void Load()
    {
        base.Load();
        Advertisement.Load(adUnitID, this);
    }

    public override void Show()
    {
        Advertisement.Show(adUnitID, this);
    }

    public void OnUnityAdsAdLoaded(string _adUnitId)
    {
        OnLoaded();
    }

    public void OnUnityAdsFailedToLoad(string _adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit: {_adUnitId} - {error.ToString()} - {message}");
    }

    public void OnUnityAdsShowFailure(string _adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {_adUnitId}: {error.ToString()} - {message}");
        OnAdEnded();
    }

    public void OnUnityAdsShowStart(string _adUnitId)
    {
        OnAdStarted();
    }

    public void OnUnityAdsShowClick(string _adUnitId)
    {
        //Tracking events
    }

    public void OnUnityAdsShowComplete(string _adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        OnAdEnded();
    }
}
