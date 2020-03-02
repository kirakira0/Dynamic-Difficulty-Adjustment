using UnityEngine;
using System.Collections;

public class PickupPoints : MonoBehaviour 
{
    public int scoreToGive;

    private ScoreManager scoreManager;
    private Agent Agent; 

	// Use this for initialization
	void Start () 
	{
        scoreManager = FindObjectOfType<ScoreManager>();
        Agent = FindObjectOfType<Agent>(); 
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}

    //Increase score when coins are collected
    void OnTriggerEnter2D(Collider2D other)
    {
        if(other.gameObject.name == "Player")
        {
            scoreManager.AddScore(scoreToGive);
            gameObject.SetActive(false);
        }
    }
}
