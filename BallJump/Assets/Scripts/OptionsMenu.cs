using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OptionsMenu : MonoBehaviour
{
	public static bool soundIsOn = true;
	public GameObject optionsMenuUI;
	public GameObject mainMenuUI;
	public Text toggleSoundButtonText;

	public void OpenOptionsMenu()
	{
		optionsMenuUI.SetActive(true);
		mainMenuUI.SetActive(false);
		if (soundIsOn)
		{
			SoundManagerScript.PlaySound("tap");
		}
	}

	public void CloseOptionsMenu()
	{
		optionsMenuUI.SetActive(false);
		mainMenuUI.SetActive(true);
		if (soundIsOn)
		{
			SoundManagerScript.PlaySound("tap");
		}
	}

	public void toggleSound()
	{
		
		if (soundIsOn)
		{
			soundIsOn = false;
			toggleSoundButtonText.text = "Sound On";
			SoundManagerScript.PlaySound("tap");
		}
		else
		{
			soundIsOn = true;
			toggleSoundButtonText.text = "Sound Off";
		}
	}
}
