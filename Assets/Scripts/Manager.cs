using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public PlayerData PlayerData;
    public Text idAndInstructionsText;

    public Text metricsText; 
    public GameObject startingPlatform; 
    public Transform startingPlayerPoint; 
    public Transform startingPlatformPoint;
    public Text livesText; 
    public Text infoText; 
    public Text coinText; 
    public GameObject deathCanvas; 
    public Text summaryText; 
    public Text reportText;  
    
    private bool gameIsPaused = true; 
    private Logger LOGGER; 
    private PlayerController PlayerController;
    private Generator Generator;      
    private Agent Agent; 
    // private FirebaseHandler FirebaseHandler;

    private bool x = true;
    private float xpos;  

    void Awake() {
        PlayerData = new PlayerData(Random.Range(0, 10001)); 
        idAndInstructionsText.text = "Thank you for participating in our research trial. Your player id is: " + PlayerData.GetPlayerId() +
                                      ". Please save this number as it will be asked about in the player feedback form.\n\n The game that you are about to play is a platformer. To play, press the [ENTER] key or [SPACE] key to jump. You may “double jump”, or press the jump keys again while in the air to jump a second time. The longer a jump key is held, the higher the character will jump. You may wish to leverage this scaling jump force when you encounter large gaps between platforms or small gaps that require short jumps. The objective is to jump from platform to platform and collect as many coins as possible. Falling between the platforms will count as a death. The game will conclude after a certain number of deaths or after the completion of ten rounds." + 
                                      "\n\nTo start moving, press one of the two jump keys. ";


        LOGGER = GameObject.Find("Logger").GetComponent<Logger>(); 
        PlayerController = GameObject.Find("Player").GetComponent<PlayerController>();
        Generator = GameObject.Find("PlatformGenerator").GetComponent<Generator>(); 
        Agent = GameObject.Find("Agent").GetComponent<Agent>(); 
        // FirebaseHandler = GameObject.Find("FirebaseHandler").GetComponent<FirebaseHandler>(); 
        // deathCanvas.SetActive(false);
        xpos = PlayerController.GetPlayerTransform().x; 
    }

    void Update() {
        livesText.text = "Round: " + Agent.roundIndex + ", Policy: " + Agent.policyIndex;
        coinText.text = "COINS COLLECTED: " + Agent.GetTotalCoinsCollected();         
        infoText.text = "\nAcclimated: " + Agent.GetIsAcclimated() + 
                        "\nCoins Per Current Window: " + Agent.GetCoinsPerCurrentWindow() +
                        "\nCoins Collection " + Agent.GetCoinsCollected() + 
                        "\nScores: " + Agent.ScoresToString() + 
                        "\nScore SD: " + Agent.scoreSD;
    }

    public void HandleFall() { 
        Generator.p.acclimated = false;
        Agent.NextPolicy(Generator.p);

        // Pause game. 
        SetPaused(true);

        // Stop coroutine.
        Generator.StopGeneration();
  
        // Destroy all platforms. 
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
        foreach(GameObject platform in platforms) {
            GameObject.Destroy(platform);
        }
        // Reset platforms. 
        PlayerController.SetPlayerPosition(new Vector3(xpos, startingPlayerPoint.position.y, 0));
        Instantiate(startingPlatform, new Vector3(xpos, startingPlayerPoint.position.y - 3, 0), Quaternion.identity);                         
    }

    public void HandleGameOver() {
            // HandleFall();
            // x = false;
            // StartCoroutine(Web.WriteResults());
            summaryText.text = "You completed " + Agent.roundIndex + " rounds and collected a total of " + Agent.GetTotalCoinsCollected() + " coins.";
            // summaryText.text = Agent.REPORT;
            deathCanvas.SetActive(true);         
    }

    public bool GetPaused() {
        return gameIsPaused;
    }

    public void SetPaused(bool paused) {
        gameIsPaused = paused;
    }

}




public class PlayerData  {

    private int playerId; 
    private string gameData;

    public PlayerData(int playerId) {
        this.playerId = playerId;
    }

    public void SetGameData(string gameData) {
        this.gameData = gameData;
    }

    public int GetPlayerId() {
        return this.playerId;
    }
}
