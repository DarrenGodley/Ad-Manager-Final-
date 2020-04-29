using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Advertisements;

public class RewardedVideo : MonoBehaviour, IUnityAdsListener
{
    public Text rewardText;
    private int rewardCount = 0;

    private string gameId = "3578625";
    bool testMode = true;

    string placementId = "bannerPlacement";
    string myPlacementId = "rewardedVideo";

    // Start is called before the first frame update
    void Start()
    {
        // Initialize the Ads listener and service:
        Advertisement.AddListener(this);
        Advertisement.Initialize(gameId, testMode);
        StartCoroutine(ShowBannerWhenReady());

    }

    // Implement a function for showing a rewarded video ad:
    public void ShowRewardedVideo()
    {
        Advertisement.Show(myPlacementId);
    }

    // Implement IUnityAdsListener interface methods:
    public void OnUnityAdsReady(string placementId)
    {
        // If the ready Placement is rewarded, activate the button: 
        if (placementId == myPlacementId)
        {
            //     myButton.interactable = true;
        }
    }

    public void OnUnityAdsDidFinish(string placementId, ShowResult showResult)
    {
        // Define conditional logic for each ad completion status:
        if (showResult == ShowResult.Finished)
        {
            // Reward the user for watching the ad to completion.
            rewardCount++;
            rewardText.text = rewardCount.ToString();
        }
        else if (showResult == ShowResult.Skipped)
        {
            // Do not reward the user for skipping the ad.
        }
        else if (showResult == ShowResult.Failed)
        {
            Debug.LogWarning("The ad did not finish due to an error.");
        }
    }

    IEnumerator ShowBannerWhenReady()
    {
        while (!Advertisement.IsReady(placementId))
        {
            yield return new WaitForSeconds(0.5f);
        }
        Advertisement.Banner.Show(placementId);
    }
    public void ShowBannerAd()
    {
        Advertisement.Banner.Show(placementId);
    }

    public void ShowInterstitialAd()
    {
        // Initialize the Ads service:
        Advertisement.Initialize(gameId, testMode);
        // Show an ad:
        Advertisement.Show();
    }

    public void OnUnityAdsDidError(string message)
    {
        // Log the error.
    }

    public void OnUnityAdsDidStart(string placementId)
    {
        // Optional actions to take when the end-users triggers an ad.
    }
}