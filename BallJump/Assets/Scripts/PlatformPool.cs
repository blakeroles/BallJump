using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPool : MonoBehaviour
{

	public int platformPoolSize = 15;
	public GameObject platformPrefab;
	public float spawnRate = 2f;
	public float platformMin = -1f;
	public float platformMax = 3.5f;
	public float gapBetweenPlatforms = 2f;


	private GameObject[] platforms;
	private Vector2 objectPoolPosition = new Vector2(-5f, -10f);
	private float timeSinceLastSpawned;
	private int currentPlatform = 0;
	private float[] initialPlatformXPositions = {0.0f, -1.36f, -2.66f, -1.25f, 0.41f, 2.13f, 1.3f, 0.2f};
	private float[] initialPlatformYPositions = {-4.64f, -3.49f, -2.37f, -0.85f, 0.47f, 1.6f, 3.0f, 4.5f};
	private float lastPlatformHeight;


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
    }
}
