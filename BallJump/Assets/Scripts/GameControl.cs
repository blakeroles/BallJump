using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class GameControl : MonoBehaviour
{

	public static GameControl instance;
	public GameObject gameOverText;
	public Text scoreText;
	public bool gameOver = false;

	private int score = 0;

    // Start is called before the first frame update
    void Awake()
    {
        if (instance == null)
        {
        	instance = this;
        }
        else if (instance != this)
        {
        	Destroy(gameObject);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (gameOver && Input.GetMouseButtonDown(0))
        {
        	SceneManager.LoadScene("TitleScene");
        }
    }


    public void BallDied()
    {
    	gameOverText.SetActive(true);
    	gameOver = true;
    }

    public void BallScored()
    {
    	if (gameOver)
    	{
    		return;
    	}
    	score++;
    	scoreText.text = "Score: " + score.ToString(); 
    	
    }
}
