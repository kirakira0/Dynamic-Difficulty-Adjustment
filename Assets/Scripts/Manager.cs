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
    
    private bool gameIsPaused = true; 
    private Logger LOGGER; 
    private PlayerController PLAYERCONTROLLER; 

    void Awake() {
        LOGGER = GameObject.Find("Logger").GetComponent<Logger>(); 
        PLAYERCONTROLLER = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    public void HandleFall() {
        // Reset room. 
        Scene scene = SceneManager.GetActiveScene(); 
        SceneManager.LoadScene(scene.name);
        /*
        // Pause game. 
        SetPaused(true);
        // Destroy all platforms. 
        GameObject[] platforms = GameObject.FindGameObjectsWithTag("Platform");
        foreach(GameObject platform in platforms) {
            GameObject.Destroy(platform);
        }
        // Reset platforms. 
        PLAYERCONTROLLER.SetPlayerPosition(new Vector3(startingPlayerPoint.position.x, startingPlayerPoint.position.y + 1, 0));
        // Instantiate(startingPlatform, new Vector3(startingPlatformPoint.position.x, startingPlatformPoint.position.y, 0), Quaternion.identity);        
        */ 
    }

    public void HandleDeath() {
        HandleFall();
        // + other death stuff 
    }

    public bool GetPaused() {
        return gameIsPaused;
    }

    public void SetPaused(bool paused) {
        gameIsPaused = paused;
    }

}
