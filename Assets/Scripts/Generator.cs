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

    void Awake() {
        Agent = GameObject.Find("Agent").GetComponent<Agent>(); 
        Manager = GameObject.Find("Manager").GetComponent<Manager>(); 
        LOGGER = GameObject.Find("Logger").GetComponent<Logger>(); 
        LOGGER.Start();
    }


    public IEnumerator GenerateSequence(Subpolicy sbp) {

        int policyIndex = 0; 

        generatorRunning = true;
        // Add sequence to the logger.
        
        LOGGER.AddSubpolicy(sbp);
        LOGGER.sbpStack.Push(sbp); 
        
        // string s = sbp.GetStringRepresentation();
        // Debug.Log(s);
        // subpolicyText.text = s;

        // Repeated platform generation while player is not acclimated. 
        List<Platform> sequence = sbp.getSequence();
        while (!Agent.GetIsAcclimated() && !Manager.GetPaused()) { 
            for (int i = 0; i < sequence.Count; i++) {
                if (!Manager.GetPaused()) {
                    GeneratePlatform(sequence[i]);
                    yield return new WaitForSeconds(1.6f);
                }
            }
            // Acclimation calculations ...
            // Get standard deviation 
            Agent.scoreSD = Agent.CalculateStandardDeviation();
            // Keep length to 5
            if (Agent.scores.Count > 2) {
                Agent.scores.Dequeue();
                // IF ACCLIMATED.
                if (Agent.scoreSD < 0.2) {
                    // Agent.acclimated = true;
                    Debug.Log("ACCLIMATED"); 

                    policyIndex++; 
                    if (policyIndex % 3 == 0) {
                        sequence = Agent.sqn1; 
                    } else if (policyIndex % 3 == 1) {
                        sequence = Agent.sqn2; 
                    } else {
                        sequence = Agent.sqn3; 
                    } 
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
