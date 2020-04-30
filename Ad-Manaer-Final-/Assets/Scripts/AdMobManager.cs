using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds;
using GoogleMobileAds.Api;
using System;
using System.ComponentModel.Design;
using UnityEngine.UI;

public class AdMobManager : MonoBehaviour
{

    private BannerView bannerView;
    private InterstitialAd interstitial;
    private RewardBasedVideoAd rewardedVideo;
    public Text rewardText;
    private int gameAmount = 0;
    private int rewardAmount = 0;

    // Start is called before the first frame update
    void Start()
    {
        string appID = "ca-app-pub-3940256099942544/5224354917";

        MobileAds.Initialize(appID);

        //RequestBanner();
        rewardedVideo = RewardBasedVideoAd.Instance;
        rewardedVideo.OnAdRewarded += HandleRewardBasedVideoRewarded;
        rewardedVideo.OnAdClosed += HandleRewardedVideoClosed;
        RequestRewardVideo();
    }

    void Update()
    {
        if (rewardAmount > 0)
        {
            gameAmount += rewardAmount;
            rewardText.text = gameAmount.ToString();
            rewardAmount = 0;
        }

    }

    private void RequestBanner()
    {
        string bannerUnitID = "ca-app-pub-3940256099942544/6300978111";

        bannerView = new BannerView(bannerUnitID, AdSize.Banner, AdPosition.Top);

        AdRequest request = new AdRequest.Builder().Build();

        bannerView.LoadAd(request);
    }

    public void ShowRewardVideo()
    {
        if (rewardedVideo.IsLoaded())
            rewardedVideo.Show();
    }



    private void RequestRewardVideo()
    {
        string rewardAdID = "ca-app-pub-3940256099942544/5224354917";

        rewardedVideo.LoadAd(CreateNewRequest(), rewardAdID);
    }

    private void HandleRewardBasedVideoRewarded(object sender, Reward args)
    {
        double amount = args.Amount;
        rewardAmount = (int)amount;

    }

    private void HandleRewardedVideoClosed(object sender, EventArgs args)
    {
        RequestRewardVideo();
    }

    //Interstitial
    public void ShowInterstitial()
    {
        RequestInterstitial();
    }

    private void RequestInterstitial()
    {
        string interstitialID = "ca-app-pub-3940256099942544/1033173712";

        if (interstitial != null)
            interstitial.Destroy();

        interstitial = new InterstitialAd(interstitialID);
        interstitial.OnAdLoaded += HandleOnAdLoaded;

        interstitial.LoadAd(CreateNewRequest());
    }

    public void HandleOnAdLoaded(object sender, EventArgs args)
    {
        if (interstitial.IsLoaded())
            interstitial.Show();
    }

    private AdRequest CreateNewRequest()
    {
        return new AdRequest.Builder().Build();
    }
}
