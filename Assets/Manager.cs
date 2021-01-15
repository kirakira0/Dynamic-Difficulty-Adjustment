using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    // public PlayerMovement player;
    public GameObject startingPlatform;
    public Transform startingPlatformPoint;
    public Transform startingPlayerPoint;

    private GameObject player;
    private Text livesText;
    private Text coinsText;
    private PlayerMovement playerInfo;
    private int coinsCollected; 
    

    void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        livesText = GameObject.Find("LivesText").GetComponent<Text>();
        coinsText = GameObject.Find("CoinsText").GetComponent<Text>();
        playerInfo = player.GetComponent<PlayerMovement>();
    }

    void Update()
    {
        livesText.text = "REMAINING LIVES: " + player.GetComponent<PlayerMovement>().GetRemainingLives().ToString();
        coinsText.text = "COINS COLLECTED: " + coinsCollected;
        // What to do if the player falls off of the platforms
        // into the killzone.
        if (playerInfo.HasFallen()) {
            playerInfo.DecrementRemainingLives();
            // Pause player movement.
            playerInfo.SetPlayerSpeed(0);
            // Remove all lingering platforms. 
            GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
            foreach(GameObject platform in platforms) {
                GameObject.Destroy(platform);
            }
            if (playerInfo.IsDead()) {

            } else {
                // Reset platforms. 
                Instantiate(startingPlatform, new Vector3(startingPlatformPoint.position.x, startingPlatformPoint.position.y, 0), Quaternion.identity);
                playerInfo.SetPlayerPosition(new Vector3(startingPlayerPoint.transform.position.x, startingPlayerPoint.transform.position.y, 0));
                playerInfo.SetHasFallen(false);
            }
        }
    }

    // GETTERS 
    // ---------------------------------
    public int GetCoinsCollected() {return coinsCollected;}

    // SETTERS 
    // ---------------------------------
    public void SetCoinsCollected(int n) {coinsCollected = n;}
    public void IncrementCoinCount() {coinsCollected++;}

}
