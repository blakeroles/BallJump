using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

	public Transform target;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    	if (target.position.y > 0)
    	{

        	Vector3 targetPos = new Vector3(transform.position.x, target.position.y, transform.position.z);

        	transform.position = targetPos;
    	}


    }
}
