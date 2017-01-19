using UnityEngine;
using System.Collections;

public class ShowAds : MonoBehaviour
{

    public float delayTime = 0.5f;
    // Use this for initialization
    void Start()
    {
        Invoke("ShowAd", delayTime);
    }

    public void ShowAd()
    {
        FUGSDK.Ads.Instance.ShowInterstitial();
    }

   
}
