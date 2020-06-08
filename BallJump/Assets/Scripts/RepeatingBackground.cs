using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RepeatingBackground : MonoBehaviour
{

	public GameObject cam;
	public GameObject background;

	private float verticalLength;
	private float camHeight;
	private float camWidth;



    // Start is called before the first frame update
    void Start()
    {
        //verticalLength = transform.position.y;
        Camera camSize = Camera.main;
        camHeight = 2f * camSize.orthographicSize;
        //verticalLength = cam.transform.position.y + 0.5f * camHeight;
        verticalLength = background.transform.position.y + 0.5f * background.GetComponent<Renderer>().bounds.size.y;
    }

    // Update is called once per frame
    void Update()
    {


        if ((cam.transform.position.y - 0.5f * camHeight) > verticalLength)
        {
        	RepositionBackground();

        }
    }

    private void RepositionBackground()
    {
    	Vector2 offset = new Vector2(0, 2f*background.GetComponent<Renderer>().bounds.size.y - 0.05f);
    	transform.position = (Vector2) transform.position + offset;
    	verticalLength = background.transform.position.y + 0.5f * background.GetComponent<Renderer>().bounds.size.y;
    }
}
