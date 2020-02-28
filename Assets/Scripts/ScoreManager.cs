using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;

public class ScoreManager : MonoBehaviour 
{
    public Text scoreText;
    public Text highScoreText;
    public Environment env;

    public float scoreCount;
    public float highScoreCount;
    public bool scoreIncreasing; 

    public bool acclimated;
    public float acclimationScore;
    Queue scores = new Queue();


	// Use this for initialization
	void Start () 
	{
	    if(PlayerPrefs.HasKey("highScore"))
        {
            highScoreCount = PlayerPrefs.GetFloat("highScore");
        }
	}
	
	// Update is called once per frame
	void Update () 
	{
        if(scoreCount > highScoreCount)
        {
            highScoreCount = scoreCount;
            PlayerPrefs.SetFloat("highScore", highScoreCount);
        }

        scoreText.text = "Score: " + Mathf.Round(scoreCount);
        highScoreText.text = "High Score: " + Mathf.Round(highScoreCount);
	}

    public void AddScore (int pointsScored)
    { 
        //Debug.Log("ScoreCount is: " + scoreCount);
        scoreCount += pointsScored;
        scores.Enqueue(scoreCount);
        if (scores.Count > 5){
            Debug.Log("Dequeueing ");
            scores.Dequeue();
            Debug.Log("Number of elemenets in queue is: " + scores.Count);
        }
        Debug.Log("Number of elemenets in queue is: " + scores.Count);
        // checkAcclimation(scoreCount);
    }

    public void checkAcclimation (float scoreCount) {
        // if(scores.){
        //     acclimated = true;
        //     Debug.Log("Acclimated!");
        // }
        acclimated = false;
                    
    }
}
