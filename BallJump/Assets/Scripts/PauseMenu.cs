using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
	public static bool gameIsPaused = false;
	public GameObject pauseMenuUI;
	public GameObject pauseButton;

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PauseGame()
    {
		if (!GameControl.instance.gameOver)
		{
			if (OptionsMenu.soundIsOn)
			{
				SoundManagerScript.PlaySound("tap");
			}
			
			pauseMenuUI.SetActive(true);
			pauseButton.SetActive(false);
			Time.timeScale = 0f;
			gameIsPaused = true;
		}


    }

    public void ResumeGame()
    {
    	if (OptionsMenu.soundIsOn)
    	{
    		SoundManagerScript.PlaySound("tap");
    	}
    	pauseMenuUI.SetActive(false);
    	pauseButton.SetActive(true);
    	Time.timeScale = 1f;
    	gameIsPaused = false;
    }

    public void LoadMainMenu()
    {
    	if (OptionsMenu.soundIsOn)
    	{
    		SoundManagerScript.PlaySound("tap");
    	}
		GameControl.instance.PlayerDied();
    	SceneManager.LoadScene("TitleScene");
    }

    public void QuitGame()
    {
    	if (OptionsMenu.soundIsOn)
    	{
    		SoundManagerScript.PlaySound("tap");
    	}
    	Application.Quit();
    }

	public void ContinueGame()
	{
		if (OptionsMenu.soundIsOn)
    	{
    		SoundManagerScript.PlaySound("tap");
    	}

		if (PlayerPrefs.GetInt("GameContinued") == 0)
		{
			PlayerPrefs.SetInt("GameContinueScore", GameControl.instance.score);
			PlayerPrefs.SetInt("GameContinued", 1);
		}
		else
		{
			PlayerPrefs.SetInt("GameContinueScore", 0);
			PlayerPrefs.SetInt("GameContinued", 0);
		}



		SceneManager.LoadScene("MainScene");
	}

}
