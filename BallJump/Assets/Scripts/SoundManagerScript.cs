using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{

	public static AudioClip collidePlatformSound;
	public static AudioClip gameOverSound;
	public static AudioClip tapSound;
	public static AudioClip coinSound;
	public static AudioClip poisonSound;
	public static AudioClip heartPickupSound;
	public static AudioClip playerHitSound;

	static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        collidePlatformSound = Resources.Load<AudioClip>("collision");
        gameOverSound = Resources.Load<AudioClip>("game_over");
        tapSound = Resources.Load<AudioClip>("tap");
		coinSound = Resources.Load<AudioClip>("coin_chime");
		poisonSound = Resources.Load<AudioClip>("poison");
		heartPickupSound = Resources.Load<AudioClip>("heart_pickup");
		playerHitSound = Resources.Load<AudioClip>("player_hit");

        audioSrc = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public static void PlaySound(string clip)
    {
		if (OptionsMenu.soundIsOn)
		{
			switch (clip)
			{
				case "collision":
					audioSrc.PlayOneShot(collidePlatformSound);
					break;
				case "game_over":
					audioSrc.PlayOneShot(gameOverSound);
					break;
				case "tap":
					audioSrc.PlayOneShot(tapSound);
					break;
				case "coin_chime":
					audioSrc.PlayOneShot(coinSound);
					break;
				case "poison":
					audioSrc.PlayOneShot(poisonSound);
					break;
				case "heart_pickup":
					audioSrc.PlayOneShot(heartPickupSound);
					break;
				case "player_hit":
					audioSrc.PlayOneShot(playerHitSound);
					break;
			}
		}

    }
}
