using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
	public static MainMenu instance;
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
	public Text unlock1Text;
	public Text unlock2Text;
	public Text unlock3Text;
	public GameObject rectangleUnlockPrefab;
	public int DEFAULT_PLAYER_NO = 0;
	public int UNLOCK_1_PLAYER_NO = 1;
	public int UNLOCK_2_PLAYER_NO = 2;
	public int UNLOCK_3_PLAYER_NO = 3;
	private int UNLOCK_1_SCORE = 100;
	private int UNLOCK_2_SCORE = 250;
	private int UNLOCK_3_SCORE = 500;
	private GameObject unlockRectangle;
	private float DEFAULT_RECT_X_POS = -1.23f;
	private float DEFAULT_RECT_Y_POS = 1.88f;
	private float UNLOCK_1_RECT_X_POS = 1.15f;
	private float UNLOCK_1_RECT_Y_POS = 1.88f;
	private float UNLOCK_2_RECT_X_POS = -1.2f;
	private float UNLOCK_2_RECT_Y_POS = -0.87f;
	private float UNLOCK_3_RECT_X_POS = 1.11f;
	private float UNLOCK_3_RECT_Y_POS = -0.87f;
	
    void Awake()
    {
        if (instance == null)
        {
        	instance = this;
        }
        else if (instance != this)
        {
        	Destroy(gameObject);
        }

		
    }
	void Start()
	{

		if (PlayerPrefs.HasKey("HighScore"))
		{
			highScoreText.text = "HIGH SCORE: " + PlayerPrefs.GetInt("HighScore").ToString();
		}

		DeviceIDText.text = SystemInfo.deviceUniqueIdentifier;

		Application.targetFrameRate = 60;



	}

	void Update()
	{
		CheckForChangePlayer();
	}

	public void CheckForChangePlayer()
	{
		if (Input.GetMouseButtonDown(0))
		{
			Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
			Vector2 mousePos2D = new Vector2(mousePos.x, mousePos.y);

			RaycastHit2D hit = Physics2D.Raycast(mousePos2D, Vector2.zero);

			if (hit.collider != null)
			{
				switch (hit.collider.gameObject.name)
				{
					case "DefaultPlayer":
						unlockRectangle.transform.position = new Vector2(DEFAULT_RECT_X_POS, DEFAULT_RECT_Y_POS);
						PlayerPrefs.SetInt("CurrentPlayer", DEFAULT_PLAYER_NO);
						break;
					case "Unlock1Player":
						if (PlayerPrefs.HasKey("HighScore"))
						{
							if (PlayerPrefs.GetInt("HighScore") > UNLOCK_1_SCORE)
							{
								unlockRectangle.transform.position = new Vector2(UNLOCK_1_RECT_X_POS, UNLOCK_1_RECT_Y_POS);
								PlayerPrefs.SetInt("CurrentPlayer", UNLOCK_1_PLAYER_NO);
							}
						}
						break;
					case "Unlock2Player":
						if (PlayerPrefs.HasKey("HighScore"))
						{
							if (PlayerPrefs.GetInt("HighScore") > UNLOCK_2_SCORE)
							{
								unlockRectangle.transform.position = new Vector2(UNLOCK_2_RECT_X_POS, UNLOCK_2_RECT_Y_POS);
								PlayerPrefs.SetInt("CurrentPlayer", UNLOCK_2_PLAYER_NO);
							}
						}
						break;
					case "Unlock3Player":
						if (PlayerPrefs.HasKey("HighScore"))
						{
							if (PlayerPrefs.GetInt("HighScore") > UNLOCK_3_SCORE)
							{
								unlockRectangle.transform.position = new Vector2(UNLOCK_3_RECT_X_POS, UNLOCK_3_RECT_Y_POS);
								PlayerPrefs.SetInt("CurrentPlayer", UNLOCK_3_PLAYER_NO);
							}
						}
						break;
				}
			}
		}
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

		// Check if any players are unlocked against high score
		if (PlayerPrefs.HasKey("HighScore"))
		{
			if (PlayerPrefs.GetInt("HighScore") > UNLOCK_1_SCORE)
			{
				unlock1PlayerSprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("unlock1sprite");
				unlock1Text.text = "UNLOCKED";
			}

			if (PlayerPrefs.GetInt("HighScore") > UNLOCK_2_SCORE)
			{
				unlock2PlayerSprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("unlock2sprite");
				unlock2Text.text = "UNLOCKED";
			}

			if (PlayerPrefs.GetInt("HighScore") > UNLOCK_3_SCORE)
			{
				unlock3PlayerSprite.GetComponent<SpriteRenderer>().sprite = Resources.Load<Sprite>("unlock3sprite");
				unlock3Text.text = "UNLOCKED";
			}
		}

		unlockRectangle = (GameObject) Instantiate(rectangleUnlockPrefab, new Vector2(-50f, -50f), Quaternion.identity);

		if (PlayerPrefs.HasKey("CurrentPlayer"))
		{
			// Set the box around the currently selected player
			if (PlayerPrefs.GetInt("CurrentPlayer") == DEFAULT_PLAYER_NO)
			{
				unlockRectangle.transform.position = new Vector2(DEFAULT_RECT_X_POS, DEFAULT_RECT_Y_POS);
			}
			else if (PlayerPrefs.GetInt("CurrentPlayer") == UNLOCK_1_PLAYER_NO)
			{
				unlockRectangle.transform.position = new Vector2(UNLOCK_1_RECT_X_POS, UNLOCK_1_RECT_Y_POS);
			}
			else if (PlayerPrefs.GetInt("CurrentPlayer") == UNLOCK_2_PLAYER_NO)
			{
				unlockRectangle.transform.position = new Vector2(UNLOCK_2_RECT_X_POS, UNLOCK_2_RECT_Y_POS);
			}
			else if (PlayerPrefs.GetInt("CurrentPlayer") == UNLOCK_3_PLAYER_NO)
			{
				unlockRectangle.transform.position = new Vector2(UNLOCK_3_RECT_X_POS, UNLOCK_3_RECT_Y_POS);
			}
		} 
		else
		{
			// Set the box around the default player
			unlockRectangle.transform.position = new Vector2(DEFAULT_RECT_X_POS, DEFAULT_RECT_Y_POS);
		}

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

		// Destroy the rectangle
		Destroy(unlockRectangle);
	}





}
