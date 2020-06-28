using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
// using GoogleMobileAds.Api;

public class MainMenu : MonoBehaviour
{

	public Text highScoreText;
	public Text DeviceIDText;

	// private InterstitialAd interstitial;

	void Start()
	{

		if (PlayerPrefs.HasKey("HighScore"))
		{
			highScoreText.text = "HIGH SCORE: " + PlayerPrefs.GetInt("HighScore").ToString();
		}

		DeviceIDText.text = SystemInfo.deviceUniqueIdentifier;

		// #if UNITY_ANDROID
        // 	string adUnitId = "ca-app-pub-3117719815913092/3090005767";
		// 	// Test ad
		// 	//string adUnitId = "ca-app-pub-3940256099942544/1033173712";
    	// #elif UNITY_IPHONE
		// 	string adUnitId = "ca-app-pub-3117719815913092/6562825846";
		// 	// Test ad
        // 	//string adUnitId = "ca-app-pub-3940256099942544/4411468910";
    	// #else
        // 	string adUnitId = "unexpected_platform";
    	// #endif

    	// // Initialize an InterstitialAd.
    	// this.interstitial = new InterstitialAd(adUnitId);

		// AdRequest request = new AdRequest.Builder().Build();
    	
		// // Load the interstitial with the request.
    	// this.interstitial.LoadAd(request);

	}

	public void PlayGame()
	{
		if (OptionsMenu.soundIsOn)
		{
			SoundManagerScript.PlaySound("tap");
		}
		
		SceneManager.LoadScene("MainScene");
	}

	// public void showInterstitialAd()
	// {
	// 	if (OptionsMenu.soundIsOn)
	// 	{
	// 		SoundManagerScript.PlaySound("tap");
	// 	}
	// 	if (this.interstitial.IsLoaded()) {
	// 		this.interstitial.Show();
	// 	}
	// }



}
