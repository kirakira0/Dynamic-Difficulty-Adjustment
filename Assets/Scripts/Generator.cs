using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

/**
 * Generates the platforms using input from the Agent.
 */

public class Generator : MonoBehaviour
{
    public Transform platformGenerationPoint;
    public GameObject shortPlatform;
    public GameObject mediumPlatform;
    public GameObject longPlatform;
    public Text subpolicyText;
    public bool generatorRunning = false; 

    private Agent Agent;
    private Logger LOGGER; 
    private Manager Manager; 
    private PlayerController Player; 

    public Subpolicy sbp; 

    public List<PolicyReport> policyReports;
    public PolicyReport currentPolicyReport; 

    public int seenWindows; 
    public List<float> acclimationScores; 
    public int livesAtStart; 

    void Awake() {
        this.sbp = null; 

        seenWindows = 0; 
        
        Agent = GameObject.Find("Agent").GetComponent<Agent>(); 
        Manager = GameObject.Find("Manager").GetComponent<Manager>(); 
        Player = GameObject.Find("Player").GetComponent<PlayerController>(); 
        LOGGER = GameObject.Find("Logger").GetComponent<Logger>(); 
        LOGGER.Start();
        livesAtStart = Player.GetLives(); 

        policyReports = new List<PolicyReport>(); 
    }

    public bool GetIsRunning() {
        return generatorRunning; 
    }


    public IEnumerator GenerateSequence(Subpolicy sbp) {

        this.sbp = sbp; 

        int policyIndex = 0; 

        generatorRunning = true;
        // Add sequence to the logger.
        
        LOGGER.AddSubpolicy(sbp);
        LOGGER.sbpStack.Push(sbp);

        // Repeated platform generation while player is not acclimated. 
        List<Platform> sequence = sbp.getSequence();
        while (!Agent.GetIsAcclimated() && !Manager.GetPaused()) { 

            // POLICY START!
            // Make a new report. 
            currentPolicyReport = new PolicyReport(sbp.index);
            currentPolicyReport.AddLife(Player.remainingLives);



            for (int i = 0; i < sequence.Count; i++) {
                if (!Manager.GetPaused()) {
                    GeneratePlatform(sequence[i]);
                    yield return new WaitForSeconds(1.6f);
                }
            }
            seenWindows++; 
            // Acclimation calculations ...
            // Get standard deviation 
            Agent.scoreSD = Agent.CalculateStandardDeviation();
            // Keep length to 5
            // if (Agent.scores.Count > 4) {
            if (Agent.scores.Count > 2) {

                Agent.scores.Dequeue();
                
                // IF ACCLIMATED.
                
                if (Agent.scoreSD < Agent.ACCLIMATION_THRESHOLD) {
                    // Fill out info in Policy Report. 
                    currentPolicyReport.AddWindowCount(seenWindows);
                    currentPolicyReport.AddAScores(Agent.scores); 
                    // Add the report to the list. 
                    policyReports.Add(currentPolicyReport);

                    Debug.Log("ACCLIMATED"); 
                    Agent.subpolicies++; 
                    
                    Agent.scores.Clear();
                    Agent.scoreSD = 1; 

                    policyIndex++; 
                    if (policyIndex % 3 == 0) {
                        // PLAYER HAS REACHED ENDGAME
                        Debug.Log("PLAYER HAS WON"); 
                        Manager.HandleDeath();
                        // sequence = Agent.sqn1;
                        // Agent.currentSubpolicy = Agent.sbp1;  
                    } else if (policyIndex % 3 == 1) {
                        sequence = Agent.sqn2; 
                        Agent.currentSubpolicy = Agent.sbp2;  
                    } else {
                        sequence = Agent.sqn3;
                        Agent.currentSubpolicy = Agent.sbp3;   
                    } 

                    seenWindows = 0; 
                    // Agent.NextPolicy();  
                } 
            } 
            // Add new value
            Agent.scores.Enqueue((float)Agent.GetCoinsCollected()/sbp.GetTotalCoins());
            // Reset coins collected.
            Agent.ResetCoinsCollected(); 
            sbp.IncrementWindowCount();
        }
    }

    public void GeneratePlatform(Platform platform) {
        GameObject type; 
        // Debug.Log(platform.getWidth());
        if (platform.getWidth() == Width.Short) { type = shortPlatform; }
        else if (platform.getWidth() == Width.Medium) { type = mediumPlatform; }
        else { type = longPlatform; }

        int yOffset = 0;
        if (platform.getHeight() == Height.Low) { yOffset = -5; }
        else if (platform.getHeight() == Height.Middle) { yOffset = -2; }
        else { yOffset = 1; }

        Instantiate(type, new Vector3(platformGenerationPoint.position.x, platformGenerationPoint.position.y + yOffset, 0), Quaternion.identity);
    }

    public void StopGeneration() {
        StopCoroutine("GenerateSequence");
    }
}
