using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
	public float speed = 2.0f;
	public float chanceOfMoving = 0.7f;

	private bool isMoving;
	private int direction = 1;
	private Vector3 movement;
	private float RANDOM_MIN_RANGE = 0.0f;
	private float RANDOM_MAX_RANGE = 1.0f;

    // Start is called before the first frame update
    void Start()
    {

    	if (Random.Range(RANDOM_MIN_RANGE, RANDOM_MAX_RANGE) < chanceOfMoving)
    	{
    		isMoving = false;
    	}
    	else
    	{
    		isMoving = true;
    	}
    }

    // Update is called once per frame
    void Update()
    {
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
}
