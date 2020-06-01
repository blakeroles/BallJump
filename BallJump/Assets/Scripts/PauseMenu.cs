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
    	SoundManagerScript.PlaySound("tap");
    	pauseMenuUI.SetActive(true);
    	pauseButton.SetActive(false);
    	Time.timeScale = 0f;
    	gameIsPaused = true;

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
    	Time.timeScale = 1f;
    	SceneManager.LoadScene("TitleScene");
    }

    public void QuitGame()
    {
    	SoundManagerScript.PlaySound("tap");
    	Application.Quit();
    }

}
