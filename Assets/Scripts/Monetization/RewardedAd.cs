using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;
using System;

public class RewardedAd : Ad, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public event Action OnRewardedCallback;

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
        //Rewarded not shown correctly -> no reward for the player
        Debug.Log($"Error showing Ad Unit {_adUnitId}: {error.ToString()} - {message}");
        OnAdEnded(false);
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
        OnAdEnded(true);
    }

    protected override void OnAdEnded(bool success = true)
    {
        base.OnAdEnded();
        //Let's reward the player
        OnRewardedCallback?.Invoke();
    }
}
