using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

	public Text highScoreText;
	public Text DeviceIDText;

	

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





}
