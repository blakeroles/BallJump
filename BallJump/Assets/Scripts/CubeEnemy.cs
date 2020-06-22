using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeEnemy : MonoBehaviour
{

    private Vector3 movement;
    private int direction = 1;
    public float speed;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
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
