using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
	public float speed = 2.0f;
	public float chanceOfMoving = 0.7f;
	public float chanceOfSpikes = 1.0f;
	public int scoreToStartMovingPlatforms = 30;

	private bool isMoving = false;
	private bool changedMoveState = false;
	private bool isSpiky;
	private int direction = 1;
	private Vector3 movement;
	private float RANDOM_MIN_RANGE = 0.0f;
	private float RANDOM_MAX_RANGE = 1.0f;
	//private Dictionary<int, float> speedDict;
	//private bool changedSpeed = false;

    // Start is called before the first frame update
    void Start()
    {

		//speedDict = new Dictionary<int, float>();
		//speedDict.Add(0, 0.0f);
		//speedDict.Add(30, 1.0f);
		//speedDict.Add(50, 1.25f);
		//speedDict.Add(75, 1.5f);
		//speedDict.Add(100, 2.0f);

    	if (Random.Range(RANDOM_MIN_RANGE, RANDOM_MAX_RANGE) < chanceOfSpikes)
    	{
    		isSpiky = false;
    	}
    	else 
    	{
    		isSpiky = true;
    	}
    }

    // Update is called once per frame
    void Update()
    {

		

		if (GameControl.instance.score > scoreToStartMovingPlatforms && !changedMoveState)
		{
			if (Random.Range(RANDOM_MIN_RANGE, RANDOM_MAX_RANGE) < chanceOfMoving)
			{
				isMoving = false;
			}
			else
			{
				isMoving = true;
			}
			changedMoveState = true;
		}

        if (isMoving)
        {
        	if (transform.position.x > GameControl.instance.screenMax)
        	{
        		direction = -1;
        	}
        	else if (transform.position.x < GameControl.instance.screenMin)
        	{
        		direction = 1;
        	}
        	movement = Vector3.right * direction * speed * Time.deltaTime;
        	transform.Translate(movement);
        }
    }

    void OnCollisionEnter2D()
    {
    	if (isSpiky)
    	{
    		//GameControl.instance.PlayerDied();
    	}
    }
}
