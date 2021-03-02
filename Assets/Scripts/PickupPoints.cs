using UnityEngine;
using System.Collections;

public class PickupPoints : MonoBehaviour 
{
    public int scoreToGive;

    private Manager manager; 
    private Agent Agent; 
    // private ScoreManager scoreManager;
    
	// Use this for initialization
	void Start () 
	{
        manager = GameObject.Find("Manager").GetComponent<Manager>();
        Agent = GameObject.Find("Agent").GetComponent<Agent>();
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
            // manager.IncrementCoinCount();
            Agent.IncrementCoinsCollected(); 
            // scoreManager.AddScore(scoreToGive);
            gameObject.SetActive(false);
        }
    }
}
