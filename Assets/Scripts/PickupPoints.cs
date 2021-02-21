using UnityEngine;
using System.Collections;

public class PickupPoints : MonoBehaviour 
{
    public int scoreToGive;

    private Manager manager; 
    // private ScoreManager scoreManager;
    
	// Use this for initialization
	void Start () 
	{
        manager = GameObject.Find("Manager").GetComponent<Manager>();
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
            // scoreManager.AddScore(scoreToGive);
            gameObject.SetActive(false);
        }
    }
}
