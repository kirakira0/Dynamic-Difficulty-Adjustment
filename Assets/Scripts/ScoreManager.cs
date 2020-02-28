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
    //USE QUEUES
    Queue scores = new Queue;
    int emptyIndex = 0;
    int platformIndex = 0;

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
        Debug.Log("ScoreCount is: " + scoreCount);
        scoreCount += pointsScored;

        if(scoreCount == 10){
            scores[emptyIndex] = scoreCount;
            emptyIndex++;
            scoreCount = 0;

       }
        checkAcclimation(scoreCount);
    }

    // public void checkAcclimation (float scoreCount) {
    //     if(scores.Length <= 5){
    //         if(scores[scores.Length - 1] == scores[scores.Length - 2] && scores[scores.Length - 1] == scores[scores.Length - 3] ){
    //             acclimated = true;
    //             Debug.Log("Acclimated!");
    //         }
    //         acclimated = false;
    //     }            
    // }
}
