﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

	public float moveSpeed;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
    	if (!GameControl.instance.gameOver)
    	{
    		float h = Input.GetAxis("Horizontal") * moveSpeed;

        	GetComponent<Rigidbody2D>().AddForce(Vector2.right * h);

    	}

    }

    void OnCollisionEnter2D()
    {
    	GameControl.instance.BallScored();
    }
}
