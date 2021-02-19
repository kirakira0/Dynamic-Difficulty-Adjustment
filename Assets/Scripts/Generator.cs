using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/**
 * Generates the platforms using input from the agent.
 */

public class Generator : MonoBehaviour
{
    public Transform platformGenerationPoint;
    public GameObject shortPlatform;
    public GameObject mediumPlatform;
    public GameObject longPlatform;

    private Agent AGENT;
    private Logger LOGGER; 

    void Awake() {
        AGENT = GameObject.Find("Agent").GetComponent<Agent>(); 
        LOGGER = GameObject.Find("Logger").GetComponent<Logger>(); 
    }


    public IEnumerator GenerateSequence(Subpolicy sbp) {
        // Add sequence to the logger.
        Debug.Log(sbp.GetStringRepresentation());
        
        LOGGER.sbpQueue.Enqueue(sbp); 
        LOGGER.sbpStack.Push(sbp); 
        // Repeated platform generation while player is not acclimated. 
        List<Platform> sequence = sbp.getSequence();
        while (!AGENT.getIsAcclimated()) {
            for (int i = 0; i < sequence.Count; i++) {
                GeneratePlatform(sequence[i]);
                yield return new WaitForSeconds(1.6f);
            } 
            sbp.IncrementWindowCount();
        }
    }

    public void GeneratePlatform(Platform platform) {
        GameObject type; 
        Debug.Log(platform.getWidth());
        if (platform.getWidth() == Width.Short) { type = shortPlatform; }
        else if (platform.getWidth() == Width.Medium) { type = mediumPlatform; }
        else { type = longPlatform; }

        int yOffset = 0;
        if (platform.getHeight() == Height.Low) { yOffset = -5; }
        else if (platform.getHeight() == Height.Middle) { yOffset = -2; }
        else { yOffset = 1; }

        Instantiate(type, new Vector3(platformGenerationPoint.position.x, platformGenerationPoint.position.y + yOffset, 0), Quaternion.identity);
    }
}
