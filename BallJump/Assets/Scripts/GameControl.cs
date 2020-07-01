using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public class GameControl : MonoBehaviour
{

	public static GameControl instance;
	public GameObject secondChanceCanvas;
	public GameObject continueButton;
	public Text scoreText;
	public bool gameOver = false;
	public bool gameContinued = false;
	public float screenMin;
	public float screenMax;
	public GameObject coinPrefab;
	public float minCoinSpawnRate;
	public float maxCoinSpawnRate;
	public int coinScoreIncrease;
	public GameObject cubeEnemyPrefab;
	public float minCubeEnemySpawnRate;
	public float maxCubeEnemySpawnRate;

	public int score = 0;
	public GameObject player;
	private int highScore;
	private float timeSinceLastCoinSpawned;
	private float timeSinceLastCubeEnemySpawned;
	private GameObject coin;
	private float height;
	private float width;
	private float coinSpawnRate;
	private GameObject cubeEnemy;
	private float cubeEnemySpawnRate;


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

		if (PlayerPrefs.GetInt("GameContinued") == 1)
		{
			score = PlayerPrefs.GetInt("GameContinueScore");
			scoreText.text = "SCORE: " + score.ToString();
		}

    	Camera cam = Camera.main;
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;

        screenMin = -1f * width / 2;
        screenMax = 1f * width / 2;

		coinSpawnRate = Random.Range(minCoinSpawnRate, maxCoinSpawnRate);
		cubeEnemySpawnRate = Random.Range(minCubeEnemySpawnRate, maxCubeEnemySpawnRate);

		List<string> deviceIds = new List<string>();
		deviceIds.Add("ee4ec563d1de0b1daa96d57376b9bbc6");
		RequestConfiguration requestConfiguration = new RequestConfiguration.Builder().SetTestDeviceIds(deviceIds).build();
		MobileAds.SetRequestConfiguration(requestConfiguration);

		Time.timeScale = 1f;

    }

    // Update is called once per frame
    void Update()
    {
		timeSinceLastCoinSpawned += Time.deltaTime;
		timeSinceLastCubeEnemySpawned += Time.deltaTime;
		if (GameControl.instance.gameOver == false && timeSinceLastCoinSpawned >= coinSpawnRate)
        {
			timeSinceLastCoinSpawned = 0;
			coinSpawnRate = Random.Range(minCoinSpawnRate, maxCoinSpawnRate);
			Destroy(coin);
			SpawnCoin();
        }

		if (GameControl.instance.gameOver == false && timeSinceLastCubeEnemySpawned >= cubeEnemySpawnRate)
		{
			timeSinceLastCubeEnemySpawned = 0;
			cubeEnemySpawnRate = Random.Range(minCubeEnemySpawnRate, maxCubeEnemySpawnRate);
			Destroy(cubeEnemy);
			SpawnCubeEnemy();
		}

    }

	public void SpawnCoin()
	{
		coin = (GameObject) Instantiate(coinPrefab, new Vector2(Random.Range(screenMin, screenMax), Random.Range(player.transform.position.y + height, player.transform.position.y + 2f * height)), Quaternion.identity);
	}

	public void SpawnCubeEnemy()
	{
		cubeEnemy = (GameObject) Instantiate(cubeEnemyPrefab, new Vector2(Random.Range(screenMin, screenMax), Random.Range(player.transform.position.y + height, player.transform.position.y + 2f * height)), Quaternion.identity);
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
		scoreText.text = "SCORE: " + score.ToString();

		UpdateHighScore();
	}

	public void PlayerSecondChance()
	{
		if (OptionsMenu.soundIsOn)
    	{
    		SoundManagerScript.PlaySound("game_over");
    	}
		secondChanceCanvas.SetActive(true);

		if (PlayerPrefs.GetInt("GameContinued") == 1)
		{
			continueButton.SetActive(false);
		}
		Time.timeScale = 0f;
	}

    public void PlayerDied()
    {
    	gameOver = true;
    }

    public void PlayerScored()
    {
    	if (gameOver)
    	{
    		return;
    	}
    	score++;
    	scoreText.text = "SCORE: " + score.ToString(); 

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
