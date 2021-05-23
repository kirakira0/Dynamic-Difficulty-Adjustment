using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

/**
 * Generates the platforms using input from the Agent.
 */

public class Generator : MonoBehaviour
{
    public int MIN_NUM_WINDOWS = 2; 

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
    }

    public bool GetIsRunning() {
        return generatorRunning; 
    }


    public IEnumerator GenerateSequence(Policy p) {

        this.p = p; 

        int policyIndex = 0; 

        generatorRunning = true;

        // Repeated platform generation while player is not acclimated. 
        List<Platform> sequence = p.sequence;
        while (!Agent.GetIsAcclimated() && !Manager.GetPaused()) { 
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

            // Only worry about acclimation if the player has seen the minimum
            // number of windows.
            if (Agent.scores.Count > MIN_NUM_WINDOWS) {
                // Only examine performance on recent windows.
                Agent.scores.Dequeue(); 
                // Determine if the player is acclimated.
                if (Agent.scoreSD < Agent.ACCLIMATION_THRESHOLD) {
                    // Update the policy.
                    p.acclimated = true;
                    // Variables that handle acclimation checking must be 
                    // reassigned.
                    Agent.subpolicies++;                   
                    Agent.scores.Clear();
                    Agent.scoreSD = 1;
                    // If the round is over, add the policy to the round, add 
                    // the round to the game and fetch a new round from the 
                    // agent.
                    p = Agent.NextPolicy(p);

                    sequence = p.GetSequence();

                    seenWindows = 0; 
                    // Agent.NextPolicy();  
                } 
            } 
            // Add score to agent scores and to policy scores. 
            float scoreToAdd = (float)Agent.GetCoinsCollected()/p.coinsPerWindow;
            Agent.scores.Enqueue(scoreToAdd);
            p.scores.Add(scoreToAdd);
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
