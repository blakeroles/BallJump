using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

	public Text highScoreText;
	public Text DeviceIDText;
	public GameObject playButton;
	public GameObject optionsButton;
	public GameObject unlocksButton;
	public GameObject gameTitleText;
	public GameObject highScoreTextGO;
	public GameObject unlocksPageCanvas;
	public GameObject unlockDefaultPlayerSprite;
	public GameObject unlock1PlayerSprite;
	public GameObject unlock2PlayerSprite;
	public GameObject unlock3PlayerSprite;
	

	void Start()
	{

		if (PlayerPrefs.HasKey("HighScore"))
		{
			highScoreText.text = "HIGH SCORE: " + PlayerPrefs.GetInt("HighScore").ToString();
		}

		DeviceIDText.text = SystemInfo.deviceUniqueIdentifier;

		Application.targetFrameRate = 60;



	}

	public void PlayGame()
	{
		SoundManagerScript.PlaySound("tap");

		PlayerPrefs.SetInt("GameContinued", 0);
		SceneManager.LoadScene("MainScene");
	}

	public void OpenUnlocksPage()
	{
		SoundManagerScript.PlaySound("tap");
		
		// Remove UI elements
		gameTitleText.SetActive(false);
		playButton.SetActive(false);
		optionsButton.SetActive(false);
		highScoreTextGO.SetActive(false);
		unlocksButton.SetActive(false);

		// Show unlocks page UI elements
		unlocksPageCanvas.SetActive(true);
		unlockDefaultPlayerSprite.SetActive(true);
		unlock1PlayerSprite.SetActive(true);
		unlock2PlayerSprite.SetActive(true);
		unlock3PlayerSprite.SetActive(true);

	}

	public void CloseUnlocksPage()
	{
		SoundManagerScript.PlaySound("tap");

		// Revert UI elements
		gameTitleText.SetActive(true);
		playButton.SetActive(true);
		//optionsButton.SetActive(true);
		highScoreTextGO.SetActive(true);
		unlocksButton.SetActive(true);

		// Show unlocks page UI elements
		unlocksPageCanvas.SetActive(false);
		unlockDefaultPlayerSprite.SetActive(false);
		unlock1PlayerSprite.SetActive(false);
		unlock2PlayerSprite.SetActive(false);
		unlock3PlayerSprite.SetActive(false);
	}





}
