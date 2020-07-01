using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public class PauseMenu : MonoBehaviour
{
	public static bool gameIsPaused = false;
	public GameObject pauseMenuUI;
	public GameObject pauseButton;
	private InterstitialAd interstitial;

	void Start()
	{
		#if UNITY_ANDROID
			#if UNITY_EDITOR
				// Test ad
				string adUnitId = "ca-app-pub-3940256099942544/1033173712";
				Debug.Log("Loading Test Ad");
			#else
        		string adUnitId = "ca-app-pub-3117719815913092/3090005767";
			#endif
    	#elif UNITY_IPHONE
			#if UNITY_EDITOR
				// Test ad
        		string adUnitId = "ca-app-pub-3940256099942544/4411468910";
				Debug.Log("Loading Test Ad");
			#else
				string adUnitId = "ca-app-pub-3117719815913092/6562825846";
			#endif
    	#else
        	string adUnitId = "unexpected_platform";
    	#endif

    	// Initialize an InterstitialAd.
    	this.interstitial = new InterstitialAd(adUnitId);

		AdRequest request = new AdRequest.Builder().Build();
    	
		// Load the interstitial with the request.
    	this.interstitial.LoadAd(request);
	}

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame()
    {
		if (!GameControl.instance.gameOver)
		{
			SoundManagerScript.PlaySound("tap");

			
			pauseMenuUI.SetActive(true);
			pauseButton.SetActive(false);
			Time.timeScale = 0f;
			gameIsPaused = true;
		}


    }

    public void ResumeGame()
    {
    	SoundManagerScript.PlaySound("tap");

    	pauseMenuUI.SetActive(false);
    	pauseButton.SetActive(true);
    	Time.timeScale = 1f;
    	gameIsPaused = false;
    }

    public void LoadMainMenu()
    {
    	SoundManagerScript.PlaySound("tap");

		GameControl.instance.PlayerDied();
    	SceneManager.LoadScene("TitleScene");
    }

    public void QuitGame()
    {
    	SoundManagerScript.PlaySound("tap");
    	Application.Quit();
    }

	public void ContinueGame()
	{
    	SoundManagerScript.PlaySound("tap");

		if (PlayerPrefs.GetInt("GameContinued") == 0)
		{
			PlayerPrefs.SetInt("GameContinueScore", GameControl.instance.score);
			PlayerPrefs.SetInt("GameContinued", 1);

			// Play in interstitial ad first
			if (this.interstitial.IsLoaded()) {
				Debug.Log("Loading Interstitial Ad...");
				this.interstitial.Show();
			}


		}
		else
		{
			PlayerPrefs.SetInt("GameContinueScore", 0);
			PlayerPrefs.SetInt("GameContinued", 0);
		}



		SceneManager.LoadScene("MainScene");
	}

}
