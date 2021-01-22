using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public GameObject startingPlatform;
    public Transform startingPlayerPoint;
    public Canvas deathScreen;
    public Text feedbackText;
    public Web webRequest;

    private float MID_Y_POS; 
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
        MID_Y_POS = GameObject.Find("MidYPos").transform.position.y;
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
            // Reset platforms. 
            Instantiate(startingPlatform, new Vector3(startingPlayerPoint.transform.position.x, MID_Y_POS - 1, 0), Quaternion.identity);
            playerInfo.SetPlayerPosition(new Vector3(startingPlayerPoint.transform.position.x, MID_Y_POS + 1, 0));
            playerInfo.SetHasFallen(false);
            playerInfo.SetPaused(true);
            if (playerInfo.IsDead()) {
                feedbackText.text = "You collected a cumulative total of " + coinsCollected + " coins and acclimated to 0 subpolicies.";
                deathScreen.gameObject.SetActive(true);
                StartCoroutine(webRequest.GetComponent<Web>().RegisterPlayer("test #8"));
                // webRequest.StartCoroutine(RegisterPlayer(registerPlayerURL, "test #8"));
            } else {

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
