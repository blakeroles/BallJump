using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GoogleMobileAds.Api;

public class AdBanner : MonoBehaviour
{

	private BannerView bannerView;

    // Start is called before the first frame update
    void Start()
    {	
        MobileAds.Initialize(initStatus => { });

        this.RequestBanner();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void RequestBanner()
    {
    	#if UNITY_ANDROID
    		string adUnitId = "ca-app-pub-3940256099942544/6300978111";
    	#elif UNITY_IPHONE
    		string adUnitId = "ca-app-pub-3940256099942544/2934735716";
    	#else
    		string adUnitId = "unexpected_platform";
    	#endif

    	this.bannerView = new BannerView(adUnitId, AdSize.Banner, AdPosition.Bottom);

    	AdRequest request = new AdRequest.Builder().Build();

    	this.bannerView.LoadAd(request);

    }
}
