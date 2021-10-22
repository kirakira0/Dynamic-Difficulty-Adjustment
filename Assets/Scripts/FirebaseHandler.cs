using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.Random;
using UnityEngine.UI;
using Proyecto26;

/* 
 *  https://github.com/proyecto26/RestClient
 */

public class FirebaseHandler : MonoBehaviour
{
    public Text scoreText;
    public int id; 
    public static int playerScore;
    public static string playerName;
    public InputField nameText;

    private Manager Manager;

    public string api = "https://dynamic-diff-adjustment-dev-default-rtdb.firebaseio.com/";

    // Start is called before the first frame update
    void Start()
    {
        Manager = GameObject.Find("Manager").GetComponent<Manager>();
        // Generate random player id 
        id = Random.Range(0, 10001); 
    }

    public void PostToDatabase(string gameData) {

        Player user = new Player(gameData);
        // RestClient.Put("https://dynamic-diff-adjustment-dev-default-rtdb.firebaseio.com/" + playerName + ".json", user);
        Debug.Log("PRE POST");
        RestClient.Put(api + Manager.PlayerData.GetPlayerId() + ".json", user);
        
        Debug.Log("POST POST");

    }

}

public class Player  {
    public string gameData;

    public Player(string gameData) {
        this.gameData = gameData;
    }
}
