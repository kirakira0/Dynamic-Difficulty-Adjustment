using UnityEngine;
using System.Collections;
using System;

public class PickupPoints : MonoBehaviour 
{
    public bool acclimated = false;

    private ScoreManager scoreManager;

	// Use this for initialization
	void Start () 
	{
        scoreManager = FindObjectOfType<ScoreManager>();
	}
	
	// Update is called once per frame
	void Update () 
	{
	
	}
    
    void OnTriggerEnter2D(Collider2D other)
    {
        //Console.WriteLine("We here ");
        if(other.gameObject.name == "Player")
        {
            scoreManager.AddScore(1);
            gameObject.SetActive(false);
        }
    }

}
