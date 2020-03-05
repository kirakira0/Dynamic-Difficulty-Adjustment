using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using System;
using System.Collections.Generic;

public class ScoreManager : MonoBehaviour 
{
    public Text scoreText;
    public Text highScoreText;
    public Environment env;

    private Agent Agent; 
    public bool scoreIncreasing;
    public int scoreCount;
    public int highScoreCount;

    public bool acclimated;
    public float acclimationScore;
    LinkedList<int> scores = new LinkedList<int>();
    LinkedListNode<int> current;
    LinkedListNode<int> previous;


	// Use this for initialization
	void Start () 
	{
        Agent = FindObjectOfType<Agent>();
	    if(PlayerPrefs.HasKey("highScore"))
        {
            highScoreCount = PlayerPrefs.GetInt("highScore");
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
        scoreCount += pointsScored;
        scores.AddLast(scoreCount);
        if (scores.Count > 5){
            scores.RemoveFirst();
            Debug.Log("Number of elemenets in queue is: " + scores.Count);
        }
        Debug.Log("listcount is: " + scores.Count);
        //Debug.Log("Number of elemenets in queue is: " + listCount);
        //checkAcclimation(scoreCount);
    }
}
