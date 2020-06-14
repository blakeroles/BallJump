using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
	public float INITIAL_SPEED = 2.0f;
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
	private float speed;
	private int SCORE_THIRTY = 30;
	private int SCORE_FIFTY = 50;
	private int SCORE_SEVENTYFIVE = 75;
	private int SCORE_ONEHUNDRED = 100;
	private float ZERO_MULT = 0.0f;
	private float ONE_MULT = 1.0f;
	private float ONE_POINT_FIVE_MULT = 1.5f;
	private float TWO_MULT = 2.0f;
	private float TWO_POINT_FIVE_MULT = 2.5f;
	

	

    // Start is called before the first frame update
    void Start()
    {


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

		if (GameControl.instance.score < SCORE_THIRTY)
		{
			speed = ZERO_MULT * INITIAL_SPEED;
		}
		else if (GameControl.instance.score < SCORE_FIFTY)
		{
			speed = ONE_MULT * INITIAL_SPEED;
		}
		else if (GameControl.instance.score < SCORE_SEVENTYFIVE)
		{
			speed = ONE_POINT_FIVE_MULT * INITIAL_SPEED;
		}
		else if (GameControl.instance.score < SCORE_ONEHUNDRED)
		{
			speed = TWO_MULT * INITIAL_SPEED;
		}
		else if (GameControl.instance.score >= SCORE_ONEHUNDRED)
		{
			speed = TWO_POINT_FIVE_MULT * INITIAL_SPEED;
		}
		

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
