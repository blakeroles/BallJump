using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraControl : MonoBehaviour
{

	public Transform target;
	public GameObject backgroundImage1 = null;
	public GameObject gatesImage1 = null;
	public GameObject rocksImage1 = null;
	public GameObject bigRocksImage1 = null;
	public GameObject backgroundImage2 = null;
	public GameObject gatesImage2 = null;
	public GameObject rocksImage2 = null;
	public GameObject bigRocksImage2 = null;
	public Camera mainCam = null;
	public bool trackingTarget = true;

	private float lastYPosition;



    // Start is called before the first frame update
    void Start()
    {
        lastYPosition = 0;
        scaleBackgroundImageFitScreenSize();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
    	if (target.position.y > lastYPosition && target.GetComponent<Rigidbody2D>().velocity.y >= 0 && trackingTarget)
    	{

        	Vector3 targetPos = new Vector3(transform.position.x, target.position.y, transform.position.z);

        	transform.position = targetPos;
        	lastYPosition = target.position.y;
    	}


    }

    private void scaleBackgroundImageFitScreenSize()
    {
    	float srcHeight = Screen.height;
    	float srcWidth = Screen.width;

    	Vector2 deviceScreenResolution = new Vector2(srcWidth, srcHeight);

    	float DEVICE_SCREEN_ASPECT = srcWidth / srcHeight;

    	mainCam.aspect = DEVICE_SCREEN_ASPECT;

    	float camHeight = 100.0f * mainCam.orthographicSize * 2.0f;
    	float camWidth = camHeight * DEVICE_SCREEN_ASPECT;

    	SpriteRenderer backgroundImageSR1 = backgroundImage1.GetComponent<SpriteRenderer>();
    	SpriteRenderer gatesImageSR1 = gatesImage1.GetComponent<SpriteRenderer>();
    	SpriteRenderer rocksImageSR1 = rocksImage1.GetComponent<SpriteRenderer>();
    	SpriteRenderer bigRocksImageSR1 = bigRocksImage1.GetComponent<SpriteRenderer>();

    	float bg1ImgH = backgroundImageSR1.sprite.rect.height;
    	float bg1ImgW = backgroundImageSR1.sprite.rect.width;
    	float g1ImgH = gatesImageSR1.sprite.rect.height;
    	float g1ImgW = gatesImageSR1.sprite.rect.width;
     	float r1ImgH = rocksImageSR1.sprite.rect.height;
    	float r1ImgW = rocksImageSR1.sprite.rect.width;
        float br1ImgH = bigRocksImageSR1.sprite.rect.height;
    	float br1ImgW = bigRocksImageSR1.sprite.rect.width;


    	float bg1Img_scale_ratio_Height = camHeight / bg1ImgH;
    	float bg1Img_scale_ratio_Width = camWidth / bg1ImgW;
    	float g1Img_scale_ratio_Height = camHeight / g1ImgH;
    	float g1Img_scale_ratio_Width = camWidth / g1ImgW;
    	float r1Img_scale_ratio_Height = camHeight / r1ImgH;
    	float r1Img_scale_ratio_Width = camWidth / r1ImgW;
    	float br1Img_scale_ratio_Height = camHeight / br1ImgH;
    	float br1Img_scale_ratio_Width = camWidth / br1ImgW;


    	backgroundImage1.transform.localScale = new Vector3(bg1Img_scale_ratio_Width, bg1Img_scale_ratio_Height, 1);
    	gatesImage1.transform.localScale = new Vector3(g1Img_scale_ratio_Width, g1Img_scale_ratio_Height, 1);
    	rocksImage1.transform.localScale = new Vector3(r1Img_scale_ratio_Width, r1Img_scale_ratio_Height, 1);
     	bigRocksImage1.transform.localScale = new Vector3(br1Img_scale_ratio_Width, br1Img_scale_ratio_Height, 1);
      	

      	backgroundImage1.transform.position = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y, 0);
      	gatesImage1.transform.position = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y, 0);
      	rocksImage1.transform.position = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y, 0);
      	bigRocksImage1.transform.position = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y, 0);



      	if (trackingTarget)
      	{
      		SpriteRenderer backgroundImageSR2 = backgroundImage2.GetComponent<SpriteRenderer>();
      		SpriteRenderer gatesImageSR2 = gatesImage2.GetComponent<SpriteRenderer>();
      		SpriteRenderer rocksImageSR2 = rocksImage2.GetComponent<SpriteRenderer>();
    		SpriteRenderer bigRocksImageSR2 = bigRocksImage2.GetComponent<SpriteRenderer>();

	        float bg2ImgH = backgroundImageSR2.sprite.rect.height;
	    	float bg2ImgW = backgroundImageSR2.sprite.rect.width;
	    	float g2ImgH = gatesImageSR2.sprite.rect.height;
	    	float g2ImgW = gatesImageSR2.sprite.rect.width;
	      	float r2ImgH = rocksImageSR2.sprite.rect.height;
	    	float r2ImgW = rocksImageSR2.sprite.rect.width;
	        float br2ImgH = bigRocksImageSR2.sprite.rect.height;
	    	float br2ImgW = bigRocksImageSR2.sprite.rect.width;

	    	float bg2Img_scale_ratio_Height = camHeight / bg2ImgH;
	    	float bg2Img_scale_ratio_Width = camWidth / bg2ImgW;
	     	float g2Img_scale_ratio_Height = camHeight / g2ImgH;
	    	float g2Img_scale_ratio_Width = camWidth / g2ImgW;
	     	float r2Img_scale_ratio_Height = camHeight / r2ImgH;
	    	float r2Img_scale_ratio_Width = camWidth / r2ImgW;
	    	float br2Img_scale_ratio_Height = camHeight / br2ImgH;
	    	float br2Img_scale_ratio_Width = camWidth / br2ImgW;

      		backgroundImage2.transform.localScale = new Vector3(bg2Img_scale_ratio_Width, bg2Img_scale_ratio_Height, 1);
      		gatesImage2.transform.localScale = new Vector3(g2Img_scale_ratio_Width, g2Img_scale_ratio_Height, 1);
      		rocksImage2.transform.localScale = new Vector3(r2Img_scale_ratio_Width, r2Img_scale_ratio_Height, 1);
      		bigRocksImage2.transform.localScale = new Vector3(br2Img_scale_ratio_Width, br2Img_scale_ratio_Height, 1);

	      	backgroundImage2.transform.position = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y + mainCam.orthographicSize * 2.0f, 0);
	      	gatesImage2.transform.position = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y + mainCam.orthographicSize * 2.0f, 0);
	      	rocksImage2.transform.position = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y + mainCam.orthographicSize * 2.0f, 0);
	      	bigRocksImage2.transform.position = new Vector3(mainCam.transform.position.x, mainCam.transform.position.y + mainCam.orthographicSize * 2.0f, 0);
      	}

    }
}
