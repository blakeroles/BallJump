using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{

	public float moveSpeed;
    public float yForce;
    public float xMobileTapForce = 5f;

    private List<float> hitPlatformYs = new List<float>();
    private float camHeight;
    private float camWidth;
    private Animator myAnimator;
    private bool facingRight;

    // Start is called before the first frame update
    void Start()
    {
        Camera camSize = Camera.main;
        camHeight = 2f * camSize.orthographicSize;
        camWidth = camHeight * camSize.aspect;

        myAnimator = GetComponent<Animator>();
        facingRight = true;




    }

    // Update is called once per frame
    void Update()
    {
    	if (!GameControl.instance.gameOver)
    	{
    		float h = Input.GetAxis("Horizontal") * moveSpeed;

        	GetComponent<Rigidbody2D>().AddForce(Vector2.right * h);

            #if UNITY_EDITOR
                ComputerFlip(h);
                Debug.Log(Mathf.Abs(h).ToString());
                myAnimator.SetFloat("speed", Mathf.Abs(h));
            #endif

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
                    TouchFlip(touch.position.x);
                    myAnimator.SetFloat("speed", Mathf.Abs(touch.position.x));
                }
                else
                {
                    myAnimator.SetFloat("speed", 0f);
                }
            #endif
                

            #if UNITY_IPHONE
                
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
                    TouchFlip(touch.position.x);
                    myAnimator.SetFloat("speed", Mathf.Abs(touch.position.x));
                }
                else
                {
                    myAnimator.SetFloat("speed", 0f);
                }
                
            
            #endif

            

            if (transform.position.y < GameControl.instance.mainCam.transform.position.y - 0.5f * camHeight - 0.5f)
            {
                GameControl.instance.PlayerSecondChance();
            }

            if (transform.position.x > 0.5f * camWidth)
            {
                transform.position = new Vector3(-0.5f * camWidth, transform.position.y, transform.position.z);
            }

            if (transform.position.x < -0.5f * camWidth)
            {
                transform.position = new Vector3(0.5f * camWidth, transform.position.y, transform.position.z);
            }

    	}

    }

    private void TouchFlip(float touchValue)
    {
        if (touchValue > Screen.width/2 && !facingRight || touchValue < Screen.width/2 && facingRight)
        {
            facingRight = !facingRight;

            Vector3 theScale = transform.localScale;

            theScale.x *= -1;

            transform.localScale = theScale;
        }
    }

    private void ComputerFlip(float horizontal)
    {
        if (horizontal > 0f && !facingRight || horizontal < 0f && facingRight)
        {
            
            facingRight = !facingRight;
            
            Vector3 theScale = transform.localScale;

            theScale.x *= -1;

            transform.localScale = theScale;
        }
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        SoundManagerScript.PlaySound("collision");
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

        if (col.gameObject.tag == "Platform")
        {
            GetComponent<Rigidbody2D>().velocity = new Vector2(GetComponent<Rigidbody2D>().velocity.x, 0);
            GetComponent<Rigidbody2D>().AddForce(new Vector2(0, yForce));
        }



    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if (col.gameObject.tag == "Coin")
        {
            GameControl.instance.PlayerHitCoin();
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag == "Cube Enemy")
        {
            if (GameControl.instance.hearts.Count > 0)
            {
                GameControl.instance.DeductHeart();
                Destroy(col.gameObject);
            } else 
            {
                GameControl.instance.PlayerSecondChance();
            }
            
        }

        if (col.gameObject.tag == "BadPotion")
        {
            GameControl.instance.PlayerHitBadPotion();
            Destroy(col.gameObject);
        }

        if (col.gameObject.tag == "Circle Enemy")
        {
            if (GameControl.instance.hearts.Count > 0)
            {
                GameControl.instance.DeductHeart();
                Destroy(col.gameObject);
            } else
            {
                GameControl.instance.PlayerSecondChance();
            }
            
        }

        if (col.gameObject.tag == "Heart")
        {
            GameControl.instance.PlayerHitHeart();
            Destroy(col.gameObject);
        }
    }
}
