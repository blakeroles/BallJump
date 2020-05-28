using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPool : MonoBehaviour
{

	public int platformPoolSize = 15;
	public GameObject platformPrefab;
	public float spawnRate = 2f;
	public float gapBetweenPlatforms = 2f;
	public GameObject player;


	private GameObject[] platforms;
	private Vector2 objectPoolPosition = new Vector2(-50f, -5f);
	private float timeSinceLastSpawned;
	private int currentPlatform = 0;
	private float[] initialPlatformXPositions = {0.0f, -1.36f, -2.66f, -1.25f, 0.41f, 2.13f, 1.3f, 0.2f};
	private float[] initialPlatformYPositions = {-4.64f, -3.49f, -2.37f, -0.85f, 0.47f, 1.6f, 3.0f, 4.5f};
	private float lastPlatformHeight;
    private float platformMin;
    private float platformMax;


    // Start is called before the first frame update
    void Start()
    {
        platforms = new GameObject[platformPoolSize];
        int j = 0;
        for (int i = 0; i < platformPoolSize; i++)
        {
        	
        	if (i >= platformPoolSize - initialPlatformXPositions.Length)
        	{
        		platforms[i] = (GameObject) Instantiate(platformPrefab, new Vector2(initialPlatformXPositions[j], initialPlatformYPositions[j]), Quaternion.identity);
        		j++;
        	}
        	else
        	{
        		platforms[i] = (GameObject) Instantiate(platformPrefab, objectPoolPosition, Quaternion.identity);
        	}

        	lastPlatformHeight = platforms[i].transform.position.y;
        }

        Camera cam = Camera.main;
        float height = 2f * cam.orthographicSize;
        float width = height * cam.aspect;

        platformMin = -1f * width/2;
        platformMax = 1f * width/2;
    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSpawned += Time.deltaTime;
        if (GameControl.instance.gameOver == false && timeSinceLastSpawned >= spawnRate)
        {
        	timeSinceLastSpawned = 0;
        	float spawnXPosition = Random.Range(platformMin, platformMax);
        	float spawnYPosition = lastPlatformHeight + gapBetweenPlatforms;
        	platforms[currentPlatform].transform.position = new Vector2(spawnXPosition, spawnYPosition);
        	lastPlatformHeight = spawnYPosition;
        	currentPlatform++;
        	if (currentPlatform >= platformPoolSize)
        	{
        		currentPlatform = 0;
        	}
        }

        // if the player is below the lowest platform, trigerr ball died event
        float minPlatformHeight = 100f;
        for (int i = 0; i < platformPoolSize; i++)
        {
        	if (platforms[i].transform.position.y < minPlatformHeight)
        	{
        		minPlatformHeight = platforms[i].transform.position.y;
        	}
        }

        if (GameControl.instance.gameOver == false && player.transform.position.y < minPlatformHeight)
        {
        	GameControl.instance.BallDied();
        }


    }
}
