﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{

	public Text highScoreText;

	void Start()
	{

		if (PlayerPrefs.HasKey("HighScore"))
		{
			highScoreText.text = "High Score: " + PlayerPrefs.GetInt("HighScore").ToString();
		}

	}

	public void PlayGame()
	{
		SoundManagerScript.PlaySound("tap");
		SceneManager.LoadScene("MainScene");
	}

}
