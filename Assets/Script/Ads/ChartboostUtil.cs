using UnityEngine;
using System.Collections;
using ChartboostSDK;
using System;

public class ChartboostUtil : MonoBehaviour
{

    static ChartboostUtil _instance = null;



    public static ChartboostUtil Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = FindObjectOfType<ChartboostUtil>();
                if (_instance == null)
                {
                    GameObject signton = new GameObject();
                    signton.name = "Chartboost Container";
                    _instance = signton.AddComponent<ChartboostUtil>();
                }
            }
            return _instance;
        }
        private set
        {
            _instance = value;
        }
    }

    void Awake()
    {
        if (_instance == null)
        {
            _instance = this;
            DontDestroyOnLoad(gameObject);
            LeanTween.addListener((int)Events.GAMEFINISH, OnGameFinish);
            LeanTween.addListener((int)Events.GAMEPAUSE, OnGamePause);
            LeanTween.addListener((int)Events.MENULOADED, OnGameMenu);
            LeanTween.addListener((int)Events.GAMEMORE, OnGameMore);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void OnGameMore(LTEvent obj)
    {
        //  throw new NotImplementedException();
        //this.ShowMoreAppOnDefault();
        if (FUGSDK.Ads.Instance.HasMoreApp())
            FUGSDK.Ads.Instance.ShowMoreApp();
        else
        {
            FUGSDK.Ads.Instance.ShowInterstitial();
        }
    }

    private void OnGameMenu(LTEvent obj)
    {
        //throw new NotImplementedException();
       // this.ShowInterstitialOnHomescreen();
    }

    private void OnGamePause(LTEvent obj)
    {
        //throw new NotImplementedException();
        //this.ShowInterstitialOnDefault();
        FUGSDK.Ads.Instance.ShowInterstitial();
    }

    private void OnGameFinish(LTEvent obj)
    {
        //throw new NotImplementedException();
        FUGSDK.Ads.Instance.ShowInterstitial();
    }

}
