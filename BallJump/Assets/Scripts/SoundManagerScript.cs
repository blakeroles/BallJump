using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SoundManagerScript : MonoBehaviour
{

	public static AudioClip collidePlatformSound;
	public static AudioClip gameOverSound;
	public static AudioClip tapSound;
	public static AudioClip coinSound;

	static AudioSource audioSrc;

    // Start is called before the first frame update
    void Start()
    {
        collidePlatformSound = Resources.Load<AudioClip>("collision");
        gameOverSound = Resources.Load<AudioClip>("game_over");
        tapSound = Resources.Load<AudioClip>("tap");
		coinSound = Resources.Load<AudioClip>("coin_chime");

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
			}
		}

    }
}
