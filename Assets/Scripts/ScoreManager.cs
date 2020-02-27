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
    public float[] scores;
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
         Console.WriteLine("We here ");
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
        Console.WriteLine("ScoreCount is: " + scoreCount);
        //while(scoreCount < 10){
            scoreCount += pointsScored;
        //}   
       
        // scores[emptyIndex] = scoreCount;
        // emptyIndex++;
        // checkAcclimation(scoreCount);
    }

    // public void checkAcclimation (float scoreCount) {
    //     if(scores.Length <= 3){
    //         if(scores[scores.Length - 1] == scores[scores.Length - 2] && scores[scores.Length - 1] == scores[scores.Length - 3] ){
    //             acclimated = true;
    //             Console.WriteLine("Acclimated!");
    //         }
    //         acclimated = false;
    //     }            
    // }
}
