using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Ad : MonoBehaviour
{
    [SerializeField] string androidAdUnitID;
    [SerializeField] string iOSAdUnitID;

    public bool IsReady;

    protected string adUnitID;

    void Awake()
    {
#if UNITY_IOS
        adUnitID = iOSAdUnitID;
#elif UNITY_ANDROID
        adUnitID = androidAdUnitID;
#elif UNITY_EDITOR
        adUnitID = androidAdUnitID;
#endif
    }

    public virtual void Load()
    {
        Debug.Log("Loading Ad: " + adUnitID);
    }

    public virtual void Show()
    {
        Debug.Log("Showing Ad: " + adUnitID);
    }

    public virtual void OnLoaded()
    {
        IsReady = true;
        Debug.Log("Ad " + adUnitID + " loaded");
    }

    protected virtual void OnAdStarted()
    {
        //Stop game when ad is showing
        //Handle it in an other way if you need Time.timeScale
        Time.timeScale = 0;
    }

    protected virtual void OnAdEnded(bool success = true)
    {
        //Recover game speed
        //Same as OnAdStarted for handle this way for the tutorial
        Time.timeScale = 1;
    }
}