using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

	public float moveSpeed;
    public float yForce;
    public float xMobileTapForce = 5f;
    public GameObject cam;

    private List<float> hitPlatformYs = new List<float>();
    private float camHeight;

    // Start is called before the first frame update
    void Start()
    {
        Camera camSize = Camera.main;
        camHeight = 2f * camSize.orthographicSize;
    }

    // Update is called once per frame
    void Update()
    {
    	if (!GameControl.instance.gameOver)
    	{
    		float h = Input.GetAxis("Horizontal") * moveSpeed;

        	GetComponent<Rigidbody2D>().AddForce(Vector2.right * h);

            #if UNITY_ANDROID
                
                if (Input.touchCount > 0)
                {

                    Touch touch = Input.GetTouch(0);

                    if (touch.position.x < Screen.width/2)
                    {
                        GetComponent<Rigidbody2D>().AddForce(new Vector2(-xMobileTapForce, 0));
                    }
                    else if (touch.position.x > Screen.width/2)
                    {
                        GetComponent<Rigidbody2D>().AddForce(new Vector2(xMobileTapForce, 0));
                    }
                }
            #endif

            if (transform.position.y < cam.transform.position.y - 0.5f * camHeight)
            {
                GameControl.instance.PlayerDied();
            }

    	}

    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if (OptionsMenu.soundIsOn)
        {
            SoundManagerScript.PlaySound("collision");
        }
        bool beenHit = false;
        foreach (float y in hitPlatformYs)
        {
            if (y == col.gameObject.transform.position.y)
            {
                beenHit = true;
            }
        }

        if (!beenHit)
        {

            hitPlatformYs.Add(col.gameObject.transform.position.y);
            GameControl.instance.PlayerScored();
        }

        GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
        GetComponent<Rigidbody2D>().AddForce(new Vector2(0, yForce));
    }
}
