using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;
using UnityEngine.UI;
using Proyecto26;

public class PlayerScores : MonoBehaviour
{
    public Text scoreText;
    public static int playerScore;
    public static string playerName;
    public InputField nameText;

    // Start is called before the first frame update
    void Start()
    {
        // playerScore = Random.Next(0, 101); 
        playerScore = Random.Range(0, 101);
        scoreText.text = "Score: " + playerScore; 
    }

    public void OnSubmit() {
        playerName = nameText.text;

        PostToDatabase();
    }

    private void PostToDatabase() {
        User user = new User();
        RestClient.Put("https://dynamic-diff-adjustment-dev-default-rtdb.firebaseio.com/" + playerName + ".json", user);

    }

}
