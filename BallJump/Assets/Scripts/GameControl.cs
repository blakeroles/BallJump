using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GoogleMobileAds.Api;

public class GameControl : MonoBehaviour
{

	public static GameControl instance;
	public GameObject mainCam;
	public GameObject secondChanceCanvas;
	public GameObject defaultPlayerPrefab;
	public GameObject player;
	public GameObject unlock1PlayerPrefab;
	public GameObject unlock2PlayerPrefab;
	public GameObject unlock3PlayerPrefab;
	public GameObject continueButton;
	public Text scoreText;
	public bool gameOver = false;
	public bool gameContinued = false;
	public int maxNumberOfHearts;
	public float screenMin;
	public float screenMax;
	public GameObject coinPrefab;
	public GameObject badPotionPrefab;
	public GameObject heartPrefab;
	public float minCoinSpawnRate;
	public float maxCoinSpawnRate;
	public float minBadPotionSpawnRate;
	public float maxBadPotionSpawnRate;
	public float minHeartSpawnRate;
	public float maxHeartSpawnRate;
	public int coinScoreIncrease;
	public int badPotionScoreDecrease;
	public GameObject cubeEnemyPrefab;
	public float minCubeEnemySpawnRate;
	public float maxCubeEnemySpawnRate;
	public GameObject circleEnemyPrefab;
	public float minCircleEnemySpawnRate;
	public float maxCircleEnemySpawnRate;
	public int scoreToStartSpawningCircleEnemies;
	public List<GameObject> hearts;

	public int score = 0;
	public float heartXOffset;
	public float heartYOffset;
	private int highScore;
	private float timeSinceLastCoinSpawned;
	private float timeSinceLastCubeEnemySpawned;
	private float timeSinceLastBadPotionSpawned;
	private float timeSinceLastCircleEnemySpawned;
	private float timeSinceLastHeartSpawned;
	private GameObject coin;
	private GameObject badPotion;
	private GameObject heart;
	private float height;
	private float width;
	private float coinSpawnRate;
	private GameObject cubeEnemy;
	private float cubeEnemySpawnRate;
	private float badPotionSpawnRate;
	private GameObject circleEnemy;
	private float circleEnemySpawnRate;
	private float heartSpawnRate;
	
	private float heightMin;
	private float heightMax;
	private Camera cam;


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

		// Set the correct player sprite based on current player
        if (PlayerPrefs.HasKey("CurrentPlayer"))
        {
			if (PlayerPrefs.GetInt("CurrentPlayer") == MainMenu.instance.DEFAULT_PLAYER_NO)
			{
				player = (GameObject) Instantiate(defaultPlayerPrefab, new Vector2(0.0f, 0.0f), Quaternion.identity);
			}
			else if (PlayerPrefs.GetInt("CurrentPlayer") == MainMenu.instance.UNLOCK_1_PLAYER_NO)
			{
				player = (GameObject) Instantiate(unlock1PlayerPrefab, new Vector2(0.0f, 0.0f), Quaternion.identity);
			}
			else if (PlayerPrefs.GetInt("CurrentPlayer") == MainMenu.instance.UNLOCK_2_PLAYER_NO)
			{
				player = (GameObject) Instantiate(unlock2PlayerPrefab, new Vector2(0.0f, 0.0f), Quaternion.identity);
			}
			else if (PlayerPrefs.GetInt("CurrentPlayer") == MainMenu.instance.UNLOCK_3_PLAYER_NO)
			{
				player = (GameObject) Instantiate(unlock3PlayerPrefab, new Vector2(0.0f, 0.0f), Quaternion.identity);
			}
			
			
        }
		else
		{
			player = (GameObject) Instantiate(defaultPlayerPrefab, new Vector2(0.0f, 0.0f), Quaternion.identity);
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
			if (score >= highScore)
			{
				scoreText.color = Color.yellow;
			}
		}

    	cam = Camera.main;
        height = 2f * cam.orthographicSize;
        width = height * cam.aspect;

        screenMin = -1f * width / 2;
        screenMax = 1f * width / 2;

		heightMin = -1f * height / 2;
		heightMax = 1f * height / 2;

		coinSpawnRate = Random.Range(minCoinSpawnRate, maxCoinSpawnRate);
		cubeEnemySpawnRate = Random.Range(minCubeEnemySpawnRate, maxCubeEnemySpawnRate);
		badPotionSpawnRate = Random.Range(minBadPotionSpawnRate, maxBadPotionSpawnRate);
		circleEnemySpawnRate = Random.Range(minCircleEnemySpawnRate, maxCircleEnemySpawnRate);
		heartSpawnRate = Random.Range(minHeartSpawnRate, maxHeartSpawnRate);

		List<string> deviceIds = new List<string>();
		deviceIds.Add("ee4ec563d1de0b1daa96d57376b9bbc6");
		deviceIds.Add("23baaa41c248b5765cc77f732a706877");
		RequestConfiguration requestConfiguration = new RequestConfiguration.Builder().SetTestDeviceIds(deviceIds).build();
		MobileAds.SetRequestConfiguration(requestConfiguration);

		Time.timeScale = 1f;

		hearts = new List<GameObject>();

    }

    // Update is called once per frame
    void Update()
    {
		timeSinceLastCoinSpawned += Time.deltaTime;
		timeSinceLastCubeEnemySpawned += Time.deltaTime;
		timeSinceLastBadPotionSpawned += Time.deltaTime;
		timeSinceLastCircleEnemySpawned += Time.deltaTime;
		timeSinceLastHeartSpawned += Time.deltaTime;

		if (!gameOver && timeSinceLastCoinSpawned >= coinSpawnRate)
        {
			timeSinceLastCoinSpawned = 0;
			coinSpawnRate = Random.Range(minCoinSpawnRate, maxCoinSpawnRate);
			Destroy(coin);
			SpawnCoin();
        }

		if (!gameOver && timeSinceLastCubeEnemySpawned >= cubeEnemySpawnRate)
		{
			timeSinceLastCubeEnemySpawned = 0;
			cubeEnemySpawnRate = Random.Range(minCubeEnemySpawnRate, maxCubeEnemySpawnRate);
			Destroy(cubeEnemy);
			SpawnCubeEnemy();
		}

		if (!gameOver && timeSinceLastBadPotionSpawned >= badPotionSpawnRate)
		{
			timeSinceLastBadPotionSpawned = 0;
			badPotionSpawnRate = Random.Range(minBadPotionSpawnRate, maxBadPotionSpawnRate);
			Destroy(badPotion);
			SpawnBadPotion();
		}

		if (!gameOver && timeSinceLastHeartSpawned >= heartSpawnRate)
		{
			timeSinceLastHeartSpawned = 0;
			heartSpawnRate = Random.Range(minHeartSpawnRate, maxHeartSpawnRate);
			Destroy(heart);
			SpawnHeart();
		}

		if (!gameOver && score > scoreToStartSpawningCircleEnemies && timeSinceLastCircleEnemySpawned >= circleEnemySpawnRate)
		{
			timeSinceLastCircleEnemySpawned = 0;
			circleEnemySpawnRate = Random.Range(minCircleEnemySpawnRate, maxCircleEnemySpawnRate);
			Destroy(circleEnemy);
			SpawnCircleEnemy();
		}


		UpdateHeartPositions();

    }

	public void UpdateHeartPositions()
	{
		foreach (GameObject heart in hearts)
		{
			heart.transform.position = new Vector3(heart.transform.position.x, cam.transform.position.y - heightMax + heartYOffset, heart.transform.position.z);
		}
	}

	public void SpawnCoin()
	{
		coin = (GameObject) Instantiate(coinPrefab, new Vector2(Random.Range(screenMin, screenMax), Random.Range(player.transform.position.y + height, player.transform.position.y + 2f * height)), Quaternion.identity);
	}

	public void SpawnHeart()
	{
		heart = (GameObject) Instantiate(heartPrefab, new Vector2(Random.Range(screenMin, screenMax), Random.Range(player.transform.position.y + height, player.transform.position.y + 2f * height)), Quaternion.identity);
	}

	public void SpawnCubeEnemy()
	{
		cubeEnemy = (GameObject) Instantiate(cubeEnemyPrefab, new Vector2(Random.Range(screenMin, screenMax), Random.Range(player.transform.position.y + height, player.transform.position.y + 2f * height)), Quaternion.identity);
	}

	public void SpawnCircleEnemy()
	{
		circleEnemy = (GameObject) Instantiate(circleEnemyPrefab, new Vector2(Random.Range(screenMin, screenMax), Random.Range(player.transform.position.y + height, player.transform.position.y + 2f * height)), Quaternion.identity);
	}

	public void SpawnBadPotion()
	{
		badPotion = (GameObject) Instantiate(badPotionPrefab, new Vector2(Random.Range(screenMin, screenMax), Random.Range(player.transform.position.y + height, player.transform.position.y + 2f * height)), Quaternion.identity);
	}

	public void PlayerHitCoin()
	{
		if (gameOver)
		{
			return;
		}

		SoundManagerScript.PlaySound("coin_chime");

		score += coinScoreIncrease;
		scoreText.text = "SCORE: " + score.ToString();

		UpdateHighScore();
	}

	public void PlayerHitBadPotion()
	{
		if (gameOver)
		{
			return;
		}

		SoundManagerScript.PlaySound("poison");

		score -= badPotionScoreDecrease;
		scoreText.text = "SCORE: " + score.ToString();
	}

	public void PlayerHitHeart()
	{
		if (gameOver)
		{
			return;
		}

		SoundManagerScript.PlaySound("heart_pickup");

		if (hearts.Count < 3)
		{
			GameObject newHeart = (GameObject) Instantiate(heartPrefab, new Vector2((screenMin + (0.8f*(hearts.Count) + heartXOffset)), cam.transform.position.y - heightMax + heartYOffset), Quaternion.identity);
			newHeart.tag = "guiHeart";
			hearts.Add(newHeart);
		}

	}

	public void DeductHeart()
	{
		SoundManagerScript.PlaySound("player_hit");
		Destroy(hearts[hearts.Count - 1]);
		hearts.RemoveAt(hearts.Count - 1);
	}

	public void PlayerSecondChance()
	{
    	
		
		
		SoundManagerScript.PlaySound("game_over");

		secondChanceCanvas.SetActive(true);

		if (PlayerPrefs.GetInt("GameContinued") == 1)
		{
			continueButton.SetActive(false);
		}
		Time.timeScale = 0f;
		gameOver = true;
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
			scoreText.color = Color.yellow;
			//scoreText.color = new Color(r,g,b,a);
    	}
    }
}
