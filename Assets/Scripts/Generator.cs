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
    public Text PolicyText;
    public bool generatorRunning = false; 

    private Agent Agent;
    private Manager Manager; 
    private PlayerController Player; 

    public Policy p; 

    public List<PolicyReport> policyReports;
    public PolicyReport currentPolicyReport; 

    public int seenWindows; 
    public List<float> acclimationScores; 
    public int livesAtStart; 

    void Awake() {
        this.p = null; 

        seenWindows = 0; 
        
        Agent = GameObject.Find("Agent").GetComponent<Agent>(); 
        Manager = GameObject.Find("Manager").GetComponent<Manager>(); 
        Player = GameObject.Find("Player").GetComponent<PlayerController>(); 
        livesAtStart = Player.GetLives(); 

        policyReports = new List<PolicyReport>(); 
    }

    public bool GetIsRunning() {
        return generatorRunning; 
    }


    public IEnumerator GenerateSequence(Policy p) {

        this.p = p; 

        int policyIndex = 0; 

        generatorRunning = true;
        // Add sequence to the logger.
        
        // LOGGER.AddPolicy(p);
        // LOGGER.pStack.Push(p);

        // Repeated platform generation while player is not acclimated. 
        List<Platform> sequence = p.sequence;
        while (!Agent.GetIsAcclimated() && !Manager.GetPaused()) { 

            // POLICY START!
            // Make a new report. 
            currentPolicyReport = new PolicyReport(p.index);
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
                        // Agent.currentPolicy = Agent.p1;  
                    } else if (policyIndex % 3 == 1) {
                        sequence = Agent.p1.sequence; 
                        Agent.currentPolicy = Agent.p2;  
                    } else {
                        sequence = Agent.p3.sequence;
                        Agent.currentPolicy = Agent.p3;   
                    } 

                    seenWindows = 0; 
                    // Agent.NextPolicy();  
                } 
            } 
            // Add new value
            Agent.scores.Enqueue((float)Agent.GetCoinsCollected()/p.coinsPerWindow);
            // Reset coins collected.
            Agent.ResetCoinsCollected(); 
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
