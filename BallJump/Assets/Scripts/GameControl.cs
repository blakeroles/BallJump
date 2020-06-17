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
	public GameObject coinPrefab;
	public float minCoinSpawnRate;
	public float maxCoinSpawnRate;
	public int coinScoreIncrease;

	public int score = 0;
	public GameObject player;
	private int highScore;
	private float timeSinceLastCoinSpawned;
	private GameObject coin;
	private float height;
	private float width;
	private float coinSpawnRate;

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
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;

        screenMin = -1f * width / 2;
        screenMax = 1f * width / 2;

		coinSpawnRate = Random.Range(minCoinSpawnRate, maxCoinSpawnRate);
    }

    // Update is called once per frame
    void Update()
    {
		timeSinceLastCoinSpawned += Time.deltaTime;
		if (GameControl.instance.gameOver == false && timeSinceLastCoinSpawned >= coinSpawnRate)
        {
			timeSinceLastCoinSpawned = 0;
			coinSpawnRate = Random.Range(minCoinSpawnRate, maxCoinSpawnRate);
			Destroy(coin);
			SpawnCoin();
        }
    }

	public void SpawnCoin()
	{
		coin = (GameObject) Instantiate(coinPrefab, new Vector2(Random.Range(screenMin, screenMax), Random.Range(player.transform.position.y + height, player.transform.position.y + 2f * height)), Quaternion.identity);
	}

	public void PlayerHitCoin()
	{
		if (gameOver)
		{
			return;
		}

		if (OptionsMenu.soundIsOn)
		{
			SoundManagerScript.PlaySound("coin_chime");
		}

		score += coinScoreIncrease;
		scoreText.text = "Score: " + score.ToString();

		UpdateHighScore();
	}

    public void PlayerDied()
    {
    	if (OptionsMenu.soundIsOn)
    	{
    		SoundManagerScript.PlaySound("game_over");
    	}
    	
    	gameOverText.SetActive(true);
		Time.timeScale = 0f;
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
