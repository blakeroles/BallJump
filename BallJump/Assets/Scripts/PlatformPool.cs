using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformPool : MonoBehaviour
{

	public int platformPoolSize;
	public GameObject platformPrefab;
	public float spawnRate;
	public float gapBetweenPlatforms;
	public GameObject player;
    public int numberOfPlatformsToSpawn;


	private GameObject[] platforms;
	private Vector2 objectPoolPosition = new Vector2(-50f, -4.64f);
	private float timeSinceLastSpawned;
	private int currentPlatform = 0;
	private float initialPlatformXPosition = 0.0f;
	private float initialPlatformYPosition = -4.64f;
	private float lastPlatformHeight;


    // Start is called before the first frame update
    void Start()
    {

        platforms = new GameObject[platformPoolSize];

        for (int i = 0; i < platformPoolSize; i++)
        {

            if (i == platformPoolSize - 1)
            {
                platforms[i] = (GameObject) Instantiate(platformPrefab, new Vector2(initialPlatformXPosition, initialPlatformYPosition), Quaternion.identity);
            } 
            else if (i >= platformPoolSize - numberOfPlatformsToSpawn)
            {
                platforms[i] = (GameObject) Instantiate(platformPrefab, new Vector2(Random.Range(GameControl.instance.screenMin, GameControl.instance.screenMax), lastPlatformHeight + gapBetweenPlatforms), Quaternion.identity);
            }
            else
            {
                platforms[i] = (GameObject) Instantiate(platformPrefab, objectPoolPosition, Quaternion.identity);
            }

        	lastPlatformHeight = platforms[i].transform.position.y;

        }


        lastPlatformHeight = platforms[platformPoolSize - 2].transform.position.y;




    }

    // Update is called once per frame
    void Update()
    {
        timeSinceLastSpawned += Time.deltaTime;
        if (GameControl.instance.gameOver == false && timeSinceLastSpawned >= spawnRate)
        {
        	timeSinceLastSpawned = 0;
        	float spawnXPosition = Random.Range(GameControl.instance.screenMin, GameControl.instance.screenMax);
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
