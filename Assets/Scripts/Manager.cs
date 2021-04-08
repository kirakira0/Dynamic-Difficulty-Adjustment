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
    public Text coinText; 
    public GameObject deathCanvas; 
    public Text summaryText; 
    public Text reportText;  
    
    private bool gameIsPaused = true; 
    private Logger LOGGER; 
    private PlayerController PlayerController;
    private Generator Generator;      
    private Agent Agent; 
    private Web Web;

    private bool x = true;
    private float xpos;  
 


    void Awake() {
        LOGGER = GameObject.Find("Logger").GetComponent<Logger>(); 
        PlayerController = GameObject.Find("Player").GetComponent<PlayerController>();
        Generator = GameObject.Find("PlatformGenerator").GetComponent<Generator>(); 
        Agent = GameObject.Find("Agent").GetComponent<Agent>(); 
        Web = GameObject.Find("Web").GetComponent<Web>(); 
        // deathCanvas.SetActive(false);
        xpos = PlayerController.GetPlayerTransform().x; 
    }

    void Update() {
        livesText.text = "REMAINING LIVES: " + PlayerController.remainingLives;
        coinText.text = "COINS COLLECTED: " + Agent.GetTotalCoinsCollected();
        infoText.text = "Current Subpolicy: " + Agent.GetCurrentSubpolicy() +                        
                        "\nAcclimated: " + Agent.GetIsAcclimated() + 
                        "\nCoins Per Current Window: " + Agent.GetCoinsPerCurrentWindow() +
                        "\nCoins Collection " + Agent.GetCoinsCollected() + 
                        "\nScores: " + Agent.ScoresToString() + 
                        "\nScore SD: " + Agent.scoreSD;
    }

    public void HandleFall() {  
        // Pause game. 
        SetPaused(true);

        // Add to the current policy report.
        Generator.currentPolicyReport.AddLife(PlayerController.remainingLives); 
        Debug.Log("ADDING LIFE");
        Generator.currentPolicyReport.AddWindowCount(Generator.seenWindows); 
        Debug.Log("ADD LIFE TO QUEUE");

        
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

        // Instantiate(startingPlatform, new Vector3(PlayerController.GetPlayerTransform().x - 3, startingPlatformPoint.position.y, 0), Quaternion.identity);                         
    }

    public void HandleDeath() {
        if (x) {

            HandleFall();
            
            // StartCoroutine(Web.InsertData(Agent.totalCoinsCollected));
            x = false; 

            // Show summary. 
            summaryText.text = "You collected a total of " + Agent.totalCoinsCollected + 
                                " and acclimated to " + Agent.subpolicies + " subpolicies. " +
                                "Acclimation threshold set to: " + Agent.ACCLIMATION_THRESHOLD;
            string report = ""; 

            if (Generator.policyReports.Count > 0) {
                foreach (var policyReport in Generator.policyReports) {
                    report += policyReport.GetReport() + "\n\n";
                }
            } else {
                report += "You did not acclimate to any subpolicies with the lives allotted."; 
            }
            
            reportText.text = report; 


            deathCanvas.SetActive(true); 
        } 
    }

    public bool GetPaused() {
        return gameIsPaused;
    }

    public void SetPaused(bool paused) {
        gameIsPaused = paused;
    }

}
