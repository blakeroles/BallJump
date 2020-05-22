using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameOverPlatformScript : MonoBehaviour
{

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.GetComponent<Ball>() != null)
		{
			GameControl.instance.BallDied();
		}
	}

}
