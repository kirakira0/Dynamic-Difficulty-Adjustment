using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Manager : MonoBehaviour
{
    public Text metricsText; 
    public GameObject startingPlatform; 
    public Transform startingPlayerPoint; 
    public Transform startingPlatformPoint;
    public Text livesText; 
    public Text infoText; 
    
    private bool gameIsPaused = true; 
    private Logger LOGGER; 
    private PlayerController PlayerController;
    private Generator Generator;      
    private Agent Agent;  


    void Awake() {
        LOGGER = GameObject.Find("Logger").GetComponent<Logger>(); 
        PlayerController = GameObject.Find("Player").GetComponent<PlayerController>();
        Generator = GameObject.Find("PlatformGenerator").GetComponent<Generator>();
        Agent = GameObject.Find("Agent").GetComponent<Agent>();
    }

    void Update() {
        livesText.text = "Remaining Lives: " + PlayerController.GetLives();
        infoText.text = "Current Subpolicy: " + Agent.GetCurrentSubpolicy() +                        
                        "\nAcclimated: " + Agent.GetIsAcclimated() + 
                        "\nCoins Per Current Window: " + Agent.GetCoinsPerCurrentWindow() +
                        "\nCoins Collection " + Agent.GetCoinsCollected();
    }

    public void HandleFall() {  
        // Pause game. 
        SetPaused(true);
        
        // Stop coroutine.
        Generator.StopGeneration();
  
        // Destroy all platforms. 
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
        foreach(GameObject platform in platforms) {
            GameObject.Destroy(platform);
        }
        // // Reset platforms. 
        PlayerController.SetPlayerPosition(new Vector3(startingPlayerPoint.position.x + 3, startingPlayerPoint.position.y, 0));
        Instantiate(startingPlatform, new Vector3(startingPlayerPoint.position.x, startingPlatformPoint.position.y, 0), Quaternion.identity);                 
    }

    public void HandleDeath() {
        HandleFall();
        // Reset room. 
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);   
    }

    public bool GetPaused() {
        return gameIsPaused;
    }

    public void SetPaused(bool paused) {
        gameIsPaused = paused;
    }

}
