using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;
using UnityEngine.UI;
using Proyecto26;

public class FirebaseHandler : MonoBehaviour
{
    public Text scoreText;
    public int id; 
    public static int playerScore;
    public static string playerName;
    public InputField nameText;

    // Start is called before the first frame update
    void Start()
    {
        // Generate random player id 
        id = Random.Range(0, 101);
        // playerScore = Random.Next(0, 101); 
        // playerScore = Random.Range(0, 101);
        // scoreText.text = "Score: " + playerScore; 
    }

    public void OnSubmit() {
        // playerName = nameText.text;

        // PostToDatabase();
    }

    public void PostToDatabase(string gameData) {
        Player user = new Player(gameData);
        // RestClient.Put("https://dynamic-diff-adjustment-dev-default-rtdb.firebaseio.com/" + playerName + ".json", user);
        Debug.Log("PRE POST");
        RestClient.Put("https://dynamic-diff-adjustment-dev-default-rtdb.firebaseio.com/" + "TESTPLAYER" + ".json", user);
        Debug.Log("POST POST");

    }

}

public class Player  {
    public string gameData;

    public Player(string gameData) {
        this.gameData = gameData;
    }
}
