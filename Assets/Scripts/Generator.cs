using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI; 

/**
 * Generates the platforms using input from the agent.
 */

public class Generator : MonoBehaviour
{
    public Transform platformGenerationPoint;
    public GameObject shortPlatform;
    public GameObject mediumPlatform;
    public GameObject longPlatform;
    public Text subpolicyText;

    private Agent AGENT;
    private Logger LOGGER; 

    void Awake() {
        AGENT = GameObject.Find("Agent").GetComponent<Agent>(); 
        LOGGER = GameObject.Find("Logger").GetComponent<Logger>(); 
        LOGGER.Start();
    }


    public IEnumerator GenerateSequence(Subpolicy sbp) {
        // Add sequence to the logger.
        
        LOGGER.AddSubpolicy(sbp);
        LOGGER.sbpStack.Push(sbp); 
        
        // string s = sbp.GetStringRepresentation();
        // Debug.Log(s);
        // subpolicyText.text = s;

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

    void Update() {
        // if (LOGGER.sbpStack.Count == 0) {
        //     subpolicyText.text = "SUBPOLICY: "  + 
        //                     "\nWINDOWS GENERATED: ";
        // } else {
        //     subpolicyText.text = "SUBPOLICY: " + LOGGER.sbpStack.Peek().GetStringRepresentation() + 
        //                     "\nWINDOWS GENERATED: " + LOGGER.sbpStack.Peek().getWindows();
        // }
    }
}
