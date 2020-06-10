using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{

	public static GameControl instance;
	public GameObject gameOverText;
	public Text scoreText;
	public Text highScoreText;
	public bool gameOver = false;
	public float screenMin;
	public float screenMax;

	private int score = 0;
	private int highScore;

    // Start is called before the first frame update
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
    		highScore = PlayerPrefs.GetInt("HighScore");
    	}

    	Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        screenMin = -1f * width / 2;
        screenMax = 1f * width / 2;
    }

    // Update is called once per frame
    void Update()
    {

    }


    public void PlayerDied()
    {
    	if (OptionsMenu.soundIsOn)
    	{
    		SoundManagerScript.PlaySound("game_over");
    	}
    	
    	gameOverText.SetActive(true);
    	gameOver = true;
    }

    public void PlayerScored()
    {
    	if (gameOver)
    	{
    		return;
    	}
    	score++;
    	scoreText.text = "Score: " + score.ToString(); 

    	UpdateHighScore();
    	
    }

    public void UpdateHighScore()
    {
    	if (score > highScore)
    	{
    		highScore = score;
    		PlayerPrefs.SetInt("HighScore", highScore);
    	}
    }
}
